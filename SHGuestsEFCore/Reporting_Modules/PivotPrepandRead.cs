using System.Data;
using System.Data.SqlClient;

namespace SHGuestsEFCore.Reporting_Modules
{
    public class PivotPrepandRead
    {
        public SqlConnection connect;
        public SqlCommand cmnd;
        public SqlDataAdapter dataAdapter;

        public DataTableReader PivotPrep ( ref string sqlText, out DataTable dataTable )
        {
            dataTable = new DataTable ( "SqlPivot Report" );
            DataTableReader dataTableReader;
            connect = new SqlConnection ( Properties.Settings.Default.Production_Connect );
            if (connect.State != ConnectionState.Open)
            {
                connect.Open ( );
            }
            cmnd = new SqlCommand ( sqlText, connect );
            dataAdapter = new SqlDataAdapter ( sqlText, connect );
            dataAdapter.SelectCommand = cmnd;
            dataAdapter.Fill ( dataTable );
            dataTableReader = dataTable.CreateDataReader ( );
            connect.Close ( );
            return dataTableReader;
        }
    }
}
