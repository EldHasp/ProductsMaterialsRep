using ProductsMaterialsSQLite.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatisticsWpf
{
    /// <summary>Производная VM дополненная работой с Model</summary>
    public class ViewModelStatistic : ViewModelBase
    {
        /// <summary>Модель</summary>
        private readonly ModelStatistic Model;

        /// <summary>Конструктор для работы с Моделью</summary>
        /// <param name="model">Модель</param>
        public ViewModelStatistic(ModelStatistic model)
            : base(false)
        {

            Model = model;
            RangeBegin = DateTime.Now;
            GetProductsMethod(null);
        }

        /// <summary>Конструктор с демонстрационными данными</summary>
        public ViewModelStatistic()
            : base(true)
        { }

        /// <summary>Метод обновляющий список Продуктов по парамерам в свойствах VM</summary>
        /// <param name="parameter">Не используется</param>
        protected override void GetProductsMethod(object parameter)
        {
            Materials.Clear();
            foreach (var material in Model.GetMaterials())
                Materials.Add(material.ID.Value, material.Name);

            Products.Clear();

            IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> products;
            if (IsRangeEnd)
                products = Model.GetProducts(RangeBegin, RangeEnd);
            else
                products = Model.GetProducts(RangeBegin);

            foreach (var product in products)
                Products.Add(product);

            RenderProductsTable();
        }

    }

}
