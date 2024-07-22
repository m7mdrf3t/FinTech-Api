using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Stock;

public record CreatStockDto
{
    [Required]
    [MaxLength(10,ErrorMessage ="sympol is too long")]
    public string sympol { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10,ErrorMessage ="Company name must be more than 3 letters")]
    public string CompanyName {get; set; } = string.Empty;
    
    [Required]
    [Range(1,100000000)]
    public decimal Purchase { get; set; } 

    [Required]
    [Range(0.1,100)]
    public decimal Lastdev { get; set; }

    [Required]
    [MaxLength(10,ErrorMessage =" Industry must be over 4 letters")]
    public string Industry { get; set; } = string.Empty;

    [Required]
    [Range(100,40000000000)]
    public long MarketCap{set; get;} 
}