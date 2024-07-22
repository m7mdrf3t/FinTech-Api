using api.Data;
using api.Interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Reposatory;

public class CommentReposatory : ICommentReposatory
{
    private readonly ApplicationDbContext _applicationDbContext;

    public CommentReposatory(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
         await _applicationDbContext.Comments.AddAsync(comment);
         await _applicationDbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commentModel = await _applicationDbContext.Comments.FirstOrDefaultAsync(d => d.Id == id);

        if(commentModel == null){
            return null;
        }

        _applicationDbContext.Comments.Remove(commentModel);
        await _applicationDbContext.SaveChangesAsync();
        return commentModel;
    }

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        List<Comment> comments = await _applicationDbContext.Comments.Include(x => x.appUser).ToListAsync();
        return comments;
    }

    public async Task<Comment?> GetCommentByIDAsync(int id)
    {
        var commentModel = await _applicationDbContext.Comments.Include(x => x.appUser).FirstOrDefaultAsync(d => d.Id == id);

        if(commentModel == null){
            return null;
        }
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
    {
        var existingComment = await _applicationDbContext.Comments.FindAsync(id);

        if(existingComment == null){
            return null;
        }

        existingComment.Title = commentModel.Title;
        existingComment.Content = commentModel.Content;

        await _applicationDbContext.SaveChangesAsync();
        return existingComment;

    }
}
