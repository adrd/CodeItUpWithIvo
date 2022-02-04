namespace WebServerImages.Data
{
    using System;

    public class ImageFile
    {
        // int Id -> use MD5(MD5(Id)) to protect against bots.

        public Guid Id { get; set; }

        public string Folder { get; set; }

        // public string UserId { get; set; }

        // public int ArticleId { get; set; }
    }
}
