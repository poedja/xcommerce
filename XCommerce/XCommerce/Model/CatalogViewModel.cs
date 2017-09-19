using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommerce
{
    public class CatalogViewModel : ObservableObject
    {
        private ObservableCollection<Product> _Products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get {
                return _Products;
            }
            set {
                SetProperty(ref _Products, value);
            }
        }

        public CatalogViewModel()
        {
            
        }

        public async Task Load()
        {
            Product item = new Product();
            item.Name = "Buffalo Chicken";
            item.Price = "12";
            item.Photo = "fake_product_01.jpg";

            Product item2 = new Product();
            item2.Name = "Smoky BBQ Chicken";
            item2.Price = "15";
            item2.Photo = "fake_product_02.jpg";


            Product item3 = new Product();
            item3.Name = "Origial Topperstix";
            item3.Price = "7";
            item3.Photo = "fake_product_03.jpg";


            Product item4 = new Product();
            item4.Name = "Mild Bonelss Wings";
            item4.Price = "15";
            item4.Photo = "fake_product_04.jpg";


            Product item5 = new Product();
            item5.Name = "Veggie Classic";
            item5.Price = "10";
            item5.Photo = "fake_product_05.jpg";


            Products.Add(item);
            Products.Add(item2);
            Products.Add(item3);
            Products.Add(item4);
            Products.Add(item5);


        }

    }
}
