using CRM.Models;
using System.Collections.ObjectModel;

namespace CRM.Library.Services
{
    public class ItemServiceProxy
    {
        private List<Item> inventory = new List<Item>();
        private Dictionary<Item, int> cart = new Dictionary<Item, int>();
        private int nextId = 1;

        public void CreateItem(string name, string description, decimal price, int quantity)
        {
            Item item = new Item { Name = name, Description = description, Price = price, Id = nextId++, Quantity = quantity };
            inventory.Add(item);
            Console.WriteLine("Item added successfully!");
        }

        public void ReadItems()
        {
            foreach (Item item in inventory)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Description: {item.Description}, Price: {item.Price:C}, Quantity: {item.Quantity}");
            }
        }

        public void UpdateItem(int id, string newName, string newDescription, decimal? newPrice, int? newQuantity)
        {
            Item item = inventory?.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                Console.WriteLine("Item not found!");
                return;
            }
            if (!string.IsNullOrWhiteSpace(newName)) item.Name = newName;
            if (!string.IsNullOrWhiteSpace(newDescription)) item.Description = newDescription;
            if (newPrice.HasValue) item.Price = newPrice.Value;
            if (newQuantity.HasValue) item.Quantity = newQuantity.Value;
            Console.WriteLine("Item updated successfully!");
        }

        public void DeleteItem(int id)
        {
            Item itemToRemove = inventory?.FirstOrDefault(i => i.Id == id);
            if (itemToRemove != null)
            {
                inventory.Remove(itemToRemove);
                Console.WriteLine("Item deleted successfully!");
            }
            else
            {
                Console.WriteLine("Item not found!");
            }
        }

        public void AddToCart(int id, int quantityToAdd)
        {
            Item itemToAdd = inventory?.FirstOrDefault(i => i.Id == id);
            if (itemToAdd != null && itemToAdd.Quantity >= quantityToAdd)
            {
                if (cart.ContainsKey(itemToAdd))
                {
                    cart[itemToAdd] += quantityToAdd;
                }
                else
                {
                    cart.Add(itemToAdd, quantityToAdd);
                }
                itemToAdd.Quantity -= quantityToAdd;
                Console.WriteLine("Item added to cart successfully!");
            }
            else
            {
                Console.WriteLine("Not enough stock available.");
            }
        }

        public void RemoveFromCart(int id, int quantityToRemove)
        {
            Item itemToRemove = cart?.Keys?.FirstOrDefault(i => i.Id == id);
            if (itemToRemove != null && cart[itemToRemove] >= quantityToRemove)
            {
                cart[itemToRemove] -= quantityToRemove;
                itemToRemove.Quantity += quantityToRemove;
                if (cart[itemToRemove] == 0)
                {
                    cart.Remove(itemToRemove);
                }
                Console.WriteLine("Item removed from cart successfully!");
            }
            else
            {
                Console.WriteLine("Not enough quantity in the cart.");
            }
        }

        public void Checkout()
        {
            decimal subtotal = 0;
            Console.WriteLine("\nItemized Receipt:");
            foreach (var entry in cart)
            {
                Item item = entry.Key;
                int quantity = entry.Value;
                decimal totalItemPrice = item.Price * quantity;
                Console.WriteLine($"{item.Name} - Quantity: {quantity}, Price per unit: {item.Price:C}, Total: {totalItemPrice:C}");
                subtotal += totalItemPrice;
            }

            decimal tax = subtotal * 0.07m;
            decimal total = subtotal + tax;
            Console.WriteLine($"Subtotal: {subtotal:C}");
            Console.WriteLine($"Tax (7%): {tax:C}");
            Console.WriteLine($"Total: {total:C}");

            cart.Clear(); // Clear the cart after checkout
            Console.WriteLine("Thank you for your purchase!");
        }
    }
}
