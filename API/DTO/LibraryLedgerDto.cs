namespace API.DTO
{
    public class LibraryLedgerDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public BookDto Book { get; set; }
        public DateTime Created { get; set; }
        public DateTime LentFrom { get; set; }
        public DateTime LentTo { get; set; }
        public bool LentOut { get; set; }
        public bool Overdue { get; set; }
    }
}