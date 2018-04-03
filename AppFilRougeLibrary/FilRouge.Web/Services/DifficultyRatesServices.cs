using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.Web.ViewModels;
using FilRouge.Web.Entities;
using FilRouge.Web.ViewModels.Maps;

namespace FilRouge.Web.Services
{
    public class DifficultyRatesServices
    {

        public List<DifficultyRate> GetAllDifficultyRates()
        {
            var difficultyRateViewModel = new List<DifficultyRate>();
            using (var dbContext = new FilRougeDBContext())
            {
                var difficultyEntities = dbContext.DifficultyRates.Include("Difficulty").Include("DifficultyMaster").ToList();

                foreach (var difficulty in difficultyEntities)
                {

                    difficultyRateViewModel.Add(difficulty);
                }
            }

            return difficultyRateViewModel;
        }

        public DifficultyRateViewModel GetDifficultyRateById(int difficultyRateId, int difficultyRateMasterId)
        {
            var difficultyRateViewModel = new DifficultyRateViewModel();
            using (var dbContext = new FilRougeDBContext())
            {
                var difficultyRateEntities = dbContext.Difficulties.Find(difficultyRateId).DifficultyId;
                var difficultyMasterRateEntities = dbContext.Difficulties.Find(difficultyRateMasterId).DifficultyId;
                difficultyRateViewModel.DifficultyId = difficultyRateEntities;
                difficultyRateViewModel.DifficultyMasterId = difficultyMasterRateEntities;
                var rateDifficulty = from rate in dbContext.DifficultyRates
                           where (rate.DifficultyId == difficultyRateEntities && rate.DifficultyMasterId == difficultyMasterRateEntities)
                           select (rate.Rate);
                difficultyRateViewModel.Rate = rateDifficulty.First();
            }

            return difficultyRateViewModel;
        }

    }
}