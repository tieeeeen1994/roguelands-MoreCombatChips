using GadgetCore.API;
using HarmonyLib;
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
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
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
                    var labels = codes[i].labels.ToList();
                    var instruction = new CodeInstruction(OpCodes.Ldloca_S, 4);
                    instruction.labels.AddRange(labels);
                    modifiedCodes.Add(instruction);
                    modifiedCodes.Add(new CodeInstruction(OpCodes.Call, delegateMethod));
                    codes[i].labels.Clear();
                }

                modifiedCodes.Add(codes[i]);
            }
            return modifiedCodes;
        }

        private static void ExtraAugmentComputation(ref int[] array2)
        {
            MoreCombatChips.Log("Patch_GameScript_LevelUp2: It works!");
            if (Menuu.curAugment == 22) // Rebellion Headpiece
            {
                array2[2] += 2;
            }
        }
    }
}
