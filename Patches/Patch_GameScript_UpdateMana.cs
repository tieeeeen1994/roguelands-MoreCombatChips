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
    /// This will handle HP constraints for modded effects.
    /// </summary>
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("UpdateMana")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_GameScript_UpdateMana
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var modifiedCodes = new List<CodeInstruction>();
            var matcherOperand = typeof(Menuu).GetField("curAugment", BindingFlags.Public | BindingFlags.Static);
            var manaOperand = typeof(GameScript).GetField("mana", BindingFlags.Public | BindingFlags.Static);
            var maxManaOperand = typeof(GameScript).GetField("maxmana", BindingFlags.Public | BindingFlags.Static);
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldsfld && codes[i].operand == matcherOperand &&
                    codes[i + 1].opcode == OpCodes.Ldc_I4_7 &&
                    codes[i + 2].opcode == OpCodes.Bne_Un &&
                    codes[i + 3].opcode == OpCodes.Ldc_I4_0 &&
                    codes[i + 4].opcode == OpCodes.Stsfld && codes[i + 4].operand == manaOperand &&
                    codes[i + 5].opcode == OpCodes.Ldc_I4_0 &&
                    codes[i + 6].opcode == OpCodes.Stsfld && codes[i + 6].operand == maxManaOperand)
                {
                    MoreCombatChips.Log("Patch_GameScript_UpdateMana: Remove GLibglob Hat 0 Mana effects!");
                    codes[i + 7].labels.AddRange(codes[i].labels);
                    i += 7;
                }
                modifiedCodes.Add(codes[i]);
            }
            return modifiedCodes;
        }
    }
}
