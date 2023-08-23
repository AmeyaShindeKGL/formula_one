using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories
{
    public class AchievementRepository : GenericRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(AppDbContext context, ILogger logger) : base(context, logger)
        { }

        public async Task<Achievement?> GetDriverAchievementAsync(Guid driverId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.DriverId == driverId);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, message: "{Repo} GetDriverAchievementAsync function error", args: typeof(AchievementRepository));
                throw;
            }
        }

        public override async Task<IEnumerable<Achievement>> All()
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1)
                                   .AsNoTracking()
                                   .AsSplitQuery()
                                   .OrderBy(x => x.AddedDate)
                                   .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, message: "{Repo} All function error", args: typeof(AchievementRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                // get my entity
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (result is null)
                {
                    return false;
                }

                result.Status = 0;
                result.UpdatedDate = DateTime.UtcNow;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, message: "{Repo} Delete function error", args: typeof(AchievementRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Achievement achievement)
        {
            try
            {
                // get my entity
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == achievement.Id);

                if (result is null)
                {
                    return false;
                }

                result.UpdatedDate = DateTime.UtcNow;
                result.FastestLap = achievement.FastestLap;
                result.PolePosition = achievement.PolePosition;
                result.RaceWins = achievement.RaceWins;
                result.WorldChampionship = achievement.WorldChampionship;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, message: "{Repo} Update function error", args: typeof(AchievementRepository));
                throw;
            }
        }
    }
}