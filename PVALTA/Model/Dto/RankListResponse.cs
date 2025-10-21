namespace PVALTA.Model.Dto
{
    public class RankListResponse
    {
        public List<RankingUserInWeek> RankList { get; set; } = new();

        public RankingUserInWeek? CurrentUser { get; set; }

        public int TotalCount { get; set; }
    }
}
