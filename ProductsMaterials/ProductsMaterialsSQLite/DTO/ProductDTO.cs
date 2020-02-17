using System;

namespace ProductsMaterialsSQLite.DTO
{
    /// <summary>Неизменяемый DTO тип для Продукта</summary>
    public class ProductDTO : IEquatable<ProductDTO>
    {
        /// <summary>Идентификатор экземпляра</summary>
        /// <remarks>Может задать только Модель</remarks>
        public int? ID { get; }

        /// <summary>Короткое имя</summary>
        public int Type { get; }

        /// <summary>Количество</summary>
        public int Quantity { get; }

        /// <summary>Допуск</summary>
        public int? Tolerance { get; }

        /// <summary>Время записи</summary>
        /// <remarks>Может задать только Модель</remarks>
        public DateTime? Timestamp { get; }

        public override string ToString() => $"Продукт: {ID}, {Type}\", {Quantity}, {Tolerance}, {Timestamp}";

        public bool Equals(ProductDTO other)
            => ID == other.ID;

        public override bool Equals(object obj)
            => obj is ProductDTO other && Equals(other);

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
            int? tolerance,
            DateTime? timestamp)
        {
            if (id != null && id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Значение должно быть больше нуля");
            if (type < 1)
                throw new ArgumentOutOfRangeException(nameof(type), "Значение должно быть больше нуля");
            if (quantity < 1)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Значение должно быть больше нуля");
            if (tolerance != null && tolerance < 0)
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
            int? tolerance)
            : this(null, type, quantity, tolerance, null) { }
    }
}
