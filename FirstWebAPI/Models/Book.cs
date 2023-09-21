namespace FirstWebAPI.Models
{
    public class Book
    {
        public int BookID { set; get; }
        public String Title { set; get; } = String.Empty;
        public float Cost { set; get; }
        public String AuthorName { set; get; } = String.Empty;
    }
}
