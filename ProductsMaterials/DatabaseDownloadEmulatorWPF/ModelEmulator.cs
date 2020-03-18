using ProductsMaterialsSQLite.DTO;
using ProductsMaterialsSQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DatabaseDownloadEmulatorWPF
{
    public class ModelEmulator
    {
        /// <summary>Событие запрашивающее данные о Продукте и его материалах</summary>
        public event ProductDataHandler ProductDataEvent;
        /// <summary>Вспомогательный метод для вызова события ProductDataEvent</summary>
        /// <returns>Группу с ключом Продукт и перечнем его материалов/returns>
        protected IGrouping<ProductDTO, MaterialInProductDTO> OnProductData()
            => ProductDataEvent?.Invoke(this);

        /// <summary>Событие возникающее после добавления Продукта</summary>
        public event ProductAddHandler ProductAddEvent;
        /// <summary>Вспомогательный иетод для вызова события ProductDataEvent</summary>
        /// <returns>Группу с ключом Продукт и перечнем его материалов/returns>
        protected void OnProductAdd(ProductDTO product)
            => ProductAddEvent?.Invoke(this, product);

        /// <summary>Таймер для переодического создания ProductDataEvent</summary>
        protected readonly Timer TimerProductData;

        /// <summary>Список всех материалов</summary>
        public IEnumerable<MaterialDTO> Materials;

        private static readonly ProductsRepData ProductsrRep = new ProductsRepData();
        private static readonly MaterialsRepData MaterialsRep = new MaterialsRepData();
        private static readonly MaterialsInProductsRepData MaterialsInProductsRep = new MaterialsInProductsRepData();
        private static readonly ComprehensiveRepData ComprehensiveRep = new ComprehensiveRepData();

        /// <summary>Интервал таймера</summary>
        public int Interval
        {
            get
            {
                return (int)TimerProductData.Interval;
            }

            set
            {
                TimerProductData.Interval = value;
            }
        }

        /// <summary>Запуск таймера</summary>
        public void Start()
        {
            TimerProductData.Start();
            IsWorking = true;
        }

        /// <summary>Остановка таймера</summary>
        public void Stop()
        {
            TimerProductData.Stop();
            IsWorking = false;
        }

        /// <summary>Модель работает</summary>
        public bool IsWorking { get; private set; }

        public ModelEmulator()
        {
            Materials = MaterialsRep.GetMaterials();

            TimerProductData = new Timer();
            TimerProductData.Interval = 1000;
            TimerProductData.Elapsed += TimerProductData_Elapsed;

        }
        /// <summary>Метод таймера</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerProductData_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimerProductData.Stop();
            if (!IsWorking)
                return;

            var group = OnProductData();
            if (group == null || !group.Any())
                return;

            var grp = ComprehensiveRep.AddProduct(group.Key, group.ToDictionary(mp => mp.MaterialID, mp => mp.Quantity));
            OnProductAdd(grp.Key);

            if(IsWorking)
                TimerProductData.Start();
        }
    }

    /// <summary>Делегат метода для события возвращающего Продукт и его материалы</summary>
    /// <param name="sender">Источник события</param>
    /// <returns>Группу с ключом Продукт и перечнем его материалов</returns>
    public delegate IGrouping<ProductDTO, MaterialInProductDTO> ProductDataHandler(object sender);

    /// <summary>Делегат метода для события после добавления Продукта</summary>
    /// <param name="sender">Источник события</param>
    public delegate void ProductAddHandler(object sender, ProductDTO product);
}
