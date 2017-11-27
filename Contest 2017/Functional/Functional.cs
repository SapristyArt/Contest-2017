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
        DataTable _costsTable;
        DataTable _expensionTable;
        int _rowsCount;
        int _columnsCount;

        public Func() { }

        ~Func() { }

        public void CreateNewTable( int rowsCount, int columnsCount, DataGrid costsDataGrid, DataGrid expensionDataGrid )
        {
            _costsTable = new DataTable ( );
            _expensionTable = new DataTable ( );

            _rowsCount = rowsCount;
            _columnsCount = columnsCount;

            FildingTable ( _costsTable, costsDataGrid );
            FildingTable ( _expensionTable, expensionDataGrid );
        }

        void FildingTable( DataTable contentTable, DataGrid contentDataGrid)
        {
            contentTable.Rows.Clear ( );
            contentTable.Clear ( );
            contentDataGrid.ItemsSource = null;

            DataRow dataRow;
            DataColumn dataColum;

            dataColum = new DataColumn ( );
            dataColum.DataType = System.Type.GetType ( "System.String" );
            dataColum.ColumnName = "Services Users";
            contentTable.Columns.Add ( dataColum );

            for ( int column = 1; column <= _columnsCount; ++column )
            {
                dataColum = new DataColumn ( );
                dataColum.DataType = System.Type.GetType ( "System.String" );
                dataColum.ColumnName = "User №" + column;
                contentTable.Columns.Add ( dataColum );
            }
;
            for ( int row = 1; row <= _rowsCount; ++row )
            {
                dataRow = contentTable.NewRow ( );
                dataRow["Services Users"] = "Service №" + row;
                contentTable.Rows.Add ( dataRow );
            }
            contentDataGrid.ItemsSource = _costsTable.DefaultView;
        }

        public void Calculate( DataGrid costsDataGrid, DataGrid expensionDataGrid, DataGrid transactionDataGrid )
        {

            for ( int column = 0; column < _columnsCount; column++ )
            {
                string[] array = _costsTable.Rows.OfType<DataRow> ( ).Select ( k => k[column].ToString ( ) ).ToArray ( );

                for ( int i = 0; i < array.Length; ++i )
                {
                    Console.WriteLine ( array[i] );
                } 
            }
            //for ( int column = 1; column <= _columnsCount; ++column )
            //{
            //    for ( int row = 1; row <= _rowsCount; ++row )
            //    {
            //        _costsTable.Rows()[row, column];
            //    }
            //           
            //}
            //ItemCollection a = 
            ////////////// List<string> c = new List<string>();
            //for (int i = 0; i < costsDataGrid.Columns.Count; ++i )
            // {
            //     Console.WriteLine(costsDataGrid.Columns.ElementAt ( i ));
            //object b = a.GetItemAt ( i );
            //
            //Console.WriteLine ( b );
            //
            //c.Add ( b );
            // }


        }
    }
}
