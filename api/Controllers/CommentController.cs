using api.DTOs.Comment;
using api.Extenstions;
using api.Helpers;
using api.Interfaces;
using api.Mapper;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentReposatory _commentReposatory;
    private readonly IStockReposatory _stockReposatory;
    private readonly UserManager<AppUser> _userManager;

    public CommentController(ICommentReposatory commentReposatory , IStockReposatory stockReposatory , UserManager<AppUser> userManager)
    {
        _commentReposatory = commentReposatory;
        _stockReposatory = stockReposatory;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(){

        if(!ModelState.IsValid) return BadRequest(ModelState);
        List<Comment> comments = await _commentReposatory.GetAllCommentsAsync();
        // We need to Change all the Comments to DTos before returning and we can do so with select.
        var commentDto = comments.Select(s => s.ToCommentDto());

        return Ok(commentDto);

    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute]int id){

        if(!ModelState.IsValid) return BadRequest(ModelState);

        var comment = await _commentReposatory.GetCommentByIDAsync(id);

        if(comment == null ){
            return NotFound();
        }

        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute]int stockId ,[FromBody] CreateCommentDto CreateCommentDto)
    {
        if(!await _stockReposatory.IsExsit(stockId)) 
        {
            return BadRequest("There is no Stock founded");
        }

        if(!ModelState.IsValid) return BadRequest(ModelState);

        var username = User.GetUserName();
        var _user = await _userManager.FindByNameAsync(username);

        if(_user == null) return BadRequest("Please Login");

        var comment = CreateCommentDto.toCommentFromCreateDto(stockId);
        comment.appUserId = _user.Id;
        
        var commentCreateVM = await _commentReposatory.CreateAsync(comment);

        return CreatedAtAction(nameof(GetById) , new { id = comment.Id } , comment.ToCommentDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute]int id ,[FromBody] UpdateCommentDto UpdateComment)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var comment = UpdateComment.toCommentFromUpdateDto();
        
        var commentUpdateVM = await _commentReposatory.UpdateAsync(id, comment);

        if(commentUpdateVM == null){
            return NotFound("Comment Not Found");
        }

        return Ok(commentUpdateVM.ToCommentDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id){

        if(!ModelState.IsValid) return BadRequest(ModelState);

        var deleteComment = await _commentReposatory.DeleteAsync(id);

        if(deleteComment == null){
            return BadRequest("Comment is not valid");
        }

        return NotFound();

    }






}

