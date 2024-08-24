using GadgetCore.API;
using static TienContentMod.TienContentMod;

namespace TienContentMod
{
    [Gadget(GADGET_NAME, true)]
    public class Miscellaneous : Gadget<Miscellaneous>
    {
        public const string GADGET_NAME = "Miscellaneous";
        public const string CONFIG_VERSION = "1.0.0"; // Increment this whenever you change your mod's config file.

        internal static bool DebugLog = true;

        public static void Log(string message)
        {
            if (TEST)
            {
                GetLogger().LogConsole(message);
            }
            else if (DebugLog)
            {
                GetLogger().Log(message);
            }
        }

        public static void Error(string message)
        {
            GetLogger().LogError(message);
        }

        protected override void LoadConfig()
        {
            Config.Load();

            string fileVersion = Config.ReadString(
                "ConfigVersion", CONFIG_VERSION,
                comments: "The Config Version (not to be confused with mod version)"
            );

            if (fileVersion != CONFIG_VERSION)
            {
                Config.Reset();
                Config.WriteString(
                    "ConfigVersion", CONFIG_VERSION,
                    comments: "The Config Version (not to be confused with mod version)"
                );
            }

            DebugLog = Config.ReadBool(
                "DebugLog", false,
                comments: "Enable debug logging."
            );

            Config.Save();
        }

        public override string GetModDescription()
        {
            return "WIP";
        }

        protected override void Initialize()
        {
            Logger.Log($"{GADGET_NAME} v{Info.Mod.Version}");
        }
    }
}
