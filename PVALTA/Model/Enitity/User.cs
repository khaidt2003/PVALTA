namespace PVALTA.Model.Enitity
{
    public class User
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public ICollection<History> Histories { get; set; } = new List<History>();
    }
}
