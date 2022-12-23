using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace cifrarioROT_13.ROT13;

public class Decrypt
{

    public static ObjectResult CipheredText(string cipheredText)
    {
        var boredA = "abcdefghijklmnopqrstuvwxyz";
        var alphabet = boredA.ToCharArray();
        var toLower = cipheredText.ToLower();
        var charsC = toLower.ToCharArray();
        var controller = 0;//variabile per gestire errori


        for (int i = 0; i < toLower.Length; i++) 
        {

            for (int j = 0; j < alphabet.Length; j++) 
            {

                if (alphabet[j] == toLower[i] && j >= 13) 
                {

                    if (char.IsUpper(cipheredText[i])) 
                    { //per tenere l'input dei caratteri grandi
                        charsC[i]  = char.ToUpper(alphabet[j - 13]);
                    } else 
                    {
                        charsC[i] = alphabet[j-13]; 
                    } //nei parametri della funzione casto a string perchè lo richiede la funzione, e perchè il singolo carattere non viene preso come stringa ma come Byte (sinonimo di utf-8)

                    controller++;

                } else if (alphabet[j] == toLower[i] && j < 13) 
                {
                    if (char.IsUpper(cipheredText[i])) 
                    {
                        charsC[i] = char.ToUpper( alphabet[j + 13]);
                    } else
                    {
                        charsC[i] = alphabet[j + 13];
                    }

                    controller++;
                }
            }
        }

        var tempFlag = cipheredText.Replace(" ", string.Empty);

        if (controller != tempFlag.Length)
        {
            return new ObjectResult("error") { StatusCode = (int) HttpStatusCode.PreconditionFailed };
        }
        return new ObjectResult(new string(charsC)) { StatusCode = (int) HttpStatusCode.OK};
    }
}
