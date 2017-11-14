using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SHGuestsEFCore.DataModel;

namespace SHGuestsEFCore.Reporting_Modules
{
    public class LINQ_Reports
    {
        public DataTable dataTable;
        public static char [ ] _defaulttrim = new char [ ] { ' ', '\t', '\r', '\n' };
        public static DateTime ParkRoadCutOffDate = new DateTime ( 2011, 06, 05 );

        public DataTable Roster_Report ( )
        {
            dataTable = new DataTable ( "Roster" );
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
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
                return dataTable;
            }
        }
        public DataTable LongCurrentsReport ( )
        {
            dataTable = new DataTable ( "LongStays" );
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;

            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from jd in db.Guests
                               join vd in db.Visits
                               on jd.GuestId equals vd.GuestId
                               let bdays = myMethod(to_Date, vd.AdmitDate)
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

                foreach( var i in roster)
                {
                    dataTable.Rows.Add ( i.Name, i.Gender, i.InDate, i.Days, i.Reason, i.Agency );
                }
                return dataTable;
            }
        }

        public DataTable QuestionableData()
        {
            dataTable = new DataTable ( "HuhData" );
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
            using (var db = new DataModel.SHGuests ( ))
            {
                var roster = ( from jd in db.Guests
                               join vd in db.Visits
                               on jd.GuestId equals vd.GuestId
                               where ( ( vd.Worker.Contains ( "No file" ) || vd.Worker.Contains ( "Signature Ill" ) ) ||
                                      ( jd.Ssn == 999999999 || jd.BirthDate == to_Date )
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
            return dataTable;
        }

        public DataTable Ineligibles ( )
        {
            dataTable = new DataTable ( "Ineligibles" );
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
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
            return dataTable;
        }

        public DataTable DeceasedGuests()
        {
            dataTable = new DataTable ( "Ineligibles" );
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
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

            return dataTable;
        }

        public DataTable CompleteGuestList()
        {
            dataTable = new DataTable ( "Guest List" );
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
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
            }
            return dataTable;
        }

        public DataTable RoomAssignments()
        {
            dataTable = new DataTable ( "Guest List" );
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
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
            return dataTable;
        }

        public DataTable SocialWorkerGuestList()
        {
            dataTable = new DataTable ( "Social Worker Guest List" );
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
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
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
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
            return dataTable;
        }

        public DataTable WalkOffsReport ( )
        {
            dataTable = new DataTable ( "No SHows" );
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
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
            return dataTable;
        }
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
