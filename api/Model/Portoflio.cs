using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model;

[Table("Portoflio")]
public record Portoflio
{
    public int StockId { get; set; }
    public string AppUserId { get; set; }
    public Stock stock { get; set; }
    public AppUser appUser { get; set; }
}
