using GadgetCore.API;
using HarmonyLib;
using MoreCombatChips.ID;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will handle HP constraints for modded effects
    /// </summary>
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("UpdateHP")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_GameScript_UpdateHP
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var modifiedCodes = new List<CodeInstruction>();
            var matcherOperand = typeof(GameScript).GetField("hp", BindingFlags.Public | BindingFlags.Static);
            var delegateMethod = typeof(Patch_GameScript_UpdateHP).GetMethod(
                nameof(ExtraAugmentEffects),
                BindingFlags.Static | BindingFlags.NonPublic
            );
            for (int i = 0; i < codes.Count; i++)
            {
                if (i >= 1 &&
                    codes[i - 1].opcode == OpCodes.Stsfld && codes[i - 1].operand == matcherOperand &&
                    codes[i].opcode == OpCodes.Ldsfld && codes[i].operand == matcherOperand &&
                    codes[i + 1].opcode == OpCodes.Ldc_I4_S &&
                    codes[i + 2].opcode == OpCodes.Ble)
                {
                    MoreCombatChips.Log("Patch_GameScript_UpdateHP: Emit ExtraAugmentEffects");
                    var labels = codes[i].labels.ToList();
                    var instruction = new CodeInstruction(OpCodes.Call, delegateMethod);
                    instruction.labels.AddRange(labels);
                    modifiedCodes.Add(instruction);
                    codes[i].labels.Clear();
                }
                modifiedCodes.Add(codes[i]);
            }
            return modifiedCodes;
        }

        private static void ExtraAugmentEffects()
        {
            MoreCombatChips.Log("Patch_GameScript_UpdateHP: It works!");
            switch (Menuu.curAugment)
            {
                case AugmentID.RebellionHeadpiece:
                    if (GameScript.maxhp > 75)
                    {
                        GameScript.maxhp = 75;
                    }
                    if (GameScript.hp > 75)
                    {
                        GameScript.hp = 75;
                    }
                    break;
            }
        }
    }
}
