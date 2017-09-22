namespace Products.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contains {1} characteres length.")]
        public string Description { get; set; }
    }
}
