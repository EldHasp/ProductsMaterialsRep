using Common;
using DatabaseDownloadEmulatorWPF;
using ProductsMaterialsSQLite.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using ProductsMaterialsSQLite.Models;

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
                            .Select(m => new MaterialInProductDTO(product.ID.Value, m.ID.Value, rand.Next(1,(product.Quantity + 400) / materials.Count)))
                            .ToList();
                        Products.Add(new Grouping<ProductDTO, MaterialInProductDTO>(product, list));
                    }
                }

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

        protected virtual void GetProductsMethod(object parameter){}
    }

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
        }

        /// <summary>Конструктор с демонстрационными данными</summary>
        public ViewModelStatistic()
            :base(true)
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
        }

    }

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

    /// <summary>Конвертер преобразующий ключ в значение по привязанному словарю</summary>
    public class DictionaryKeyToValueConverter : IMultiValueConverter
    {
        ///// <summary>Словарь для преобразования ключей в значения</summary>
        //public Object Dictionary
        //{
        //    get { return (object)GetValue(DictionaryProperty); }
        //    set { SetValue(DictionaryProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Dictionary.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DictionaryProperty =
        //    DependencyProperty.Register(nameof(Dictionary), typeof(object), typeof(DictionaryKeyToValueConverter),
        //        new PropertyMetadata(null, (d, e) =>((DictionaryKeyToValueConverter) d).DictionaryChange(e)));

        //private void DictionaryChange(DependencyPropertyChangedEventArgs e)
        //{
           
        //}

        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    //if (Dictionary?.Contains(value) == true)
        //    //    return Dictionary[value];

        //    return null;
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values.Length != 2)
                return null;

            IDictionary dictionary = values[1] as IDictionary;

            if(dictionary == null)
                return null;

            if (dictionary?.Contains(values[0]) == true)
                return dictionary[values[0]];

                return null;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
