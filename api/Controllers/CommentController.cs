using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{

    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepository commentRepository ,
                                    IStockRepository stockRepository ,
                                    UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var Comments = await _commentRepository.GetAllAsync();
            var commentDto = Comments.Select(c => c.toCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetById(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var Comment = await _commentRepository.GetByIdAsync(id);
            if (Comment is null)
                return NotFound();
            return Ok(Comment.toCommentDto());
        }

        [HttpPost("{StockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int StockId , CreatedCommentDto commentDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!await _stockRepository.StockExistsAsync(StockId))
                return BadRequest("Stock is not found");

            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);

            var commentModel = commentDto.ToCommentFromCreate(StockId);
            commentModel.AppUserId = appUser.Id;

            await _commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new{id = commentModel.Id},commentModel.toCommentDto());
        }
        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult>Update([FromRoute]int id , [FromBody] UpdateCommentRequestDto updateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var Comment = await _commentRepository.UpdateAsync(id ,updateDto.ToCommentFromUpdate() );
            if(Comment is null)
                return NotFound();
            return Ok(Comment.toCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var Comment = await _commentRepository.DeleteAsync(id);
            if(Comment is null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}