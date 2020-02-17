using ProductsMaterialsSQLite.DTO;
using System.Collections.Generic;
using System.Linq;

namespace ProductsMaterialsSQLite.Interfaces
{
    /// <summary>Интерфейс с методами получения связей Материалов с Продуктами</summary>
    public interface IMaterialsInProducts
    {
        /// <summary>Возвращает все связи Материалов с Продуктами</summary>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<MaterialInProductDTO> GetMaterialsInProducts();

        /// <summary>Возвращает Материалов с указанного Продуктом</summary>
        /// <param name="productID">Идентификатор Продукта</param>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<MaterialInProductDTO> GetMaterialsInProducts(int productID);

        /// <summary>Возвращает связь с указанными ID Продукта и Mатериала</summary>
        /// <param name="productID">Идентификатор Продукта</param>
        /// <param name="materialID">Идентификатор Материала</param>
        /// <returns>Данные Материала в Продукте</returns>
        MaterialInProductDTO GetMaterialInProduct(int productID, int materialID);

        /// <summary>Добавляет новую связь Материала с Продуктом</summary>
        /// <param name="materialInProduct">Данные связи</param>
        MaterialInProductDTO AddMaterialInProduct(MaterialInProductDTO materialInProduct);

        /// <summary>Добавляет Продукт с коллекцией связей Материалов</summary>
        /// <param name="productDTO">Данные Продукта</param>
        /// <param name="materials">Словарь ID Материалов и их количества</param>
        IGrouping<ProductDTO, MaterialInProductDTO> AddProduct(ProductDTO product, Dictionary<int, int> materials);
    }
}
