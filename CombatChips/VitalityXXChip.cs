﻿using GadgetCore.API;
using TienContentMod.Services;

namespace TienContentMod.CombatChips
{
    public class VitalityXXChip : CombatChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Vitality XX";

        public override string Description => "+24 Vitality";

        public override EquipStats Stats => StatService.EquipStats(VIT: 24);

        public override bool Advanced => true;
    }
}