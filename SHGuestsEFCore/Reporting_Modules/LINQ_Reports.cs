﻿using CommonRoutines;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SHGuestsEFCore.Called_Dialogs;

namespace SHGuestsEFCore.Reporting_Modules
{
    public class LINQ_Reports
    {
        public DataTable dataTable;
        public static char [ ] _defaulttrim = new char [ ] { ' ', '\t', '\r', '\n' };
        public DateTime ParkRoadCutOffDate = new DateTime ( 2011, 06, 05 ), to_Date = DateTime.Today;
        public string report_Title = string.Empty;
        public Report_Results rpt_dlg;
        public int num_rows = 0;
        public static Func<DateTime, DateTime, int> myMethod = CalcDays;
        public Commons cr = new Commons ( );

        public LINQ_Reports ( )
        {
        }

        public DataTable Roster_Report ( )
        {
            dataTable = new DataTable ( "Roster" );
            string [ ] colHeadings = new string [ ]
            {
                "Admitted", "Name", "Gender", "Reason", "Agency (Worker)", "Bed Days", "Visit"
            };
            Type [ ] colTypes = new Type [ ]
            {
                typeof(DateTime), typeof(string), typeof(string), typeof(string), typeof(string), typeof(int), typeof(int)
            };
            for (int i = 0; i < colHeadings.Count ( ); i++)
            {
                dataTable.Columns.Add ( colHeadings [i], colTypes [i] );
            }
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from jd in db.Guests
                               join vd in db.Visits
                               on jd.GuestId equals vd.GuestId
                               orderby vd.AdmitDate, jd.LastName, jd.FirstName
                               where vd.Roster == "C"
                               select new
                               {
                                   Admitted = vd.AdmitDate,
                                   Name = string.Concat ( jd.LastName, ", ", jd.FirstName ),
                                   Gender = ( jd.Gender == "M" ) ? "Male" : "Female",
                                   Reason = vd.AdmitReason.TrimEnd ( _defaulttrim ),
                                   AgencyWorker = string.Concat ( vd.Agency, " (", vd.Worker, ")" ),
                                   Days = myMethod ( DateTime.Today, vd.AdmitDate ),
                                   Visit = vd.VisitNumber
                               } ).ToList ( );

                foreach (var rr in roster)
                {
                    dataTable.Rows.Add ( rr.Admitted, rr.Name, rr.Gender, rr.Reason, rr.AgencyWorker, rr.Days, rr.Visit );
                }
                report_Title = $"Samaritan House Current Guest List: {dataTable.Rows.Count:N0} records as of: {DateTime.Today:D}";
                ShowReport ( dataTable, report_Title, false );
                return dataTable;
            }
        }

        public DataTable LongCurrentsReport ( )
        {
            dataTable = new DataTable ( "LongStays" );

            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from jd in db.Guests
                               join vd in db.Visits
                               on jd.GuestId equals vd.GuestId
                               let bdays = myMethod ( to_Date, vd.AdmitDate )
                               orderby vd.AdmitDate, jd.LastName, jd.FirstName
                               where vd.Roster == "C" && bdays > 44
                               select new
                               {
                                   Name = string.Concat ( jd.LastName, ", ", jd.FirstName ),
                                   Gender = ( jd.Gender == "M" ) ? "Male" : "Female",
                                   InDate = vd.AdmitDate,
                                   Days = bdays,
                                   Reason = vd.AdmitReason,
                                   Agency = vd.Agency
                               } ).ToList ( );
                dataTable.Columns.Add ( "Name", typeof ( string ) );
                dataTable.Columns.Add ( "Gender", typeof ( string ) );
                dataTable.Columns.Add ( "Admitted", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Days", typeof ( int ) );
                dataTable.Columns.Add ( "Admit Reason", typeof ( string ) );
                dataTable.Columns.Add ( "Hospital", typeof ( string ) );

                foreach (var i in roster)
                {
                    dataTable.Rows.Add ( i.Name, i.Gender, i.InDate, i.Days, i.Reason, i.Agency );
                }
                report_Title = $"Samaritan House Guest with > 45 Days: {dataTable.Rows.Count:N0} records as of: {DateTime.Today:D}";
                ShowReport ( dataTable, report_Title, false );
                return dataTable;
            }
        }

        public DataTable QuestionableData ( )
        {
            dataTable = new DataTable ( "HuhData" );
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from jd in db.Guests
                               join vd in db.Visits
                               on jd.GuestId equals vd.GuestId
                               let ssnbool = cr.RegValidateSSN ( jd.Ssn.ToString ( "000-00-0000" ) )
                               let invalidbd = ( jd.BirthDate == new DateTime ( 1980, 01, 01 ) )
                               where ( ( vd.Worker.Contains ( "No file" ) || vd.Worker.Contains ( "Signature Ill" ) ) ||
                                      ( !ssnbool || invalidbd )
                                      && vd.Roster == "D" )
                               orderby jd.LastName, jd.FirstName, vd.VisitNumber
                               select new
                               {
                                   Name = string.Concat ( jd.LastName, ", ", jd.FirstName ),
                                   BirthDate = jd.BirthDate,
                                   SSNorW7 = jd.Ssn,
                                   InDate = vd.AdmitDate,
                                   OutDate = vd.Discharged,
                                   Days = vd.VisitDays,
                                   Agency_Worker = string.Concat ( vd.Agency, " (", vd.Worker, ")" )
                               } ).ToList ( );

                dataTable.Columns.Add ( "Name", typeof ( string ) );
                dataTable.Columns.Add ( "BirthDate", typeof ( DateTime ) );
                dataTable.Columns.Add ( "SSN/W7", typeof ( long ) );
                dataTable.Columns.Add ( "Admitted", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Discharged", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Days", typeof ( int ) );
                dataTable.Columns.Add ( "Agency (Worker)", typeof ( string ) );

                foreach (var i in roster)
                {
                    dataTable.Rows.Add ( i.Name, i.BirthDate, i.SSNorW7, i.InDate, i.OutDate, i.Days, i.Agency_Worker );
                }
            }
            report_Title = $"Samaritan House Guests with Questionable Information: {dataTable.Rows.Count:N0} records as of: {DateTime.Today:D}";
            ShowReport ( dataTable, report_Title, false );
            return dataTable;
        }

        public DataTable Ineligibles ( )
        {
            dataTable = new DataTable ( "Ineligibles" );
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from jd in db.Guests
                               join vd in db.Visits
                               on jd.GuestId equals vd.GuestId
                               where ( !vd.CanReturn ) && ( !vd.Deceased && !vd.DischargeReason.Contains ( "No Show" ) )
                               orderby jd.LastName, jd.FirstName, vd.Discharged
                               select new
                               {
                                   Name = string.Concat ( jd.LastName, ", ", jd.FirstName ),
                                   InDate = vd.AdmitDate,
                                   OutDate = vd.Discharged,
                                   Days = vd.VisitDays,
                                   OutReason = vd.DischargeReason,
                                   Agency_Worker = string.Concat ( vd.Agency, " (", vd.Worker, ")" )
                               } ).ToList ( );

                dataTable.Columns.Add ( "Name", typeof ( string ) );
                dataTable.Columns.Add ( "Admitted", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Discharged", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Days", typeof ( int ) );
                dataTable.Columns.Add ( "Discharge Reason", typeof ( string ) );
                dataTable.Columns.Add ( "Agency (Worker)", typeof ( string ) );

                foreach (var i in roster)
                {
                    dataTable.Rows.Add ( i.Name, i.InDate, i.OutDate, i.Days, i.OutReason, i.Agency_Worker );
                }
            }
            report_Title = $"Samaritan House Guests Ineligible for Return: {dataTable.Rows.Count:N0} records as of: {DateTime.Today:D}";
            ShowReport ( dataTable, report_Title, false );
            return dataTable;
        }

        public DataTable DeceasedGuests ( )
        {
            dataTable = new DataTable ( "Deceased" );
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = from jd in db.Guests
                             join vd in db.Visits
                             on jd.GuestId equals vd.GuestId
                             orderby jd.LastName, jd.FirstName
                             where vd.Roster == "D" && vd.Deceased
                             select new
                             {
                                 Name = string.Concat ( jd.LastName, ", ", jd.FirstName ),
                                 Visit = vd.VisitNumber,
                                 InDate = vd.AdmitDate,
                                 OutDate = vd.Discharged,
                                 AdmitReason = vd.AdmitReason,
                                 DischargeReason = vd.DischargeReason
                             };
                dataTable.Columns.Add ( "Name", typeof ( string ) );
                dataTable.Columns.Add ( "Visit", typeof ( int ) );
                dataTable.Columns.Add ( "Admitted", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Discharged", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Admit Reason", typeof ( string ) );
                dataTable.Columns.Add ( "Discharge Reason", typeof ( string ) );

                foreach (var i in roster)
                {
                    dataTable.Rows.Add ( i.Name, i.Visit, i.InDate, i.OutDate, i.AdmitReason, i.DischargeReason );
                }
            }

            report_Title = $"Samaritan House  - {dataTable.Rows.Count:N0} Former Guests Listed as Deceased As of: {DateTime.Today:D}"; 
            ShowReport ( dataTable, report_Title, false );
            return dataTable;
        }

        public DataTable CompleteGuestList ( )
        {
            dataTable = new DataTable ( "Guest List" );
            int guest_count = 0;
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from nc in db.Guests
                               join vd in db.Visits
                               on nc.GuestId equals vd.GuestId
                               orderby nc.LastName, nc.FirstName
                               where ( nc.Roster == "D" ) && vd.Discharged >= ParkRoadCutOffDate
                               select new
                               {
                                   Name = string.Concat ( nc.LastName, ", ", nc.FirstName ),
                                   Visit = vd.VisitNumber,
                                   Admitted = vd.AdmitDate,
                                   Discharged = vd.Discharged,
                                   Days = vd.VisitDays,
                                   DischargeReason = vd.DischargeReason,
                                   Return = ( vd.CanReturn ) ? "Yes" : "No"
                               }
                                   ).ToList ( );

                dataTable.Columns.Add ( "Name", typeof ( string ) );
                dataTable.Columns.Add ( "Visit", typeof ( int ) );
                dataTable.Columns.Add ( "Admitted", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Discharged", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Days", typeof ( int ) );
                dataTable.Columns.Add ( "Discharge Reason", typeof ( string ) );
                dataTable.Columns.Add ( "Return", typeof ( string ) );

                foreach (var i in roster)
                {
                    dataTable.Rows.Add ( i.Name, i.Visit, i.Admitted, i.Discharged, i.Days, i.DischargeReason, i.Return );
                }
                guest_count = db.Visits.Count ( nl => nl.VisitNumber == 1 );
            }
            report_Title = $"Fortune Street Discharged Guest Listing of {guest_count:N0} Guests As of: {DateTime.Today:D}"; 
            ShowReport ( dataTable, report_Title, false );
            return dataTable;
        }

        public DataTable RoomAssignments ( )
        {
            dataTable = new DataTable ( "Guest List" );
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from jd in db.Guests
                               join vd in db.Visits
                               on jd.GuestId equals vd.GuestId
                               orderby vd.Room, vd.Bed
                               where vd.Roster == "C"
                               select new
                               {
                                   Room = ( int )vd.Room,
                                   Bed = ( int )vd.Bed,
                                   Name = string.Concat ( jd.LastName, ", ", jd.FirstName ),
                                   Gender = ( jd.Gender == "M" ) ? "Male" : "Female",
                                   InDate = vd.AdmitDate,
                                   Days = myMethod ( DateTime.Today, vd.AdmitDate ),
                                   Reason = vd.AdmitReason,
                                   Agency = vd.Agency
                               } ).ToList ( );
                dataTable.Columns.Add ( "Room", typeof ( int ) );
                dataTable.Columns.Add ( "Bed", typeof ( int ) );
                dataTable.Columns.Add ( "Name", typeof ( string ) );
                dataTable.Columns.Add ( "Gender", typeof ( string ) );
                dataTable.Columns.Add ( "Admitted", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Days", typeof ( int ) );
                dataTable.Columns.Add ( "Admit Reason Reason", typeof ( string ) );
                dataTable.Columns.Add ( "Hospital", typeof ( string ) );

                foreach (var i in roster)
                {
                    dataTable.Rows.Add ( i.Room, i.Bed, i.Name, i.Gender, i.InDate, i.Days, i.Reason, i.Agency );
                }
            }
            report_Title = $"Samaritan House Current Guest Room Assignments As of: {DateTime.Today:D}";
            ShowReport ( dataTable, report_Title, false );
            return dataTable;
        }

        public DataTable SocialWorkerGuestList ( )
        {
            dataTable = new DataTable ( "Social Worker Guest List" );
            string [ ] colHeadings = new string [ ]
            {
                "Worker", "Agency", "Admit", "Discharge", "Name", "Admit Reason", "Days", "Ret"
            };
            Type [ ] colTypes = new Type [ ]
            {
                typeof(string), typeof(string), typeof(DateTime), typeof(DateTime), typeof(string), typeof(string), typeof(int), typeof(string)
            };
            for (int i = 0; i < colHeadings.Count ( ); i++)
            {
                dataTable.Columns.Add ( colHeadings [i], colTypes [i] );
            }
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from g in db.Guests
                               join v in db.Visits
                               on g.GuestId equals v.GuestId
                               where v.AdmitDate >= ParkRoadCutOffDate
                               orderby v.Worker, v.Agency, v.AdmitDate, g.LastName, g.FirstName
                               select new
                               {
                                   Worker = v.Worker.TrimEnd ( _defaulttrim ),
                                   Agency = v.Agency.TrimEnd ( _defaulttrim ),
                                   Name = string.Concat ( g.LastName, ", ", g.FirstName ),
                                   InDate = v.AdmitDate,
                                   OutDate = v.Discharged,
                                   AdmitReason = v.AdmitReason.TrimEnd ( _defaulttrim ),
                                   Days = ( v.Roster.Equals ( "D" ) ) ? v.VisitDays : myMethod ( to_Date, v.AdmitDate ),
                                   Return = ( v.CanReturn ) ? "Yes " : "No  "
                               } ).ToList ( );

                foreach (var item in roster)
                {
                    dataTable.Rows.Add ( item.Worker, item.Agency, item.InDate, item.OutDate, item.Name, item.AdmitReason, item.Days, item.Return );
                }
            }
            return dataTable;
        }

        public DataTable NoShowsReport ( )
        {
            dataTable = new DataTable ( "No Shows" );
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = from jd in db.Guests
                             join vd in db.Visits
                             on jd.GuestId equals vd.GuestId
                             orderby jd.LastName, jd.FirstName
                             where vd.Roster == "D" && vd.DischargeReason.Contains ( "No Show" ) && vd.Discharged >= ParkRoadCutOffDate
                             select new
                             {
                                 Name = string.Concat ( jd.LastName, ", ", jd.FirstName ),
                                 InDate = vd.AdmitDate,
                                 OutDate = vd.Discharged,
                                 Days = vd.VisitDays,
                                 AdmitReason = vd.AdmitReason,
                                 DischargeReason = vd.DischargeReason
                             };
                dataTable.Columns.Add ( "Name", typeof ( string ) );
                dataTable.Columns.Add ( "Admitted", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Discharged", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Days", typeof ( int ) );
                dataTable.Columns.Add ( "Admit Reason", typeof ( string ) );
                dataTable.Columns.Add ( "Discharge Reason", typeof ( string ) );

                foreach (var i in roster)
                {
                    dataTable.Rows.Add ( i.Name, i.InDate, i.OutDate, i.Days, i.AdmitReason, i.DischargeReason );
                }
            }
            report_Title = $"Samaritan House Hospital No Show List ({dataTable.Rows.Count:N0} Records) As of: {DateTime.Today:D}"; 
            ShowReport ( dataTable, report_Title, false );
            return dataTable;
        }

        public DataTable WalkOffsReport ( )
        {
            dataTable = new DataTable ( "No Shows" );
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from jd in db.Guests
                               join vd in db.Visits
                               on jd.GuestId equals vd.GuestId
                               orderby jd.LastName, jd.FirstName
                               where vd.Roster == "D" && ( vd.DischargeReason.Contains ( "Walk off" ) || vd.DischargeReason.Contains ( "Walked off" ) )
                                     && vd.Discharged >= ParkRoadCutOffDate
                               select new
                               {
                                   Name = string.Concat ( jd.LastName, ", ", jd.FirstName ),
                                   InDate = vd.AdmitDate,
                                   OutDate = vd.Discharged,
                                   Days = vd.VisitDays,
                                   AdmitReason = vd.AdmitReason,
                                   DischargeReason = vd.DischargeReason
                               } ).ToList ( );

                dataTable.Columns.Add ( "Name", typeof ( string ) );
                dataTable.Columns.Add ( "Admitted", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Discharged", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Days", typeof ( int ) );
                dataTable.Columns.Add ( "Admit Reason", typeof ( string ) );
                dataTable.Columns.Add ( "Discharge Reason", typeof ( string ) );

                foreach (var i in roster)
                {
                    dataTable.Rows.Add ( i.Name, i.InDate, i.OutDate, i.Days, i.AdmitReason, i.DischargeReason );
                }
            }
            report_Title = $"Samaritan House Walk-Offs List ({dataTable.Rows.Count:N0} Records) As of: {DateTime.Today:D}";
            ShowReport ( dataTable, report_Title, false );
            return dataTable;
        }

        public DataTable MultipleVisitsReport ( )
        {
            dataTable = new DataTable ( "MultiVisits" );
            int multiv = 0;
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from jd in db.Guests
                               join vd in db.Visits
                               on jd.GuestId equals vd.GuestId
                               orderby jd.LastName, jd.FirstName
                               where jd.Visits > 1
                               select new
                               {
                                   Name = string.Concat ( jd.LastName, ", ", jd.FirstName ),
                                   Visit = vd.VisitNumber,
                                   InDate = vd.AdmitDate,
                                   OutDate = vd.Discharged,
                                   Days = ( vd.Roster.Equals ( "D" ) ) ? vd.VisitDays : myMethod ( to_Date, vd.AdmitDate ),
                                   AdmitReason = vd.AdmitReason,
                                   DischargeReason = vd.DischargeReason
                               } ).ToList ( );

                dataTable.Columns.Add ( "Name", typeof ( string ) );
                dataTable.Columns.Add ( "Visit", typeof (int ) );
                dataTable.Columns.Add ( "Admitted", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Discharged", typeof ( DateTime ) );
                dataTable.Columns.Add ( "Days", typeof ( int ) );
                dataTable.Columns.Add ( "Admit Reason", typeof ( string ) );
                dataTable.Columns.Add ( "Discharge Reason", typeof ( string ) );

                foreach (var i in roster)
                {
                    dataTable.Rows.Add ( i.Name, i.Visit, i.InDate, i.OutDate, i.Days, i.AdmitReason, i.DischargeReason );
                }
                multiv = ( db.Visits.Count ( ) - db.Guests.Count ( ) );
           }
            report_Title = $"Samaritan House Multiple Visit Guest List ({multiv:N0} Guests) As of: {DateTime.Today:D}";
            ShowReport ( dataTable, report_Title, false );
            return dataTable;
        }

        #region Report the Results

        private void ShowReport ( DataTable theList, string title, bool rpt_type )
        {
            rpt_dlg = new Report_Results ( theList, rpt_type );
            try
            {
                num_rows = rpt_dlg.NumberofRows;
                if (num_rows > 0)
                {
                    rpt_dlg.Text = title;
                    rpt_dlg.ShowDialog ( );
                    rpt_dlg.ResetFont ( );
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show ( "Error " + exc.Message );
            }
            return;
        }

        #endregion Report the Results

        #region My Functions for LINQ

        public static int CalcDays ( DateTime from, DateTime to )
        {
            TimeSpan ts = new TimeSpan ( 0, 0, 0 );
            ts = from.AddDays ( 1 ) - to;
            return ts.Days;
        }

        #endregion My Functions for LINQ
    }
}