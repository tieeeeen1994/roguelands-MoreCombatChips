using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.ID;

namespace TienContentMod.Patches.BetterAugmentsPatches
{
    [HarmonyPatch]
    [HarmonyGadget(BetterAugments.GADGET_NAME)]
    public static class Patch_Patch_GameScript_UseSkill_Prefix
    {
        private static FieldInfo ChipInfoType
        {
            get => typeof(ChipInfo).GetField("Type", BindingFlags.Public | BindingFlags.Instance);
        }

        private static FieldInfo CostType
        {
            get => typeof(ChipInfo).GetField("CostType", BindingFlags.Public | BindingFlags.Instance);
        }

        private static FieldInfo Mana
        {
            get => typeof(GameScript).GetField("mana", BindingFlags.Public | BindingFlags.Static);
        }

        private static FieldInfo Energy
        {
            get => typeof(GameScript).GetField("energy", BindingFlags.Public | BindingFlags.Static);
        }

        private static FieldInfo Health
        {
            get => typeof(GameScript).GetField("hp", BindingFlags.Public | BindingFlags.Static);
        }

        private static MethodInfo OnCostCheckMethod
        {
            get => typeof(Patch_Patch_GameScript_UseSkill_Prefix).GetMethod(
                nameof(OnCostCheck),
                BindingFlags.Static | BindingFlags.NonPublic
            );
        }

        private static MethodInfo OnManaConsumeMethod
        {
            get => typeof(Patch_Patch_GameScript_UseSkill_Prefix).GetMethod(
                nameof(OnManaConsume),
                BindingFlags.Static | BindingFlags.NonPublic
            );
        }

        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(GadgetCore.CoreMod.GadgetCoreMod));
            Type type = assembly.GetType("GadgetCore.Patches.Patch_GameScript_UseSkill");
            return type.GetMethod("Prefix", BindingFlags.Static | BindingFlags.Public);
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            EmitCheck(p);
            EmitConsume(p);
            return p.Insns;
        }

        private static void EmitCheck(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, ChipInfoType),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.And),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Ble),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, CostType),
                new CodeInstruction(OpCodes.Brtrue_S),
                new CodeInstruction(OpCodes.Ldsfld, Mana),
                new CodeInstruction(OpCodes.Ldloc_2),
                new CodeInstruction(OpCodes.Bge_S),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, CostType),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Bne_Un_S),
                new CodeInstruction(OpCodes.Ldsfld, Energy),
                new CodeInstruction(OpCodes.Ldloc_2),
                new CodeInstruction(OpCodes.Bge_S),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, CostType),
                new CodeInstruction(OpCodes.Ldc_I4_2),
                new CodeInstruction(OpCodes.Bne_Un_S),
                new CodeInstruction(OpCodes.Ldsfld, Health),
                new CodeInstruction(OpCodes.Ldloc_2),
                new CodeInstruction(OpCodes.Bgt_S),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, CostType),
                new CodeInstruction(OpCodes.Ldc_I4_3),
                new CodeInstruction(OpCodes.Beq_S),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, CostType),
                new CodeInstruction(OpCodes.Ldc_I4_4),
                new CodeInstruction(OpCodes.Bne_Un)
            });
            if (ilRef == null)
            {
                BetterAugments.Log("Patch_Patch_GameScript_UseSkill_Prefix: Reference point not found. (1)");
            }
            else
            {
                ilRef = p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldloc_1),
                    new CodeInstruction(OpCodes.Ldloc_2),
                    new CodeInstruction(OpCodes.Call, OnCostCheckMethod),
                    new CodeInstruction(OpCodes.Brfalse, ilRef.GetRefByOffset(33).GetInsn().operand)
                }, insert: true);
                p.RemoveInsns(ilRef + 4, 34);
            }
        }

        private static void EmitConsume(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldsfld, Mana),
                new CodeInstruction(OpCodes.Ldloc_2),
                new CodeInstruction(OpCodes.Sub),
                new CodeInstruction(OpCodes.Stsfld, Mana)
            });
            if (ilRef == null)
            {
                BetterAugments.Log("Patch_Patch_GameScript_UseSkill_Prefix: Reference point not found. (2)");
            }
            else
            {
                ilRef = p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldloc_2),
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Call, OnManaConsumeMethod)
                }, insert: false);
                p.RemoveInsn(ilRef + 3);
            }
        }

        private static bool OnCostCheck(ChipInfo chip, int cost)
        {
            switch (Menuu.curAugment)
            {
                case AugmentID.GasMask:
                    bool newManaCondition = chip.CostType == ChipInfo.ChipCostType.MANA &&
                                            (GameScript.mana >= cost || GameScript.energy >= cost);
                    return newManaCondition ||
                           (chip.CostType == ChipInfo.ChipCostType.ENERGY && GameScript.energy >= cost) ||
                           (chip.CostType == ChipInfo.ChipCostType.HEALTH_SAFE && GameScript.hp > cost) ||
                           chip.CostType == ChipInfo.ChipCostType.HEALTH_LETHAL ||
                           chip.CostType == ChipInfo.ChipCostType.HEALTH_LETHAL_POSTMORTEM;

                default:
                    return (chip.CostType == ChipInfo.ChipCostType.MANA && GameScript.mana >= cost) ||
                           (chip.CostType == ChipInfo.ChipCostType.ENERGY && GameScript.energy >= cost) ||
                           (chip.CostType == ChipInfo.ChipCostType.HEALTH_SAFE && GameScript.hp > cost) ||
                           chip.CostType == ChipInfo.ChipCostType.HEALTH_LETHAL ||
                           chip.CostType == ChipInfo.ChipCostType.HEALTH_LETHAL_POSTMORTEM;
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
