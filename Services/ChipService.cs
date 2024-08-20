using GadgetCore.API;
using GadgetCore.Util;
using MoreCombatChips.CombatChips;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreCombatChips.Services
{
    public static class ChipService
    {
        private static readonly List<CombatChip> _moddedChips = new List<CombatChip>();

        public static List<CombatChip> ModdedChips => _moddedChips.ToList();

        public static List<CombatChip> AllAdvancedChips => ModdedChips.FindAll(mc => mc.Advanced);

        public static bool Register(CombatChip combatChip)
        {
            if (_moddedChips.Exists(mc => mc.ChipInfo.GetRegistryName() == combatChip.ChipInfo.GetRegistryName()))
            {
                return false;
            }
            else
            {
                _moddedChips.Add(combatChip);
                return true;
            }
        }

        public static int RandomlyGetIDFromAdvanced()
        {
            var advancedChips = AllAdvancedChips;
            int randNum = UnityEngine.Random.Range(0, advancedChips.Count);
            return advancedChips[randNum].ChipInfo.GetID();
        }

        public static int GetIndexFromList(int id)
        {
            return ModdedChips.FindIndex(mc => mc.ChipInfo.GetID() == id);
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
                string message = "GameScript.combatChips field not found.";
                MoreCombatChips.Error(message);
                throw new Exception(message);
            }
        }

        public static int IsChipEquipped(string keyName)
        {
            var moddedChip = ModdedChips.Find(mc => mc.ChipInfo.GetRegistryName() == $"More Combat Chips:{keyName}");
            if (moddedChip == null)
            {
                string message = $"{keyName} chip not found.";
                MoreCombatChips.Error(message);
                throw new Exception(message);
            }
            else
            {
                return IsChipEquipped(moddedChip.ChipInfo.GetID());
            }
        }
    }
}
