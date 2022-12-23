using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CriptographyAPIs.Models
{
    public class RSAmodel
    {
        [Required]
        public int P { get; set; }
        public int Q { get; set; }

        public string? clearText { get; set; }
    }
}
