using ProductsMaterialsSQLite.DTO;
using System;
using System.Collections.Generic;

namespace ProductsMaterialsSQLite.Interfaces
{
    /// <summary>Интерфейс с методами получения Продуктов</summary>
    public interface IProducts
    {
        /// <summary>Возвращает все Продукты</summary>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<ProductDTO> GetProducts();

        /// <summary>Возвращает Продукты начиная с указанной даты</summary>
        /// <param name="begin">Начало периода</param>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<ProductDTO> GetProducts(DateTime begin);

        /// <summary>Возвращает Продукты за указанный период</summary>
        /// <param name="begin">Начало периода</param>
        /// <param name="end">Конец периода</param>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<ProductDTO> GetProducts(DateTime begin, DateTime end);

        /// <summary>Возвращает Продукт с указанным id</summary>
        /// <param name="id">Идентификатор Продукта</param>
        /// <returns>Данные Продукта</returns>
        ProductDTO GetProduct(int id);

        /// <summary>Добавляет новый Продукт</summary>
        /// <param name="product">Данные Продукта</param>
        ProductDTO AddProduct(ProductDTO product);
    }
}
