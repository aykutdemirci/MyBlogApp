using MyBlogApp.Domain.Entities.Common;

namespace MyBlogApp.Domain.Entities
{
    public class Blog : BaseEntitiy
    {
        public string Title { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
