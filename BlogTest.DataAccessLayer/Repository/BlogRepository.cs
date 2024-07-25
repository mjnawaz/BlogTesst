using BlogTest.DataAccessLayer.Interface;
using BlogTest.DataAccessLayer.Repository.RepositoryBase;
using BlogTest.ModelMigrations.Data;
using BlogTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlogTest.DataAccessLayer.Repository
{
    public class BlogRepository : RepositoryBase<BlogPost>, IBlogService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
          : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public BlogRepository(ApplicationDbContext context) : base(context)
        {

        }
        public IQueryable<BlogPost> GetBlogPosts()
        {
            return _context.BlogPost.Include(b => b.Comments);
        }
        public BlogPost GetBlogPostById(int ID)
        {
            return _context.BlogPost.Include(b => b.Comments).FirstOrDefault(m => m.Id == ID);
        }
        public async Task<Comment> AddComment(int blogPostId, Comment comment)
        {
            var blogPost = await _context.BlogPost.FindAsync(blogPostId);
            if (blogPost == null)
            {
                return null;
            }
            blogPost.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        public async Task<BlogPost> CreateBlogPost(BlogPost newBlogPost)
        {
            _context.BlogPost.Add(newBlogPost);
            await _context.SaveChangesAsync();
            return newBlogPost;
        }

    }
}
