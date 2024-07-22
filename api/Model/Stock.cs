using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model;

[Table("Stock")]
public record Stock
{
    [Key]
    public int Id { get; set; }
    public string sympol { get; set; } = string.Empty;
    public string CompanyName {get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Purchase { get; set; } 

    [Column(TypeName = "decimal(18,2)")]
    public decimal Lastdev { get; set; }

    public string Industry { get; set; }

    public long MarketCap{set; get;} 

    public List<Comment> Comments { get; set; } = new List<Comment>();

    public List<Portoflio> portoflios = new List<Portoflio>();

}
