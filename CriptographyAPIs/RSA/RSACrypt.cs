using CriptographyAPIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace CriptographyAPIs.RSA
{
    public class RSACrypt
    {
        public static ObjectResult Crypt(RSAmodel data)
        {
            if (data.P == data.Q || !IsPrime(data.P) || !IsPrime(data.Q))
            {
                return new ObjectResult("mistyped input") { StatusCode = (int)HttpStatusCode.PreconditionFailed };
            }

            // Calcolo coefficiente n
            BigInteger n = data.P * data.Q;

            if (n < 255)
            {
                return new ObjectResult("error, try to use bigger prime numbers for p and q") { StatusCode = (int)HttpStatusCode.PreconditionFailed };
            }

            string resultOfn = ("Coefficiente n: " + n + "\n");

            // Calcolo  z
            BigInteger z = (data.P - 1) * (data.Q - 1);
            string resultOfz = ("Funzione di Eulero z: " + z + "\n");

            // Calcolo la chiave pubblica (e, n)
            BigInteger e = GetPublicKey(z);
            string publicKey = ("Chiave pubblica (e, n): (" + e + ", " + n + ")\n");

            // Calcolo la chiave privata (d, n)
            BigInteger d = GetPrivateKey(e, z);
            string privateKey = ("Chiave privata (d, n): (" + d + ", " + n + ")\n");


            // Associo a ogni carattere un numero intero secondo la codifica ASCII
            BigInteger[] messageNumbers = data.clearText.Select(c => (BigInteger)c).ToArray();

            // Criptare la sequenza di numeri usando la chiave pubblica (e, n)
            BigInteger[] encryptedMessageNumbers = Encrypt(messageNumbers, e, n);
            BigInteger[] decryptedMessageNumbers = Decrypt(encryptedMessageNumbers, d, n);

            string finalResult = resultOfn + resultOfz + publicKey + privateKey + "Sequenza di numeri: " + string.Join(" ", messageNumbers) + "\n" + "sequenza di numeri criptata: " + string.Join(" ", encryptedMessageNumbers)+ "\n" + "Sequenza di numeri decriptata: " + string.Join(" ", decryptedMessageNumbers);
            return new ObjectResult(finalResult) { StatusCode = (int)(HttpStatusCode.OK) };
        }

        private static bool IsPrime(int number) //utilizzando l'algoritmo aks
        {
            if (number < 2) return false;
            if (number == 2 || number == 3) return true; 
            if (number % 2 == 0) return false;

            BigInteger r = BigInteger.Zero;
            BigInteger d = number - 1;
            while (d % 2 == 0)
            {
                r++;
                d /= 2;
            }

            for (BigInteger a = 2; a <= number - 1; a++)
            {
                if (BigInteger.ModPow(a, d, number) != 1)
                {
                    bool isComposite = true;
                    for (BigInteger i = 0; i < r; i++)
                    {
                        if (BigInteger.ModPow(a, (BigInteger)Math.Pow(2, (int)i) * d, number) == number - 1)
                        {
                            isComposite = false;
                            break;
                        }
                    }
                    if (isComposite) return false;
                }
            }

            return true;
        }

        private static BigInteger GetPublicKey(BigInteger z)
        {
            for (BigInteger i = 2; i < z; i++)
            {
                if (BigInteger.GreatestCommonDivisor(i, z) == 1)
                {
                    return i;
                }
            }

            throw new Exception("Non è stato possibile trovare una chiave pubblica valida.");
        }

        private static BigInteger GetPrivateKey(BigInteger e, BigInteger z)
        {
            for (BigInteger i = 1; i < z; i++)
            {
                if ((e * i) % z == 1)
                {
                    return i;
                }
            }

            throw new Exception("Non è stato possibile trovare una chiave privata valida.");
        }

        private static BigInteger[] Encrypt(BigInteger[] message, BigInteger e, BigInteger n)
        {
            return message.Select(num => BigInteger.ModPow(num, e, n)).ToArray();
        }

        private static BigInteger[] Decrypt(BigInteger[] message, BigInteger d, BigInteger n)
        {
            return message.Select(num => BigInteger.ModPow(num, d, n)).ToArray();
        }

    }
}
