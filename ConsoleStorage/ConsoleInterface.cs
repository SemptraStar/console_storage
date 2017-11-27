using System;
using System.Linq;
using ConsoleStorage.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ConsoleStorage
{
    class ConsoleInterface
    {
        public void Help()
        {
            Console.Clear();
            Console.WriteLine("Use commands in main menu by typing it's numbers in console.");
            Console.WriteLine("\nCommands:");
            Console.WriteLine("-b | -back \t Back to main menu");
            Console.WriteLine("-e | -exit \t Exit application");

            Console.Write("\nPress any button to continue...");
            Console.Read();
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Console Storage Main Menu\n");
                Console.WriteLine("1. Display all products");
                Console.WriteLine("2. Add new product");
                Console.WriteLine("3. View batches");
                Console.Write("\nEnter the command number: ");

                string command = Console.ReadLine();

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
                        Console.Clear();
                        DisplayProducts();
                        Console.Write("\nPress any button to continue...");
                        Console.ReadKey();
                        break;

                    case "2":
                        AddProduct();
                        break;

                    case "3":
                        ViewBatches();
                        break;

                    default:
                        continue;
                }
            }      
        }

        public void DisplayProducts()
        {
            using (var context = new StorageContext())
            {
                foreach (var product in context.Products.Include(p => p.Unit))
                {
                    Console.WriteLine($"{product.Id}. {product.Name}" +
                        $" - {product.UnitPrice}$ for a {product.Unit.UnitName}");
                }
            }
        }
        public void AddProduct()
        {
            try
            {
                string productName = InputProductName();
                int productUnitId = InputProductUnit(productName);
                decimal productUnitPrice = InputProductPrice(productName, FindUnit(productUnitId).UnitName);

                using (var context = new StorageContext())
                {
                    var product = new Product()
                    {
                        Name = productName,
                        UnitId = productUnitId,
                        UnitPrice = productUnitPrice
                    };

                    context.Products.Add(product);
                    context.SaveChanges();

                    Console.Clear();
                    DisplayProducts();
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
                Console.Read();
            }
        }
        public void ViewBatches()
        {
            while (true)
            {
                Console.Clear();
                DisplayBatches();
                Console.Write("\nEnter batch number to view detailed information " +
                    "(or -b to back: ");

                string batchNumberStr = Console.ReadLine();

                if (batchNumberStr == "-b" || batchNumberStr == "-back")
                    return;

                try
                {
                    int batchNumber = Convert.ToInt32(batchNumberStr);
                    DisplayBatchDetailed(batchNumber);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nAn error ocures during adding process.");
                    Console.WriteLine(ex.Message);
                    Console.Write("\nPress any button to continue...");
                    Console.ReadKey();
                }
            }
        }     

        private string InputProductName()
        {
            while (true)
            {
                Console.Clear();
                DisplayProducts();
                Console.Write("\nEnter new product's name (or -b to back): ");

                string productName = Console.ReadLine();

                if (productName == "-b")
                    throw new BackToMenuException();

                using (var context = new StorageContext())
                {
                    if (context.Products.FirstOrDefault(p => p.Name == productName) != null)
                    {
                        Console.WriteLine(
                            $"\nError. Product with name {productName} is already in a storage");
                        Console.Write("\nPress any button to continue...");
                        Console.Read();
                        continue;
                    }
                }

                return productName;
            }
        }                  
        private int InputProductUnit(string currentProductName)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"New product: {currentProductName}.\n");
                DisplayUnits();
                Console.Write("\nEnter new product's unit by typing it's ID (or -b to back): ");

                string productUnitStr = Console.ReadLine();

                if (productUnitStr == "-b")
                    throw new BackToMenuException();

                int productUnit = Convert.ToInt32(productUnitStr);

                using (var context = new StorageContext())
                {
                    if (context.Units.FirstOrDefault(u => u.Id == productUnit) == null)
                    {
                        Console.WriteLine(
                            $"\nError. Unit with ID {productUnit} does not exist.");
                        Console.Write("\nPress any button to continue...");
                        Console.Read();
                        continue;
                    }
                }

                return productUnit;
            }
        }
        private decimal InputProductPrice(string currentProductName, string currentProductUnit)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"New product: {currentProductName}. Unit: {currentProductUnit}\n");
                Console.Write("Enter new product's unit price (or -b to back): ");

                string productUnitPriceStr = Console.ReadLine();

                if (productUnitPriceStr == "-b")
                    throw new BackToMenuException();

                decimal productUnitPrice = Convert.ToDecimal(productUnitPriceStr, new CultureInfo("en-US"));

                return productUnitPrice;
            }
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
        private Unit FindUnit(int unitId)
        {
            using (var context = new StorageContext())
            {
                return context.Units.FirstOrDefault(u => u.Id == unitId);
            }
        }

        private void DisplayBatches()
        {
            using (var context = new StorageContext())
            {
                foreach (var batch in context.Batches)
                {
                    string state = batch.IsDelivery ? "Delivery" : "Dispatch";
                    Console.WriteLine($"{batch.Id}. {state}. Date: {batch.Date.ToShortDateString()}");
                }
            }
        }
        private void DisplayBatchDetailed(int batchId)
        {
            using (var context = new StorageContext())
            {
                var batchInfo = context.Batches.FirstOrDefault(b => b.Id == batchId);

                if (batchInfo == null)
                {
                    Console.WriteLine($"Batch with ID {batchId} does not exists.");
                    Console.Write("\nPress any button to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Clear();
                string batchState = batchInfo.IsDelivery ? "Delivery" : "Dispatch";
                Console.WriteLine($"Batch #{batchInfo.Id}. {batchState}. " +
                    $"Date: {batchInfo.Date.ToShortDateString()}\n");

                int i = 1;

                foreach (var productBatch in context.ProductBatches
                    .Where(pb => pb.BatchId == batchId)
                    .Include(pb => pb.Product)
                    .Include(pb => pb.Batch)
                    .Include(pb => pb.Product.Unit))
                {
                    Console.WriteLine($"{i}. {productBatch.Product.Name} - " +
                        $"{productBatch.Quantity} {productBatch.Product.Unit.UnitName}(s)");
                    i++;
                }

                Console.Write("\nPress any button to continue...");
                Console.ReadKey();
            }
        }
    }
}
