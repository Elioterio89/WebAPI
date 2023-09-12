using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model
{
    public class Talento
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Curriculo { get; set; }
    }
}