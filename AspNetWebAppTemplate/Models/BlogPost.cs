namespace AspNetWebAppTemplate.Models
{
    public class BlogPost
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }

        public string Title { get; set; }

        public string HtmlContent { get; set; }

        //public List<int>AuthorizedEditors { get; set; }
        // TODO: foreign key
        // public User Author { get; set; }

        // public List<User> AuthorizedEdtiors { get; set; }
    }
}
