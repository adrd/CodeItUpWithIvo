namespace WebServerImages.Data
{
    using System;

    public class ImageData
    {
        // int Id -> use MD5(MD5(Id)) to protect against bots.

        public ImageData() => this.Id = Guid.NewGuid();

        public Guid Id { get; set; }

        public string OriginalFileName { get; set; }

        public string OriginalType { get; set; }

        public byte[] OriginalContent { get; set; }

        public byte[] ThumbnailContent { get; set; }

        public byte[] FullscreenContent { get; set; }

        // public string UserId { get; set; }

        // public int PostId { get; set; }

        // public int ArticleId { get; set; }
    }
}
