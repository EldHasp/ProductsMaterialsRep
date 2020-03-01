using Common;
using ProductsMaterialsSQLite.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDownloadEmulatorWPF
{
    public class ViewModelBase : OnPropertyChangedClass
    {
        /// <summary>Тип Продукта</summary>
        public uint ProductType
        {
            get
            {
                return _productType;
            }

            set
            {
                SetProperty(ref _productType, value);
            }
        }

        /// <summary>Количество Продукта</summary>
        public uint ProductQuantity
        {
            get
            {
                return _productQuantity;
            }

            set
            {
                SetProperty(ref _productQuantity, value);
            }
        }

        /// <summary>Допуск Продукта</summary>
        public uint ProductTolerance
        {
            get
            {
                return _productTolerance;
            }

            set
            {
                SetProperty(ref _productTolerance, value);
            }
        }

        /// <summary>Все материалы</summary>
        public ObservableCollection<MaterialVM> Materials { get; }
            = new ObservableCollection<MaterialVM>();

        public ObservableCollection<ProductDTO> Products { get; }
            = new ObservableCollection<ProductDTO>();

        /// <summary>Модель</summary>
        protected ModelEmulator Model;

        /// <summary>Интервал добавления в секундах </summary>
        public int Interval
        {
            get
            {
                return (Model?.Interval ?? 10000) / 1000;
            }

            set { Model.Interval = (value < 1 ? 1 : value) * 1000; OnPropertyChanged(); }
        }

        #region Поля для хранения значений свойств
        private uint _productType;
        private uint _productQuantity;
        private uint _productTolerance;
        #endregion

        public ViewModelBase(bool design)
        {
            if (design)
                return;
            Model = new ModelEmulator();
            foreach (MaterialDTO material in Model.Materials)
                Materials.Add(MaterialVM.CreateFrom(material));
        }
    }
}
