using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FilRouge.Web.Entities;
using FilRouge.Web.ViewModels;
using FilRouge.Web.ViewModels.Maps;

namespace FilRouge.Web.Services
{
    public class DifficultyServices
    {
        public List<DifficultyViewModel> GetAllDifficulties()
        {
            var diffiicultyViewModel = new List<DifficultyViewModel>();
            using (var dbContext = new FilRougeDBContext())
            {
                var difficultyEntities = dbContext.Difficulties.ToList();

                foreach (var difficulty in difficultyEntities)
                {
                    diffiicultyViewModel.Add(difficulty.MapToDifficultyViewModel());
                }
            }

            return diffiicultyViewModel;
        }

        public DifficultyViewModel GetDifficultyById(int difficultyId)
        {
            var difficultyViewModel = new DifficultyViewModel();
            using (var dbContext = new FilRougeDBContext())
            {
               var difficultyEntities = dbContext.Difficulties.Find(difficultyId);
               difficultyViewModel = difficultyEntities.MapToDifficultyViewModel();
            }

            return difficultyViewModel;
        }

        public int AddDifficulty(Difficulty difficulty)
        {
            int id = 0;

            using (var dbContext = new FilRougeDBContext())
            {
                dbContext.Difficulties.Add(difficulty);
                dbContext.SaveChanges();
                id = difficulty.DifficultyId;
            }
            return id;
        }

        public int EditDifficulty(Difficulty difficultyViewModel)
        {
            var id = 0;
            using (var dbContext = new FilRougeDBContext())
            {
                var difficulty = dbContext.Difficulties.Find(difficultyViewModel.DifficultyId);
                difficulty.DifficultyId = difficultyViewModel.DifficultyId;
                difficulty.DifficultyName = difficultyViewModel.DifficultyName;
                dbContext.Entry(difficulty).State = EntityState.Modified;
                dbContext.SaveChanges();
                id = difficulty.DifficultyId;
            }
            return id;
        }
    }
}