using Common;
using ProductsMaterialsSQLite.DB;
using ProductsMaterialsSQLite.DTO;
using ProductsMaterialsSQLite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsMaterialsSQLite.Models
{
    /// <summary>Класс репозитория для Комплексных данных</summary>
    public class ComprehensiveRepData : IComprehensive
    {
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

                return new Grouping<ProductDTO, MaterialInProductDTO>
                    (
                        ProductsRepData.DbToDto(productDB),
                        list.Select(mt => MaterialsInProductsRepData.DbToDto(mt))
                    );
            }

        }

        public IGrouping<ProductDTO, MaterialInProductDTO> GetProduct(int productID)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                return new Grouping<ProductDTO, MaterialInProductDTO>
                      (
                          ProductsRepData.DbToDto(pmContext.Products.Find(productID)),
                          pmContext.MaterialsInProducts
                              .Where(mp => mp.ProductID == productID)
                              .ToList()
                              .Select(mt => MaterialsInProductsRepData.DbToDto(mt))
                      );
            }
        }

        public IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> GetProducts(DateTime begin)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                return Array.AsReadOnly(pmContext.Products.Where(pr => pr.Timestamp >= begin).ToList()
                        .Select(pr => new Grouping<ProductDTO, MaterialInProductDTO>
                          (
                              ProductsRepData.DbToDto(pr),
                              pmContext.MaterialsInProducts
                                  .Where(mp => mp.ProductID == pr.ID)
                                  .ToList()
                                  .Select(mt => MaterialsInProductsRepData.DbToDto(mt))
                          ))
                        .ToArray()
                    );
            }
        }

        public IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> GetProducts(DateTime begin, DateTime end)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                return Array.AsReadOnly(pmContext.Products.Where(pr => pr.Timestamp >= begin && pr.Timestamp <= end).ToList()
                        .Select(pr => new Grouping<ProductDTO, MaterialInProductDTO>
                          (
                              ProductsRepData.DbToDto(pr),
                              pmContext.MaterialsInProducts
                                  .Where(mp => mp.ProductID == pr.ID)
                                  .ToList()
                                  .Select(mt => MaterialsInProductsRepData.DbToDto(mt))
                          ))
                        .ToArray()
                    );
            }
        }


        public IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> GetProducts(int type, DateTime begin)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                return Array.AsReadOnly(pmContext.Products.Where(pr => pr.Timestamp >= begin)
                    .Where(pr => pr.Type == type).ToList()
                        .Select(pr => new Grouping<ProductDTO, MaterialInProductDTO>
                          (
                              ProductsRepData.DbToDto(pr),
                              pmContext.MaterialsInProducts
                                  .Where(mp => mp.ProductID == pr.ID)
                                  .ToList()
                                  .Select(mt => MaterialsInProductsRepData.DbToDto(mt))
                          ))
                        .ToArray()
                    );
            }
        }

        public IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> GetProducts(int type, DateTime begin, DateTime end)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                return Array.AsReadOnly(pmContext.Products.Where(pr => pr.Timestamp >= begin && pr.Timestamp <= end)
                    .Where(pr => pr.Type == type).ToList()
                        .Select(pr => new Grouping<ProductDTO, MaterialInProductDTO>
                          (
                              ProductsRepData.DbToDto(pr),
                              pmContext.MaterialsInProducts
                                  .Where(mp => mp.ProductID == pr.ID)
                                  .ToList()
                                  .Select(mt => MaterialsInProductsRepData.DbToDto(mt))
                          ))
                        .ToArray()
                    );
            }
        }
    }
}
