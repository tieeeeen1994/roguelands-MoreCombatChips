using GadgetCore.API;
using MoreCombatChips.CombatChips;
using MoreCombatChips.DataStructures;
using System.Collections.Generic;

namespace MoreCombatChips
{
    [Gadget("More Combat Chips", true)]
    public class MoreCombatChips : Gadget<MoreCombatChips>
    {
        public const string MOD_VERSION = "1.0"; // Set this to the version of your mod.
        public const string CONFIG_VERSION = "0.0"; // Increment this whenever you change your mod's config file.

        public static List<ModdedChip> ModdedChipsList = new List<ModdedChip>();

        public static void Log(string message)
        {
            GetLogger().Log(message);
        }

        //protected override void LoadConfig()
        //{
        //    Config.Load();

        //    string fileVersion = Config.ReadString("ConfigVersion", CONFIG_VERSION, comments: "The Config Version (not to be confused with mod version)");

        //    if (fileVersion != CONFIG_VERSION)
        //    {
        //        Config.Reset();
        //        Config.WriteString("ConfigVersion", CONFIG_VERSION, comments: "The Config Version (not to be confused with mod version)");
        //    }

        //    // Do stuff with `Config`

        //    Config.Save();
        //}

        public override string GetModDescription()
        {
            return "This mod adds a wide array of custom Combat Chips! Vanilla chips are untouched.";
        }

        protected override void Initialize()
        {
            Logger.Log("MoreCombatChips v" + Info.Mod.Version);
            new AttackerDroneChip().Register();
            new MessyMkIChip().Register();
            new VitalityXXChip().Register();
            new DexterityXXChip().Register();
            new StrengthXXChip().Register();
            new IntelligenceXXChip().Register();
            new TechXXChip().Register();
            new FaithXXChip().Register();
        }

        protected override void Unload()
        {
            ModdedChipsList.Clear();
        }
    }
}
