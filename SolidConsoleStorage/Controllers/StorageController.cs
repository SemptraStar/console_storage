using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SolidConsoleStorage.Models;
using SolidConsoleStorage.ViewModels;

namespace SolidConsoleStorage.Controllers
{
    class StorageController
    {
        readonly IStorageContext _storageContext = Program.Services.GetService<IStorageContext>();

        public int AddProduct(Product product)
        {
            _storageContext.Products.Add(product);
            return _storageContext.SaveChanges();
        }

        public int AddBatch(Batch batch)
        {
            _storageContext.Batches.Add(batch);
            return _storageContext.SaveChanges();
        }

        public List<Product> SelectProducts()
        {
            return _storageContext.Products.ToList();
        } 

        public List<ProductOnStorage> SelectProductsOnStorage()
        {
            return _storageContext.Products
                .Include(p => p.Unit)
                .Select(p => new ProductOnStorage()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Unit = p.Unit,
                    UnitPrice = p.UnitPrice,

                    Amount = _storageContext.ProductBatches
                        .Include(pb => pb.Batch)
                        .Where(pb => pb.ProductId == p.Id)
                        .Sum(pb => pb.Quantity * (pb.Batch.IsDelivery ? 1 : -1))
                })
                .ToList();
        }

        public Product FindProduct(int productId)
        {
            return _storageContext.Products.FirstOrDefault(p => p.Id == productId);
        }

        public List<Product> SelectProductsNotWithId(IEnumerable<int> ids)
        {
            return _storageContext.Products.Where(p => !ids.Contains(p.Id)).ToList();
        }

        public int AddBatch(bool type, Dictionary<int, double> productsQuantity)
        {
            int batchIdIdentity = _storageContext.Batches.Add(new Batch()
                {
                    IsDelivery = type,
                    Date = DateTime.Now
                })
                .Entity.Id;

            foreach (var productQuantity in productsQuantity)
            {
                _storageContext.ProductBatches.Add(new ProductBatch()
                {
                    ProductId = productQuantity.Key,
                    BatchId = batchIdIdentity,
                    Quantity = productQuantity.Value
                });
            }

            return _storageContext.SaveChanges();
        }
    }
}
