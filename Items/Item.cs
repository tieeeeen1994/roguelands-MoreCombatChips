using GadgetCore.API;
using UnityEngine;
using II = GadgetCore.API.ItemInfo;

namespace TienContentMod.Items
{
    public abstract class Item
    {
        public II ItemInfo => _itemInfo;

        private readonly II _itemInfo;

        protected Item()
        {
            _itemInfo = new II(Type, Name, Description, ObjectTexture, Value, Stats,
                               HeldTex, HeadTex, BodyTex, ArmTex);
        }

        protected abstract string Gadget { get; }

        protected virtual ItemType Type => ItemType.GENERIC;

        protected virtual string Name => "Tien's Item";

        protected virtual string Description => "Tien's Modded Item.";

        protected virtual Texture2D ObjectTexture => GadgetCoreAPI.LoadTexture2D(GetType().Name);

        protected virtual int Value => -1;

        protected virtual EquipStats Stats => default;

        protected virtual Texture HeldTex => null;

        protected virtual Texture HeadTex => null;

        protected virtual Texture BodyTex => null;

        protected virtual Texture ArmTex => null;

        protected virtual void CustomActions(ItemInfo itemInfo) { }

        protected virtual void AddRequiredResources() { }

        public void Register()
        {
            CustomActions(_itemInfo);
            _itemInfo.Register(GetType().Name);
            AddRequiredResources();
            TienContentMod.Log(
                Gadget,
                $"Registered Item: {_itemInfo.Name} with ID {_itemInfo.GetID()}" +
                $" as {_itemInfo.GetRegistryName()}"
            );
        }
    }

    public static class Item<T> where T : Item, new()
    {
        private static readonly T _instance = new T();

        public static T Instance => _instance;

        public static T I => Instance;

        public static int ID => Instance.ItemInfo.GetID();
    }
}
