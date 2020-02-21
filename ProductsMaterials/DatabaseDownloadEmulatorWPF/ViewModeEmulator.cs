using Common;
using ProductsMaterialsSQLite.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace DatabaseDownloadEmulatorWPF
{
    public class ViewModeEmulator : ViewModelBase
    {

        public readonly Dispatcher Dispatcher;

        public RelayCommand StartCommand { get; }
        public RelayCommand StopCommand { get; }

        public ViewModeEmulator()
            : this(true, null) { }
        public ViewModeEmulator(bool design , Dispatcher dispatcher)
            :base (design)
        {
            Dispatcher = dispatcher;

            if (design)
            {
                new List<MaterialVM>()
                {
                    new MaterialVM(1, "Первый") { IsSelected = true, Quantity=111},
                    new MaterialVM(2, "Второй") { IsSelected = false, Quantity=222},
                    new MaterialVM(6, "Шестой") { IsSelected = true, Quantity=666},
                    new MaterialVM(9, "Пятый") { IsSelected = false, Quantity=999},
                }
                .ForEach(mt => Materials.Add(mt));

                new List<ProductDTO>()
                {
                    new ProductDTO(23, 456, 76),
                    new ProductDTO (98, 54, 12)
                }
                .ForEach(pr => Products.Add(pr));

                ProductType = 15;
                ProductQuantity = 12345;
                return;
            }
            Model.ProductDataEvent += Model_ProductDataEvent;
            Model.ProductAddEvent += Model_ProductAddEvent;

            StartCommand = new RelayCommand(p => Model.Start(), p => !Model.IsWorking);
            StopCommand = new RelayCommand(p => Model.Stop(), p => Model.IsWorking);

        }

        private void Model_ProductAddEvent(object sender, ProductDTO product)
            => Dispatcher?.BeginInvoke(new Action(()=> Products.Insert(0, product)));

        protected static Random random = new Random();

        private IGrouping<ProductDTO, MaterialInProductDTO> Model_ProductDataEvent(object sender)
        {
            ProductDTO product = new ProductDTO((int)ProductType, (int)ProductQuantity, random.Next(0, 200));
            List<MaterialInProductDTO> materials = new List<MaterialInProductDTO>();
            foreach (MaterialVM material in Materials)
                if (material.IsSelected)
                {
                    MaterialInProductDTO mp = new MaterialInProductDTO(1, material.ID, material.Quantity);
                    materials.Add(mp);
                }

            Grouping<ProductDTO, MaterialInProductDTO> group = new Grouping<ProductDTO, MaterialInProductDTO>(product, materials);
            return group;
        }
    }
}
