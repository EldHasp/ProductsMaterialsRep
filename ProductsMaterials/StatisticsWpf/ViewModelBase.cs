using Common;
using ProductsMaterialsSQLite.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;

namespace StatisticsWpf
{
    /// <summary>Базовая VM со свойствами и данными времени разработки</summary>
    public class ViewModelBase : OnPropertyChangedClass
    {
        /// <summary>Коллекция групп Продуктов с Материалами</summary>
        public ObservableCollection<IGrouping<ProductDTO, MaterialInProductDTO>> Products { get; }
            = new ObservableCollection<IGrouping<ProductDTO, MaterialInProductDTO>>();

        /// <summary>Коллекция групп Продуктов с Материалами</summary>
        public Dictionary<int, string> Materials { get; }
            = new Dictionary<int, string>();

        private DataTable _productsTable;
        public DataTable ProductsTable
        {
            get { return _productsTable; }
            private set { SetProperty(ref _productsTable, value); }
        }
        /// <summary>Экземпляр с данными Времени Разработки</summary>
        public bool IsDesign { get; }

        public ViewModelBase(bool isDesign)
        {

            /// Данные времени конструирования
            if (IsDesign = isDesign)
            {

                foreach (MaterialDTO material in MaterialDTO.MaterialsExample)
                    Materials.Add(material.ID.Value, material.Name);

                Random rand = new Random();
                foreach (var type in ProductDTO.ProductsExample.GroupBy(pr => pr.Type))
                {
                    List<MaterialDTO> materials = MaterialDTO.MaterialsExample
                        .OrderBy(m => rand.Next())
                        .Take(rand.Next(MaterialDTO.MaterialsExample.Count) + 1)
                        .ToList();

                    foreach (ProductDTO product in type)
                    {
                        List<MaterialInProductDTO> list = materials
                            .Select(m => new MaterialInProductDTO(product.ID.Value, m.ID.Value, rand.Next(1, (product.Quantity + 400) / materials.Count)))
                            .ToList();
                        Products.Add(new Grouping<ProductDTO, MaterialInProductDTO>(product, list));
                    }
                }

                RenderProductsTable();
            }
        }

        private DateTime _rangeBegin = new DateTime(2020, 1, 1);
        private DateTime _rangeEnd = new DateTime(2020, 1, 1);
        /// <summary>Начало периода</summary>
        public DateTime RangeBegin
        {
            get { return _rangeBegin; }
            set { SetProperty(ref _rangeBegin, value); }
        }
        /// <summary>Конец периода</summary>
        public DateTime RangeEnd
        {
            get { return _rangeEnd; }
            set { SetProperty(ref _rangeEnd, value); }
        }
        /// <summary>Есть конец диапазона</summary>
        private bool _isRangeEnd;
        public bool IsRangeEnd
        {
            get { return _isRangeEnd; }
            set { SetProperty(ref _isRangeEnd, value); }
        }

        private RelayCommand _getProducts;

        public RelayCommand GetProducts
        {
            get
            {
                if (_getProducts == null)
                    _getProducts = new RelayCommand(GetProductsMethod, GetProductsCanMethod);
                return _getProducts;
            }
        }

        protected virtual bool GetProductsCanMethod(object parameter)
        {
            if (RangeBegin < new DateTime(2020, 1, 1))
                return false;
            return
                !IsRangeEnd
                || RangeEnd >= RangeBegin;
        }

        protected virtual void GetProductsMethod(object parameter)
        {
            RenderProductsTable();
        }

        protected void RenderProductsTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("дата и время записи", typeof(string));
            table.Columns.Add("Тип изделия", typeof(int));

            foreach (string material in Materials.Values)
                table.Columns.Add(material,typeof(int));

            foreach (IGrouping<ProductDTO, MaterialInProductDTO> product in Products)
            {
                DataRow row = table.NewRow();
                row["дата и время записи"] = product.Key.Timestamp.Value.ToString(CultureInfo.InstalledUICulture);
                row["Тип изделия"] = product.Key.Type;

                foreach (MaterialInProductDTO material in product)
                    row[Materials[material.MaterialID]] = material.Quantity;

                table.Rows.Add(row);
            }

            ProductsTable = table;
        }
    }
}
