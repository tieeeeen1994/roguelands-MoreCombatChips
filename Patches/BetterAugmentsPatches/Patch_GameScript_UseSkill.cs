using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using TienContentMod.Gadgets;
using TienContentMod.ID;

namespace TienContentMod.Patches.BetterAugmentsPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("UseSkill")]
    [HarmonyGadget(BetterAugments.GADGET_NAME)]
    public static class Patch_GameScript_UseSkill
    {
        private static FieldInfo Mana
        {
            get => typeof(GameScript).GetField("mana", BindingFlags.Public | BindingFlags.Static);
        }

        private static FieldInfo Energy
        {
            get => typeof(GameScript).GetField("energy", BindingFlags.Public | BindingFlags.Static);
        }

        private static MethodInfo OnManaCheckMethod
        {
            get => typeof(Patch_GameScript_UseSkill).GetMethod(
                nameof(OnManaCheck),
                BindingFlags.Static | BindingFlags.NonPublic
            );
        }

        private static MethodInfo OnManaConsumeMethod
        {
            get => typeof(Patch_GameScript_UseSkill).GetMethod(
                nameof(OnManaConsume),
                BindingFlags.Static | BindingFlags.NonPublic
            );
        }

        [HarmonyTranspiler]
        [HarmonyOverridden("GadgetCore.core")]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            EmitManaCheck(p);
            EmitManaConsume(p);
            return p.Insns;
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

        private static void EmitManaCheck(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldsfld, Mana),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Blt)
            });
            if (ilRef == null)
            {
                BetterAugments.Log("Patch_GameScript_UseSkill: Reference not found. (EmitManaCheck)");
            }
            else
            {
                p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldloc_1),
                    new CodeInstruction(OpCodes.Call, OnManaCheckMethod),
                    new CodeInstruction(OpCodes.Brfalse, ilRef.GetRefByOffset(2).GetInsn().operand)
                }, insert: false);
            }
        }

        private static void EmitManaConsume(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldsfld, Mana),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Sub),
                new CodeInstruction(OpCodes.Stsfld, Mana)
            });
            if (ilRef == null)
            {
                BetterAugments.Log("Patch_GameScript_UseSkill: Reference not found. (EmitManaConsume)");
            }
            else
            {
                p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldloc_1),
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Call, OnManaConsumeMethod)
                }, insert: false);
                p.RemoveInsn(ilRef + 3);
            }
        }
    }
}
