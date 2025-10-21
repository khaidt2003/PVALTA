using PVALTA.Model.Dto;

namespace PVALTA.Service
{
    public interface IHistoryService
    {
        Task<RankListResponse> GetByRangeAsync(RankingBoardRequest req);
    }
}
