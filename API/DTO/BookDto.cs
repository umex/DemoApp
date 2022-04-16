using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

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

        public DateTime LendFrom { get; set; }
        public DateTime LendTo { get; set; }
    }
}