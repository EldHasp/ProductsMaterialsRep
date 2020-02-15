using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsMaterialsSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                OutputProducts();
                OutputMaterials();
                OutputMaterialsInProducts();

                InputProduct();
                InputMaterial();
                InputMaterialInProduct();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Object: " + validationError.Entry.Entity.ToString());
                    foreach (DbValidationError err in validationError.ValidationErrors)
                        Console.WriteLine(err.ErrorMessage);
                }
            }

            OutputProducts();
            OutputMaterials();
            OutputMaterialsInProducts();
        }

        private static void OutputProducts()
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                try
                {
                    Console.WriteLine(string.Join("\r\n", pmContext.Products.ToList()));
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        Console.WriteLine("Object: " + validationError.Entry.Entity.ToString());
                        foreach (DbValidationError err in validationError.ValidationErrors)
                            Console.WriteLine(err.ErrorMessage);
                    }
                }
        }
        private static void OutputMaterials()
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                Console.WriteLine(string.Join("\r\n", pmContext.Materials));
        }
        private static void OutputMaterialsInProducts()
        {
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                Console.WriteLine(string.Join("\r\n", pmContext.MaterialsInProducts));
        }

        private static void InputProduct()
        {
            Console.WriteLine("Продукт. Через пробел: ID, ShotName, FullName, Description, Quantity. Если Id меньше равен нуля - добавление.");
            string[] input = Console.ReadLine().Split(" /\\;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int id = -1;
            if (int.TryParse(input[0], out int _id))
                id = _id;
            if (id < 1)
                using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                {
                    pmContext.Products.Add(new ProductDB() { ShortName = input[1], FullName = input[2], Description = input[3], Quantity = int.Parse(input[4]), Tolerance = 10 });
                    pmContext.SaveChanges();
                }
            else
                using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                {
                    ProductDB product = pmContext.Products.Find(id);
                    product.ShortName = input[1];
                    product.FullName = input[2];
                    product.Description = input[3];
                    product.Quantity = int.Parse(input[4]);
                    pmContext.Products.Attach(product);
                    pmContext.SaveChanges();
                }
        }

        private static void InputMaterial()
        {
            Console.WriteLine("Материал. Через пробел: ID, ShotName, FullName, Description. Если Id меньше равен нуля - добавление.");
            string[] input = Console.ReadLine().Split(" /\\;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int id = -1;
            if (int.TryParse(input[0], out int _id))
                id = _id;
            if (id < 1)
                using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                {
                    pmContext.Materials.Add(new MaterialDB() { ShortName = input[1], FullName = input[2], Description = input[3] });
                    pmContext.SaveChanges();
                }
            else
                using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
                {
                    MaterialDB material = pmContext.Materials.Find(id);
                    material.ShortName = input[1];
                    material.FullName = input[2];
                    material.Description = input[3];
                    pmContext.Materials.Attach(material);
                    pmContext.SaveChanges();
                }
        }

        private static void InputMaterialInProduct()
        {
            Console.WriteLine("Материал. Через пробел: ProductID, MaterialID, Quantity. Если Id меньше равен нуля - добавление.");
            int[] input = Console.ReadLine().Split(" /\\;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();
            using (ProductsMaterialsContext pmContext = new ProductsMaterialsContext())
            {
                pmContext.MaterialsInProducts.Add(new MaterialsInProductsDB() { ProductID = input[0], MaterialID = input[1], Quantity = input[2] });
                pmContext.SaveChanges();
            }
        }

    }
}
