﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.Web.Entities;
using FilRouge.Web.ViewModels;
using FilRouge.Web.ViewModels.Maps;

namespace FilRouge.Web.Services
{
    public class DifficultyMasterServices
    {
        public List<DifficultyMasterViewModel> GetAllDifficulties()
        {
            var difficultyViewModel = new List<DifficultyMasterViewModel>();
            using (var dbContext = new FilRougeDBContext())
            {
                var difficultyEntities = dbContext.DifficultyMasters.ToList();

                foreach (var difficulty in difficultyEntities)
                {
                    difficultyViewModel.Add(difficulty.MapToDifficultyMaster());
                }
            }

            return difficultyViewModel;
        }

        public DifficultyMasterViewModel GetDifficultyById(int difficultyId)
        {
            var difficultyViewModel = new DifficultyMasterViewModel();
            using (var dbContext = new FilRougeDBContext())
            {
                var difficultyEntities = dbContext.Difficulties.Find(difficultyId);
                difficultyViewModel = difficultyEntities.MapToDifficultyViewModel();
            }

            return difficultyViewModel;
        }

        public int AddDifficulty(DifficultyMasterViewModel difficultyViewModel)
        {
            int id = 0;

            using (var dbContext = new FilRougeDBContext())
            {
                var difficulty = difficultyViewModel.MapToDifficulty();
                dbContext.Difficulties.Add(difficulty);
                dbContext.SaveChanges();
                id = difficulty.DifficultyId;
            }
            return id;
        }

        public int EditDifficulty(DifficultyMasterViewModel difficultyViewModel)
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