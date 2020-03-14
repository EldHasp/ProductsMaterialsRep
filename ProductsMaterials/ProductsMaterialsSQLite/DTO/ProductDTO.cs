using System;
using System.Collections.ObjectModel;

namespace ProductsMaterialsSQLite.DTO
{
    /// <summary>Неизменяемый DTO тип для Продукта</summary>
    public class ProductDTO : IEquatable<ProductDTO>
    {
        /// <summary>Идентификатор экземпляра</summary>
        /// <remarks>Может задать только Модель</remarks>
        public int? ID { get; }

        /// <summary>Тип</summary>
        public int Type { get; }

        /// <summary>Количество</summary>
        public int Quantity { get; }

        /// <summary>Допуск</summary>
        public int Tolerance { get; }

        /// <summary>Время записи</summary>
        /// <remarks>Может задать только Модель</remarks>
        public DateTime? Timestamp { get; }

        public override string ToString() => $"Продукт: {ID}, {Type}\", {Quantity}, {Tolerance}, {Timestamp}";

        public bool Equals(ProductDTO other)
            => ID == other.ID;

        public override bool Equals(object obj)
        {
            ProductDTO other = obj as ProductDTO;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
            => ID.GetHashCode();

        /// <summary>Конструктор с заданием всех свойств.
        /// Вызывается только в сборке с Моделью</summary>
        /// <param name="id">Идентификатор экземпляра</param>
        /// <param name="type">Тип</param>
        /// <param name="quantity">Количество</param>
        /// <param name="tolerance">Допуск</param>
        /// <param name="timestamp">Время записи</param>
        internal ProductDTO(
            int? id,
            int type,
            int quantity,
            int tolerance,
            DateTime? timestamp)
        {
            if (id != null && id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Значение должно быть больше нуля");
            if (type < 1)
                throw new ArgumentOutOfRangeException(nameof(type), "Значение должно быть больше нуля");
            if (quantity < 1)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Значение должно быть больше нуля");
            if (tolerance < 0)
                throw new ArgumentOutOfRangeException(nameof(tolerance), "Значение не может быть отрицательным");
            if (timestamp != null && (timestamp < new DateTime(2010, 1, 1) || timestamp > new DateTime(2050, 1, 1)))
                throw new ArgumentOutOfRangeException(nameof(timestamp), "Значение должно быть от 01.01.2010 до 01.01.2050");

            ID = id;
            Type = type;
            Quantity = quantity;
            Tolerance = tolerance;
            Timestamp = timestamp;
        }
        /// <summary>Публичный конструктор с заданием части свойств.</summary>
        /// <param name="type">Тип</param>
        /// <param name="quantity">Количество</param>
        /// <param name="tolerance">Допуск</param>
        public ProductDTO(
            int type,
            int quantity,
            int tolerance)
            : this(null, type, quantity, tolerance, null) { }

        /// <summary>Коллекция экземпляров ProductDTO для примера</summary>
        public static ReadOnlyCollection<ProductDTO> ProductsExample { get; }
            = Array.AsReadOnly(new ProductDTO[]
            {
                new ProductDTO(15, 3, 1000, 150, new DateTime(2020, 1,1)),
                new ProductDTO(17, 3, 2000, 120, new DateTime(2020, 2,1)),
                new ProductDTO(12, 3, 3000, 190, new DateTime(2020, 3,1)),
                new ProductDTO(25, 10, 150, 155, new DateTime(2020, 1,10)),
                new ProductDTO(27, 10, 2500, 125, new DateTime(2020, 2,10)),
                new ProductDTO(22, 10, 3500, 195, new DateTime(2020, 3,10)),
                new ProductDTO(35, 55, 153, 157, new DateTime(2020, 1,20)),
                new ProductDTO(37, 55, 2503, 127, new DateTime(2020, 2,20)),
                new ProductDTO(32, 55, 3503, 197, new DateTime(2020, 3,20))
            });

    }
}
