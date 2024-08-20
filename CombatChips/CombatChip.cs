using GadgetCore.API;
using MoreCombatChips.Services;
using System;
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

        public virtual bool Advanced => false;

        protected virtual void Action(int slot) { }

        protected virtual void AddRequiredResources() { }

        public void Register()
        {
            if (ChipService.Register(this))
            {
                _chipInfo.OnUse += Action;
                _chipInfo.Register(GetType().Name);
                AddRequiredResources();
                MoreCombatChips.Log($"Registered Chip: {_chipInfo.Name} with ID {_chipInfo.GetID()}" +
                                    $" as {_chipInfo.GetRegistryName()}");
            }
            else
            {
                string message = $"{GetType().Name} is already registered. This is not supposed to happen!";
                MoreCombatChips.Error(message);
                throw new Exception(message);
            }
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
