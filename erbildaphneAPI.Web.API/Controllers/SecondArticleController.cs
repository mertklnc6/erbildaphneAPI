using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace erbildaphneAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("ED")]
    [ApiController]
    public class SecondArticleController : ControllerBase
    {

        private readonly ISecondArticleService _service;

        public SecondArticleController(ISecondArticleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSecondArticles()
        {
            var list = await _service.GetAllAsync();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sArticle = await _service.GetById(id);
                if (sArticle == null)
                {
                    return NotFound($"Kayıt bulunamadı: ID={id}");
                }
                return Ok(sArticle);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public IActionResult Create(SecondArticleDto model)
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


        [HttpPut("{id}")]
        [Authorize(Roles = "Editor")]
        public IActionResult Edit(int id, SecondArticleDto model)
        {           
           
            
            if (model.Id != id)
            {
                return BadRequest("ID değerleri uyuşmuyor.");
            }

            
            _service.Update(model);
           

            
            return Ok();
           
        }

        [HttpDelete("{id}")]
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
