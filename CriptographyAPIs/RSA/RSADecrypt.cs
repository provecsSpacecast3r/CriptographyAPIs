using CriptographyAPIs.Models;
using Microsoft.AspNetCore.Mvc;
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
                BigInteger d = BigInteger.Parse(data.D);
                BigInteger n = BigInteger.Parse(data.N);

                string[] separatedValues = data.CipheredText.Split(' ');
                BigInteger[] decryptedInts = Array.ConvertAll(separatedValues, s => BigInteger.Parse(s));
                BigInteger[] decryptedNumbers = decryptedInts.Select(num => BigInteger.ModPow(num, d, n)).ToArray();

                char[] arrayChar = decryptedNumbers.Select(num => (char)num).ToArray();
                string s = new string(arrayChar);
                string numSequence = "Sequenza di numeri decriptata: " + string.Join(" ", decryptedNumbers)+"\n";

                return new ObjectResult($"{numSequence}Testo decriptato: {s}") { StatusCode = (int)HttpStatusCode.OK};

            }catch(Exception ex) { return new ObjectResult(ex) { StatusCode = (int)(HttpStatusCode.PreconditionFailed) }; }
        }
    }
}
