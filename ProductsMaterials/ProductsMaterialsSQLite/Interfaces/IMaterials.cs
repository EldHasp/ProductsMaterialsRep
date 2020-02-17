using ProductsMaterialsSQLite.DTO;
using System.Collections.Generic;

namespace ProductsMaterialsSQLite.Interfaces
{
    /// <summary>Интерфейс с методами получения Материалов</summary>
    public interface IMaterials
    {
        /// <summary>Возвращает все Материалы</summary>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<MaterialDTO> GetMaterials();

        /// <summary>Возвращает Материалы с указанного Продукта</summary>
        /// <param name="productID">Идентификатор Продукта</param>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<MaterialDTO> GetMaterials(int productID);

        /// <summary>Возвращает Материал с указанным id</summary>
        /// <param name="id">Идентификатор Материала</param>
        /// <returns>Данные Материала</returns>
        MaterialDTO GetMaterial(int id);
    }
}
