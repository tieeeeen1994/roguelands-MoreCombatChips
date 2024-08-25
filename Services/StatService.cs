using System.Collections.Generic;
using GadgetCore.API;

namespace TienContentMod.Services
{
    public static class StatService
    {
        public static EquipStats EquipStats(int VIT = 0, int STR = 0, int DEX = 0,
                                            int TEC = 0, int MAG = 0, int FTH = 0)
        {
            return new EquipStats(VIT, STR, DEX, TEC, MAG, FTH);
        }

        public static int[] NewStats(int VIT = 0, int STR = 0, int DEX = 0,
                                     int TEC = 0, int MAG = 0, int FTH = 0)
        {
            return new int[] { VIT, STR, DEX, TEC, MAG, FTH };
        }

        public static List<int> ListStats(int VIT = 0, int STR = 0, int DEX = 0,
                                          int TEC = 0, int MAG = 0, int FTH = 0)
        {
            return new List<int> { VIT, STR, DEX, TEC, MAG, FTH };
        }
    }
}
