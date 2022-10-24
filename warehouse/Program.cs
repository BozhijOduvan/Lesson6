using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using static System.Reflection.Metadata.BlobBuilder;

namespace warehouse
{

    public class product
    {
        private string _name; //название продуктов
        private decimal _storeName; // количество
        private decimal _price; // цена
        private string _product_type; // тип продукта
        public string ProductType => _product_type;
        public string Name => _name;
        public decimal StoreName => _storeName;
        public decimal Price => _price;
        public product(string product_type, string name, decimal storeName, decimal price)
        {
            _product_type = product_type;
            _name = name;
            _storeName = storeName;
            _price = price;
        }
        public static decimal operator +(product right, product left)
        {
            var price = right.Price + left.Price;
            return price;
        }
    }

    public class Stock
    {
        public decimal c = 0;
        private List<product> _goods;
        public product this[int index] => _goods[index];
        public product this[string name] => _goods.FirstOrDefault(g => g.Name.Equals(name));
        public Stock()
        {
            _goods = new List<product>();
        }
        public void AddGood(product good)
        {
            _goods.Add(good);
        }
        public void SortByPrice()
        {
            _goods = _goods.OrderBy(g => g.Price).ToList();
        }
        public void SortByName()
        {
            _goods = _goods.OrderBy(g => g.Name).ToList();
        }
        public void SortByStoreName()
        {
            _goods = _goods.OrderBy(g => g.StoreName).ToList();
        }
        public void Finds(string ProductType)
        {
            decimal s;
            var Book = _goods.Where(item => item.ProductType == ProductType);
            foreach (var v in Book)
            {
                s = v.Price * v.StoreName;
                Console.WriteLine($"{v.ProductType}, {v.Name}, стоимость товара: {v.Price}руб., количество: {v.StoreName}, общая стоимость: {s}руб.");
            }
        }
    }

    internal class Program
    {
        private static Stock _stock;
        static void Main(string[] args)
        {
            _stock = new Stock();
            var good = new product("еда", "молоко", 100, 75);
            var good2 = new product("еда", "сахар", 1000, 61);
            var good3 = new product("химия", "фери", 50, 110);
            _stock.AddGood(good);
            _stock.AddGood(good2);
            _stock.AddGood(good3);
            Console.Write("Введите название товара для поиска:");
            var findName = Console.ReadLine();
            var foundGood = _stock[findName];
            Console.WriteLine(foundGood == null ? "Такого товара нет" : $"{foundGood.Name} стоимость товара: {foundGood.Price}руб., количество:  {foundGood.StoreName}");
            Console.Write("Введите тип товара для поиска:");
            var product_type2 = Console.ReadLine();
            _stock.Finds(product_type2);
            Console.ReadLine();
        }
    }
}