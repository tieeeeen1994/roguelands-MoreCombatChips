namespace TienContentMod.Services
{
    public static class ItemService
    {
        public static Item NewItem(int id, int quantity = 1, int tier = 0)
        {
            Item newItem = new Item(id, quantity, 0, tier, 0, new int[3], new int[3]);
            return newItem;
        }
    }
}