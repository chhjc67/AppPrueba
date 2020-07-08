using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApplication1.Controls
{
    public partial class CustomComboBox1 : UserControl
    {
        public ObservableCollection<Employee> MyColeccion = new ObservableCollection<Employee>();

        public CustomComboBox1()
        {
            InitializeComponent();

            MyColeccion.Add(new Employee("Chris", "Sells", "New York", new DateTime(2008, 2, 5), null));
            MyColeccion.Add(new Employee("Luka", " Abrus", "Berlin", new DateTime(2007, 4, 3), null));
            MyColeccion.Add(new Employee("Jim", "Hance", "Madrid", new DateTime(2007, 2, 6), null));
            MyColeccion.Add(new Employee("Alicia", " Keys", "Madrid", new DateTime(2009, 12, 26), null));
            MyColeccion.Add(new Employee("Madonna", "Frozen", "Bogotá", new DateTime(2009, 12, 26), null));
            MyColeccion.Add(new Employee("Miguel", "Bose", "Medellin", new DateTime(2000, 11, 26), null));
            MyColeccion.Add(new Employee("Annie", "Lennox", "Rio de Janeiro", new DateTime(2001, 10, 26), null));
            MyColeccion.Add(new Employee("Julissa Andrea", "Javela Valencia", "Buenos Aires", new DateTime(2009, 12, 26), null));
            MyColeccion.Add(new Employee("Elton", " John", "Barcelona", new DateTime(2002, 08, 26), null));
            MyColeccion.Add(new Employee("Enya", "Angel", "Ciuidad de Mexico", new DateTime(1989, 09, 26), null));
            customComboBox1.DataContext = MyColeccion;
        }
    }
}