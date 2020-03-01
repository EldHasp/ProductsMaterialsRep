using System;

namespace ProductsMaterialsSQLite.DTO
{
    /// <summary>Неизменяемый DTO тип для Материала</summary>
    public class MaterialDTO : IEquatable<MaterialDTO>
    {
        /// <summary>Идентификатор экземпляра</summary>
        /// <remarks>Может задать только Модель</remarks>
        public int? ID { get; }

        /// <summary>Короткое имя</summary>
        public string Name { get; }

        public bool Equals(MaterialDTO other)
            => ID == other.ID;

        public override bool Equals(object obj)
        {
            MaterialDTO other = obj as MaterialDTO;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
            => ID.GetHashCode();

        public override string ToString() => $"Материал: {ID}, \"{Name}\"";

        /// <summary>Конструктор с заданием всех свойств.
        /// Вызывается только в сборке с Моделью</summary>
        /// <param name="id">Идентификатор экземпляра</param>
        /// <param name="name">Короткое имя</param>
        internal MaterialDTO(
            int? id,
            string name)
        {
            if (id != null && id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Значение должно быть больше нуля");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Значение не может быть пустым");

            ID = id;
            Name = name;
        }
        /// <summary>Публичный конструктор с заданием части свойств.</summary>
        /// <param name="name">Короткое имя</param>
        public MaterialDTO(string name)
            : this(null, name) { }
    }

}
