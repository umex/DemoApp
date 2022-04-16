using API.DTO;

namespace API.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }  = DateTime.Now;
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Author  { get; set; } 
        public Boolean LentOut  { get; set; } 
        public AppUser User { get; set; }

        public DateTime LendFrom { get; set; }
        public DateTime LendTo { get; set; }

    }
}