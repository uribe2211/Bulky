using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0, 100, ErrorMessage = "Only 0 to 100 values")]
    [DisplayName("Display order")]
    public int DisplayOrder { get; set; }
}
