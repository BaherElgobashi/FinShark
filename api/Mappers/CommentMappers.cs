using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto toCommentDto(this Comment CommentModel)
        {
            return new CommentDto
            {
              Id = CommentModel.Id,
              Title = CommentModel.Title,
              Content = CommentModel.Content,
              CreatedOn = CommentModel.CreatedOn,
              StockId = CommentModel.StockId
            };
        }
        public static Comment ToCommentFromCreatedDto(this CreatedCommentDto commentDto , int StockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = StockId
                
            };
        }
        
    }
}