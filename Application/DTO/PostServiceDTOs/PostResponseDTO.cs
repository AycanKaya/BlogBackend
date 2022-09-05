
namespace Application.DTO
{
    public class PostResponseDTO
    {
        public int? PostId { get; set; }
       public string? AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsApprove { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDeleted { get; set; }





    }
}
