using GadgetCore.API;
using MoreCombatChips.CombatChips;
using System.Collections.Generic;

namespace MoreCombatChips
{
    [Gadget("More Combat Chips", true)]
    public class MoreCombatChips : Gadget<MoreCombatChips>
    {
        public const string MOD_VERSION = "1.5"; // Set this to the version of your mod.
        public const string CONFIG_VERSION = "1.5.1"; // Increment this whenever you change your mod's config file.

        internal static bool QuadracopterCost = true;

        public static void Log(string message)
        {
            GetLogger().Log(message);
        }

        protected override void LoadConfig()
        {
            Config.Load();

            string fileVersion = Config.ReadString("ConfigVersion", CONFIG_VERSION, comments: "The Config Version (not to be confused with mod version)");

            if (fileVersion != CONFIG_VERSION)
            {
                Config.Reset();
                Config.WriteString("ConfigVersion", CONFIG_VERSION, comments: "The Config Version (not to be confused with mod version)");
            }

            QuadracopterCost = Config.ReadBool(
                "QuadracopterCost", true,
                requiresRestart: true,
                comments: "Changes mana cost to 30."
            );

            Config.Save();
        }

        public override string GetModDescription()
        {
            return "This mod adds a wide array of custom Combat Chips, all available in Old Earth.\n" +
                   "It also applies a few changes and fixes.\n" +
                   "- Gadget RPG now increases TEC instead of DEX and FTH.\n" +
                   "- Rebellion Headpiece augment now has an effect.\n" +
                   "- Quadracopter is functionally fixed for multiplayer.\n" +
                   "- Quadracopter now only costs 30 MP.\n" +
                   "- Add more random names from franchises for character creation.";
        }

        protected override void Initialize()
        {
            Logger.Log("MoreCombatChips v" + Info.Mod.Version);
            CombatChip<AttackerDroneChip>.I.Register();
            CombatChip<MessyMkIChip>.I.Register();
            CombatChip<VitalityXXChip>.I.Register();
            CombatChip<DexterityXXChip>.I.Register();
            CombatChip<StrengthXXChip>.I.Register();
            CombatChip<IntelligenceXXChip>.I.Register();
            CombatChip<TechXXChip>.I.Register();
            CombatChip<FaithXXChip>.I.Register();
            CombatChip<RejuvenationWaveChip>.I.Register();
        }
    }
}
