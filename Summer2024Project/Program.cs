using CRM.Library.Services;
using CRM.Models;
using System.Net.Mail;
using System.Xml.Linq;

namespace Summer2024Project
{
    internal class Program
    {
        static ItemServiceProxy itemSvc = new ItemServiceProxy();
        static void Main()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("1. Inventory Management\n2. Shop\n3. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageInventory();
                        break;
                    case "2":
                        ManageShopping();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        private static void ManageInventory()
        {
            bool inInventoryManagement = true;
            while (inInventoryManagement)
            {
                Console.WriteLine("\nInventory Management:\n1. Create Item\n2. Read Items\n3. Update Item\n4. Delete Item\n5. Return to Main Menu");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateItem();
                        break;
                    case "2":
                        itemSvc.ReadItems();
                        break;
                    case "3":
                        UpdateItem();
                        break;
                    case "4":
                        DeleteItem();
                        break;
                    case "5":
                        inInventoryManagement = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        private static void CreateItem()
        {
            Console.Write("Enter item name: ");
            string name = Console.ReadLine();
            Console.Write("Enter item description: ");
            string description = Console.ReadLine();
            Console.Write("Enter item price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price. Please enter a valid decimal.");
                return;
            }
            Console.Write("Enter item quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity. Please enter a valid integer.");
                return;
            }

            itemSvc.CreateItem(name, description, price, quantity);
        }

        private static void UpdateItem()
        {
            Console.Write("Enter the ID of the item you want to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer.");
                return;
            }
            Console.Write("Enter new name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            Console.Write("Enter new description (leave blank to keep current): ");
            string newDescription = Console.ReadLine();
            Console.Write("Enter new price (leave blank to keep current): ");
            string newPriceStr = Console.ReadLine();
            decimal? newPrice = string.IsNullOrEmpty(newPriceStr) ? null : decimal.TryParse(newPriceStr, out decimal parsedPrice) ? parsedPrice : (decimal?)null;
            Console.Write("Enter new quantity (leave blank to keep current): ");
            string newQuantityStr = Console.ReadLine();
            int? newQuantity = string.IsNullOrEmpty(newQuantityStr) ? null : int.TryParse(newQuantityStr, out int parsedQuantity) ? parsedQuantity : (int?)null;

            itemSvc.UpdateItem(id, newName, newDescription, newPrice, newQuantity);
        }

        private static void DeleteItem()
        {
            Console.Write("Enter the ID of the item you want to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer.");
                return;
            }
            itemSvc.DeleteItem(id);
        }

        private static void ManageShopping()
        {
            bool inShopping = true;
            while (inShopping)
            {
                Console.WriteLine("\nShop:\n1. Add Item to Cart\n2. Remove Item from Cart\n3. Checkout\n4. Return to Main Menu");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddToCart();
                        break;
                    case "2":
                        RemoveFromCart();
                        break;
                    case "3":
                        itemSvc.Checkout();
                        break;
                    case "4":
                        inShopping = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        private static void AddToCart()
        {
            Console.Write("Enter the ID of the item to add to cart: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer.");
                return;
            }
            Console.Write("Enter the quantity to add: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity. Please enter a valid integer.");
                return;
            }

            itemSvc.AddToCart(id, quantity);
        }

        private static void RemoveFromCart()
        {
            Console.Write("Enter the ID of the item to remove from cart: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid integer.");
                return;
            }
            Console.Write("Enter the quantity to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Invalid quantity. Please enter a valid integer.");
                return;
            }

            itemSvc.RemoveFromCart(id, quantity);
        }
    }
}
