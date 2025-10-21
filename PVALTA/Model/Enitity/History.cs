namespace PVALTA.Model.Enitity
{
    public class History
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }
    }
}
