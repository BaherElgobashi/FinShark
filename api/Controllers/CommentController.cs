using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
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
        public CommentController(ICommentRepository commentRepository , IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Comments = await _commentRepository.GetAllAsync();
            var commentDto = Comments.Select(c => c.toCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var Comment = await _commentRepository.GetByIdAsync(id);
            if (Comment is null)
                return NotFound();
            return Ok(Comment.toCommentDto());
        }

        [HttpPost("{StockId}")]
        public async Task<IActionResult> Create([FromRoute] int StockId , CreatedCommentDto commentDto)
        {
            if(!await _stockRepository.StockExistsAsync(StockId))
                return BadRequest("Stock is not found");

            var comment = commentDto.ToCommentFromCreate(StockId);
            await _commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new{id = comment},comment.toCommentDto());
        }
        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult>Update([FromRoute]int id , [FromBody] UpdateCommentRequestDto updateDto)
        {
            var Comment = await _commentRepository.UpdateAsync(id ,updateDto.ToCommentFromUpdate() );
            if(Comment is null)
                return NotFound();
            return Ok(Comment.toCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var Comment = await _commentRepository.DeleteAsync(id);
            if(Comment is null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}