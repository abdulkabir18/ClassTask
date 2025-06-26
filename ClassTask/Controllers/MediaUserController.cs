using ClassTask.Dtos;
using ClassTask.Services.Interfaces;
using ClassTask.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ClassTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaUserController : ControllerBase
    {
        private readonly IMediaUserService _mediaUserService;
        public MediaUserController(IMediaUserService mediaUserService)
        {
            _mediaUserService = mediaUserService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAccount([FromForm] SignUpRequestDto register)
        {
            //var validationResult = await validator.ValidateAsync(register);
            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.ToString());
            //}

            var result = await _mediaUserService.RegisterUser(register);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
             
        }
    }
}
