namespace BlogSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Page : ContentHolder
    {
        [Required]
        public string Permalink { get; set; }
    }
}
