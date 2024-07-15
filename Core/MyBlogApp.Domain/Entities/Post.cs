using System.ComponentModel.DataAnnotations.Schema;
using MyBlogApp.Domain.Entities.Common;

namespace MyBlogApp.Domain.Entities
{
    public class Post : BaseEntitiy
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public Guid AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }

        public Guid BlogId { get; set; }

        [ForeignKey(nameof(BlogId))]
        public Blog Blog { get; set; }
    }
}
