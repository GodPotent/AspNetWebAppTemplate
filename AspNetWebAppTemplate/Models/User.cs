namespace AspNetWebAppTemplate.Models
{
    public class User
    {
        public int ID { get; set; }

        //Username should be unique
        public string Username { get; set; }
        public string PasswordHash { get; set; }

    }
}
