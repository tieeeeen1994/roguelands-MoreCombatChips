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
    /// This will add effects when using Combat Chips.
    /// </summary>
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("UseSkill")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_GameScript_UseSkill
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var modifiedCodes = new List<CodeInstruction>();
            var matcherOperand = typeof(GameScript).GetField("mana", BindingFlags.Public | BindingFlags.Static);
            var energyOperand = typeof(GameScript).GetField("energy", BindingFlags.Public | BindingFlags.Static);
            var delegateMethod = typeof(Patch_GameScript_UseSkill).GetMethod(
                nameof(OnManaCheck),
                BindingFlags.Static | BindingFlags.NonPublic
            );
            var consumeMethod = typeof(Patch_GameScript_UseSkill).GetMethod(
                nameof(OnManaConsume),
                BindingFlags.Static | BindingFlags.NonPublic
            );
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldsfld && codes[i].operand == matcherOperand &&
                    codes[i + 1].opcode == OpCodes.Ldloc_1 &&
                    codes[i + 2].opcode == OpCodes.Blt)
                {
                    var instruction = new CodeInstruction(OpCodes.Ldloc_1);
                    instruction.labels.AddRange(codes[i].labels);
                    modifiedCodes.Add(instruction);
                    modifiedCodes.Add(new CodeInstruction(OpCodes.Call, delegateMethod));
                    modifiedCodes.Add(new CodeInstruction(OpCodes.Brfalse, codes[i + 2].operand));
                    i += 2;
                    continue;
                }
                if (codes[i].opcode == OpCodes.Ldsfld && codes[i].operand == matcherOperand &&
                    codes[i + 1].opcode == OpCodes.Ldloc_1 &&
                    codes[i + 2].opcode == OpCodes.Sub &&
                    codes[i + 3].opcode == OpCodes.Stsfld && codes[i + 3].operand == matcherOperand)
                {
                    var instruction = new CodeInstruction(OpCodes.Ldloc_1);
                    instruction.labels.AddRange(codes[i].labels);
                    modifiedCodes.Add(instruction);
                    modifiedCodes.Add(new CodeInstruction(OpCodes.Ldarg_0));
                    modifiedCodes.Add(new CodeInstruction(OpCodes.Call, consumeMethod));
                    i += 3;
                    continue;
                }
                modifiedCodes.Add(codes[i]);
            }
            return modifiedCodes;
        }

        private static bool OnManaCheck(int cost)
        {
            switch (Menuu.curAugment)
            {
                case AugmentID.GasMask:
                    return GameScript.mana >= cost || GameScript.energy >= cost;
                default:
                    return GameScript.mana >= cost;
            }
        }

        private static void OnManaConsume(int cost, GameScript instance)
        {
            switch (Menuu.curAugment)
            {
                case AugmentID.GasMask:
                    if (GameScript.mana >= cost)
                    {
                        GameScript.mana -= cost;
                    }
                    else if (GameScript.energy >= cost)
                    {
                        GameScript.energy -= cost;
                        instance.UpdateEnergy();
                    }
                    break;
                default:
                    GameScript.mana -= cost;
                    break;
            }
        }
    }
}
