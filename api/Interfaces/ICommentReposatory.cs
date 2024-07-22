using api.Model;

namespace api.Interfaces;

public interface ICommentReposatory
{
    Task<List<Comment>> GetAllCommentsAsync();
    Task<Comment?> GetCommentByIDAsync(int id);
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(int id , Comment comment);
    Task<Comment?> DeleteAsync(int id);
}