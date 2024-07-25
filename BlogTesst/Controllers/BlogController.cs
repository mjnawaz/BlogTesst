using BlogTesstAPI.Helper.Error;
using BlogTesstAPI.Models;
using BlogTest.DataAccessLayer.Interface.IRepositoryWrapper;
using BlogTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BlogTesstAPI.Controllers
{
    [Route("v1/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IRepositoryWrapper _uow;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public BlogController(IRepositoryWrapper uow, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, ILogger<BlogController> logger)
        {
            _uow = uow;
            _environment = environment;
            //_logger = logger;
        }
        [HttpGet]
        [Route("GetBlogs")]
        public async Task<IActionResult> GetBlogPosts()
        {
            try
            {

                var getBlogs = _uow.Blogs.GetBlogPosts();
                if (!getBlogs.Any())
                {
                    return BadRequest(new ErrorModel
                    {
                        ErrorMessage = ApiErrorCode.NO_BLOGS_FOUND.ToString(),
                        ErrorCode = (int)ApiErrorCode.NO_BLOGS_FOUND
                    });
                }
                var getBlogsList = await getBlogs.ToListAsync();
                List<ShowBlogsList> showBlogsList = getBlogsList.Select(blog => new ShowBlogsList
                {
                    blog_id = blog.Id,
                    blog_title = blog.Title,
                    blog_content = blog.Content,
                    comments = blog.Comments.Select(c => new Comments
                    {
                        text = c.Text
                    }).ToList()
                }).ToList();

                return Ok(showBlogsList);
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorModel
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = (int)ApiErrorCode.OTHER_ERROR
                };
                return new JsonResult(errorModel) { StatusCode = Convert.ToInt32(API_Status_Code.BadRequest) };
            }

        }
        [HttpGet]
        [Route("GetBlogPostByID")]
        public async Task<IActionResult> GetBlogPost(int id)
        {
            var blogPost = _uow.Blogs.GetBlogPostById(id);
            if (blogPost == null)
            {
                return NotFound(new ErrorModel
                {
                    ErrorMessage = ApiErrorCode.NO_BLOG_AGAINST_THIS_ID.ToString(),
                    ErrorCode = (int)ApiErrorCode.NO_BLOG_AGAINST_THIS_ID
                });
            }
            return Ok(new ShowBlogsList
            {
                blog_id = blogPost.Id,
                blog_title = blogPost.Title,
                blog_content = blogPost.Content,
                comments = blogPost.Comments.Select(c => new Comments { text = c.Text }).ToList()
            });
        }
        [HttpPost] 
        [Route("AddComment")]
        public async Task<IActionResult> AddComment(int blogPostId, [FromBody] Comments model)
        {
            if (blogPostId < 1)
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = ApiErrorCode.INVALID_REQUEST_BODY.ToString(),
                    ErrorCode = (int)ApiErrorCode.INVALID_REQUEST_BODY
                });
            }
            var newComment = new Comment
            {
                Text = model.text,
                BlogPostId = blogPostId
            };

            var addedComment = await _uow.Blogs.AddComment(blogPostId, newComment);
            if (addedComment == null)
            {
                return NotFound(new ErrorModel
                {
                    ErrorMessage = ApiErrorCode.OTHER_ERROR.ToString(),
                    ErrorCode = (int)ApiErrorCode.OTHER_ERROR
                });
            }
            var response = new ShowComments
            {
                CommentId = addedComment.Id,
                Text = addedComment.Text,
                BlogPostId = addedComment.BlogPostId
            };

            return CreatedAtAction(nameof(GetBlogPost), new { id = blogPostId }, response);
        }
        [HttpPost]
        [Route("CreateBlogPost")]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPost model)
        {
            try
            {
                var newBlogPost = new BlogPost
                {
                    Title = model.title,
                    Content = model.content
                };

                var createdBlogPost = await _uow.Blogs.CreateBlogPost(newBlogPost);
                return CreatedAtAction(nameof(GetBlogPost), new { id = createdBlogPost.Id }, createdBlogPost);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = (int)ApiErrorCode.OTHER_ERROR
                });
            }
        }

    }
}
