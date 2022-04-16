namespace API.Entities
{
    public class LibraryLedger
    {
        public int Id { get; set; }
        public AppUser User { get; set; }
        public Book Book { get; set; }
        public DateTime Created { get; set; }  = DateTime.Now;
        public DateTime LentFrom { get; set; }
        public DateTime LentTo { get; set; }
        public bool LentOut { get; set; }
        public bool Overdue { get; set; }

    }
}