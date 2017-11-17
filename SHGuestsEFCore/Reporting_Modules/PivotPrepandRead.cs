using System;
using System.Data;
using System.Data.SqlClient;
using SHGuestsEFCore.Called_Dialogs;

namespace SHGuestsEFCore.Reporting_Modules
{
    public class PivotPrepandRead
    {
        public SqlConnection connect;
        public SqlCommand cmnd;
        public SqlDataAdapter dataAdapter;
        public SHGuests.pivot_rpt_type rpt_Type;

        public DataTableReader PivotPrep ( ref string sqlText, out DataTable dataTable,
                                           SHGuests.pivot_rpt_type rpt_Type, bool refersw, ref string rpt_Title)
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
            if (dataTableReader.HasRows)
            {
                PivotTable_Reports sptf = new PivotTable_Reports ( dataTable, dataTableReader );
                string query_title = rpt_Title;
                sptf.report_type = rpt_Type;
                sptf.Text = query_title;
                sptf.referring_switch = refersw;
                sptf.ShowDialog ( );
                dataTableReader.Close ( );
            }
            return dataTableReader;
        }
        /// <summary>
        /// This function has been deprecated
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        /// 
        [Obsolete("Unused Code", false)]
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
