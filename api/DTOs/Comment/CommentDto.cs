using System;
using api.Model;

namespace api.DTOs.Comment;

public record CommentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime? CreatedAt {get; set;}
    public int? StockID { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
