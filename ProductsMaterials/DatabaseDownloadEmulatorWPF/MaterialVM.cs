using Common;
using ProductsMaterialsSQLite.DTO;

namespace DatabaseDownloadEmulatorWPF
{
    /// <summary>Класс для Материала с INPC и дополнительными свойствами</summary>
    public class MaterialVM : OnPropertyChangedClass
    {
        #region Поля для хранения значений свойств
        private bool _isSelected;
        private int _quantity;
        #endregion

        /// <summary>Идентификатор экземпляра</summary>
        public int ID { get; }

        /// <summary>Короткое имя</summary>
        public string Name { get; }

        /// <summary>Материал выбран</summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }


        /// <summary>Количество Mатериала</summary>
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                SetProperty(ref _quantity, value);
            }
        }

        /// <summary>Конструктор с заданием неизменяемых свойств</summary>
        /// <param name="id">Идентификатор экземпляра</param>
        /// <param name="name">Короткое имя</param>
        public MaterialVM(
            int id,
            string name)
        {
            ID = id;
            Name = name;
        }

        /// <summary>Создание экземпляра по DTO образцу</summary>
        /// <param name="material">Экземпляр DTO</param>
        /// <returns>Новый экземпляр MaterialVM</returns>
        public static MaterialVM CreateFrom(MaterialDTO material)
            => new MaterialVM(material.ID ?? 0, material.Name);

    }
}
