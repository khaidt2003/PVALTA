namespace PVALTA.Model.Dto
{
    public class RankingBoardRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? currentUserId { get; set; }

        public int? TopN { get; set; }

        public int pageIndex { get; set; }

        public int pageSize { get; set; }


    }
}
