using GadgetCore.API;
using static TienContentMod.TienContentMod;

namespace TienContentMod
{
    public abstract class TienGadget<T> : Gadget<T> where T : TienGadget<T>
    {
        public abstract string ConfigVersion { get; }
        protected abstract string GadgetDescription { get; }

        protected virtual void GadgetConfig()
        { }

        internal static bool DebugLog = false;

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

        protected override sealed void LoadConfig()
        {
            Config.Load();

            string fileVersion = Config.ReadString(
                "ConfigVersion", ConfigVersion,
                comments: "The Config Version (not to be confused with mod version)"
            );

            if (fileVersion != ConfigVersion)
            {
                Config.Reset();
                Config.WriteString(
                    "ConfigVersion", ConfigVersion,
                    comments: "The Config Version (not to be confused with mod version)"
                );
            }

            DebugLog = Config.ReadBool(
                "DebugLog", false,
                comments: "Enable debug logging."
            );

            GadgetConfig();

            Config.Save();
        }

        public override sealed string GetModDescription() => GadgetDescription;
    }
}