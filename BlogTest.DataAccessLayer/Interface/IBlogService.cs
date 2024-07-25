using BlogTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BlogTest.DataAccessLayer.Interface
{
    public interface IBlogService
    {
        IQueryable<BlogPost> GetBlogPosts();
        BlogPost GetBlogPostById(int ID);
        Task<Comment> AddComment(int blogPostId, Comment comment);
        Task<BlogPost> CreateBlogPost(BlogPost newBlogPost);
    }
}
