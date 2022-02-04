namespace BlogSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BlogSystem.Data.Contracts;

    public class ContentHolder : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string SubTitle { get; set; }

        [DataType(DataType.Html)]
        [Required]
        public string Content { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [MaxLength(1000)]
        public string MetaDescription { get; set; }

        [Required]
        [MaxLength(1000)]
        public string MetaKeywords { get; set; }
    }
}
