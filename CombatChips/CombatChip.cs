using GadgetCore.API;
using UnityEngine;
using CI = GadgetCore.API.ChipInfo;

namespace MoreCombatChips.CombatChips
{
    public abstract class CombatChip
    {
        public CI ChipInfo => _chipInfo;

        private readonly CI _chipInfo;

        protected CombatChip()
        {
            _chipInfo = new CI(Type, Name, Description, Cost, ObjectTexture, Stats, CostType);
        }

        public virtual int Damage => 0;

        public virtual ChipType Type => ChipType.PASSIVE;

        public virtual string Name => "Tien's Chip";

        public virtual string Description => "Tien's Modded Combat Chip.";

        public virtual int Cost => -1;

        public virtual Texture2D ObjectTexture => GadgetCoreAPI.LoadTexture2D(GetType().Name);

        public virtual EquipStats Stats => new EquipStats(0);

        public virtual CI.ChipCostType CostType => CI.ChipCostType.MANA;

        protected virtual void Action(int slot) { }

        protected virtual void AddRequiredResources() { }

        public void Register()
        {
            _chipInfo.OnUse += Action;
            _chipInfo.Register(GetType().Name);
            AddRequiredResources();
            MoreCombatChips.ModdedChips.Add(this);
            MoreCombatChips.Log($"Registered Chip: {_chipInfo.Name} with ID {_chipInfo.GetID()}" +
                                $" as {_chipInfo.GetRegistryName()}");
        }
    }

    public static class CombatChip<T> where T : CombatChip, new()
    {
        private static readonly T _instance = new T();

        public static T Instance => _instance;

        public static T I => Instance;

        public static int ID => Instance.ChipInfo.GetID();
    }
}
