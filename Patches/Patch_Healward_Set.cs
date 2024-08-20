using GadgetCore.API;
using HarmonyLib;
using MoreCombatChips.CombatChips;
using MoreCombatChips.ID;
using MoreCombatChips.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will handle HP constraints for modded effects
    /// </summary>
    [HarmonyPatch(typeof(Healward))]
    [HarmonyPatch("Set")]
    [HarmonyPatch(new Type[] { typeof(int) })]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_Healward_Set
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var modifiedCodes = new List<CodeInstruction>();
            var matcherOperand = typeof(GameObject).GetMethod(
                "SendMessage",
                BindingFlags.Instance | BindingFlags.Public,
                null,
                new Type[] { typeof(string), typeof(object) },
                null
            );
            var scaledAugur = typeof(Patch_Healward_Set).GetMethod(
                nameof(ScaledAugur),
                BindingFlags.NonPublic | BindingFlags.Static
            );
            var scaledHealWard = typeof(Patch_Healward_Set).GetMethod(
                nameof(ScaledHealWard),
                BindingFlags.NonPublic | BindingFlags.Static
            );
            for (int i = 0; i < codes.Count; i++)
            {
                if (i >= 3 &&
                    codes[i - 3].opcode == OpCodes.Ldarg_0 &&
                    codes[i - 2].opcode == OpCodes.Ldfld &&
                    codes[i - 1].opcode == OpCodes.Ldstr &&
                    codes[i].opcode == OpCodes.Ldc_I4_2 &&
                    codes[i + 1].opcode == OpCodes.Box &&
                    codes[i + 2].opcode == OpCodes.Callvirt && codes[i + 2].operand == matcherOperand &&
                    codes[i + 3].opcode == OpCodes.Br)
                {
                    MoreCombatChips.Log("Patch_GameScript_UpdateHP: Inserting ScaledAugur...");
                    modifiedCodes.Add(new CodeInstruction(OpCodes.Call, scaledAugur));
                    continue;
                }
                if (i >= 3 &&
                    codes[i - 3].opcode == OpCodes.Ldarg_0 &&
                    codes[i - 2].opcode == OpCodes.Ldfld &&
                    codes[i - 1].opcode == OpCodes.Ldstr &&
                    codes[i].opcode == OpCodes.Ldc_I4_1 &&
                    codes[i + 1].opcode == OpCodes.Box &&
                    codes[i + 2].opcode == OpCodes.Callvirt && codes[i + 2].operand == matcherOperand)
                {
                    MoreCombatChips.Log("Patch_GameScript_UpdateHP: Inserting ScaledHealWard...");
                    var instruction = new CodeInstruction(OpCodes.Call, scaledHealWard);
                    instruction.labels.AddRange(codes[i].labels.ToList());
                    modifiedCodes.Add(instruction);
                    continue;
                }
                modifiedCodes.Add(codes[i]);
            }
            return modifiedCodes;
        }

        private static int ScaledAugur()
        {
            MoreCombatChips.Log("Patch_GameScript_UpdateHP: ScaledAugur works!");
            int healPoints = 2;
            if (ChipService.IsChipEquipped(CombatChip<RejuvenationWaveChip>.ID) > 0)
            {
                healPoints += InstanceTracker.GameScript.GetFinalStat(StatID.FTH) / 50;
            }
            return healPoints;
        }

        private static int ScaledHealWard()
        {
            MoreCombatChips.Log("Patch_GameScript_UpdateHP: ScaledHealWard works!");
            int healPoints = 1;
            if (ChipService.IsChipEquipped(CombatChip<RejuvenationWaveChip>.ID) > 0)
            {
                healPoints += InstanceTracker.GameScript.GetFinalStat(StatID.FTH) / 100;
            }
            return healPoints;
        }
    }
}
