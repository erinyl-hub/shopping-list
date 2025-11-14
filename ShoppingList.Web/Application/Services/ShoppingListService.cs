using ShoppingList.Application.Interfaces;
using ShoppingList.Domain.Models;

namespace ShoppingList.Application.Services;

public class ShoppingListService : IShoppingListService
{
    private ShoppingItem[] _items = new ShoppingItem[5];
    private int _nextIndex;

    public ShoppingListService()
    {
        // Initialize with demo data for UI demonstration
        // TODO: Students can remove or comment this out when running unit tests
        // _items = GenerateDemoItems();
        _nextIndex = 0; // We have 4 demo items initialized
    }

    public IReadOnlyList<ShoppingItem> GetAll()
    {
        // TODO: Students - Return all items from the array (up to _nextIndex)
        ShoppingItem[] localitems = new ShoppingItem[_nextIndex];
        for (int i = 0; i < _nextIndex; i++)
        {
            localitems[i] = _items[i];
        }

        return localitems;
    }

    public ShoppingItem? GetById(string id)
    {
        // TODO: Students - Find and return the item with the matching id
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].Id == id)
                return _items[i];
        }

        return null;

        //return ((ICollection<ShoppingItem>)_items).Where(x => x.Id == id).FirstOrDefault();
    }

    public ShoppingItem? Add(string name, int quantity, string? notes)
    {
        // TODO: Students - Implement this method

        //instantiate item

        var it = new ShoppingItem()
        {
            Name = name,
            Quantity = quantity,
            Notes = notes
        };        
        //look for empty spot
        
        if(_nextIndex == (_items.Length - 1))
        {
            Array.Resize(ref _items, _items.Length *2);
        }
        
        _items[_nextIndex] = it;
        _nextIndex++;
        return it;
    }

    public ShoppingItem? Update(string id, string name, int quantity, string? notes)
    {
        // TODO: Students - Implement this method
        // Return the updated item, or null if not found
        return null;
    }

    public bool Delete(string id)
    {
        // TODO: Students - Implement this method
        for(int i = 0; i < _nextIndex-1; i++)
            if (_items[i].Id == id)
            {
                _items[i] = null;
                return true;
            }

        for (int i = 0; i < _nextIndex - 1; i++)
        {
            if (_items[i] == null)
            {
                MoveItem(i);
                return true;
                   
            }
            
        }
        
        return false;
    }

    private void MoveItem(int start)
    {
        _nextIndex--;
        for (int i = start; i < _nextIndex - 1; i++)
        {
            _items[i] = _items[i + 1];
        }
    }

    public IReadOnlyList<ShoppingItem> Search(string query)
    {
        // TODO: Students - Implement this method
        // Return the filtered items
        return [];
    }

    public int ClearPurchased()
    {
        // TODO: Students - Implement this method
        // Return the count of removed items
        return 0;
    }

    public bool TogglePurchased(string id)
    {
        // TODO: Students - Implement this method
        // Return true if successful, false if item not found
        return false;
    }

    public bool Reorder(IReadOnlyList<string> orderedIds)
    {
        // TODO: Students - Implement this method
        // Return true if successful, false otherwise
        return false;
    }

    private ShoppingItem[] GenerateDemoItems()
    {
        var items = new ShoppingItem[5];
        items[0] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Dishwasher tablets",
            Quantity = 1,
            Notes = "80st/pack - Rea",
            IsPurchased = false
        };
        items[1] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Ground meat",
            Quantity = 1,
            Notes = "2kg - origin Sweden",
            IsPurchased = false
        };
        items[2] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Apples",
            Quantity = 10,
            Notes = "Pink Lady",
            IsPurchased = false
        };
        items[3] = new ShoppingItem
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Toothpaste",
            Quantity = 1,
            Notes = "Colgate",
            IsPurchased = false
        };
        return items;
    }
}