using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Interfaces;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlShortenerService _service; 

        public UrlShortenerController(IUrlShortenerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.Get());
        }

        [HttpGet("{shortLink}")]
        public IActionResult GetUrlBy(string shortLink)
        {
            var item = _service.GetBy(shortLink);

            if (item == null)
                return NotFound("Item not found");

            return Ok(item.URL);
        }

        [HttpPost]
        public IActionResult Post([FromBody]UrlShortenerInput input)
        {
            var response = _service.Create(input);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]UrlShortenerInput input)
        {
            var response = _service.Edit(id,input);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var response = _service.Delete(id);
            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
