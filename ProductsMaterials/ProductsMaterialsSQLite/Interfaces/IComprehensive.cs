using ProductsMaterialsSQLite.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsMaterialsSQLite.Interfaces
{
    /// <summary>Интерфейс с комплекснымми методами</summary>
    public interface IComprehensive
    {

        /// <summary>Добавляет Продукт с коллекцией связей Материалов</summary>
        /// <param name="productDTO">Данные Продукта</param>
        /// <param name="materials">Словарь ID Материалов и их количества</param>
        IGrouping<ProductDTO, MaterialInProductDTO> AddProduct(ProductDTO product, Dictionary<int, int> materials);

        /// <summary>Получает Продукт с коллекцией связей Материалов</summary>
        /// <param name="productID">Идентификатор Продукта</param>
        IGrouping<ProductDTO, MaterialInProductDTO> GetProduct(int productID);


        /// <summary>Возвращает Продукты с коллекцией связей Материалов начиная с указанной даты</summary>
        /// <param name="begin">Начало периода</param>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> GetProducts(DateTime begin);

        /// <summary>Возвращает Продукты с коллекцией связей Материалов за указанный период</summary>
        /// <param name="begin">Начало периода</param>
        /// <param name="end">Конец периода</param>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> GetProducts(DateTime begin, DateTime end);


        /// <summary>Возвращает Продукты заданного типа с коллекцией связей Материалов начиная с указанной даты</summary>
        /// <param name="type">Тип продуката</param>
        /// <param name="begin">Начало периода</param>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> GetProducts(int type, DateTime begin);

        /// <summary>Возвращает Продукты заданного типа с коллекцией связей Материалов за указанный период</summary>
        /// <param name="type">Тип продуката</param>
        /// <param name="begin">Начало периода</param>
        /// <param name="end">Конец периода</param>
        /// <returns>Коллекция только для чтения</returns>
        IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> GetProducts(int type, DateTime begin, DateTime end);

    }
}
