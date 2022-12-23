using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CriptographyAPIs.Models
{
    public class RSADecryptModel
    {
        [Required]
        public string CipheredText { get; set; }
        public int D { get; set; }
        public int N { get; set; }

    }
}
