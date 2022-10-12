using System;
using System.Collections.Generic;
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

namespace SQLite_t
{
    public partial class MainWindow : Window
    {
        public static DataGrid datagrid;
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            using (AppContext db = new AppContext())
            {
                Data.ItemsSource = db.Employees.ToList();
                datagrid = Data;
            }
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext()) 
            {
                int Id = (Data.SelectedItem as Employee).id;
                var delete = db.Employees.Where(m => m.id == Id).Single();
                db.Employees.Remove(delete);
                db.SaveChanges();
                Data.ItemsSource = db.Employees.ToList();
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext()) {
                Employee employee = new Employee()
                {
                    name = Name_t.Text,
                    surname = Surname_t.Text,
                    post = Post_t.Text,
                    email = Email_t.Text
                };
                db.Employees.Add(employee);
                db.SaveChanges();
                Load();
            }
        }

        private void UpdBtn_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                int Id = (Data.SelectedItem as Employee).id;

                Employee upd = (from m in db.Employees where m.id == Id select m).Single();
                upd.name = Name_t.Text;
                upd.surname = Surname_t.Text;
                upd.post = Post_t.Text;
                upd.email = Email_t.Text;
                db.SaveChanges();
                Data.ItemsSource = db.Employees.ToList();
                Load();
            }
        }
    }
}
