using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsMaterialsSQLite
{
    /// <summary>Класс для представления вида материала из БД</summary>
    [Table("Materials")]
    public class MaterialDB
    {
        /// <summary>Идентификатор экземпляра</summary>
        /// <remarks>Задаётся базой при создании строки</remarks>
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        /// <summary>Короткое имя</summary>
        /// <remarks>Задаётся обязательно и должно быть уникальным для таблицы</remarks>
        [Required]
        [Column("Name")]
        public string Name { get; set; }

        public override string ToString() => $"Материал: {ID}, \"{Name}\"";
    }
}
