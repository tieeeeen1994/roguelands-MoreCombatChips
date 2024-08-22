using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using MoreCombatChips.ID;
using MoreCombatChips.Services;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will add modded stats upon level up.
    /// </summary>
    [HarmonyPatch]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_GameScript_LevelUp2
    {
        private static FieldInfo PlayerLevel
        {
            get => typeof(GameScript).GetField("playerLevel", BindingFlags.Static | BindingFlags.Public);
        }

        private static MethodInfo ExtraAugmentComputationMethod
        {
            get => typeof(Patch_GameScript_LevelUp2).GetMethod(
                nameof(ExtraAugmentComputation),
                BindingFlags.Static | BindingFlags.NonPublic
            );
        }

        private static FieldInfo RaceStat
        {
            get => typeof(Menuu).GetField("raceStat", BindingFlags.Static | BindingFlags.Public);
        }

        private static FieldInfo CurAugment
        {
            get => typeof(Menuu).GetField("curAugment", BindingFlags.Static | BindingFlags.Public);
        }

        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            return typeof(GameScript).GetNestedType("<LevelUp2>c__IteratorD", BindingFlags.NonPublic)
                                     .GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.Public);
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            EmitDelegateExtraAugmentComputation(p);
            if (MoreCombatChips.EyepodHatChange)
            {
                EyepodHatChange1(p);
                EyepodHatChange2(p);
            }
            return p.Insns;
        }

        private static void ExtraAugmentComputation(ref int[] array2)
        {
            MoreCombatChips.Log("Patch_GameScript_LevelUp2: It works!");
            switch (Menuu.curAugment)
            {
                case AugmentID.RebellionHeadpiece:
                    array2[StatID.DEX] += 2;
                    break;
                case AugmentID.CreatorMask:
                    if (Util.RandomCheck(25))
                    {
                        array2[StatID.MAG] += 1;
                    }
                    else
                    {
                        array2[StatID.FTH] += 1;
                    }
                    break;
            }
        }

        private static void EmitDelegateExtraAugmentComputation(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldsfld, PlayerLevel),
                new CodeInstruction(OpCodes.Ldc_I4_3),
                new CodeInstruction(OpCodes.Rem),
                new CodeInstruction(OpCodes.Brtrue)
            });
            if (ilRef == null)
            {
                string message = "Patch_GameScript_LevelUp2: Transpiler could not find any reference point. (1)";
                MoreCombatChips.Log(message);
            }
            else
            {
                p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldloca_S, 4),
                    new CodeInstruction(OpCodes.Call, ExtraAugmentComputationMethod)
                }, insert: true);
            }
        }

        private static void EyepodHatChange1(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldsfld, RaceStat),
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Ldelem_I4),
                new CodeInstruction(OpCodes.Ldc_I4_2),
                new CodeInstruction(OpCodes.Sub),
                new CodeInstruction(OpCodes.Stloc_S),
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Ldelem_I4),
                new CodeInstruction(OpCodes.Add),
                new CodeInstruction(OpCodes.Stloc_S),
                new CodeInstruction(OpCodes.Ldloc_S)
            });
            if (ilRef == null)
            {
                string message = "Patch_GameScript_LevelUp2: Transpiler could not find any reference point. (2)";
                MoreCombatChips.Log(message);
            }
            else
            {
                ilRef = ilRef.GetRefByOffset(12);
                ilRef = p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldsfld, CurAugment),
                    new CodeInstruction(OpCodes.Ldc_I4, AugmentID.EyepodHat),
                    new CodeInstruction(OpCodes.Bne_Un_S),
                    new CodeInstruction(OpCodes.Ldloc_S, 6),
                    new CodeInstruction(OpCodes.Ldc_I4_2),
                    new CodeInstruction(OpCodes.Mul),
                    new CodeInstruction(OpCodes.Stloc_S, 6)
                }, insert: true);
                ilRef.GetRefByOffset(2).GetInsn().operand = p.MarkLabel(ilRef + 7);
            }
        }

        private static void EyepodHatChange2(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Add),
                new CodeInstruction(OpCodes.Stloc_S),
                new CodeInstruction(OpCodes.Ldloc_S),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Ble)
            });
            if (ilRef == null)
            {
                string message = "Patch_GameScript_LevelUp2: Transpiler could not find any reference point. (3)";
                MoreCombatChips.Log(message);
            }
            else
            {
                ilRef = ilRef.GetRefByOffset(4);
                ilRef = p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldsfld, CurAugment),
                    new CodeInstruction(OpCodes.Ldc_I4, AugmentID.EyepodHat),
                    new CodeInstruction(OpCodes.Bne_Un_S),
                    new CodeInstruction(OpCodes.Ldloc_S, 8),
                    new CodeInstruction(OpCodes.Ldc_I4_2),
                    new CodeInstruction(OpCodes.Mul),
                    new CodeInstruction(OpCodes.Stloc_S, 8)
                }, insert: true);
                ilRef.GetRefByOffset(2).GetInsn().operand = p.MarkLabel(ilRef + 7);
            }
        }
    }
}
