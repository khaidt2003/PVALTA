namespace PVALTA.Model.Dto
{
    public class RankingUserInWeek
    {
        public int UserId { get; set; }
        public string? Avatar { get; set; }
        public string? Name { get; set; }
        public int TotalScore { get; set; }

        public int Rank { get; set; }
    }
}
