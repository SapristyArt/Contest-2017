using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Windows.Controls;
using System.Data;
using System.Windows.Media;

namespace Contest_2017.Logger
{
    class Log
    {
        FileStream _logFile;
        DataTable _logTable = new DataTable();
        bool fileOpenFlag = false;

        public Log( ) { }

        ~Log()
        {
            if ( fileOpenFlag )
            {
                _logFile.Close ( );
                fileOpenFlag = false;
            }
        }

        public void CreateLog( DataGrid dataGrid )
        {
            _logTable.Columns.Add ( "Время" );
            _logTable.Columns.Add ( "Источник" );
            _logTable.Columns.Add ( "Тип" );
            _logTable.Columns.Add ( "Сообщение" );

            logFilling ( "Журнал", "Информация", "Журнал успешно инициализирован.", dataGrid );
        }

        public void CreateLogFile ()
        {
            if ( !fileOpenFlag )
            {
                DateTime localDate = DateTime.Now;

                string path = @"logs\log" + localDate.ToString ( ).Replace ( '.', '_' ).Replace ( ':', '_' ) + ".txt";

                if ( File.Exists ( path ) )
                {
                    File.Delete ( path );
                }

                _logFile = File.Open ( path, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite );
                fileOpenFlag = true;
            }
        }

        public void logFilling ( string sender, string type, string messenge, DataGrid dataGrid )
        {
            DateTime localDate = DateTime.Now;

            _logTable.Rows.Add ( localDate.ToString ( ), sender, type, messenge );

            dataGrid.ItemsSource = _logTable.DefaultView;
        }

        public void Info( string sender, string messenge, DataGrid dataGrid )
        {
            DateTime localDate = DateTime.Now;
            byte[] info = new UTF8Encoding ( true ).GetBytes ( "[" + localDate.ToString ( ) + "];[" + sender + "];[Information];[" + messenge + "[];\n" );
            _logFile.Write ( info, 0, info.Length );
            logFilling ( sender, "Информация", messenge, dataGrid );
        }

        public void Warning( string sender, string messenge, DataGrid dataGrid)
        {
            DateTime localDate = DateTime.Now;
            byte[] info = new UTF8Encoding ( true ).GetBytes ( "[" + localDate.ToString ( ) + "];[" + sender + "];[Warning];[" + messenge + "[];\n" );
            _logFile.Write ( info, 0, info.Length );
            logFilling ( sender, "Предупреждение", messenge, dataGrid );
        }

        public void Error( string sender, string messenge, DataGrid dataGrid )
        {
            DateTime localDate = DateTime.Now;
            byte[] info = new UTF8Encoding ( true ).GetBytes ( "[" + localDate.ToString ( ) + "];[" + sender + "];[Error];[" + messenge + "[];\n" );
            _logFile.Write ( info, 0, info.Length );
            logFilling ( sender, "Ошибка", messenge, dataGrid );
        }
    }
}
