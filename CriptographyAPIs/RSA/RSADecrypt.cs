using CriptographyAPIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;
using System.Numerics;

namespace CriptographyAPIs.RSA
{
    public class RSADecrypt
    {
        public static ObjectResult Decrypt(RSADecryptModel data)
        {
            try
            {
                string[] separatedValues = data.CipheredText.Split(' ');
                int[] decryptedInts = Array.ConvertAll(separatedValues, s => int.Parse(s));
                string numSequence = "Sequenza di numeri decriptata: " + decryptedInts.ToString();

                string decryptedText = new string(charArray);

                return new ObjectResult($"{numSequence}Testo decriptato: {decryptedText}") { StatusCode = (int)HttpStatusCode.OK};

            }catch(Exception ex) { return new ObjectResult(ex) { StatusCode = (int)(HttpStatusCode.PreconditionFailed) }; }
        }
    }
}
