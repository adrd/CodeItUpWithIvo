namespace BlogSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using BlogSystem.Data.Contracts;

    public class PostComment : DeletableEntity
    {
        public PostComment()
        {
            this.IsVisible = true;
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Content { get; set; }

        public int BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsVisible { get; set; }
    }
}
