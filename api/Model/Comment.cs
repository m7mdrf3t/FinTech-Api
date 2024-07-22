using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model;

[Table("Comment")]
public class Comment
{
    [Key]
    
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime? CreatedAt {get; set;}
    public int? StockID { get; set; }
    public Stock? Stock { get; set; }
    public string appUserId {get; set;}
    public AppUser appUser {get; set;}
}
