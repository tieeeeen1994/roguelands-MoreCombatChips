using GadgetCore.API;
using UnityEngine;

namespace MoreCombatChips.DataStructures
{
    public class ModdedChip
    {
        public ChipInfo chipInfo;
        public bool isAdvanced = false;
        public int minCost = -1;
        public int maxCost = -1;

        public ModdedChip(ChipInfo chip)
        {
            chipInfo = chip;
        }

        public void SetCost(int min = 0, int max = 0)
        {
            minCost = min;
            maxCost = max;
        }

        public void SetCost(int cost = 0)
        {
            minCost = cost;
            maxCost = minCost;
        }

        public int ComputeCost()
        {
            return Random.Range(minCost, maxCost);
        }
    }
}