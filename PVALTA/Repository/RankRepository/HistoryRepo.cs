using Microsoft.EntityFrameworkCore;
using PVALTA.Model;
using PVALTA.Model.Dto;
using PVALTA.Model.Enitity;
using System;
using System.Security.Cryptography;

namespace PVALTA.Repository.RankRepository
{
    public class HistoryRepo : IHistoryRepo
    {
        private readonly AppDbContext _context;

        public HistoryRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RankListResponse> GetRankingScore(RankingBoardRequest request)
        {
            if (request.StartDate >= request.EndDate)
                throw new ArgumentException("EndDate must be greater than StartDate");

            var start = request.StartDate;
            var end = request.EndDate;

            var sumsQ =
                from h in _context.Histories
                where h.Date >= start && h.Date < end
                group h by h.UserId into g
                select new
                {
                    UserId = g.Key,
                    TotalScore = g.Sum(x => x.Score)
                };

            var baseQ =
                from s in sumsQ
                join u in _context.Users on s.UserId equals u.Id
                select new
                {
                    s.UserId,
                    u.Name,
                    u.Avatar,
                    s.TotalScore
                };

            baseQ = baseQ.OrderByDescending(x => x.TotalScore).ThenBy(x => x.Name);

            if (request.TopN is int top && top > 0)
                baseQ = baseQ.Take(top);

            var all = await baseQ.ToListAsync();

            var ranked = new List<RankingUserInWeek>(all.Count);
            int pos = 0;           
            int currentRank = 0;   
            int? lastScore = null; 

            foreach (var u in all)
            {
                pos++;
                if (lastScore is null || u.TotalScore != lastScore.Value)
                {
                    currentRank = pos;       
                    lastScore = u.TotalScore;
                }

                ranked.Add(new RankingUserInWeek
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Avatar = u.Avatar,
                    TotalScore = u.TotalScore,
                    Rank = currentRank
                });
            }

            RankingUserInWeek? currentUser = null;
            if (request.currentUserId is int uid)
            {
                currentUser = ranked.FirstOrDefault(x => x.UserId == uid);

                if (currentUser == null)
                {
                    var info = await _context.Users
                        .Where(x => x.Id == uid)
                        .Select(x => new { x.Id, x.Name, x.Avatar })
                        .FirstOrDefaultAsync();

                    if (info != null)
                    {
                        currentUser = new RankingUserInWeek
                        {
                            UserId = info.Id,
                            Name = info.Name,
                            Avatar = info.Avatar,
                            TotalScore = 0,
                            Rank = ranked.Count + 1
                        };
                    }
                }
            }

            int pageIndex = request.pageIndex < 0 ? 0 : request.pageIndex;
            int pageSize = request.pageSize <= 0 ? 30 : request.pageSize; // mặc định 30 nếu không truyền
            var pageItems = ranked.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            var response = new RankListResponse
            {
                RankList = pageItems,
                TotalCount = ranked.Count,
                CurrentUser = currentUser
            };

            return response;
        }

    }
}
