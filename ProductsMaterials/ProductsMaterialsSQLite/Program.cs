using ProductsMaterialsSQLite.DTO;
using ProductsMaterialsSQLite.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsMaterialsSQLite
{
    class Program
    {
        static IReadOnlyCollection<ProductDTO> Products;
        static IReadOnlyCollection<MaterialDTO> Materials;
        static IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> MatInProd;
        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine(new string('*', 80));
                Console.WriteLine(new string('*', 80));
                Products = OutputProducts();
                Console.WriteLine(new string('*', 80));
                Materials = OutputMaterials();
                Console.WriteLine(new string('*', 80));
                MatInProd = OutputMaterialsInProducts();
                Console.WriteLine(new string('*', 80));

            inp:
                Console.WriteLine("0 - AddProduct,  2 - AddMaterialInProduct,  5 - AddProductAndMaterials, Empty - Cancel");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    break;
                switch (input[0])
                {
                    case '0': AddProduct(); break;
                    case '2': AddMaterialInProduct(); break;
                    case '5': AddProductAndMaterials(); break;
                    default:
                        Console.WriteLine("Чё непонятного?!");
                        goto inp;
                }

            }
        }

        private static ProductsRepData ProductsrRep = new ProductsRepData();
        private static MaterialsRepData MaterialsRep = new MaterialsRepData();
        private static MaterialsInProductsRepData MaterialsInProductsRep = new MaterialsInProductsRepData();

        private static IReadOnlyCollection<ProductDTO> OutputProducts()
        {
            var products = ProductsrRep.GetProducts();
            Console.WriteLine(string.Join("\r\n", products));
            return products;
        }
        private static IReadOnlyCollection<MaterialDTO> OutputMaterials()
        {
            var materials = MaterialsRep.GetMaterials();
            Console.WriteLine(string.Join("\r\n", materials));
            return materials;
        }

        private static IReadOnlyCollection<IGrouping<ProductDTO, MaterialInProductDTO>> OutputMaterialsInProducts()
        {
            var groups = Array.AsReadOnly(MaterialsInProductsRep.GetMaterialsInProducts().GroupBy(mp => Products.First(pr => pr.ID == mp.ProductID)).ToArray());
            foreach (var group in groups)
            {
                Console.WriteLine(group.Key);
                Console.WriteLine(string.Join("\r\n", group.Select(mp => "\t" + Materials.First(mt => mt.ID == mp.MaterialID) + " - " + mp.Quantity)));
            }

            return groups;
        }

        private static ProductDTO AddProduct()
        {
            Console.WriteLine("Добавление Продукта. Через '\\' или '/': Type, Quantity, Tolerance.");
            int[] input = Console.ReadLine().Split("/\\;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            return ProductsrRep.AddProduct(new ProductDTO(input[0], input[1], input[2]));
        }

        private static MaterialInProductDTO AddMaterialInProduct()
        {
            Console.WriteLine("Добавление Материала в Продукт. Через пробел: ProductID, MaterialID, Quantity.");
            int[] input = Console.ReadLine().Split(" /\\;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();

            return MaterialsInProductsRep.AddMaterialInProduct(new MaterialInProductDTO(input[0], input[1], input[2]));
        }
        private static IGrouping<ProductDTO, MaterialInProductDTO> AddProductAndMaterials()
        {
            Console.WriteLine("Добавление Продукта. Через '\\' или '/': Type, Quantity, Tolerance.");
            int[] input = Console.ReadLine().Split("/\\;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            ProductDTO product = new ProductDTO(input[0], input[1], input[2]);
            Console.WriteLine("Добавление Материалов в Продукт. Через пробел: MaterialID, Quantity. Empty - Exit");
            string inp;
            Dictionary<int, int> materials = new Dictionary<int, int>();
            while(!string.IsNullOrWhiteSpace(inp = Console.ReadLine()))
            {
                int[] nums = inp.Split(" /\\;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();
                materials.Add(nums[0], nums[1]);
            }

            return MaterialsInProductsRep.AddProduct(product, materials);
        }

    }
}
