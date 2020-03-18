using ProductsMaterialsSQLite.DTO;
using ProductsMaterialsSQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatisticsWpf
{
    /// <summary>Модель Статистики</summary>
    public class ModelStatistic
    {
        private static readonly ProductsRepData ProductsrRep = new ProductsRepData();
        private static readonly MaterialsRepData MaterialsRep = new MaterialsRepData();
        private static readonly MaterialsInProductsRepData MaterialsInProductsRep = new MaterialsInProductsRepData();
        private static readonly ComprehensiveRepData ComprehensiveRep = new ComprehensiveRepData();

        /// <summary>Получение Продуктов начиная с указанной данной даты</summary>
        /// <param name="begin">Дата с которой возвращаются продукты</param>
        /// <returns>Коллекция Продуктов с их материалами</returns>
        public IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> GetProducts(DateTime begin)
            => ComprehensiveRep.GetProducts(begin);

        /// <summary>Получение Продуктов начиная с указанный период</summary>
        /// <param name="begin">Начало периода</param>
        /// <param name="end">Конец периода</param>
        /// <returns>Коллекция Продуктов с их материалами</returns>
        public IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> GetProducts(DateTime begin, DateTime end)
            => ComprehensiveRep.GetProducts(begin, end);

        /// <summary>Получение всех материалов</summary>
        /// <returns>Коллекция всех материалов</returns>
        public IReadOnlyCollection<MaterialDTO> GetMaterials()
            => MaterialsRep.GetMaterials();

    }
}
