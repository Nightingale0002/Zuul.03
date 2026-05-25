using System.Transactions;

class Inventory
{
    // fields
     private int maxWeight;
  private Dictionary<string, Item> items;
  // constructor
   public Inventory(int maxWeight)
 {
    this.maxWeight = maxWeight;
    this.items = new Dictionary<string, Item>();
}
    // methods
public bool Put(string itemName, Item item)
{
    // TODO implementeer:
    // Check het gewicht van het Item
//if (item.Weight + TotalWeight> maxWeight)
if (TotalWeight() + item.Weight > maxWeight)
    {
        Console.WriteLine("Inventory is too heavy!");
        return false;
    }

    items.Add(itemName, item);
    return true;

    // Is er genoeg ruimte in de Inventory?
    // Past het Item?

    // Zet Item in de Dictionary
    // Return true/false voor succes/mislukt
//return false;
}
    public Item Get(string itemName)
{
    // TODO implementeer:
    // Zoek Item in de Dictionary
     if (!items.ContainsKey(itemName))
        return null;

    // Verwijder Item uit Dictionary (als gevonden)
     Item item = items[itemName];
     items.Remove(itemName);

    // Return Item of null
  return item;
       }

       public int TotalWeight()
{
int total = 0;
// TODO implementeer:
// Loop door alle items
// Tel alle gewichten op
foreach (var item in items.Values)
{
total += item.Weight;
}
return total;
}

//public int FreeWeight()
//{
// TODO implementeer:
// Vergelijk MaxWeight en TotalWeight() }
public int FreeWeight()
{
    return maxWeight - TotalWeight();
}
public void show()
    {
    if (items.Count == 0)
    {
        Console.WriteLine("Your inventory is empty.");
        return;
    }
    
        Console.WriteLine("You are carrying:");
        foreach (var item in items)
        {
            Console.WriteLine("- " + item.Key + " (" + item.Value.Weight + ")");
        }
        Console.WriteLine("Total weight: " + TotalWeight() + "/" + maxWeight);
    }
    public void showroom()
    {
    if (items.Count == 0)
    {
        Console.WriteLine("The room is empty.");
        return;
        }
    
        Console.WriteLine("the room contains:");
        foreach (var item in items)
        {
            Console.WriteLine("- " + item.Key + " " + item.Value.Weight + "kg" );
        }
   
        }
        public bool Has(string itemName)
    {
    return items.ContainsKey(itemName);
        }

    }

