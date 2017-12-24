using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SHGuestsEFCore.Called_Dialogs;
using SHGuestsEFCore.DataModel;
using SHGuestsEFCore.Reporting_Modules;

namespace SHGuestsEFCore
{
    public partial class SHGuests : Form
    {
        #region Constants and Variables

        public enum pivot_rpt_type { Normal = 0, MonthlyReport = 2, Worker = 3, WorkerDetail = 4 };

        public pivot_rpt_type report_type;
        public static char [ ] _defaulttrim = new char [ ] { ' ', '\t', '\r', '\n' };

        public bool rs = true, statistical_report = true, repopulate = false, readmit = false;
        public string current_combo, discharged_combo, parkroad_combo, filePath;
        public static DateTime ParkRoadCutOffDate = new DateTime ( 2011, 06, 05 );
        public static Font lbl_Font = new Font ( "Tahoma", 10F, FontStyle.Regular, GraphicsUnit.Point );
        public static Font status_Font = new Font ( "Tahoma", 10F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point );
        public static Font combo_Font = new Font ( "Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point );

        public Report_Results rpt_dlg;
        public Grouped_Reports gr;
        public DialogResult res;

        public int num_rows = 0;

        public DataModel.SHGuests db;
        public List<Guests> main_List;

        #endregion Constants and Variables

        #region Constructor

        public SHGuests ( )
        {
            db = new DataModel.SHGuests ( );
            filePath = Path.GetDirectoryName ( Application.ExecutablePath );
            int ndx = filePath.LastIndexOf ( @"\bin" );
            string tmp_Path = filePath.Substring ( 0, ndx );
            filePath = tmp_Path + @"\T-SQL_Queries\";
            this.BackColor = ColorTranslator.FromHtml ( "#5F9EA0" );
            //this.BackColor = ColorTranslator.FromHtml ( "#003B6F" );                          Tardis Blue;May be a little too dark
            InitializeComponent ( );
        }

        #endregion Constructor

        #region Main Data Loading

        private void NewNextGenGuestProcess_Load ( object sender, EventArgs e )
        {
            var t_Guests = db.Guests
                          .Include ( g => g.VisitsNavigation )
                          .Include ( g => g.Photos )
                          .OrderBy ( g => g.LastName )
                          .ThenBy ( g => g.FirstName );
            main_List = new List<Guests> ( t_Guests );
            LoadtheComboBoxes ( );
            return;
        }

        private void LoadtheComboBoxes ( )
        {
            string current_combo = string.Empty;
            string discharged_combo = string.Empty;
            string parkroad_combo = string.Empty;

            comboBox_Currents.Items.Clear ( );
            comboBox_Dischargeds.Items.Clear ( );
            comboBox_Ineligible.Items.Clear ( );

            foreach (Guests i in main_List)
            {
                string Name = string.Concat ( i.LastName, ", ", i.FirstName );
                string Guestid = i.GuestId.ToString ( "00000" );

                foreach (Visits v in i.VisitsNavigation)
                {
                    switch (v.Roster)
                    {
                        case "C":
                            current_combo = string.Concat ( Name, ", ", Guestid, ", ", v.VisitNumber.ToString ( ) );
                            comboBox_Currents.Items.Add ( current_combo );
                            break;

                        case "D":

                            discharged_combo = string.Concat ( Name, ", ", Guestid, ", ", v.VisitNumber.ToString ( ), ", ",
                                 ( v.CanReturn ) ? "Yes" : "No " );
                            switch (v.CanReturn)
                            {
                                case true:
                                    if (!v.Deceased || !v.DischargeReason.Contains ( "No Show" ))
                                    {
                                        comboBox_Dischargeds.Items.Add ( discharged_combo );
                                    }
                                    else
                                    {
                                        comboBox_Ineligible.Items.Add ( discharged_combo );
                                    }
                                    break;

                                case false:
                                    if (v.Deceased || v.DischargeReason.Contains ( "No Show" ))
                                    {
                                        comboBox_Dischargeds.Items.Add ( discharged_combo );
                                    }
                                    else
                                    {
                                        comboBox_Ineligible.Items.Add ( discharged_combo );
                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            comboBox_Currents.Refresh ( );
            comboBox_Dischargeds.Refresh ( );
            comboBox_Ineligible.Refresh ( );
            DoTotalsandLabels ( );
            return;
        }

        private void DoTotalsandLabels ( )
        {
            label_CurrentCount.Font = lbl_Font;
            label_DischargedCount.Font = lbl_Font;
            label_MultiVisits.Font = lbl_Font;
            label_SingleVisits.Font = lbl_Font;
            label_TotalVisits.Font = lbl_Font;
            label_ParkRoadVisits.Font = lbl_Font;
            label_NoReturnCount.Font = lbl_Font;
            label_FortuneStreetGuests.Font = lbl_Font;
            label_FortuneStreetGuests.ForeColor = Color.Blue;
            label_ParkRoadGuests.Font = lbl_Font;
            label_ParkRoadGuests.ForeColor = Color.Red;
            label_cboxtot_discharged.Font = lbl_Font;
            label_cboxtot_discharged.ForeColor = Color.Maroon;
            toolStripStatusLabel_statusLabel.Font = status_Font;
            toolStripStatusLabel_statusLabel.ForeColor = Color.DarkBlue;
            int currentcount = db.Guests.Where ( v => v.Roster.Equals ( "C" ) ).Count ( );
            int dischcount = db.Guests.Where ( v => v.Roster.Equals ( "D" ) ).Count ( );               
            int totalguests = main_List.Count ( );
            int totalvisits = db.Visits.Count ( );
            int singlevisits = db.Visits.Where ( v => v.VisitNumber == 1 ).Count ( );
            //int multivisits = db.Visits.Where ( g => g.VisitNumber > 1 ).Where ( g => g.Roster.Equals ( "D" ) ).Count ( );
            int multivisits = totalvisits - totalguests;
            int parkrdvisits = db.Visits.Count ( v => v.Discharged <= ParkRoadCutOffDate );
            int no_returns = db.Visits.Count ( v => !v.CanReturn && !v.Deceased && !v.DischargeReason.Contains ( "No Show" ) );
            int fortunestreetVisits = ( dischcount - no_returns );
            label_CurrentCount.Text = ( currentcount > 9 ) ? $"{currentcount,9:N0} Current Guests" : $"{currentcount,10:N0} Current Guests";
            label_DischargedCount.Text = $"{dischcount,7:N0} Discharged Guests";
            label_MultiVisits.Text = $"{multivisits,8:N0} Guests Here More Than Once";
            label_SingleVisits.Text = $"{totalguests,7:N0} Total Guests";
            label_TotalVisits.Text = $"{totalvisits,7:N0} Total Guest Visits";
            label_ParkRoadVisits.Text = $"{parkrdvisits,8:N0} Park Road Guest Visits";
            label_NoReturnCount.Text = $"{no_returns,8:N0} Guests Ineligible to Return";
            label_FortuneStreetGuests.Text = $"{fortunestreetVisits,8:N0} Eligible to Return Guest Count";
            label_ParkRoadGuests.Text = $"{comboBox_Ineligible.Items.Count,9:N0} Ineligible Guest Count";
            label_cboxtot_discharged.Text = $"{( fortunestreetVisits + comboBox_Ineligible.Items.Count ),8:N0} Total Discharged Guests";
            var databasename = db.Database.GetDbConnection ( ).Database;
            Version myver = new Version ( );
            myver = typeof ( SHGuests ).Assembly.GetName ( ).Version;
            String [ ] cmd_line = Environment.GetCommandLineArgs ( );
            String cmd_ln = String.Join ( "", cmd_line );
            string filepath = Path.GetFileNameWithoutExtension ( cmd_ln );
            toolStripStatusLabel_statusLabel.Text = $"Program: {filepath} Version {myver.Major}.{myver.Minor}.{myver.MajorRevision}.{myver.MinorRevision} " +
                                                    $"Database: {databasename}";
            statusStrip_mainStatusStrip.BackColor = SystemColors.Control;
            statusStrip_mainStatusStrip.Update ( );
            statusStrip_mainStatusStrip.Show ( );
            this.Update ( );
            return;
        }

        #endregion Main Data Loading

        #region Event and Message Handlers

        #region Combobox Selected Items Processing

        private void comboBox_Currents_SelectedIndexChanged ( object sender, EventArgs e )
        {
            string dlg_header_str = null;
            string [ ] pass_parms = null;
            string [ ] delimiter = { ", " };
            string selected = comboBox_Currents.GetItemText ( comboBox_Currents.SelectedItem );
            int count = 4;
            // Passed Parameters are as follow: [0] = Last Name, [1] = First Name, [2] = Date of
            // Birth, [3] = Number of Visits 3rd level of key to table
            pass_parms = selected.Split ( delimiter, count, System.StringSplitOptions.None );
            string guestID = pass_parms [2];
            string last_name = pass_parms [0];
            string first_name = pass_parms [1];
            string visit_num = pass_parms [3];
            int.TryParse ( guestID, out int GiD );
            int.TryParse ( visit_num, out int visit_int );
            dlg_header_str = DateTime.Now.ToString ( "dddd, MMMM dd, yyyy @ HH:mm" );
            Current_Guest_Display cgd = new Current_Guest_Display ( GiD, visit_int, dlg_header_str );
            Hide ( );
            cgd.ShowDialog ( );
            LoadtheComboBoxes ( );
            Refresh ( );
            Show ( );
            return;
        }

        private void comboBox_Dischargeds_SelectedIndexChanged ( object sender, EventArgs e )
        {
            string dlg_header_str = null;
            string [ ] pass_parms = null;
            string [ ] delimiter = { ", " };
            string selected = comboBox_Dischargeds.GetItemText ( comboBox_Dischargeds.SelectedItem );
            int count = 5;
            // Passed Parameters are as follow: [0] = Last Name, [1] = First Name, [2] = Date of
            // Birth, [3] = Number of Visits 3rd level of key to table
            pass_parms = selected.Split ( delimiter, count, System.StringSplitOptions.None );
            string guestID = pass_parms [2];
            string last_name = pass_parms [0];
            string first_name = pass_parms [1];
            string visit_num = pass_parms [3];
            int.TryParse ( guestID, out int GiD );
            int.TryParse ( visit_num, out int visit_int );
            dlg_header_str = DateTime.Now.ToString ( "dddd, MMMM dd, yyyy @ HH:mm" );
            Discharged_Guest_Display gd = new Discharged_Guest_Display ( GiD, visit_int, dlg_header_str );
            Hide ( );
            gd.ShowDialog ( );
            LoadtheComboBoxes ( );
            Refresh ( );
            Show ( );
            return;
        }

        private void combobox_Ineligible_SelectedIndexChanged ( object sender, EventArgs e )
        {
            string dlg_header_str = null;
            string [ ] pass_parms = null;
            string [ ] delimiter = { ", " };
            string selected = comboBox_Ineligible.GetItemText ( comboBox_Ineligible.SelectedItem );
            int count = 5;
            // Passed Parameters are as follow: [0] = Last Name, [1] = First Name, [2] = Date of
            // Birth, [3] = Number of Visits 3rd level of key to table
            pass_parms = selected.Split ( delimiter, count, System.StringSplitOptions.None );
            string guestID = pass_parms [2];
            string last_name = pass_parms [0];
            string first_name = pass_parms [1];
            string visit_num = pass_parms [3];
            int.TryParse ( guestID, out int GiD );
            int.TryParse ( visit_num, out int visit_int );
            dlg_header_str = DateTime.Now.ToString ( "dddd, MMMM dd, yyyy @ HH:mm" );
            Discharged_Guest_Display gd = new Discharged_Guest_Display ( GiD, visit_int, dlg_header_str );
            Hide ( );
            gd.ShowDialog ( );
            LoadtheComboBoxes ( );
            Refresh ( );
            Show ( );
            return;
        }

        #endregion Combobox Selected Items Processing

        private void button_QuitApp_Click ( object sender, EventArgs e )
        {
            Application.Exit ( );
        }

        #region Menu Items - Non-Reporting

        private void aboutToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            About_this_App ata = new About_this_App ( );
            Hide ( );
            ata.ShowDialog ( );
            Show ( );
            return;
        }

        private void addANewGuestToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            bool readmit = false;
            Add_CG add_guest = new Add_CG ( readmit, 0 );
            Hide ( );
            add_guest.ShowDialog ( );
            var t_Guests = db.Guests
                          .Include ( g => g.VisitsNavigation )
                          .Include ( g => g.Photos )
                          .OrderBy ( g => g.LastName )
                          .ThenBy ( g => g.FirstName );
            main_List = new List<Guests> ( t_Guests );
            LoadtheComboBoxes ( );
            Refresh ( );
            Show ( );
            return;
        }

        private void toolStripMenuItem_AddFormerGuest_Click ( object sender, EventArgs e )
        {
            DialogResult res = MessageBox.Show ( "This should only be used for a former guest is not yet on the database." + Environment.NewLine +
                                                 "For an existing guest, Re Admit and then discharge",
                                                 "Information",
                                                 MessageBoxButtons.OKCancel,
                                                 MessageBoxIcon.Information );
            if (res == DialogResult.OK)
            {
                Former_Guest_Add shga = new Former_Guest_Add ( );
                Hide ( );
                shga.ShowDialog ( );
                var t_Guests = db.Guests
                              .Include ( g => g.VisitsNavigation )
                              .Include ( g => g.Photos )
                              .OrderBy ( g => g.LastName )
                              .ThenBy ( g => g.FirstName );
                main_List = new List<Guests> ( t_Guests );
                LoadtheComboBoxes ( );
                Show ( );
            }
            return;
        }

        #endregion Menu Items - Non-Reporting

        #region LINQ Reporting

        private void rosterToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );
            Hide ( );
            dt = lr.Roster_Report ( );
            Show ( );
            return;
        }

        private void guestsHere45DaysToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );

            Hide ( );
            dt = lr.LongCurrentsReport ( );
            Show ( );
            return;
        }

        private void guestWithQuestionableInformationToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );

            Hide ( );
            dt = lr.QuestionableData ( );
            Show ( );
            return;
        }

        private void inelegibleForReturnToSamaritanHouseToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );

            Hide ( );
            dt = lr.Ineligibles ( );
            Show ( );
            return;
        }

        private void completeGuestListToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );
            var db = new DataModel.SHGuests ( );

            Hide ( );
            dt = lr.CompleteGuestList ( );
            Show ( );
            return;
        }

        private void roomAssignmentsToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );
            var db = new DataModel.SHGuests ( );

            Hide ( );
            dt = lr.RoomAssignments ( );
            Show ( );
            return;
        }

        private void deceasedGuestsToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );

            Hide ( );
            dt = lr.DeceasedGuests ( );
            Show ( );
            return;
        }

        private void totalVisitStatisticsToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            gr = new Grouped_Reports ( );
            DataTable grp = new DataTable ( );

            Hide ( );
            grp = gr.TotalVisitsStatistics ( );
            Show ( );
            return;
        }

        private void totalBedDaysByRosterAndGenderToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            gr = new Grouped_Reports ( );
            DataTable grp = new DataTable ( );

            Hide ( );
            grp = gr.TotalVisitsbyGenderandVisists ( );
            Show ( );
            return;
        }

        private void Social_Worker_Guest_List_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );

            dt = lr.SocialWorkerGuestList ( );
            DataTableReader dtr = dt.CreateDataReader ( );
            if (dtr.HasRows)
            {
                PivotTable_Reports sptf = new PivotTable_Reports ( dt, dtr );
                string query_title = $"Samaritan House SW Guest(s) List  Since {ParkRoadCutOffDate.ToShortDateString ( )} ({dt.Rows.Count:N0} Records) As of: {DateTime.Today:D}"; ;
                sptf.report_type = pivot_rpt_type.WorkerDetail;
                sptf.Text = query_title;
                sptf.referring_switch = false;
                Hide ( );
                sptf.ShowDialog ( );
                Show ( );
                dtr.Close ( );
            }
            return;
        }

        private void hospitalNoShowsToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );

            Hide ( );
            dt = lr.NoShowsReport ( );
            Show ( );
            return;
        }

        private void guestWalkOffsToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );

            Hide ( );
            dt = lr.WalkOffsReport ( );
            Show ( );
            return;
        }

        private void multipleVisitGuestToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            LINQ_Reports lr = new LINQ_Reports ( );
            DataTable dt = new DataTable ( );

            Hide ( );
            dt = lr.MultipleVisitsReport ( );
            Show ( );
            return;
        }

        #endregion LINQ Reporting

        #region SQL Pivot Reporting (Native SQL Queries)

        private void monthlyAdmissionsToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            PivotPrepandRead ppar = new PivotPrepandRead ( );
            FileInfo sql_file = new FileInfo ( filePath + "admits_by_month.sql" );
            string str_sql = sql_file.OpenText ( ).ReadToEnd ( );
            string query_title = $"Samaritan House Monthly Admissions Since 06/05/2011 as of: {DateTime.Today.ToString ( "MM/dd/yyyy" )}";
            Hide ( );
            DataTableReader dtr = ppar.PivotPrep ( ref str_sql, out DataTable dt, pivot_rpt_type.MonthlyReport, true, ref query_title );
            Show ( );
            return;
        }

        private void annualAdmissionsToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            PivotPrepandRead ppar = new PivotPrepandRead ( );
            FileInfo sql_file = new FileInfo ( filePath + "admit_by_months.sql" );
            string str_sql = sql_file.OpenText ( ).ReadToEnd ( );
            SqlConnection en_conn = new SqlConnection ( Properties.Settings.Default.ConnectionString );
            string query_title = $"Samaritan House Annual Admissions Since 06/05/2011 as of: {DateTime.Today.ToString ( "MM/dd/yyyy" )}";
            Hide ( );
            DataTableReader dtr = ppar.PivotPrep ( ref str_sql, out DataTable dt, pivot_rpt_type.Normal, true, ref query_title );
            Show ( );
            return;
        }

        private void monthlyDischargesToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            PivotPrepandRead ppar = new PivotPrepandRead ( );
            FileInfo sql_file = new FileInfo ( filePath + "discharges_by_month.sql" );
            string str_sql = sql_file.OpenText ( ).ReadToEnd ( );
            string query_title = $"Samaritan House Monthly Discharges Since 06/05/2011 as of: {DateTime.Today.ToString ( "MM/dd/yyyy" )}";
            Hide ( );
            DataTableReader dtr = ppar.PivotPrep ( ref str_sql, out DataTable dt, pivot_rpt_type.MonthlyReport, true, ref query_title );
            Show ( );
            return;
        }

        private void annualDischargesToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            PivotPrepandRead ppar = new PivotPrepandRead ( );
            FileInfo sql_file = new FileInfo ( filePath + "discharges_by_months.sql" );
            string str_sql = sql_file.OpenText ( ).ReadToEnd ( );
            string query_title = $"Samaritan House Yearly Discharges Since 06/05/2011 as of: {DateTime.Today.ToString ( "MM/dd/yyyy" )}";
            Hide ( );
            DataTableReader dtr = ppar.PivotPrep ( ref str_sql, out DataTable dt, pivot_rpt_type.Normal, true, ref query_title );
            Show ( );
            return;
        }

        private void agencyAnnualAdmissionsToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            PivotPrepandRead ppar = new PivotPrepandRead ( );
            FileInfo sql_file = new FileInfo ( filePath + "agency_admissions_by_year.sql" );
            string str_sql = sql_file.OpenText ( ).ReadToEnd ( );
            string query_title = $"Samaritan House Admissions by Hospital Since 06/05/2011 as of: {DateTime.Today.ToString ( "MM/dd/yyyy" )}";
            Hide ( );
            DataTableReader dtr = ppar.PivotPrep ( ref str_sql, out DataTable dt, pivot_rpt_type.Normal, true, ref query_title );
            Show ( );
            return;
        }

        private void agencySocialWorkerAnnualAdmissionsToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            PivotPrepandRead ppar = new PivotPrepandRead ( );
            FileInfo sql_file = new FileInfo ( filePath + "annual_worker_admissions.sql" );
            string query_title = $"Samaritan House Yearly Admissions by Agency and Social Worker Since 06-05-2011 as of: {DateTime.Today.ToString ( "MM/dd/yyyy" )}";
            string str_sql = sql_file.OpenText ( ).ReadToEnd ( );
            Hide ( );
            DataTableReader dtr = ppar.PivotPrep ( ref str_sql, out DataTable dt, pivot_rpt_type.Worker, false, ref query_title );
            Show ( );
            return;
        }

        private void socialWorkerCanReturnReportToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            PivotPrepandRead ppar = new PivotPrepandRead ( );
            FileInfo sql_file = new FileInfo ( filePath + "sw_returns_report.sql" );
            string str_sql = sql_file.OpenText ( ).ReadToEnd ( );
            string query_title = $"Samaritan House Social Worker Return Report Since 06/05/2011 as of: {DateTime.Today.ToString ( "MM/dd/yyyy" )}";
            Hide ( );
            DataTableReader dtr = ppar.PivotPrep ( ref str_sql, out DataTable dt, pivot_rpt_type.Worker, false, ref query_title );
            Show ( );
            return;
        }

        #endregion SQL Pivot Reporting (Native SQL Queries)

        #region Results Reporting

        //private void ViewReport ( DataTable theList, string title, bool rpt_type )
        //{
        //    rpt_dlg = new Report_Results ( theList, rpt_type );
        //    try
        //    {
        //        Hide ( );
        //        num_rows = rpt_dlg.NumberofRows;
        //        if (num_rows > 0)
        //        {
        //            rpt_dlg.Text = title;
        //            rpt_dlg.ShowDialog ( );
        //            rpt_dlg.ResetFont ( );
        //        }
        //        Show ( );
        //    }
        //    catch (Exception exc)
        //    {
        //        MessageBox.Show ( "Error " + exc.Message );
        //    }
        //    return;
        //}

        #endregion Results Reporting

        #endregion Event and Message Handlers

        #region My Functions for LINQ

        public int CalcDays ( DateTime from, DateTime to )
        {
            TimeSpan ts = new TimeSpan ( 0, 0, 0 );
            ts = from.AddDays ( 1 ) - to;
            return ts.Days;
        }

        #endregion My Functions for LINQ
    }
}