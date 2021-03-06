namespace BlogSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Setting
    {
        [Key]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Value { get; set; }
    }
}
