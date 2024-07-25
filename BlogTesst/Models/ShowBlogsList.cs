namespace BlogTesstAPI.Models
{
    public class ShowBlogsList
    {
        public int blog_id { get; set; }
        public string blog_title { get; set; }
        public string blog_content { get; set; }
        public List<Comments> comments { get; set; }
    }
}
