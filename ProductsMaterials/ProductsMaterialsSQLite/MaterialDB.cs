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
        [Column("ShortName")]
        public string ShortName { get; set; }

        /// <summary>Полное имя</summary>
        /// <remarks>Задаётся обязательно и должно быть уникальным для таблицы</remarks>
        [Required]
        [Column("FullName")]
        public string FullName { get; set; }

        /// <summary>Описание</summary>
        /// <remarks>Задавать не обязательно</remarks>
        [Column("Description")]
        public string Description { get; set; }

        /// <summary>Время записи</summary>
        /// <remarks>Задаётся базой при изменении строки</remarks>
        [Column("Timestamp")]
        public DateTime? Timestamp { get; set; }

        public override string ToString() => $"Материал: {ID}, \"{ShortName}\", \"{FullName}\", \"{Description}\", \"{Timestamp}\"";
    }
}
