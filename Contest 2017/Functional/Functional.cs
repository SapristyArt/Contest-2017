using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Controls;
using System.Windows.Media;
using Contest_2017.Logger;
using Contest_2017;

namespace Contest_2017.Functional
{
    class Func
    {
        Log _logger = new Log();
        DataTable _costsTable;
        DataTable _expensionTable;
        int _rowsCount;
        int _columnsCount;
        string[][] _dataCost;
        string[][] _dataExpension;
        double[] _costsSumm;
        double[] _expensionsSumm;
        double[] _actualysettlementSheet;
        double[] _result;

        public Func() { }

        ~Func() { }

        public void CreateNewTable( int rowsCount, int columnsCount, DataGrid costsDataGrid, DataGrid expensionDataGrid )
        {
            _costsTable = new DataTable();
            _expensionTable = new DataTable();

            _rowsCount = rowsCount;
            _columnsCount = columnsCount;

            FildingTable( _costsTable, costsDataGrid );
            FildingTable( _expensionTable, expensionDataGrid );
        }

        void FildingTable( DataTable contentTable, DataGrid contentDataGrid )
        {
            contentTable.Rows.Clear( );
            contentTable.Clear( );
            contentDataGrid.ItemsSource = null;

            DataRow dataRow;
            DataColumn dataColum;

            dataColum = new DataColumn();
            dataColum.DataType = System.Type.GetType( "System.String" );
            dataColum.ColumnName = "Services Users";
            contentTable.Columns.Add(dataColum );

            for (int column = 1; column <= _columnsCount; ++column )
            {
                dataColum = new DataColumn();
                dataColum.DataType = System.Type.GetType( "System.String" );
                dataColum.ColumnName = "User №" + column;
                contentTable.Columns.Add(dataColum );
            }
;
            for (int row = 1; row <= _rowsCount; ++row )
            {
                dataRow = contentTable.NewRow( );
                dataRow["Services Users"] = "Service №" + row;
                contentTable.Rows.Add(dataRow );
            }
            contentDataGrid.ItemsSource = _costsTable.DefaultView;
        }

        public void Calculate( DataGrid costsDataGrid, DataGrid expensionDataGrid, DataGrid transactionDataGrid, DataGrid loggerDataGrid )
        {
            FillingMatrix ( _dataCost, _costsTable, loggerDataGrid );
            FillingMatrix ( _dataExpension, _expensionTable, loggerDataGrid );

            CostSumm ( );

            for ( int column = 0; column < _columnsCount; column++ )
            {
                if ( _costsSumm[column] > 1 )
                {
                    _logger.Error ( "Сумма коэффициента использования услуг", "Сумма коэффициентов использования услуг пользователей больше 100%, в столбце №" + column, loggerDataGrid );
                    return;
                }
            }

            ExpensionsSumm ( );

            double coefficient = 100 / _columnsCount;
            double[][] actualCoefficients = new double[_rowsCount][];
            for ( int column = 0; column < _columnsCount; column++ )
            {
                for ( int row = 0; row < _rowsCount; row++ )
                {
                    actualCoefficients[row][column] = coefficient - Convert.ToDouble ( _dataCost[row][column] );
                    if ( actualCoefficients[row][column] < 0.0 )
                    {
                        while ( actualCoefficients[row][column] < 0.0 )
                        {
                            actualCoefficients[row][column]++;
                        }
                    }
                }

            }
            double[][] settlementSheet = new double[_rowsCount][];
            for ( int column = 0; column < _columnsCount; column++ )
            {
                for ( int row = 0; row < _rowsCount; row++ )
                {
                    settlementSheet[row][column] = Convert.ToDouble ( _dataExpension[row][column] ) - Convert.ToDouble ( _dataExpension[row][column] ) * actualCoefficients[row][column];
                }
            }

            for ( int column = 0; column < _columnsCount; column++ )
            {
                for ( int row = 0; row < _rowsCount; row++ )
                {
                    _actualysettlementSheet[column] += Convert.ToDouble ( settlementSheet[row][column] );
                }
            }

            for ( int column = 0; column < _columnsCount; column++ )
            {
                _result[column] = _actualysettlementSheet[column] - _expensionsSumm[column];
            }
        }

        
        void CoutingTransaction()
        {
            for ( int column = 0; column < _columnsCount; column++ )
            {
                cmoExeptions ( column );
            }

            while ( _result.Min ( ) < 0 )
            {
                if ( Math.Abs ( _result.Max ( ) ) < Math.Abs ( _result.Min ( ) ) )
                {
                    int i = 0;
                    while ( _result[i] != _result.Max ( ) )
                    {
                        _result[i] -= _result.Max ( );
                    }
                    i = 0;
                    while ( _result[i] != _result.Min ( ) )
                    {
                        _result[i] += _result.Max ( );
                    }
                }
                else
                {
                    int i = 0;
                    while ( _result[i] != _result.Max ( ) )
                    {
                        _result[i] -= _result.Min ( );
                    }
                    i = 0;
                    while ( _result[i] != _result.Min ( ) )
                    {
                        _result[i] += _result.Min ( );
                    }
                }
            }
            Console.WriteLine ( _result );
        }

        void cmoExeptions (int i)
        {

            for ( int column = 0; column < _columnsCount; column++ )
            {
                if ( Math.Abs ( _result[i]) == Math.Abs( _result[column] ) )
                {
                    if ( _result[i] < _result[column] )
                    {
                        _result[column] -= _result[column];
                        _result[i] += _result[column];
                    }
                    else
                    {
                        _result[i] -= _result[i];
                        _result[column] += _result[column];
                    }
                }
            }
        }

        void FillingMatrix( string[][] data, DataTable dataTable, DataGrid loggerDataGrid )
        {
            for ( int column = 0; column < _columnsCount; column++ )
            {
                string[] array = dataTable.Rows.OfType<DataRow> ( ).Select ( k => k[column].ToString ( ) ).ToArray ( );
                for ( int row = 0; row < array.Length; ++row )
                {
                    data[row][column] = array[row];
                }
                
            }

            
        }

        void ExpensionsSumm ( )
        {
            for ( int column = 0; column < _columnsCount; column++ )
            {
                for ( int row = 0; row < _rowsCount; row++ )
                {
                    _expensionsSumm[column] += Convert.ToDouble ( _dataExpension[row][column] );
                }
            }
        }
        void CostSumm( )
        {
            for ( int row = 0; row < _rowsCount; row++ )
            {
                for ( int column = 0; column < _columnsCount; column++ )
                {
                    _costsSumm[row] += Convert.ToDouble ( _dataCost[row][column] );
                } 
            }
        }
    }
}