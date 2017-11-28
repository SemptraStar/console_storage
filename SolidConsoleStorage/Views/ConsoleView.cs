using System;
using System.Collections.Generic;

using SolidConsoleStorage.Models;
using SolidConsoleStorage.Controllers;
using SolidConsoleStorage.Exceptions;

namespace SolidConsoleStorage.Views
{
    class ConsoleView
    {
        readonly StorageController _storageController;

        public ConsoleView(StorageController storageController)
        {
            _storageController = storageController;
        }

        public void Run()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Console Storage Main Menu\n");
                Console.WriteLine("1. Display products on storage");
                Console.WriteLine("2. Add new product");
                Console.WriteLine("3. Add new batch");
                Console.Write("\nEnter the command number: ");

                string command = Console.ReadLine().ToLower();

                switch (command)
                {
                    case "-e":
                    case "-exit":
                        return;

                    case "-h":
                    case "-help":
                        Help();
                        break;

                    case "1":
                        DisplayProducts();
                        break;

                    case "2":
                        AddProduct();
                        break;

                    case "3":
                        AddBatch();
                        break;

                    default:
                        continue;
                }
            }
        }
        private void Help()
        {
            Console.Clear();
            Console.WriteLine("Use commands in main menu by typing it's numbers in console.");
            Console.WriteLine("\nCommands:");
            Console.WriteLine("-b | -back \t Back to main menu");
            Console.WriteLine("-e | -exit \t Exit application");

            Console.Write("\nPress any button to continue...");
            Console.ReadKey();
        }
        private void DisplayProducts()
        {
            Console.Clear();

            foreach(var product in _storageController.SelectProductsOnStorage())
            {
                Console.WriteLine($"{product.Id}. {product.Name}. " +
                    $"Price: {product.UnitPrice} for a {product.Unit.UnitName}. " +
                    $"Left on storage: {product.Amount}");
            }

            Console.WriteLine("\nPress any button to continue...");
            Console.ReadKey();
        }
        private void AddProduct()
        {
            try
            {
                Console.Clear();

                string productName = InputProductName();
                int productUnitId = InputProductUnit();
                decimal productUnitPrice = InputProductPrice();

                using (var context = new StorageContext())
                {
                    var product = new Product()
                    {
                        Name = productName,
                        UnitId = productUnitId,
                        UnitPrice = productUnitPrice
                    };

                    _storageController.AddProduct(product);

                    Console.WriteLine($"\nProduct {productName} successfully added to storage.");
                    Console.Write("\nPress any button to continue...");
                    Console.ReadKey();
                }
            }
            catch (BackToMenuException)
            {
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nAn error ocures during adding process.");
                Console.WriteLine(ex.Message);
                Console.Write("\nPress any button to continue...");
                Console.ReadKey();
            }
        }
        private void AddBatch()
        {
            try
            {
                bool batchType = InputBatchType();
                var productsQuantity = new Dictionary<int, double>();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Products:\n");
                    DisplayNotSelectedProducts(productsQuantity);
                    Console.WriteLine("\nSelected products for a batch:\n");
                    DisplaySelectedProducts(productsQuantity);
                    Console.Write("\nEnter new product's id to add to a batch " +
                        "(or -e to end adding new products): ");
                    string productIdStr = Console.ReadLine();

                    if (productIdStr == "-e")
                        break;

                    int productId = Convert.ToInt32(productIdStr);
                    var product = _storageController.FindProduct(productId);
                    Console.Write($"Enter qunatity of {product.Name}: ");
                    double productQuantity = Convert.ToDouble(Console.ReadLine());

                    if (productsQuantity.ContainsKey(productId))
                        productsQuantity[productId] += productQuantity;
                    else
                        productsQuantity.Add(productId, productQuantity);
                }

                if (productsQuantity.Count != 0)
                {
                    _storageController.AddBatch(batchType, productsQuantity);
                    Console.WriteLine("\nSelected products was successfully added to the new batch.");
                    Console.Write("\nPress any button to continue...");
                    Console.ReadKey();
                }               
            }
            catch (BackToMenuException)
            {
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nAn error ocures during adding process.");
                Console.WriteLine(ex.Message);
                Console.Write("\nPress any button to continue...");
                Console.ReadKey();
            }
        }

        private string InputProductName()
        {
            Console.Write("Enter new product's name (or -b to back): ");
            string productName = Console.ReadLine();

            if (productName == "-b")
                throw new BackToMenuException();

            return productName;
        }
        private int InputProductUnit()
        {
            Console.WriteLine();
            DisplayUnits();
            Console.Write("\nEnter new product's unit by typing it's ID (or -b to back): ");

            string productUnitStr = Console.ReadLine();

            if (productUnitStr == "-b")
                throw new BackToMenuException();

            int productUnit = Convert.ToInt32(productUnitStr);

            return productUnit;            
        }
        private void DisplayUnits()
        {
            using (var context = new StorageContext())
            {
                foreach (var unit in context.Units)
                {
                    Console.WriteLine($"{unit.Id}. {unit.UnitName}");
                }
            }
        }
        private decimal InputProductPrice()
        {
            Console.Write("\nEnter new product's price (or -b to back): ");
            string productPriceStr = Console.ReadLine();

            if (productPriceStr == "-b")
                throw new BackToMenuException();

            return Decimal.Parse(productPriceStr);
        }

        private bool InputBatchType()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter new batch type: " +
                    "delivery (1) or dispatch (2) (or -b to back): ");
                string batchType = Console.ReadLine();

                if (batchType == "-b")
                    throw new BackToMenuException();
                else if (batchType != "1" && batchType != "2")
                    continue;

                return batchType == "1" ? true : false;
            }
        }
        private void DisplaySelectedProducts(Dictionary<int, double> productsQuantity)
        {
            foreach (var productQuantity in productsQuantity)
            {
                var product = _storageController.FindProduct(productQuantity.Key);

                Console.WriteLine($"{product.Id}. {product.Name}. " +
                   $"Selected: {productQuantity.Value}");
            }
        }
        private void DisplayNotSelectedProducts(Dictionary<int, double> productsQuantity)
        {
            var products = _storageController.SelectProductsNotWithId(productsQuantity.Keys);

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}. {product.Name}. ");
            }
        }
    }
}
