using CriptographyAPIs.Models;
using CriptographyAPIs.RSA;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CriptographyAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    /// <response code="200">When the pin and the amount are correct</response>
    /// <response code="412">When you enter invalid input</response>
    public class RSAController : ControllerBase
    {
        [HttpPost("RSA-Encryption")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        public IActionResult RSAen(RSAmodel data) 
        {
            return RSACrypt.Crypt(data);
        }


        [HttpPost("RSA-Decryption")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]

        public IActionResult RSAde(RSADecryptModel data)
        {
            return RSADecrypt.Decrypt(data);
        }
    }
}
