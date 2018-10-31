using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ShopInventory
{
    public List<Item> AvailableItems { get; private set; }
    private int amountOfItems;

    public ShopInventory(int amountOfItems)
    {
        this.amountOfItems = amountOfItems;
        GenerateItems(amountOfItems);
    }

    public void ResetShop()
    {
        GenerateItems(amountOfItems);
    }

    private void GenerateItems(int amountOfItems)
    {
        AvailableItems = new List<Item>();
        for (int i = 0; i < amountOfItems; i++)
        {
            AvailableItems.Add(ItemGenerator.CreateNewItem());
        }
    }
}

