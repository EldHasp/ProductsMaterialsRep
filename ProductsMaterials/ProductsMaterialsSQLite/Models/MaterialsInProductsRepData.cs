using ProductsMaterialsSQLite.DTO;
using ProductsMaterialsSQLite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsMaterialsSQLite.Models
{
    /// <summary>Класс репозитория данных для работы с Материалами</summary>
    public class MaterialsInProductsRepData : IMaterialsInProducts
    {
        public MaterialInProductDTO GetMaterialInProduct(int productID, int materialID)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                return DbToDto(pmContext.MaterialsInProducts.Find(productID, materialID));
        }


        public IReadOnlyCollection<MaterialInProductDTO> GetMaterialsInProducts()
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                return Array.AsReadOnly(pmContext.MaterialsInProducts.ToList()
                    .Select(mp => DbToDto(mp)).ToArray());
            }
        }

        public IReadOnlyCollection<MaterialInProductDTO> GetMaterialsInProducts(int productID)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                return Array.AsReadOnly(pmContext.MaterialsInProducts
                    .Where(mp => mp.ProductID == productID).ToList()
                    .Select(mp => DbToDto(mp)).ToArray());
            }
        }

        public MaterialInProductDTO AddMaterialInProduct(MaterialInProductDTO materialInProduct)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                MaterialInProductDB dB = DtoToDb(materialInProduct);
                pmContext.MaterialsInProducts.Add(dB);
                pmContext.SaveChanges();
                return DbToDto(dB);
            }
        }

        public IGrouping<ProductDTO, MaterialInProductDTO> AddProduct(ProductDTO product, Dictionary<int, int> materials)
        {

            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                ProductDB productDB = ProductsRepData.DtoToDb(product);
                pmContext.Products.Add(productDB);
                pmContext.SaveChanges();

                List<MaterialInProductDB> list = new List<MaterialInProductDB>();
                foreach (var mtr in materials)
                {
                    var mp = new MaterialInProductDB() { ProductID = productDB.ID, MaterialID = mtr.Key, Quantity = mtr.Value };
                    list.Add(mp);
                    pmContext.MaterialsInProducts.Add(mp);
                }
                pmContext.SaveChanges();

                ProductDTO productDTO = ProductsRepData.DbToDto(productDB);

                return list.Select(mt => DbToDto(mt)).GroupBy(x => productDTO).First();
            }

        }

        /// <summary>Создание DTO типа по DB типу</summary>
        /// <param name="material">Материал в DB типе</param>
        /// <returns>Новый экземпляр MaterialDTO</returns>
        public static MaterialInProductDTO DbToDto(MaterialInProductDB materialInProduct)
                => new MaterialInProductDTO(
                    materialInProduct.ProductID,
                    materialInProduct.MaterialID,
                    materialInProduct.Quantity);

        /// <summary>Создание DB типа по DTO типу</summary>
        /// <param name="material">Материал в DTO типе</param>
        /// <returns>Новый экземпляр MaterialDB</returns>
        public static MaterialInProductDB DtoToDb(MaterialInProductDTO materialInProduct)
             => new MaterialInProductDB()
             {
                 ProductID = materialInProduct.ProductID,
                 MaterialID = materialInProduct.MaterialID,
                 Quantity = materialInProduct.Quantity
             };

        /// <summary>Сравнение экземпляров DB и DTO типов</summary>
        /// <param name="db">Связь в DB типе</param>
        /// <param name="dto">Связь в DTO типе</param>
        /// <returns><see langword="true"/> если все значения равны</returns>
        public static bool Equals(MaterialInProductDB db, MaterialInProductDTO dto)
            => db.ProductID == dto.ProductID && db.MaterialID == dto.MaterialID && db.Quantity == dto.Quantity;

    }
}
