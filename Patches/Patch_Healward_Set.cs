using GadgetCore.API;
using GadgetCore.Util;
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
        private static MethodInfo SendMessageOperand
        {
            get => typeof(GameObject).GetMethod(
                "SendMessage",
                BindingFlags.Instance | BindingFlags.Public,
                null,
                new Type[] { typeof(string), typeof(object) },
                null
            );
        }

        private static MethodInfo ScaledAugurMethod
        {
            get => typeof(Patch_Healward_Set).GetMethod(
                nameof(ScaledAugur),
                BindingFlags.NonPublic | BindingFlags.Static
            );
        }

        private static MethodInfo ScaledHealWardMethod
        {
            get => typeof(Patch_Healward_Set).GetMethod(
                nameof(ScaledHealWard),
                BindingFlags.NonPublic | BindingFlags.Static
            );
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            EmitScaledAugur(p);
            EmitScaledHealWard(p);
            var codes = new List<CodeInstruction>(insns);
            var modifiedCodes = new List<CodeInstruction>();
            for (int i = 0; i < codes.Count; i++)
            {
                if (i >= 3 &&
                    codes[i - 3].opcode == OpCodes.Ldarg_0 &&
                    codes[i - 2].opcode == OpCodes.Ldfld &&
                    codes[i - 1].opcode == OpCodes.Ldstr &&
                    codes[i].opcode == OpCodes.Ldc_I4_2 &&
                    codes[i + 1].opcode == OpCodes.Box &&
                    codes[i + 2].opcode == OpCodes.Callvirt && codes[i + 2].operand == SendMessageOperand &&
                    codes[i + 3].opcode == OpCodes.Br)
                {
                    MoreCombatChips.Log("Patch_GameScript_UpdateHP: Inserting ScaledAugur...");
                    modifiedCodes.Add(new CodeInstruction(OpCodes.Call, ScaledAugurMethod));
                    continue;
                }
                if (i >= 3 &&
                    codes[i - 3].opcode == OpCodes.Ldarg_0 &&
                    codes[i - 2].opcode == OpCodes.Ldfld &&
                    codes[i - 1].opcode == OpCodes.Ldstr &&
                    codes[i].opcode == OpCodes.Ldc_I4_1 &&
                    codes[i + 1].opcode == OpCodes.Box &&
                    codes[i + 2].opcode == OpCodes.Callvirt && codes[i + 2].operand == SendMessageOperand)
                {
                    MoreCombatChips.Log("Patch_GameScript_UpdateHP: Inserting ScaledHealWard...");
                    var instruction = new CodeInstruction(OpCodes.Call, ScaledHealWardMethod);
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

        private static void EmitScaledAugur(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Ldstr),
                new CodeInstruction(OpCodes.Ldc_I4_2),
                new CodeInstruction(OpCodes.Box),
                new CodeInstruction(OpCodes.Callvirt, SendMessageOperand),
                new CodeInstruction(OpCodes.Br)
            });
            if (ilRef == null)
            {
                MoreCombatChips.Log("Patch_Healward_Set: No reference point. (EmitScaledAugur)");
            }
            else
            {
                ilRef = ilRef.GetRefByOffset(3);
                p.InjectInsn(ilRef, new CodeInstruction(OpCodes.Call, ScaledAugurMethod), insert: false);
            }
        }

        private static void EmitScaledHealWard(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Ldstr),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Box),
                new CodeInstruction(OpCodes.Callvirt, SendMessageOperand)
            });
            if (ilRef == null)
            {
                MoreCombatChips.Log("Patch_Healward_Set: No reference point.");
            }
            else
            {
                ilRef = ilRef.GetRefByOffset(3);
                p.InjectInsn(ilRef, new CodeInstruction(OpCodes.Call, ScaledHealWardMethod), insert: false);
            }
        }
    }
}
