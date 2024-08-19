using System;
using GadgetCore.API;
using GadgetCore.Util;
using MoreCombatChips.CombatChips;
using static MoreCombatChips.MoreCombatChips;

namespace MoreCombatChips.Services
{
    public static class ChipService
    {
        public static int RandomlyGetIDFromAdvanced()
        {
            int randNum = UnityEngine.Random.Range(0, ModdedChips.Count);
            return ModdedChips[randNum].ChipInfo.GetID();
        }

        public static int GetIndexFromList(int id)
        {
            return ModdedChips.FindIndex(mc => mc.ChipInfo.GetID() == id);
        }

        public static CombatChip GetChipByIndex(int index)
        {
            return ModdedChips[index];
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
                MoreCombatChips.GetLogger().LogError(message);
                throw new Exception(message);
            }
        }

        public static int IsChipEquipped(string keyName)
        {
            var moddedChip = ModdedChips.Find(mc => mc.ChipInfo.GetRegistryName() == $"More Combat Chips:{keyName}");
            if (moddedChip == null)
            {
                string message = $"{keyName} chip not found.";
                MoreCombatChips.GetLogger().LogError(message);
                throw new Exception(message);
            }
            else
            {
                return IsChipEquipped(moddedChip.ChipInfo.GetID());
            }
        }
    }
}
