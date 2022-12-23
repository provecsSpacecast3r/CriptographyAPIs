using cifrarioROT_13.ROT13;
using CriptographyAPIs.BruteForce;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CriptographyAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriptographyController : ControllerBase
    {

        /// <response code="200">When the pin and the amount are correct</response>
        /// <response code="412">When you enter invalid input</response>

        
        [HttpPost("CryptROT-13")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public IActionResult CryptROT(string clearText)
        {
            return Crypt.ClearText(clearText);
        }

        [HttpPost("DecryptROT-13")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public IActionResult DecryptROT(string cipheredText)
        {
            return Decrypt.CipheredText(cipheredText);
        }

        [HttpPost("BruteForce")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public IActionResult Bruteforce(string cipheredText)
        {
            var fi = new FileInfo($@"BruteForce/tempBruter.txt");
            FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);

            if (Bruter.BruteForce(cipheredText, 0).Equals("error"))
            {
                return new ObjectResult("mistyped input") { StatusCode = (int)HttpStatusCode.PreconditionFailed };
            }
            else
            {

                for (int key = 1; key < 26; key++)
                {
                    writeFile(Bruter.BruteForce(cipheredText, key), sw, key);
                }

                sw.Close();
                fs.Close();

                var fileContent = readFile(fi);

                return new ObjectResult(fileContent) { StatusCode = (int)HttpStatusCode.OK }; ;
            }


        }

        private void writeFile(string update, StreamWriter sw, int key)
        {
            sw.WriteLine($"for key = {key}\t{update}\n");
        }

        private string readFile(FileInfo file)
        {
            FileStream fs = file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs);

            string fileContent = sr.ReadToEnd();

            sr.Close();
            fs.Close();

            return fileContent;
        }


    }
}
