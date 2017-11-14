using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SHGuestsEFCore.Reporting_Modules
{
    public class Grouped_Reports
    {
        #region Variables and Constants

        public DataTable grpTable;
        public int totalbeddays = 0, returned_days = 0, agencytotal = 0, totagency = 0;
        public List<Things> things, newThings;
        public bool haveprevyear = false, haveprevprevyear = false;
        public static string title = "Bed day Totals";
        public string saveRoster, saveGender;
        public decimal total_savings = 0, gt_savings = 0;
        public int? saveVisitNumber;
        public int genderCount = -1;

        public static Things blank = new Things ( true );

        public static Things blank2 = new Things ( false );

        #endregion Variables and Constants

        #region Total Visits by Roster, Gender, and VisitNumber

        public DataTable TotalVisitsbyGenderandVisists ( )
        {
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
            grpTable = new DataTable ( "Grouped" );
            using (var db = new DataModel.SHGuests ( ))
            {
                var a_Query = ( from g in db.Guests
                                join v in db.Visits
                                on g.GuestId equals v.GuestId
                                orderby v.Roster, g.Gender, v.VisitNumber
                                select new Things ( )
                                {
                                    Roster = ( v.Roster == "D" ) ? "Discharged" : "Current",
                                    Gender = ( g.Gender == "M" ) ? "Male" : "Female",
                                    VisitNumber = v.VisitNumber,
                                    Guests = 0,
                                    BedDays = ( v.Roster == "D" ) ? v.VisitDays : CalcDays ( to_Date, v.AdmitDate ),
                                    MaxDays = 0,
                                    AvgDays = 0
                                } )
                    .ToList ( );

                var gruppen = a_Query.GroupBy ( gb => new
                {
                    gb.Roster,
                    gb.Gender,
                    gb.VisitNumber
                } )
                  .OrderBy ( go => go.Key.Roster )
                  .ThenBy ( go => go.Key.Gender )
                  .Select ( gr => new Things ( )
                  {
                      Roster = gr.Key.Roster,
                      Gender = gr.Key.Gender,
                      VisitNumber = gr.Key.VisitNumber,
                      Guests = gr.Count ( ),
                      BedDays = gr.Sum ( v => v.BedDays ),
                      MinDays = gr.Min ( v => v.BedDays ),
                      MaxDays = gr.Max ( v => v.BedDays ),
                      AvgDays = gr.Average ( v => v.BedDays ),
                  } )
                  .ToList ( );

                things = new List<Things> ( gruppen );
            }

            BuildTotalsDisplay ( );
            return grpTable;
        }

        public void BuildTotalsDisplay ( )
        {
            saveGender = things [0].Gender;
            saveRoster = things [0].Roster;
            saveVisitNumber = things [0].VisitNumber;
            genderCount = 0;
            newThings = new List<Things> ( );
            foreach (Things it in things)
            {
                if (saveGender != it.Gender)
                {
                    if (genderCount > 1)
                    {
                        DoVisitTotals ( );
                        genderCount = 0;
                        saveGender = it.Gender;
                        saveVisitNumber = it.VisitNumber;
                    }
                    else
                    {
                        newThings.Add ( blank2 );
                        genderCount++;
                        saveGender = it.Gender;
                        saveVisitNumber = it.VisitNumber;
                    }
                }
                if (saveRoster != it.Roster)
                {
                    Things addTo = new Things ( )
                    {
                        Roster = "Total",
                        Gender = $"{saveRoster}",
                        VisitNumber = null,
                        Guests = things.Where ( g => g.Roster.Equals ( saveRoster ) ).Sum ( c => c.Guests ),
                        BedDays = things.Where ( g => g.Roster.Equals ( saveRoster ) ).Sum ( c => c.BedDays ),
                        MinDays = things.Where ( g => g.Roster.Equals ( saveRoster ) ).Min ( c => c.MinDays ),
                        MaxDays = things.Where ( g => g.Roster.Equals ( saveRoster ) ).Max ( c => c.MaxDays ),
                        AvgDays = 0
                    };
                    addTo.AvgDays = ( ( double )addTo.BedDays / ( double )addTo.Guests );
                    newThings.Add ( addTo );
                    newThings.Add ( blank2 );
                    newThings.Add ( it );
                    saveRoster = it.Roster;
                    saveGender = it.Gender;
                    saveVisitNumber = it.VisitNumber;
                    genderCount = 0;
                }
                else
                {
                    newThings.Add ( it );
                    genderCount++;
                }
            }
            Things add2 = new Things ( )
            {
                Roster = "Total",
                Gender = $"{saveRoster}",
                VisitNumber = null,
                Guests = things.Where ( g => g.Roster.Equals ( saveRoster ) ).Sum ( c => c.Guests ),
                BedDays = things.Where ( g => g.Roster.Equals ( saveRoster ) ).Sum ( c => c.BedDays ),
                MinDays = things.Where ( g => g.Roster.Equals ( saveRoster ) ).Min ( c => c.MinDays ),
                MaxDays = things.Where ( g => g.Roster.Equals ( saveRoster ) ).Max ( c => c.MaxDays ),
                AvgDays = 0
            };
            add2.AvgDays = ( ( double )add2.BedDays / ( double )add2.Guests );
            if (genderCount > 1)
            {
                DoVisitTotals ( );
            }
            Things tmp_Thing = newThings.Last ( );
            if (tmp_Thing == blank2)
            {
                newThings.RemoveAt ( newThings.Count - 1 );
            }
            newThings.Add ( add2 );
            newThings.Add ( tmp_Thing );
            List<Things> group_Totaled = new List<Things> ( newThings );
            Things totals = new Things ( )
            {
                Roster = "Grand Totals",
                Gender = "All Guests",
                VisitNumber = null,
                Guests = things.Sum ( s => s.Guests ),
                BedDays = things.Sum ( s => s.BedDays ),
                MinDays = things.Min ( s => s.MinDays ),
                MaxDays = things.Max ( s => s.MaxDays ),
                AvgDays = ( ( double )things.Sum ( s => s.BedDays ) / ( double )things.Sum ( s => s.Guests ) )
            };
            group_Totaled.Add ( totals );
            grpTable = new DataTable ( "Grouped" );
            grpTable.Columns.Add ( "Roster", typeof ( string ) ).AllowDBNull = false;
            grpTable.Columns.Add ( "Gender", typeof ( string ) ).AllowDBNull = false;
            grpTable.Columns.Add ( "Visits", typeof ( int ) ).AllowDBNull = true;
            grpTable.Columns.Add ( "Guests", typeof ( int ) ).AllowDBNull = true;
            grpTable.Columns.Add ( "Days", typeof ( int ) ).AllowDBNull = true;
            grpTable.Columns.Add ( "Min.", typeof ( int ) ).AllowDBNull = true;
            grpTable.Columns.Add ( "Max.", typeof ( int ) ).AllowDBNull = true;
            grpTable.Columns.Add ( "Avg.", typeof ( double ) ).AllowDBNull = true;

            foreach (Things item in group_Totaled)
            {
                if (item != blank && item != blank2)
                {
                    grpTable.Rows.Add ( item.Roster, item.Gender, item.VisitNumber, item.Guests, item.BedDays, item.MinDays, item.MaxDays, item.AvgDays );
                }
                else
                {
                    grpTable.Rows.Add ( item.Roster, item.Gender, null, null, null, null, null, null );
                }
            }
            return;
        }

        public void DoVisitTotals ( )
        {
            Things add1 = new Things ( )
            {
                Roster = string.Empty,
                Gender = $"Total {saveGender} Visits",
                VisitNumber = null,
                Guests = things.Where ( g => g.Gender.Equals ( saveGender ) ).Where ( g => g.Roster.Equals ( saveRoster ) ).Sum ( c => c.Guests ),
                BedDays = things.Where ( g => g.Gender.Equals ( saveGender ) ).Where ( g => g.Roster.Equals ( saveRoster ) ).Sum ( c => c.BedDays ),
                MinDays = things.Where ( g => g.Gender.Equals ( saveGender ) ).Where ( g => g.Roster.Equals ( saveRoster ) ).Min ( c => c.MinDays ),
                MaxDays = things.Where ( g => g.Gender.Equals ( saveGender ) ).Where ( g => g.Roster.Equals ( saveRoster ) ).Max ( c => c.MaxDays ),
                AvgDays = 0
            };
            add1.AvgDays = ( ( double )add1.BedDays / ( double )add1.Guests );
            newThings.Add ( add1 );
            if (saveRoster.Equals ( "Discharged" ))
            {
                newThings.Add ( blank2 );
            }
            genderCount = 0;
            return;
        }

        #endregion Total Visits by Roster, Gender, and VisitNumber

        #region Total Visits Stats

        public DataTable TotalVisitsStatistics ( )
        {
            Func<DateTime, DateTime, int> myMethod = CalcDays;
            DateTime to_Date = DateTime.Today;
            grpTable = new DataTable ( "Visits" );

            using (var db = new DataModel.SHGuests ( ))
            {
                var query4group = ( from v in db.Visits
                                    orderby v.Roster, v.VisitNumber
                                    select new
                                    {
                                        Roster = ( v.Roster == "D" ) ? "Discharged" : "Current",
                                        Visit = v.VisitNumber.ToString ( "N0" ),
                                        Days = ( v.Roster == "D" ) ? v.VisitDays : myMethod ( to_Date, v.AdmitDate )
                                    } ).ToList ( );

                var grouped = ( from vd in query4group
                                group vd by new
                                {
                                    vd.Roster,
                                    vd.Visit
                                }
                                into gcs
                                select new
                                {
                                    Roster = gcs.Key.Roster,
                                    Visit = gcs.Key.Visit,
                                    Guests = gcs.Count ( ),
                                    BedDays = gcs.Sum ( v => v.Days ),
                                    Minimum = gcs.Min ( vd => vd.Days ),
                                    Maximum = gcs.Max ( vd => vd.Days ),
                                    Average = gcs.Average ( vd => vd.Days )
                                } ).ToList ( );

                List<dynamic> new_lst = new List<dynamic> ( );
                int j = 0;
                foreach (var item in grouped)
                {
                    if (( item.Roster.Equals ( "Discharged" ) ) && j == 0)
                    {
                        var total_item = new
                        {
                            Roster = "Current Guest",
                            Visit = "Totals",
                            Guests = grouped.Where ( g => g.Roster.Equals ( "Current" ) ).Sum ( tg => tg.Guests ),
                            BedDays = grouped.Where ( g => g.Roster.Equals ( "Current" ) ).Sum ( td => td.BedDays ),
                            Minimum = query4group.Where ( g => g.Roster.Equals ( "Current" ) ).Min ( mn => mn.Days ),
                            Maximum = query4group.Where ( g => g.Roster.Equals ( "Current" ) ).Max ( mx => mx.Days ),
                            Average = query4group.Where ( g => g.Roster.Equals ( "Current" ) ).Average ( av => av.Days )
                        };
                        new_lst.Add ( total_item );
                        j++;
                    }
                    new_lst.Add ( item );
                }
                var totl_item = new
                {
                    Roster = "Discharged Guest",
                    Visit = "Totals",
                    Guests = grouped.Where ( g => g.Roster.Equals ( "Discharged" ) ).Sum ( tg => tg.Guests ),
                    BedDays = grouped.Where ( g => g.Roster.Equals ( "Discharged" ) ).Sum ( td => td.BedDays ),
                    Minimum = query4group.Where ( g => g.Roster.Equals ( "Discharged" ) ).Min ( mn => mn.Days ),
                    Maximum = query4group.Where ( g => g.Roster.Equals ( "Discharged" ) ).Max ( mx => mx.Days ),
                    Average = query4group.Where ( g => g.Roster.Equals ( "Discharged" ) ).Average ( av => av.Days )
                };
                new_lst.Add ( totl_item );
                var totals_item = new
                {
                    Roster = string.Empty,
                    Visit = "Grand Totals",
                    Guests = grouped.Sum ( tg => tg.Guests ),
                    BedDays = grouped.Sum ( td => td.BedDays ),
                    Minimum = query4group.Min ( mn => mn.Days ),
                    Maximum = query4group.Max ( mx => mx.Days ),
                    Average = query4group.Average ( av => av.Days )
                };
                new_lst.Add ( totals_item );
                grpTable.Columns.Add ( "Roster", typeof ( string ) ).AllowDBNull = false;
                grpTable.Columns.Add ( "Visits", typeof ( string ) ).AllowDBNull = true;
                grpTable.Columns.Add ( "Guests", typeof ( int ) ).AllowDBNull = true;
                grpTable.Columns.Add ( "Days", typeof ( int ) ).AllowDBNull = true;
                grpTable.Columns.Add ( "Min.", typeof ( int ) ).AllowDBNull = true;
                grpTable.Columns.Add ( "Max.", typeof ( int ) ).AllowDBNull = true;
                grpTable.Columns.Add ( "Avg.", typeof ( double ) ).AllowDBNull = true;
                foreach (var item in new_lst)
                {
                    grpTable.Rows.Add ( item.Roster, item.Visit, item.Guests, item.BedDays, item.Minimum, item.Maximum, item.Average );
                }
                return grpTable;
            }
        }

        #endregion Total Visits Stats

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