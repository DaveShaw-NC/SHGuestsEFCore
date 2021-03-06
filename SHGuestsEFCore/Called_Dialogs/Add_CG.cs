﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SHGuestsEFCore.DataModel;

namespace SHGuestsEFCore.Called_Dialogs
{
    public partial class Add_CG : Form
    {
        #region Variables Constants

        public Boolean re_admit, can_return, deceased, has_discharge_record;

        public DateTime dob, admit_date, last_visit_date = DateTime.Today, sugg_dischg_date, added_date,
               default_start_date = new DateTime ( 1955, 10, 19, 10, 35, 0, DateTimeKind.Local );

        public string current = "C", discharged = "D", roster;
        public int num_of_visits, ssn_in, room_number, bed_number, updated_items, incoming_ID = 0;

        public string admit_reason, admit_referrer, str_ssn, lst_date, lname, fname,
                      connStr, l_name, f_name, birthday, last_visit,
                      gender, key_lname, denial_reason, refer_sw;

        private Guests updated = new Guests ( );
        public Visits vd = new Visits ( ), foundvd = new Visits ( ), vdata = new Visits ( );
        public StringBuilder sb = new StringBuilder ( );
        public MailMessage m_Message = new MailMessage ( );
        public SmtpClient smtp_Client = new SmtpClient ( );
        public StringBuilder message_body;
        public Tuple<Guests, List<Visits>> guest_tuple;
        public Random rnd = new Random ( );

        #endregion Variables Constants

        #region Constructor and Form Loading

        public Add_CG ( bool readmission, int GuestID )

        {
            re_admit = readmission;
            InitializeComponent ( );
            dob_picker.Value = default_start_date;
            if (re_admit)
            {
                incoming_ID = GuestID;
            }
        }

        private void Add_Current_Guest_Load ( object sender, EventArgs e )
        {
            if (!re_admit)
            {
                dob_picker.Value = new DateTime ( rnd.Next ( 1945, DateTime.Today.Year - 18 ), rnd.Next ( 01, 12 ), rnd.Next ( 01, 27 ) );
            }
            using (var db = new DataModel.SHGuests ( ))
            {
                if (re_admit)
                {
                    object [ ] guestkey = new object [ ]
                    {
                        incoming_ID
                    };
                    updated = new Guests ( );
                    updated = db.Guests.Find ( guestkey );
                    updated.Roster = current;
                    updated.Visits++;
                    build_the_display ( updated );
                }
            }
            return;
        }

        private void build_the_display ( Guests rec_in )
        {
            dob_picker.Value = rec_in.BirthDate;
            last_name_box.Text = rec_in.LastName;
            first_name_box.Text = rec_in.FirstName;
            gender_box.Text = rec_in.Gender;
            admit_date_picker.Value = DateTime.Today;
            visit_num_box.Text = rec_in.Visits.ToString ( );
            str_ssn = rec_in.Ssn.ToString ( "000-00-0000" );
            ssn_id_box.Text = str_ssn;
            last_name_box.ReadOnly = true;
            first_name_box.ReadOnly = true;
            gender_box.ReadOnly = true;
            dob_picker.Enabled = false;
            ssn_id_box.ReadOnly = true;
            admit_date_picker.Focus ( );
            this.ActiveControl = admit_reason_box;
            return;
        }

        #endregion Constructor and Form Loading

        #region Event(Button) Handlers

        private void Exit_buttonClick ( object sender, EventArgs e )
        {
            Close ( );
        }

        #region Validate and Add the Guest and Visit

        private void Add_guest_buttonClick ( object sender, EventArgs e )
        {
            StringBuilder sb = new StringBuilder ( );
            using (var db = new DataModel.SHGuests ( ))
            {
                object [ ] guestkey = new object [ ]
                {
                      incoming_ID
                };
                if (re_admit)
                {
                    updated = new Guests ( );
                    updated = db.Guests.Find ( guestkey );
                    updated.Roster = current;
                    updated.Visits++;
                }
                else
                {
                    updated = new Guests ( );
                    updated.Roster = current;
                }
                TimeSpan age = new TimeSpan ( 0, 0, 0 );
                DialogResult res = new DialogResult ( );
                vdata = new Visits ( );
                updated.BirthDate = dob_picker.Value;
                age = DateTime.Today - updated.BirthDate;
                int tmp_years = ( int )( ( double )age.TotalDays / 365.2524 );
                string age_verify = $"Guest age < 18 years{Environment.NewLine}Calculated age is {tmp_years:N0} years{Environment.NewLine}Should not be admitted.";
                if (tmp_years < 18)
                {
                    res = MessageBox.Show ( age_verify,
                         "Age question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question );
                    if (res == DialogResult.Cancel)
                    {
                        Close ( );
                        return;
                    }
                }
                if (String.IsNullOrWhiteSpace ( last_name_box.Text ) ||
                    String.IsNullOrWhiteSpace ( first_name_box.Text ) ||
                    last_name_box.Text.Length > 25 ||
                    first_name_box.Text.Length > 25)
                {
                    res = MessageBox.Show ( "Name fields must contain a name. Try again.",
                          "Fatal Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error );
                    //*MessageBox.Show("Name fields must contain a name. Try again.");
                    this.ActiveControl = last_name_box;
                    last_name_box.Focus ( );
                    return;
                }
                updated.LastName = last_name_box.Text;
                updated.FirstName = first_name_box.Text;
                if (String.IsNullOrWhiteSpace ( gender_box.Text ))
                {
                    res = MessageBox.Show ( "Gender MUST be Entered and be M or F",
                          "Fatal Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error );
                    this.ActiveControl = gender_box;
                    gender_box.Focus ( );
                    return;
                }
                updated.Gender = gender_box.Text.ToUpper ( );
                if (!updated.Gender.Contains ( "M" ) && !updated.Gender.Contains ( "F" ))
                {
                    MessageBox.Show ( "Gender must be M or F. Please try again." );
                    this.ActiveControl = gender_box;
                    gender_box.Focus ( );
                    return;
                }
                vdata.AdmitDate = new DateTime ( admit_date_picker.Value.Year, admit_date_picker.Value.Month, admit_date_picker.Value.Day, 0, 0, 0 );
                vdata.AdmitReason = admit_reason_box.Text;
                vdata.Agency = referrer_box.Text;
                if (String.IsNullOrWhiteSpace ( ssn_id_box.Text ) || ssn_id_box.Text.Length > 11)
                {
                    res = MessageBox.Show ( "SSN/W7 field may not be empty. Try Again.",
                          "Fatal Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error );
                    this.ActiveControl = ssn_id_box;
                    ssn_id_box.Focus ( );
                    return;
                }
                str_ssn = ssn_id_box.Text.ToUpper ( );
                if (str_ssn.Contains ( "N/A" ) || ( !ValidateSSN ( str_ssn ) ))
                {
                    MessageBox.Show ( "SSN/W7 " + str_ssn + " is invalid. SSN/W7 must be valid." );
                    this.ActiveControl = ssn_id_box;
                    ssn_id_box.Focus ( );
                    return;
                }
                str_ssn = ( ssn_id_box.Text.Contains ( "-" ) ) ?
                           ssn_id_box.Text.Substring ( 0, 3 )
                         + ssn_id_box.Text.Substring ( 4, 2 )
                         + ssn_id_box.Text.Substring ( 7, 4 ) :
                           ssn_id_box.Text;

                if (!int.TryParse ( str_ssn, out ssn_in ))
                {
                    MessageBox.Show ( "Invalid numerics in field. Please try again" );
                    this.ActiveControl = ssn_id_box;
                    ssn_id_box.Focus ( );
                    return;
                }
                updated.Ssn = ssn_in;
                if (!int.TryParse ( visit_num_box.Text, out num_of_visits ))
                {
                    MessageBox.Show ( "Invalid number in number of visits. Try Again" );
                    this.ActiveControl = visit_num_box;
                    visit_num_box.Focus ( );
                    return;
                }
                vdata.VisitNumber = num_of_visits;
                updated.Visits = num_of_visits;
                if (!int.TryParse ( room_num_box.Text, out room_number ))
                {
                    MessageBox.Show ( "Incorrect information in field. Please try again" );
                    this.ActiveControl = room_num_box;
                    room_num_box.Focus ( );
                    return;
                }
                if (room_number < 1 || room_number > 4)
                {
                    MessageBox.Show ( "Incorrect information in field. Please try again" );
                    this.ActiveControl = room_num_box;
                    room_num_box.Focus ( );
                    return;
                }
                vdata.Room = room_number;
                if (!int.TryParse ( bed_num_box.Text, out bed_number ))
                {
                    MessageBox.Show ( "Incorrect information in field. Please try again" );
                    this.ActiveControl = bed_num_box;
                    bed_num_box.Focus ( );
                    return;
                }
                if (bed_number < 1 || bed_number > 4)
                {
                    MessageBox.Show ( "Incorrect information in field. Please try again" );
                    this.ActiveControl = bed_num_box;
                    bed_num_box.Focus ( );
                    return;
                }
                vdata.Bed = bed_number;
                vdata.Discharged = vdata.AdmitDate.AddDays ( 10 );
                updated.LastVisitDate = vdata.AdmitDate.ToUniversalTime ( );

                if (vdata.Discharged.DayOfWeek == DayOfWeek.Saturday) vdata.Discharged = vdata.Discharged.AddDays ( 2 );
                if (vdata.Discharged.DayOfWeek == DayOfWeek.Sunday) vdata.Discharged = vdata.Discharged.AddDays ( 1 );

                if (String.IsNullOrWhiteSpace ( referring_social_worker.Text ) || referring_social_worker.Text.Length > 45)
                {
                    res = MessageBox.Show ( "Referring Social Worker may not be blank or > 45 characters",
                          "Fatal Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error );
                    MessageBox.Show ( "Referring Social Worker may not be blank. Please try again" );
                    this.ActiveControl = referring_social_worker;
                    referring_social_worker.Focus ( );
                    return;
                }
                Func<DateTime, DateTime, int> myMethod = CalcDays;
                vdata.VisitDays = myMethod ( DateTime.Today, vdata.AdmitDate );
                vdata.Worker = referring_social_worker.Text;
                vdata.DischargeReason = "Current Visit";
                vdata.CanReturn = true;
                vdata.Deceased = false;
                vdata.EditDate = DateTime.Now;
                vdata.Roster = updated.Roster;
                db.Entry ( updated ).State = EntityState.Added;
                //
                //Guest Record may have been updated, Visit Record is always NEW!!!
                //
                if (re_admit)
                {
                    db.Entry ( updated ).State = EntityState.Modified;
                }
                db.SaveChanges ( );
                vdata.GuestId = updated.GuestId;
                db.Entry ( vdata ).State = EntityState.Added;
                try
                {
                    updated_items = db.SaveChanges ( );
                    MessageBox.Show ( "Successfully Added" + Environment.NewLine + updated.ToString ( ) + " added to Current Guest roster",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information );
                    Close ( );
                    return;
                }
                catch (DbUpdateException dbex)
                {
                    MessageBox.Show ( "Validation exception" + dbex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
                return;
            }
        }

        #endregion Validate and Add the Guest and Visit

        #endregion Event(Button) Handlers

        #region Miscellaneous Routines

        private static bool ValidateSSN ( string ssn_string )
        {
            return new Regex ( @"^(?!\b(\d)\1+-(\d)\1+-(\d)\1+\b)(?!123-45-6789|219-09-9999|078-05-1120)(?!666|000|9\d{2})\d{3}-(?!00)\d{2}-(?!0{4})\d{4}$" ).IsMatch ( ssn_string );
        }

        public int CalcDays ( DateTime from, DateTime to )
        {
            TimeSpan ts = new TimeSpan ( 0, 0, 0 );
            ts = from.AddDays ( 1 ) - to;
            return ts.Days;
        }

        #endregion Miscellaneous Routines
    }
}