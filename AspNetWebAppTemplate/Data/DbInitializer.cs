using AspNetWebAppTemplate.Models;
namespace AspNetWebAppTemplate.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BlogDbContext context)
        {
            // Look for any users.
            //if (context.Users.Any())
            //{
            //    return;
            //}
            //var Users = new User[]
            //{
            //    new User{ID=0, Username="localAdmin", PasswordHash=""}
            //}
            //context.posts.AddRange(posts);
            //context.SaveChanges();
        }
    }
}