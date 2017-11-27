using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contest_2017.Logger;
using System.Data;
using System.Windows.Controls;
using System.Windows.Media;

namespace Contest_2017.Functional
{
    class Func
    {
        public Func() { }

        ~Func() { }

        public void CreateNewTable( int rowsCount, int columnsCount, DataGrid costsDataGrid, DataGrid expensionDataGrid )
        {
            DataTable contentTable = new DataTable ( );

            contentTable.Rows.Clear ( );
            contentTable.Clear ( );
            costsDataGrid.ItemsSource = null;
            expensionDataGrid.ItemsSource = null;

            DataRow dataRow;
            DataColumn dataColum;

            dataColum = new DataColumn ( );
            dataColum.DataType = System.Type.GetType ( "System.String" );
            dataColum.ColumnName = "Services Users";
            contentTable.Columns.Add ( dataColum );

            for ( int column = 1; column <= columnsCount; ++column )
            {
                dataColum = new DataColumn ( );
                dataColum.DataType = System.Type.GetType ( "System.String" );
                dataColum.ColumnName = "User №" + column;
                contentTable.Columns.Add ( dataColum );
            }
;
            for ( int row = 1; row <= rowsCount; ++row )
            {
                dataRow = contentTable.NewRow ( );
                dataRow["Services Users"] = "Service №" + row;
                contentTable.Rows.Add ( dataRow );
            }
            costsDataGrid.ItemsSource = contentTable.DefaultView;
            expensionDataGrid.ItemsSource = contentTable.DefaultView;
        }

        public void Calculate( DataGrid costsDataGrid, DataGrid expensionDataGrid, DataGrid transactionDataGrid )
        {
            //ItemCollection a = 
           ////////////// List<string> c = new List<string>();
            for (int i = 0; i < costsDataGrid.Columns.Count; ++i )
            {
                Console.WriteLine(costsDataGrid.Columns.ElementAt ( i ));
                //object b = a.GetItemAt ( i );
                //
                //Console.WriteLine ( b );
                //
                //c.Add ( b );
            }

           
        }
    }
}
