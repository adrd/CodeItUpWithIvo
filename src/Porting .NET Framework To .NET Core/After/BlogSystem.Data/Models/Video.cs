namespace BlogSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BlogSystem.Data.Contracts;

    public class Video : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string VideoId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public VideoProvider Provider { get; set; }
    }
}
