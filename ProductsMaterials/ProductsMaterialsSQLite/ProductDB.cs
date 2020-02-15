﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsMaterialsSQLite
{
    /// <summary>Класс для представления вида продукции из БД</summary>
    [Table("Products")]
    public class ProductDB
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

        /// <summary>Количество</summary>
        /// <remarks>Задаётся обязательно и должно быть больше нуля</remarks>
        [Column("Quantity")]
        [Required]
        public int Quantity { get; set; }

        /// <summary>Допуск</summary>
        /// <remarks>Задавать не обязательно</remarks>
        [Column("Tolerance")]
        public int? Tolerance { get; set; }

        /// <summary>Время записи</summary>
        /// <remarks>Задаётся базой при изменении строки</remarks>
        [Column("Timestamp")]
        public DateTime? Timestamp { get; set; }

        public override string ToString() => $"Продукт: {ID}, \"{ShortName}\", \"{FullName}\", \"{Description}\", {Quantity}, {Tolerance}, \"{Timestamp}\"";

    }
}
