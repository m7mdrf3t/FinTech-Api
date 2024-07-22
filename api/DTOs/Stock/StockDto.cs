using api.DTOs.Comment;

namespace api.DTOs.Stock;

public record StockDto
{
    public int Id { get; set; }
    public string sympol { get; set; } = string.Empty;
    public string CompanyName {get; set; } = string.Empty;
    public decimal Purchase { get; set; } 
    public decimal Lastdev { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap{set; get;} 
    public List<CommentDto> Comments { get; set; }
}
