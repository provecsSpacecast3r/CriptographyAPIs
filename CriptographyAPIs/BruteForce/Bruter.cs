using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CriptographyAPIs.BruteForce
{
    public class Bruter
    {
        public static string BruteForce(string cipheredText,int key)
        {
            var boredA = "abcdefghijklmnopqrstuvwxyz";
            var alphabet = boredA.ToCharArray();
            var toLower = cipheredText.ToLower();
            var charsC = toLower.ToCharArray();
            var controller = 0;//variabile per gestire errori
            var skipValue = boredA.Length - key;
            


            for (int i = 0; i < toLower.Length; i++)
            {

                for (int j = 0; j < alphabet.Length; j++)
                {

                    if (alphabet[j] == toLower[i] && j >= key)
                    {

                        if (char.IsUpper(cipheredText[i]))
                        { //per tenere l'input dei caratteri grandi
                            charsC[i] = char.ToUpper(alphabet[j - key]);
                        }
                        else
                        {
                            charsC[i] = alphabet[j - key];
                        } //nei parametri della funzione casto a string perchè lo richiede la funzione, e perchè il singolo carattere non viene preso come stringa ma come Byte (sinonimo di utf-8)

                        controller++;

                    }
                    else if (alphabet[j] == toLower[i] && j < key)
                    {
                        if (char.IsUpper(cipheredText[i]))
                        {
                            charsC[i] = char.ToUpper(alphabet[j + skipValue]);
                        }
                        else
                        {
                            charsC[i] = alphabet[j + skipValue];
                        }

                        controller++;
                    }
                }
            }

            var tempFlag = cipheredText.Replace(" ", string.Empty);

            if (controller != tempFlag.Length)
            {
                return "error";
            }
            return new string(charsC);
        }
    }
}
