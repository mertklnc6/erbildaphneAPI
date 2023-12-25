using erbildaphneAPI.Entity.DTOs;
using erbildaphneAPI.Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace erbildaphneAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    public class RumorController : ControllerBase
    {

        private readonly IRumorService _service;

        public RumorController(IRumorService rService)
        {
            _service = rService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRumors()
        {
            var list = await _service.GetAllAsync();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("list")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
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
                var rumor = await _service.GetById(id);
                if (rumor == null)
                {
                    return NotFound($"Kayıt bulunamadı: ID={id}");
                }
                return Ok(rumor);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Create(RumorDto model)
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
        //[Authorize]
        public IActionResult Edit(int id, RumorDto model)
        {
            // Model doğrulamasını kontrol et
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Gelen ID ile modelin ID'sinin eşleşip eşleşmediğini kontrol et
            if (model.Id != id)
            {
                return BadRequest("ID değerleri uyuşmuyor.");
            }

            //try
            //{
            _service.Update(model);
            //if (updatedItem == null)
            //{
            //    return NotFound($"Güncelleme yapılamadı: ID={model.Id}");
            //}

            // Başarılı güncelleme
            return Ok();
            //return Ok(updatedItem);
            //}
            //catch (Exception ex)
            //{
            //    // Log exception here
            //    return StatusCode(500, "Güncelleme hatası: " + ex.Message);
            //}
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
