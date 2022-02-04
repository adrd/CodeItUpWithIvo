namespace BlogSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BlogPost : ContentHolder
    {
        [Required]
        [DataType(DataType.Html)]
        public string ShortContent { get; set; }

        [Required]
        public string ImageOrVideoUrl { get; set; }

        public BlogPostType Type { get; set; }

        public ICollection<PostComment> Comments { get; set; } = new HashSet<PostComment>();
    }
}
