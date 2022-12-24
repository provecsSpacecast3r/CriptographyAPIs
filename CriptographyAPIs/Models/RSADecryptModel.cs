using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CriptographyAPIs.Models
{
    public class RSADecryptModel
    {
        [Required]
        public string CipheredText { get; set; }
        public string D { get; set; }
        public string N { get; set; }

    }
}
