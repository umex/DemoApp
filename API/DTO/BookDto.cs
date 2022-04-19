using System.ComponentModel.DataAnnotations;
using API.Helpers;

namespace API.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Author  { get; set; } 
        public Boolean LentOut  { get; set; } 
        public UserDto User { get; set; }

        [DateLendFromValidation]
        [Display(Name = "LendFrom")]
        public DateTime LendFrom { get; set; }
        [DateLendToValidation("LendFrom")]
        public DateTime LendTo { get; set; }
    }
}