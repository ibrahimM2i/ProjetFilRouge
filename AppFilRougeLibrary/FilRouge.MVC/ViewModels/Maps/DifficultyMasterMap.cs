using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilRouge.Web.Entities;

namespace FilRouge.Web.ViewModels.Maps
{
    public static class DifficultyMasterMap
    {
        /// <summary>
        /// Convertir une Difficulté de Quizz en View.
        /// </summary>
        /// <param name="difficultyMaster"></param>
        /// <returns>New DifficultyMasterViewModel</returns>
        public static DifficultyMasterViewModel MapToDifficultyMasterViewModel(this DifficultyMaster difficultyMaster)
        {
            var difficultyMasterViewModel = new DifficultyMasterViewModel();
            if (difficultyMaster == null)
                return difficultyMasterViewModel;
            else
            {
                difficultyMasterViewModel = new DifficultyMasterViewModel
                {
                    DiffMasterId = difficultyMaster.DiffMasterId,
                    DiffMasterName = difficultyMaster.DiffMasterName
                };
                return difficultyMasterViewModel;
            }
        }

        /// <summary>
        /// Convertir une DifficultyViewModel en Difficulty de Quizz
        /// </summary>
        /// <param name="difficultyViewModel"></param>
        /// <returns>new Difficulty</returns>
        public static DifficultyMaster MapToDifficultyMaster(this DifficultyMasterViewModel difficultyMasterViewModel)
        {
            var difficultyMaster = new DifficultyMaster();
            if (difficultyMasterViewModel == null)
                return difficultyMaster;
            else
            {
                difficultyMaster = new DifficultyMaster
                {
                    DiffMasterId = difficultyMasterViewModel.DiffMasterId,
                    DiffMasterName = difficultyMasterViewModel.DiffMasterName
                };
                return difficultyMaster;
            }
        }
    }
}