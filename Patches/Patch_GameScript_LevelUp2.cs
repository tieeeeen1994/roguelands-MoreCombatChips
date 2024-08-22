using GadgetCore.API;
using HarmonyLib;
using MoreCombatChips.ID;
using MoreCombatChips.Services;
using System.Collections.Generic;
using System.Linq;
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
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            return typeof(GameScript).GetNestedType("<LevelUp2>c__IteratorD", BindingFlags.NonPublic)
                                     .GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.Public);
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions,
                                                              ILGenerator ilgenerator)
        {
            var codes = instructions.ToList();
            var modifiedCodes = new List<CodeInstruction>();
            var delegateMethod = typeof(Patch_GameScript_LevelUp2).GetMethod(
                nameof(ExtraAugmentComputation),
                BindingFlags.Static | BindingFlags.NonPublic
            );
            var matcherOperand = typeof(GameScript).GetField(
                "playerLevel",
                BindingFlags.Static | BindingFlags.Public
            );

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldsfld && codes[i].operand == matcherOperand &&
                    codes[i + 1].opcode == OpCodes.Ldc_I4_3 &&
                    codes[i + 2].opcode == OpCodes.Rem &&
                    codes[i + 3].opcode == OpCodes.Brtrue)
                {
                    MoreCombatChips.Log("Patch_GameScript_LevelUp2: Emit ExtraAugmentComputation");
                    var instruction = new CodeInstruction(OpCodes.Ldloca_S, 4);
                    instruction.labels.AddRange(codes[i].labels);
                    modifiedCodes.Add(instruction);
                    modifiedCodes.Add(new CodeInstruction(OpCodes.Call, delegateMethod));
                    codes[i].labels.Clear();
                }
                if (MoreCombatChips.EyepodHatChange)
                {
                    var operand = typeof(Menuu).GetField(
                        "raceStat",
                        BindingFlags.Static | BindingFlags.Public
                    );
                    var augmentOperand = typeof(Menuu).GetField(
                        "curAugment",
                        BindingFlags.Static | BindingFlags.Public
                    );
                    if (i >= 12 &&
                        codes[i - 12].opcode == OpCodes.Ldsfld && codes[i - 12].operand == operand &&
                        codes[i - 11].opcode == OpCodes.Ldloc_S &&
                        codes[i - 10].opcode == OpCodes.Ldelem_I4 &&
                        codes[i - 9].opcode == OpCodes.Ldc_I4_2 &&
                        codes[i - 8].opcode == OpCodes.Sub &&
                        codes[i - 7].opcode == OpCodes.Stloc_S &&
                        codes[i - 6].opcode == OpCodes.Ldloc_S &&
                        codes[i - 5].opcode == OpCodes.Ldloc_S &&
                        codes[i - 4].opcode == OpCodes.Ldloc_S &&
                        codes[i - 3].opcode == OpCodes.Ldelem_I4 &&
                        codes[i - 2].opcode == OpCodes.Add &&
                        codes[i - 1].opcode == OpCodes.Stloc_S &&
                        codes[i].opcode == OpCodes.Ldloc_S)
                    {
                        MoreCombatChips.Log("Patch_GameScript_LevelUp2: Add Eyepod Hat Level Up computation (1)");
                        var skip = ilgenerator.DefineLabel();
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Ldsfld, augmentOperand));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Ldc_I4, AugmentID.EyepodHat));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Bne_Un_S, skip));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Ldloc_S, 6));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Ldc_I4_2));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Mul));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Stloc_S, 6));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Nop) { labels = { skip } });
                    }
                    else if (i >= 4 &&
                             codes[i - 4].opcode == OpCodes.Ldloc_S &&
                             codes[i - 3].opcode == OpCodes.Ldc_I4_1 &&
                             codes[i - 2].opcode == OpCodes.Add &&
                             codes[i - 1].opcode == OpCodes.Stloc_S &&
                             codes[i].opcode == OpCodes.Ldloc_S &&
                             codes[i + 1].opcode == OpCodes.Ldc_I4_0 &&
                             codes[i + 2].opcode == OpCodes.Ble)
                    {
                        MoreCombatChips.Log("Patch_GameScript_LevelUp2: Add Eyepod Hat Level Up computation (2)");
                        var skip = ilgenerator.DefineLabel();
                        var instruction = new CodeInstruction(OpCodes.Ldsfld, augmentOperand);
                        instruction.labels.AddRange(codes[i].labels);
                        modifiedCodes.Add(instruction);
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Ldc_I4, AugmentID.EyepodHat));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Bne_Un_S, skip));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Ldloc_S, 8));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Ldc_I4_2));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Mul));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Stloc_S, 8));
                        modifiedCodes.Add(new CodeInstruction(OpCodes.Nop) { labels = { skip } });
                        codes[i].labels.Clear();
                    }
                }
                modifiedCodes.Add(codes[i]);
            }
            return modifiedCodes;
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
    }
}

// using GadgetCore.API;
// using GadgetCore.Util;
// using HarmonyLib;
// using MoreCombatChips.ID;
// using MoreCombatChips.Services;
// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using System.Reflection.Emit;

// namespace MoreCombatChips.Patches
// {
//     /// <summary>
//     /// This will add modded stats upon level up.
//     /// </summary>
//     [HarmonyPatch]
//     [HarmonyGadget("More Combat Chips")]
//     public static class Patch_GameScript_LevelUp2
//     {
//         private static FieldInfo PlayerLevel
//         {
//             get => typeof(GameScript).GetField("playerLevel", BindingFlags.Static | BindingFlags.Public);
//         }

//         private static MethodInfo ExtraAugmentComputationMethod
//         {
//             get => typeof(Patch_GameScript_LevelUp2).GetMethod(
//                 nameof(ExtraAugmentComputation),
//                 BindingFlags.Static | BindingFlags.NonPublic
//             );
//         }

//         private static FieldInfo RaceStat
//         {
//             get => typeof(Menuu).GetField("raceStat", BindingFlags.Static | BindingFlags.Public);
//         }

//         private static FieldInfo CurAugment
//         {
//             get => typeof(Menuu).GetField("curAugment", BindingFlags.Static | BindingFlags.Public);
//         }

//         [HarmonyTargetMethod]
//         public static MethodBase TargetMethod()
//         {
//             return typeof(GameScript).GetNestedType("<LevelUp2>c__IteratorD", BindingFlags.NonPublic)
//                                      .GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.Public);
//         }

//         [HarmonyTranspiler]
//         public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
//         {
//             var p = TranspilerHelper.CreateProcessor(insns, il);
//             var ilRef = p.FindRefByInsns(new CodeInstruction[]
//             {
//                 new CodeInstruction(OpCodes.Ldsfld, PlayerLevel),
//                 new CodeInstruction(OpCodes.Ldc_I4_3),
//                 new CodeInstruction(OpCodes.Rem),
//                 new CodeInstruction(OpCodes.Brtrue)
//             });
//             if (ilRef == null)
//             {
//                 string message = "Patch_GameScript_LevelUp2: Transpiler could not find any reference point. (1)";
//                 MoreCombatChips.Error(message);
//                 throw new Exception(message);
//             }
//             else
//             {
//                 p.InjectInsns(ilRef, new CodeInstruction[]
//                 {
//                     new CodeInstruction(OpCodes.Ldloca_S, 4).MoveLabel(ilRef),
//                     new CodeInstruction(OpCodes.Call, ExtraAugmentComputationMethod)
//                 }, insert: true);
//             }
//             if (MoreCombatChips.EyepodHatChange)
//             {
//                 ilRef = p.FindRefByInsns(new CodeInstruction[]
//                 {
//                     new CodeInstruction(OpCodes.Ldsfld, RaceStat),
//                     new CodeInstruction(OpCodes.Ldloc_S),
//                     new CodeInstruction(OpCodes.Ldelem_I4),
//                     new CodeInstruction(OpCodes.Ldc_I4_2),
//                     new CodeInstruction(OpCodes.Sub),
//                     new CodeInstruction(OpCodes.Stloc_S),
//                     new CodeInstruction(OpCodes.Ldloc_S),
//                     new CodeInstruction(OpCodes.Ldloc_S),
//                     new CodeInstruction(OpCodes.Ldloc_S),
//                     new CodeInstruction(OpCodes.Ldelem_I4),
//                     new CodeInstruction(OpCodes.Add),
//                     new CodeInstruction(OpCodes.Stloc_S),
//                     new CodeInstruction(OpCodes.Ldloc_S)
//                 });
//                 if (ilRef == null)
//                 {
//                     string message = "Patch_GameScript_LevelUp2: Transpiler could not find any reference point. (2)";
//                     MoreCombatChips.Error(message);
//                     throw new Exception(message);
//                 }
//                 else
//                 {
//                     ilRef = ilRef.GetRefByOffset(12);
//                     var skip = p.MarkLabel(ilRef);
//                     p.InjectInsns(ilRef, new CodeInstruction[]
//                     {
//                         new CodeInstruction(OpCodes.Ldsfld, CurAugment),
//                         new CodeInstruction(OpCodes.Ldc_I4, AugmentID.EyepodHat),
//                         new CodeInstruction(OpCodes.Bne_Un_S, skip),
//                         new CodeInstruction(OpCodes.Ldloc_S, 6),
//                         new CodeInstruction(OpCodes.Ldc_I4_2),
//                         new CodeInstruction(OpCodes.Mul),
//                         new CodeInstruction(OpCodes.Stloc_S, 6),
//                         new CodeInstruction(OpCodes.Nop) { labels = { skip } }
//                     }, insert: true);
//                 }
//                 ilRef = p.FindRefByInsns(new CodeInstruction[]
//                 {
//                     new CodeInstruction(OpCodes.Ldloc_S),
//                     new CodeInstruction(OpCodes.Ldc_I4_1),
//                     new CodeInstruction(OpCodes.Add),
//                     new CodeInstruction(OpCodes.Stloc_S),
//                     new CodeInstruction(OpCodes.Ldloc_S),
//                     new CodeInstruction(OpCodes.Ldc_I4_0),
//                     new CodeInstruction(OpCodes.Ble)
//                 });
//                 if (ilRef == null)
//                 {
//                     string message = "Patch_GameScript_LevelUp2: Transpiler could not find any reference point. (3)";
//                     MoreCombatChips.Error(message);
//                     throw new Exception(message);
//                 }
//                 else
//                 {
//                     ilRef = ilRef.GetRefByOffset(4);
//                     var skip = p.MarkLabel(ilRef);
//                     p.InjectInsns(ilRef, new CodeInstruction[]
//                     {
//                         new CodeInstruction(OpCodes.Ldsfld, CurAugment).MoveLabel(ilRef),
//                         new CodeInstruction(OpCodes.Ldc_I4, AugmentID.EyepodHat),
//                         new CodeInstruction(OpCodes.Bne_Un_S, skip),
//                         new CodeInstruction(OpCodes.Ldloc_S, 8),
//                         new CodeInstruction(OpCodes.Ldc_I4_2),
//                         new CodeInstruction(OpCodes.Mul),
//                         new CodeInstruction(OpCodes.Stloc_S, 8),
//                         new CodeInstruction(OpCodes.Nop) { labels = { skip } }
//                     }, insert: true);
//                 }
//             }
//             return p.Insns;
//         }

//         private static void ExtraAugmentComputation(ref int[] array2)
//         {
//             MoreCombatChips.Log("Patch_GameScript_LevelUp2: It works!");
//             switch (Menuu.curAugment)
//             {
//                 case AugmentID.RebellionHeadpiece:
//                     array2[StatID.DEX] += 2;
//                     break;
//                 case AugmentID.CreatorMask:
//                     if (Util.RandomCheck(25))
//                     {
//                         array2[StatID.MAG] += 1;
//                     }
//                     else
//                     {
//                         array2[StatID.FTH] += 1;
//                     }
//                     break;
//             }
//         }
//     }
// }
