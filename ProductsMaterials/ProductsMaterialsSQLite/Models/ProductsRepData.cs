using ProductsMaterialsSQLite.DTO;
using ProductsMaterialsSQLite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsMaterialsSQLite.Models
{
    /// <summary>Класс репозитория данных для работы с Продуктами</summary>
    public class ProductsRepData : IProducts
    {
        public ProductDTO AddProduct(ProductDTO product)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                ProductDB productDB = DtoToDb(product);
                pmContext.Products.Add(productDB);
                pmContext.SaveChanges();
                return DbToDto(productDB);
            }
        }

        public ProductDTO GetProduct(int id)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                return DbToDto(pmContext.Products.Find(id));
        }

        public IReadOnlyCollection<ProductDTO> GetProducts()
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                return Array.AsReadOnly(pmContext.Products.ToList()
                    .Select(prd => DbToDto(prd)).ToArray());
        }

        public IReadOnlyCollection<ProductDTO> GetProducts(DateTime begin)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                return Array.AsReadOnly(pmContext.Products
                     .AsNoTracking()
                    .Where(prd => prd.Timestamp >= begin)
                     .ToList()
                    .Select(prd => DbToDto(prd))
                    .ToArray());
        }

        public IReadOnlyCollection<ProductDTO> GetProducts(DateTime begin, DateTime end)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                return Array.AsReadOnly(pmContext.Products
                     .AsNoTracking()
                    .Where(prd => prd.Timestamp >= begin && prd.Timestamp <= end)
                     .ToList()
                    .Select(prd => DbToDto(prd)).ToArray());
        }

        /// <summary>Создание DTO типа по DB типу</summary>
        /// <param name="product">Продукт в DB типе</param>
        /// <returns>Новый экземпляр ProductDTO</returns>
        public static ProductDTO DbToDto(ProductDB product)
            => product == null
            ? null
            : new ProductDTO(product.ID, product.Type, product.Quantity, product.Tolerance, product.Timestamp);

        /// <summary>Создание DB типа по DTO типу</summary>
        /// <param name="product">Продукт в DTO типе</param>
        /// <returns>Новый экземпляр ProductDB</returns>
        public static ProductDB DtoToDb(ProductDTO product)
             => product == null
            ? null
            : new ProductDB()
            {
                ID = product.ID ?? 0,
                Type = product.Type,
                Quantity = product.Quantity,
                Tolerance = product.Tolerance,
                Timestamp = product.Timestamp
            };

        /// <summary>Сравнене экземпляров DB и DTO типов</summary>
        /// <param name="db">Продкут в DB типе</param>
        /// <param name="dto">Продкут в DTO типе</param>
        /// <returns><see langword="true"/> если все значения равны</returns>
        public static bool Equals(ProductDB db, ProductDTO dto)
            => db.ID == dto.ID && db.Type == dto.Type && db.Quantity == dto.Quantity
            && db.Tolerance == dto.Tolerance && db.Timestamp == dto.Timestamp;

    }
}
