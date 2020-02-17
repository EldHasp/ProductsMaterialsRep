using System;

namespace ProductsMaterialsSQLite.DTO
{
    /// <summary>Неизменяемый DTO тип для связи Материала с Продуктом</summary>
    public class MaterialInProductDTO
    {
        /// <summary>Идентификатор Продукта</summary>
        public int ProductID { get; }

        /// <summary>Идентификатор Mатериала</summary>
        public int MaterialID { get; }

        /// <summary>Количество</summary>
        public int Quantity { get; }

        public override string ToString() => $"Материал в Продукте: {ProductID}, {Quantity}";

        /// <summary>Конструктор с заданием всех свойств</summary>
        /// <param name="productID">Идентификатор экземпляра</param>
        /// <param name="materialID">Короткое имя</param>
        /// <param name="quantity">Полное имя</param>
        /// <
        public MaterialInProductDTO(
            int productID,
            int materialID,
            int quantity)
        {
            if (productID < 1)
                throw new ArgumentOutOfRangeException(nameof(productID), "Значение должно быть больше нуля");
            if (materialID < 1)
                throw new ArgumentOutOfRangeException(nameof(materialID), "Значение должно быть больше нуля");
            if (quantity < 1)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Значение должно быть больше нуля");

            ProductID = productID;
            MaterialID = materialID;
            Quantity = quantity;
        }

    }

}
