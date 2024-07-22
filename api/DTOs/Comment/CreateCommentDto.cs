using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Comment;

public class CreateCommentDto
{
    [Required]
    [MaxLength(10,ErrorMessage = "Title is Too small")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(280,ErrorMessage = "Title is Too small")]    
    public string Content { get; set; } = string.Empty;
}
