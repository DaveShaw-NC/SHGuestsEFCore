using System;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SHGuestsEFCore.DataModel;

namespace SHGuestsEFCore.Called_Dialogs
{
    public partial class Discharge_Guest : Form
    {
        #region Variables and Constants

        public DateTime dob, admit_date, last_visit_date, edit_date;
        public object [ ] guestkey, vkey;
        public bool can_return, deceased = false;
        public int num_of_visits, ssn_in, in_GuestID = 0, in_VisitID = 0;

        public string lname, fname, discharge_reason, admit_reason, connStr,
                      str_ssn, l_name, f_name, gender,
                      birthday, lst_date, referring_hospital, refer_sw, refer_hosp;

        private Guests update_record = new Guests ( );
        private Visits vd = new Visits ( );

        #endregion Variables and Constants

        #region Constructor and Form Loading

        public Discharge_Guest ( int GuestID, int VisitID )
        {
            in_GuestID = GuestID;
            in_VisitID = VisitID;
            InitializeComponent ( );
        }

        private void Discharge_GuestLoad ( object sender, EventArgs e )
        {
            StringBuilder sb = new StringBuilder ( );
            using (var db = new DataModel.SHGuests ( ))
            {
                guestkey = new object [ ]
                {
                    in_GuestID
                };
                update_record = new Guests ( );
                update_record = db.Guests.Find ( guestkey );
                vkey = new object [ ] { in_VisitID };
                vd = db.Visits.Find ( vkey );
                build_the_display ( update_record, vd );
            }
            return;
        }

        private void build_the_display ( Guests rec_in, Visits vd_in )
        {
            last_name_box.Text = rec_in.LastName;
            first_name_box.Text = rec_in.FirstName;
            gender_box.Text = rec_in.Gender;
            dob_picker.Value = rec_in.BirthDate;
            admit_date_picker.Value = vd_in.AdmitDate;
            discharge_date_picker.Value = DateTime.Today;
            ssn_id_box.Text = rec_in.Ssn.ToString ( "000-00-0000" );
            admit_reason_box.Text = vd_in.AdmitReason;
            visit_num_box.Text = vd_in.VisitNumber.ToString ( );
            referring_social_worker.Text = vd_in.Worker;
            admit_from.Text = vd_in.Agency;
            return;
        }

        #endregion Constructor and Form Loading

        #region Event(Button) Handlers

        private void Discharge_buttonClick ( object sender, EventArgs e )
        {
            using (var db = new DataModel.SHGuests ( ))
            {
                object [ ] guestkey2 = new object [ ] { in_GuestID };
                update_record = db.Guests.Find ( guestkey2 );
                vkey = new object [ ] { in_VisitID };
                vd = db.Visits.Find ( vkey );

                update_record.Roster = "D";
                vd.Roster = update_record.Roster;
                vd.AdmitDate = new DateTime ( admit_date_picker.Value.Year, admit_date_picker.Value.Month, admit_date_picker.Value.Day, 0, 0, 0 );
                update_record.LastVisitDate = vd.AdmitDate;
                vd.Discharged = new DateTime ( discharge_date_picker.Value.Year, discharge_date_picker.Value.Month, discharge_date_picker.Value.Day, 0, 0, 0 );
                vd.DischargeReason = discharge_reason_box.Text;
                vd.VisitDays = ( vd.Discharged.AddDays ( 1 ) - vd.AdmitDate ).Days;
                vd.CanReturn = can_return_check.Checked;
                vd.Deceased = deceased_check.Checked;
                vd.EditDate = DateTime.Now.ToUniversalTime ( );
                db.Entry ( update_record ).State = EntityState.Modified;
                db.Entry ( vd ).State = EntityState.Modified;
                int recs_updated = db.SaveChanges ( );
                MessageBox.Show ( "Guest has been discharged" + Environment.NewLine + update_record.ToString ( ) );
            }
            Close ( );
            return;
        }

        private void Cancel_discharge_buttonClick ( object sender, EventArgs e )
        {
            Close ( );
        }

        #endregion Event(Button) Handlers
    }
}