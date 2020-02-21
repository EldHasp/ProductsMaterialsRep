using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsMaterialsSQLite.DB
{
    /// <summary>Класс для представления вида материала из БД</summary>
    [Table("MaterialsInProducts")]
    public class MaterialInProductDB
    {
        /// <summary>Идентификатор продукта</summary>
        /// <remarks>Задаётся обязательно</remarks>
        [Key]
        [Index]
        [Required]
        [ConcurrencyCheck]
        [ForeignKey("Product")]
        [Column("ProductID", Order = 0)]
        public int ProductID { get; set; }

        /// <summary>Идентификатор материала</summary>
        /// <remarks>Задаётся обязательно</remarks>
        [Key]
        [Index]
        [Required]
        [ForeignKey("Material")]
        [Column("MaterialID", Order = 1)]
        public int MaterialID { get; set; }

        /// <summary>Количество</summary>
        /// <remarks>Задаётся обязательно и должно быть больше нуля</remarks>
        [Column("Quantity")]
        [Required]
        public int Quantity { get; set; }

        /// <summary>Связанный продукт - Свойство навигации</summary>
        [ForeignKey("ProductID")]
        public ProductDB Product { get; set; }

        /// <summary>Связанный материал - Свойство навигации</summary>
        [ForeignKey("MaterialID")]
        public MaterialDB Material { get; set; }

        public override string ToString() => $"Материал в Продукте: {ProductID}, {Quantity}";
    }
}
