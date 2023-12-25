using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace erbildaphneAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("MyCorsPolicy")]
    [ApiController]
    public class WriteController : ControllerBase
    {

        private readonly IWriteService _service;

        public WriteController(IWriteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetWrites()
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
                var write = await _service.GetById(id);
                if (write == null)
                {
                    return NotFound($"Kayıt bulunamadı: ID={id}");
                }
                return Ok(write);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Create(WriteDto model)
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
       
        public IActionResult Edit(int id, WriteDto model)
        {           
           
            
            if (model.Id != id)
            {
                return BadRequest("ID değerleri uyuşmuyor.");
            }

            
            _service.Update(model);
           

            
            return Ok();
           
        }

        [HttpDelete("{id}")]
        //[Authorize]
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
