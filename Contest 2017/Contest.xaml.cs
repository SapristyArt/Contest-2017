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
using Contest_2017.Logger;
using Contest_2017.Functional;

namespace Contest_2017
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Log _logger { get; } = new Logger.Log ( );
        Func functional = new Func ( );

        public MainWindow()
        {
            InitializeComponent ( );
            //Log logger = new Logger.Log ( );
            _logger.CreateLogFile ( ); 
            _logger.CreateLog ( loggerDataGrid );
        }


        private void usersCount_DataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
        {

        }

        private void services_DataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
        {

        }

        private void createTableButton_Click( object sender, RoutedEventArgs e )
        {
            if ( usersCount.Text == "") {
                _logger.Error ( "Кнопка создания таблицы", "Введите колличество пользователей", loggerDataGrid );
                return;
            }
            if ( servicesCount.Text == "" )
            {
                _logger.Error ( "Кнопка создания таблицы", "Введите колличество услуг", loggerDataGrid );
                return;
            }
            functional.CreateNewTable ( Convert.ToInt32 ( usersCount.Text ), Convert.ToInt32 ( servicesCount.Text ), costsDataGrid, expensesDataGrid );
        }

        private void calculateButton_Click( object sender, RoutedEventArgs e )
        {
            functional.Calculate ( costsDataGrid, expensesDataGrid, transactionDataGrid, loggerDataGrid );
        }
    }
}
