using GadgetCore.API;
using GadgetCore.Util;
using MoreCombatChips.DataStructures;
using MoreCombatChips.Exceptions;
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

        public static int IsChipEquipped(int id)
        {
            GameScript gameScript = InstanceTracker.GameScript;
            if (gameScript.GetFieldValue<int[]>("combatChips") is int[] combatChips)
            {
                int chipCount = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (combatChips[i] == id)
                    {
                        chipCount++;
                    }
                }
                return chipCount;
            }
            else
            {
                throw new GameScriptCombatChipsNotFoundException();
            }
        }

        public static int IsChipEquipped(string keyName)
        {
            var moddedChip = ModdedChipsList.Find(mc => mc.chipInfo.GetRegistryName() == $"More Combat Chips:{keyName}");
            if (moddedChip == null)
            {
                throw new ModdedChipNotFoundException(keyName);
            }
            else
            {
                return IsChipEquipped(moddedChip.chipInfo.GetID());
            }
        }

        private static List<ModdedChip> AdvancedChips()
        {
            return ModdedChipsList.FindAll(mc => mc.isAdvanced);
        }
    }
}