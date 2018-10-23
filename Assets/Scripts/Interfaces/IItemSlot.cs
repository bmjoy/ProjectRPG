public interface IItemSlot
{
    ItemVisual ItemVisual { get; }
    void Clear();
    void SetItem(Item item);
}