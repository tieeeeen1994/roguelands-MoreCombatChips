using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using TienContentMod.ID;

namespace TienContentMod.Patches.BetterAugmentsPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("UpdateHP")]
    [HarmonyGadget(BetterAugments.GADGET_NAME)]
    public static class Patch_GameScript_UpdateHP
    {
        private static FieldInfo HPMatcher
        {
            get => typeof(GameScript).GetField("hp", BindingFlags.Public | BindingFlags.Static);
        }

        private static MethodInfo ExtraAugmentEffectsMethod
        {
            get => typeof(Patch_GameScript_UpdateHP).GetMethod(
                nameof(ExtraAugmentEffects),
                BindingFlags.Static | BindingFlags.NonPublic
            );
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Stsfld, HPMatcher),
                new CodeInstruction(OpCodes.Ldsfld, HPMatcher),
                new CodeInstruction(OpCodes.Ldc_I4_S),
                new CodeInstruction(OpCodes.Ble)
            });
            if (ilRef == null)
            {
                BetterAugments.Log("Patch_GameScript_UpdateHP: Transpiler could not find any reference point.");
            }
            else
            {
                ilRef = ilRef.GetRefByOffset(1);
                p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Call, ExtraAugmentEffectsMethod)
                }, insert: true);
            }
            return p.Insns;
        }

        private static void ExtraAugmentEffects()
        {
            BetterAugments.Log("Patch_GameScript_UpdateHP: It works!");
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
