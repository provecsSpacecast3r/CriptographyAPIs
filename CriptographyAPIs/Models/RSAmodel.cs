using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CriptographyAPIs.Models
{
    public class RSAmodel
    {
        [Required]

        public string? clearText { get; set; }
        public string P { get; set; }
        public string Q { get; set; }

    }
}
