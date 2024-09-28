using MyBlogApp.Domain.Entities.Common;

namespace MyBlogApp.Domain.Entities
{
    public class User : BaseEntitiy
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
