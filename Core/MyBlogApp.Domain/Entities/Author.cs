using MyBlogApp.Domain.Entities.Common;

namespace MyBlogApp.Domain.Entities
{
    public sealed class Author : BaseEntitiy
    {
        public string Name { get; set; }

        public string? ImageURL { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
