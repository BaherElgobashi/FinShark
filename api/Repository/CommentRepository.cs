using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       

        public async Task<List<Comment>> GetAllAsync()
        {
            var Comments = await _context.Comments.ToListAsync();
            return Comments;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var Comment = await _context.Comments.FindAsync(id);
            if(Comment is null)
                return null;
            return Comment;
        }

         public async Task<Comment> CreateAsync(Comment CommentModel)
        {
            await _context.Comments.AddAsync(CommentModel);
            await _context.SaveChangesAsync();
            return CommentModel;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment CommentModel)
        {
            var Comment = await _context.Comments.FirstOrDefaultAsync(x=>x.Id==id);
            if(Comment is null)
                return null;
            Comment.Content = CommentModel.Content;
            Comment.CreatedOn = CommentModel.CreatedOn;
            await _context.SaveChangesAsync();
            return Comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var Comment = await _context.Comments.FirstOrDefaultAsync(x=>x.Id == id);
            if (Comment is null)
                return null;
            _context.Comments.Remove(Comment);
            await _context.SaveChangesAsync();
            return Comment;
        }
    }
}