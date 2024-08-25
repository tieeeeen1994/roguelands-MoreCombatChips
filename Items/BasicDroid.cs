using GadgetCore.API;
using TienContentMod.Gadgets;
using UnityEngine;

namespace TienContentMod.Items
{
    public class BasicDroid : Item
    {
        protected override string Gadget => Miscellaneous.GADGET_NAME;

        protected override string Name => "Basic Droid";

        protected override string Description => "An old second-hand basic droid for basic usage.";

        protected override Texture2D ObjectTexture => GadgetCoreAPI.LoadTexture2D("BasicDroidIcon");

        protected override ItemType Type => ItemType.DROID;
    }
}
