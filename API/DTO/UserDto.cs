namespace API.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        private ICollection<BookDto> Books { get; set; }

    }
}