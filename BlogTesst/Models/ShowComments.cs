namespace BlogTesstAPI.Models
{
    public class ShowComments
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int BlogPostId { get; set; }
    }
}
