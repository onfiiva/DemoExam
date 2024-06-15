using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using BCrypt.Net;

namespace DE3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        test1Entities testEntities = new test1Entities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query1 = from test in testEntities.Test
                         orderby test.name
                         where test.isDeleted == false
                         select test;

            productsGrid.ItemsSource = query1.ToList();

            /* var query2 = from brand in tabacoEntities.Brand
                          orderby brand.ID_Brand
                          select brand.Name_Brand;

             Brand.ItemsSource = query2.ToList();

             var query3 = from type in tabacoEntities.Type
                          orderby type.ID_Type
                          select type.Name_Type;

             Type.ItemsSource = query3.ToList();*/
        }

        private void RefreshProductsGrid()
        {
            var query1 = from test in testEntities.Test
                         orderby test.name
                         where test.isDeleted == false
                         select test;

            productsGrid.ItemsSource = query1.ToList();

            /*var query2 = from brand in tabacoEntities.Brand
                         orderby brand.ID_Brand
                         select brand.Name_Brand;

            Brand.ItemsSource = query2.ToList();

            var query3 = from type in tabacoEntities.Type
                         orderby type.ID_Type
                         select type.Name_Type;

            Type.ItemsSource = query3.ToList();*/

            name.Text = "Name";
            id.Text = "0";
            //strength.Text = "Strength";
            Type.SelectedItem = null;
            Brand.SelectedItem = null;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            int id = 1;

            if (testEntities.Test.Any())
            {
                id = testEntities.Test.Max(x => x.ID) + 1;
            }

            var newTest = new Test
            {
                ID = id,
                name = name.Text
                /*Cost_Product = costProduct,
                Strength = strengthProduct,
                Is_Deleted = false,
                Type_ID = Type.SelectedIndex + 1,
                Brand_ID = Brand.SelectedIndex + 1*/
            };

            testEntities.Test.Add(newTest);

            var listProducts = testEntities.Test.ToList();

            testEntities.SaveChanges();
            RefreshProductsGrid();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedTest = productsGrid.SelectedItem as Test;
                //int selectedTestId = selectedTest.ID; // Сохраняем ID выбранного элемента
                selectedTest.isDeleted = true;

                testEntities.SaveChanges(); // Сохраняем изменения в базе данных

                RefreshProductsGrid(); // Обновляем данные в grid
            
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
           RefreshProductsGrid();
        }

        public static string HashPassword(string password)
        {
            using(SHA256 sha256 = SHA256.Create()) 
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void productsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Test test = productsGrid.SelectedItem as Test;

            if (test != null)
            {
                name.Text = test.name;
                id.Text = test.ID.ToString();
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            int ID = 0;
            int.TryParse(id.Text, out ID);
            if (ID == 0)
            {
                MessageBox.Show("Неправильно введенное поле ID");
            } else 
            {
                var queryGetId = (from test in testEntities.Test
                                  where test.ID == ID
                                  select test).FirstOrDefault();

                if (queryGetId != null)
                {
                    queryGetId.ID = ID;
                    queryGetId.name = name.Text;
                    queryGetId.isDeleted = false;

                    try
                    {
                        testEntities.SaveChanges(); // Сохранение изменений в базе данных
                        RefreshProductsGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при изменении: " + ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Соответствующий объект не найден");
                }
            } 
        }
    }
}
