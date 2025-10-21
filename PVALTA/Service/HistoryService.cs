using PVALTA.Model.Dto;
using PVALTA.Repository.RankRepository;

namespace PVALTA.Service
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepo _repo;
        public HistoryService(IHistoryRepo repo) => _repo = repo;
        public Task<RankListResponse> GetByRangeAsync(RankingBoardRequest req)
        => _repo.GetRankingScore(req);
    }
}
