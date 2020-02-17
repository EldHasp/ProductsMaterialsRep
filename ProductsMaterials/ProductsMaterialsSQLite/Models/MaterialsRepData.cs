using ProductsMaterialsSQLite.DTO;
using ProductsMaterialsSQLite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsMaterialsSQLite.Models
{
    /// <summary>Класс репозитория данных для работы с Материалами</summary>
    public class MaterialsRepData : IMaterials
    {
        public MaterialDTO GetMaterial(int id)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                return DbToDto(pmContext.Materials.Find(id));
        }

        public IReadOnlyCollection<MaterialDTO> GetMaterials()
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                return Array.AsReadOnly(pmContext.Materials.AsNoTracking().ToList()
                    .Select(prd => DbToDto(prd)).ToArray());
        }

        public IReadOnlyCollection<MaterialDTO> GetMaterials(int productID)
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                var sss = pmContext.MaterialsInProducts
                     .Where(mp => mp.ProductID == productID)
                     .ToList();

                return Array.AsReadOnly(pmContext.MaterialsInProducts
                     .AsNoTracking()
                     .Where(mp => mp.ProductID == productID)
                     .ToList()
                     .Select(mp => DbToDto(pmContext.Materials.Find(mp.MaterialID)))
                     .ToArray());
            }
        }

        /// <summary>Создание DTO типа по DB типу</summary>
        /// <param name="material">Материал в DB типе</param>
        /// <returns>Новый экземпляр MaterialDTO</returns>
        public static MaterialDTO DbToDto(MaterialDB material)
            => material == null
            ? null
            : new MaterialDTO(material.ID, material.Name);

        /// <summary>Создание DB типа по DTO типу</summary>
        /// <param name="material">Материал в DTO типе</param>
        /// <returns>Новый экземпляр MaterialDB</returns>
        public static MaterialDB DtoToDb(MaterialDTO material)
             => material == null
            ? null
            : new MaterialDB()
            {
                ID = material.ID ?? 0,
                Name = material.Name
            };

        /// <summary>Сравнене экземпляров DB и DTO типов</summary>
        /// <param name="db">Продкут в DB типе</param>
        /// <param name="dto">Продкут в DTO типе</param>
        /// <returns><see langword="true"/> если все значения равны</returns>
        public static bool Equals(MaterialDB db, MaterialDTO dto)
            => db.ID == dto.ID && db.Name == dto.Name;

    }
}
