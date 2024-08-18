using GadgetCore.API;
using GadgetCore.Util;
using MoreCombatChips.CombatChips;
using MoreCombatChips.Exceptions;
using UnityEngine;
using static MoreCombatChips.MoreCombatChips;

namespace MoreCombatChips.Services
{
    public static class ChipManagementService
    {
        public static int RandomlyGetIDFromAdvanced()
        {
            int randNum = Random.Range(0, ModdedChips.Count);
            return ModdedChips[randNum].ChipInfo.GetID();
        }

        public static int GetIndexFromList(int id)
        {
            return ModdedChips.FindIndex(mc => mc.ChipInfo.GetID() == id);
        }

        public static BaseChip GetChipByIndex(int index)
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
                throw new GameScriptCombatChipsNotFoundException();
            }
        }

        public static int IsChipEquipped(string keyName)
        {
            var moddedChip = ModdedChips.Find(mc => mc.ChipInfo.GetRegistryName() == $"More Combat Chips:{keyName}");
            if (moddedChip == null)
            {
                throw new ModdedChipNotFoundException(keyName);
            }
            else
            {
                return IsChipEquipped(moddedChip.ChipInfo.GetID());
            }
        }
    }
}
