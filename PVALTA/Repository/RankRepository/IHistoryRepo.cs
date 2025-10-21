using PVALTA.Model.Dto;

namespace PVALTA.Repository.RankRepository
{
    public interface IHistoryRepo
    {
        Task<RankListResponse> GetRankingScore(RankingBoardRequest req);
    }
}
