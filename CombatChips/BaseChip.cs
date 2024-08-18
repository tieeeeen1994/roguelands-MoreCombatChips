using GadgetCore.API;
using MoreCombatChips.DataStructures;
using UnityEngine;

namespace MoreCombatChips.CombatChips
{
    public abstract class BaseChip
    {
        public ChipInfo combatChip;

        public BaseChip()
        {
            combatChip = new ChipInfo(Type, Name, Description, Cost, ObjectTexture, Stats, CostType);
        }

        public virtual int Damage => 0;

        public virtual ChipType Type => ChipType.PASSIVE;

        public virtual string Name => "Tien's Chip";

        public virtual string Description => "Tien's Modded Combat Chip.";

        public virtual int Cost => -1;

        public virtual ChipInfo.ChipCostType CostType => ChipInfo.ChipCostType.MANA;

        public virtual Texture ObjectTexture => GadgetCoreAPI.LoadTexture2D(GetType().Name);

        public virtual EquipStats Stats => new EquipStats(0);

        public virtual string KeyName => GetType().Name;

        protected virtual void Action(int slot)
        {
        }

        protected virtual void AddRequiredResources()
        {
        }

        protected virtual void StoreExtraDetails(ref ModdedChip moddedChip)
        {
        }

        protected void ApplyMoreAdvancedChipData(ref ModdedChip moddedChip)
        {
            moddedChip.SetCost(11, 16);
            moddedChip.isAdvanced = true;
        }

        public void Register()
        {
            combatChip.OnUse += Action;
            combatChip.Register(KeyName);
            MoreCombatChips.Log($"Registered Chip: {combatChip.Name} with ID {combatChip.GetID()}" +
                                $" as {combatChip.GetRegistryName()}");

            ModdedChip thisChip = new ModdedChip(combatChip);
            StoreExtraDetails(ref thisChip);
            MoreCombatChips.ModdedChipsList.Add(thisChip);

            AddRequiredResources();
        }
    }
}