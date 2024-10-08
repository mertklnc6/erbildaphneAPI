﻿using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace erbildaphneAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    public class GNewsController : ControllerBase
    {

        private readonly IGNewsService _service;

        public GNewsController(IGNewsService service)
        {
            _service = service;
        }

        [HttpGet("get/")]
        public async Task<IActionResult> GetGNews()
        {
            var list = await _service.GetAllAsync();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var gNews = await _service.GetById(id);
                if (gNews == null)
                {
                    return NotFound($"Kayıt bulunamadı: ID={id}");
                }
                return Ok(gNews);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        [HttpPost("create/")]
		[Authorize(Roles = "Editor")]
		//[Authorize]
		public IActionResult Create([FromBody] GNewsDto model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            try
            {
                var createdItem = _service.Create(model);
                return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Kayıt oluşturma hatası: " + ex.Message);
            }
        }


        [HttpPut("edit/{id}")]
		[Authorize(Roles = "Editor")]
		public IActionResult Edit(int id, [FromBody] GNewsDto model)
        {



            if (model.Id != id)
            {
                return BadRequest("ID değerleri uyuşmuyor.");
            }


            _service.Update(model);

            return Ok();

        }

        [HttpDelete("delete/{id}")]
		[Authorize(Roles = "Editor")]
		public async Task<IActionResult> Delete(int id)
        {

            var item = _service.GetById(id);

            if (item != null)
            {
                await _service.Delete(id);
                return Ok();
            }
            return BadRequest();
        }


    }
}
