using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using TienContentMod.Gadgets;

namespace TienContentMod.Patches.BetterAugmentsPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("UpdateMana")]
    [HarmonyGadget(BetterAugments.GADGET_NAME)]
    public static class Patch_GameScript_UpdateMana
    {
        private static FieldInfo CurAugment
        {
            get => typeof(Menuu).GetField("curAugment", BindingFlags.Public | BindingFlags.Static);
        }

        private static FieldInfo Mana
        {
            get => typeof(GameScript).GetField("mana", BindingFlags.Public | BindingFlags.Static);
        }

        private static FieldInfo MaxMana
        {
            get => typeof(GameScript).GetField("maxmana", BindingFlags.Public | BindingFlags.Static);
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insn, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insn, il);
            // Remove Glibglob Hat vanilla effect.
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldsfld, CurAugment),
                new CodeInstruction(OpCodes.Ldc_I4_7),
                new CodeInstruction(OpCodes.Bne_Un),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Stsfld, Mana),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Stsfld, MaxMana)
            });
            if (ilRef == null)
                BetterAugments.Log("Patch_GameScript_UpdateMana: Transpiler could not find any reference point.");
            else
            {
                p.RemoveInsns(ilRef, 7);
            }
            return p.Insns;
        }
    }
}
