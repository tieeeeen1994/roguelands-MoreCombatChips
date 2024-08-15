using MoreCombatChips.DataStructures;
using System.Collections.Generic;
using UnityEngine;
using static MoreCombatChips.MoreCombatChips;

namespace MoreCombatChips.Services
{
    public static class ChipManagementService
    {
        public static int RandomlyGetIDFromAdvanced()
        {
            List<ModdedChip> chipsList = AdvancedChips();
            int randNum = Random.Range(0, chipsList.Count);
            return chipsList[randNum].chipInfo.GetID();
        }

        public static int GetIndexFromList(int id)
        {
            return ModdedChipsList.FindIndex(mc => mc.chipInfo.GetID() == id);
        }

        public static ModdedChip GetChipByIndex(int index)
        {
            return ModdedChipsList[index];
        }

        private static List<ModdedChip> AdvancedChips()
        {
            return ModdedChipsList.FindAll(mc => mc.isAdvanced);
        }
    }
}