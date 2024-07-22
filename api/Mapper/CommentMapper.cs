using api.DTOs.Comment;
using api.Model;

namespace api.Mapper;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment comment){

        var commentDto = new CommentDto(){
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            StockID = comment.StockID,
            CreatedBy = comment.appUser.UserName
            
        };

        return commentDto;

    }

    public static Comment ToComment(this CommentDto commentDto){
        return new Comment{
            Id = commentDto.Id,
            Title = commentDto.Title,
            StockID = commentDto.StockID
        };
    }

    public static Comment toCommentFromCreateDto(this CreateCommentDto createCommentDto , int stockId)
    {
        return new Comment{
            Title = createCommentDto.Title,
            Content = createCommentDto.Content,
            StockID = stockId
        };
    }

    public static Comment toCommentFromUpdateDto(this UpdateCommentDto updateCommentDto)
    {
        return new Comment{
            Title = updateCommentDto.Title,
            Content = updateCommentDto.Content,
        };
    }
}
