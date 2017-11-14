using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SHGuestsEFCore.DataModel;

namespace SHGuestsEFCore.Called_Dialogs
{
    public partial class Discharged_Guest_Display : Form
    {
        #region Constants and Variables

        public object [ ] guestkey;
        public MemoryStream ms;
        public byte [ ] error_Image;
        public bool record_found = true;
        public string rosteri;

        public string lname, fname, key_lname, db_lname, db_fname,
                      denial_reason, admit_reason, connStr, str_ssn,
                      l_name, f_name, gender, birthday, lst_date, refer_sw, refer_hosp;

        public DateTime dob, last_visit_date, key_dob, key_visit_date, admit_date, lst_vis_plus1;

        public bool can_return, deceased = false;
        public int num_of_visits = 0, guestID, ssn_in, room = 0, bed = 0, update_visits = 0;
        public Font denial_font = new Font ( "Arial", 14F, FontStyle.Italic | FontStyle.Bold, GraphicsUnit.Pixel );
        public Color deceased_color = Color.Green;
        public Color denied_color = Color.Red;
        public Color walked_color = Color.Blue;
        public Guests update_record = new Guests ( ), the_guest = new Guests ( );
        public Visits vd = new Visits ( ), foundvd = new Visits ( );
        public StringBuilder sb;

        #endregion Constants and Variables

        #region Constructor

        public Discharged_Guest_Display ( int GiD, int Visit, string header_Text )
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            guestID = GiD;
            num_of_visits = Visit;
            //
            InitializeComponent ( );
            Text = $"Record as of {header_Text}";

            string filePath = Path.GetDirectoryName ( Application.ExecutablePath );
            int ndx = filePath.LastIndexOf ( @"\bin" );
            string tmp_Path = filePath.Substring ( 0, ndx );
            filePath = tmp_Path + @"\Resources\img-not-found.png";
            error_Image = ReadFile ( filePath );
        }

        #endregion Constructor

        #region Form Loading

        private void Discharged_Guest_Display_Load ( object sender, EventArgs e )
        {
            guestkey = new object [ ]
            {
                guestID
            };
            using (var db = new DataModel.SHGuests())
            {
                the_guest = new Guests ( );
                the_guest = db.Guests.Find ( guestkey );
                the_guest = db.Guests                       // Find a specific record using Primary Key Values
                               .Include ( g => g.VisitsNavigation )
                               .Include ( g => g.Photos )
                               .Where ( g => g.GuestId == guestID)
                               .Single ( );
                build_the_display ( the_guest );
                Show ( );
            }
        }

        private void build_the_display ( Guests foundrec )
        {
            Visits vd = new Visits ( );
            foundvd = new Visits ( );
            var db = new DataModel.SHGuests ( );
            foreach (Visits vvv in foundrec.VisitsNavigation)
            {
                if (vvv.VisitNumber == num_of_visits)
                {
                    vd = vvv;
                    foundvd = db.Visits.Single ( s => s.GuestId == foundrec.GuestId && s.VisitId == vd.VisitId );
                    break;
                }
            }
            if (foundrec.Gender.ToUpper ( ) == "F")
            {
                this.BackColor = Color.LightYellow;
            }
            if (!vd.CanReturn)
            {
                this.BackColor = Color.Red;
                if (vd.DischargeReason.Contains ( "No Show" ))
                {
                    this.BackColor = Color.Blue;
                }
            }
            if (vd.Deceased)
            {
                this.BackColor = deceased_color;
            }
            textBox_GuestID.Text = foundrec.GuestId.ToString ( "N0" );
            textBox_VisitID.Text = vd.VisitId.ToString ( "N0" );
            last_name.Text = foundrec.LastName;
            first_name.Text = foundrec.FirstName;
            gender_box.Text = foundrec.Gender;
            dob_dt_picker.Value = foundrec.BirthDate;
            key_dob = foundrec.BirthDate;
            db_lname = foundrec.LastName;
            can_return_checkbox.Checked = vd.CanReturn;
            last_time_discharged.Value = vd.Discharged;
            admit_date_picker.Value = vd.AdmitDate;
            key_visit_date = vd.Discharged;
            deceased_checkbox.Checked = vd.Deceased;

            str_ssn = ( foundrec.Ssn != 0 ) ?
                       foundrec.Ssn.ToString ( "000-00-0000" ) :
                       "999-99-9999";
            ssn_id_no_box.Text = str_ssn;
            ssn_id_no_box.ReadOnly = true;
            admit_reason_box.Text = vd.AdmitReason;
            return_denial_reason.Text = vd.DischargeReason;
            last_name.ResetFont ( );
            first_name.ResetFont ( );
            return_denial_reason.ResetFont ( );
            if (vd.Deceased || !vd.CanReturn)
            {
                return_denial_reason.Font = denial_font;
                last_name.Font = denial_font;
                first_name.Font = denial_font;
            }
            if (!vd.CanReturn)
            {
                last_name.ForeColor = denied_color;
                first_name.ForeColor = denied_color;
                return_denial_reason.ForeColor = denied_color;
            }
            if (vd.Deceased)
            {
                return_denial_reason.ForeColor = deceased_color;
                last_name.ForeColor = deceased_color;
                first_name.ForeColor = deceased_color;
            }
            if (vd.DischargeReason.Contains ( "No Show" )
             || vd.DischargeReason.Contains ( "Walk Off" )
             || vd.DischargeReason.Contains ( "Walked Off" ))
            {
                last_name.ForeColor = walked_color;
                first_name.ForeColor = walked_color;
            }
            dob_dt_picker.Enabled = false;
            last_name.ReadOnly = true;
            first_name.ReadOnly = true;
            gender_box.ReadOnly = true;

            num_visits_textbox.Text = vd.VisitNumber.ToString ( );
            visit_days.Text = vd.VisitDays.ToString ( ) + " guest day(s)";
            referring_social_worker.Text = vd.Worker;
            referring_hospital.Text = vd.Agency;
            room_num_box.Text = ( vd.Room != null ) ? vd.Room.ToString ( ) : "0";
            bed_num_box.Text = ( vd.Bed != null ) ? vd.Bed.ToString ( ) : "0";
            modified_box.Text = $"{vd.EditDate.ToString ( "dd MMM yyyy @ HH:mm:sss" )}";

            if (foundrec.Photos.Count != 0)
            {
                List<Photos> pic = new List<Photos> ( );
                pic = foundrec.Photos.ToList ( );
                ms = new MemoryStream ( pic [0].Photo );
                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort ( ThumbnailCallback );
                Image guest_Image = Image.FromStream ( ms );
                Image myThumbnail = guest_Image.GetThumbnailImage ( 150, 150, myCallback, IntPtr.Zero );
                pictureBox_GuestPicture.Image = myThumbnail;
            }
            else
            {
                ms = new MemoryStream ( error_Image );
                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort ( ThumbnailCallback );
                Image guest_Image = Image.FromStream ( ms );
                Image myThumbnail = guest_Image.GetThumbnailImage ( 150, 150, myCallback, IntPtr.Zero );
                pictureBox_GuestPicture.Image = myThumbnail;
            }
            return;
        }

        #endregion Form Loading

        #region Message and Event Handlers

        private void Exit_the_screenClick ( object sender, EventArgs e )
        {
            Close ( );
        }

        private void Update_guest_buttonClick ( object sender, EventArgs e )
        {
            vd = new Visits ( );
            using (var db = new DataModel.SHGuests ( ))
            {
                update_record = new Guests ( );
                update_record = the_guest;
                foreach (Visits vvv in the_guest.VisitsNavigation)
                {
                    if (vvv.VisitNumber == num_of_visits)
                    {
                        vd = vvv;
                        break;
                    }
                }
                update_record.BirthDate = dob_dt_picker.Value;
                update_record.LastName = last_name.Text;
                vd.Discharged = last_time_discharged.Value;
                update_record.FirstName = first_name.Text;
                update_record.Gender = gender_box.Text.ToUpper ( );
                if (!db_lname.Equals ( update_record.LastName ))
                {
                    MessageBox.Show ( "Last Name MAY not be changed/update_record." );
                    this.ActiveControl = last_name;
                    last_name.Text = db_lname;
                    return;
                }
                if (!update_record.Gender.Contains ( "M" ) && !update_record.Gender.Contains ( "F" ))
                {
                    MessageBox.Show ( "Gender must be M or F. Please try again." );
                    this.ActiveControl = gender_box;
                    return;
                }
                vd.CanReturn = can_return_checkbox.Checked;
                if (!int.TryParse ( num_visits_textbox.Text, out update_visits ))
                {
                    MessageBox.Show ( "Invalid number in number of visits. Try Again" );
                    this.ActiveControl = num_visits_textbox;
                    return;
                }
                update_record.Visits = update_visits;
                vd.VisitNumber = update_visits;
                vd.DischargeReason = return_denial_reason.Text;
                vd.Deceased = deceased_checkbox.Checked;
                if (( ssn_id_no_box.Text.Length < 11 ) || ( ssn_id_no_box.Text.Contains ( "N/A" ) ))
                {
                    MessageBox.Show ( "Incorrect information in field. Please try again" );
                    this.ActiveControl = ssn_id_no_box;
                    return;
                }
                str_ssn = ( ssn_id_no_box.Text.Contains ( "-" ) ) ?
                           ssn_id_no_box.Text.Substring ( 0, 3 )
                         + ssn_id_no_box.Text.Substring ( 4, 2 )
                         + ssn_id_no_box.Text.Substring ( 7, 4 ) :
                           ssn_id_no_box.Text;

                if (!int.TryParse ( str_ssn, out ssn_in ))
                {
                    MessageBox.Show ( "Incorrect information in field. Please try again" );
                    this.ActiveControl = ssn_id_no_box;
                    return;
                }
                update_record.Ssn = ssn_in;
                vd.AdmitReason = admit_reason_box.Text;
                vd.AdmitDate = admit_date_picker.Value;
                if (string.IsNullOrWhiteSpace ( referring_social_worker.Text ))
                {
                    MessageBox.Show ( "Referring Social Worker may not be blank. Please try again" );
                    this.ActiveControl = referring_social_worker;
                    return;
                }
                vd.Worker = referring_social_worker.Text;
                if (string.IsNullOrWhiteSpace ( referring_hospital.Text ))
                {
                    MessageBox.Show ( "Referring Hospital may not be blank. Please try again" );
                    this.ActiveControl = referring_hospital;
                    return;
                }
                vd.Agency = referring_hospital.Text;
                if (room_num_box.Text != "0")
                {
                    if (int.TryParse ( room_num_box.Text, out room ))
                    {
                        vd.Room = room;
                    }
                }
                if (bed_num_box.Text != "0")
                {
                    if (int.TryParse ( bed_num_box.Text, out bed ))
                    {
                        vd.Bed = bed;
                    }
                }
                TimeSpan days = new TimeSpan ( 0, 0, 0, 0 );
                days = vd.Discharged.AddDays ( 1 ) - vd.AdmitDate;
                vd.VisitDays = days.Days;
                vd.EditDate = DateTime.Now;
                db.Entry ( update_record ).State = EntityState.Modified;
                db.Entry ( vd ).State = EntityState.Modified;
                int updated_recs = db.SaveChanges ( );
            }
            Close ( );
            return;
        }


        public bool ThumbnailCallback ( )
        {
            return false;
        }

        #region Readmit a guest

        private void Readmit_guest_buttonClick ( object sender, EventArgs e )
        {
            if (deceased_checkbox.Checked)
            {
                MessageBox.Show ( "You are trying to re-admit a person who was reported Deceased. Rejected!" );
                Close ( );
                return;
            }
            if (can_return_checkbox.Checked)
            {
                bool readmit = true;
                dob = dob_dt_picker.Value;
                l_name = last_name.Text;
                f_name = first_name.Text;
                last_visit_date = last_time_discharged.Value;
                Add_CG add_guest = new Add_CG ( readmit, the_guest.GuestId );
                Hide ( );
                add_guest.ShowDialog ( );
                Close ( );
            }
            else
            {
                MessageBox.Show ( "Not eligible for re-admission. Rejected!" );
                Close ( );
                return;
            }
        }

        #endregion Readmit a guest

        #region Delete a visit

        private void delete_guest_button_Click ( object sender, EventArgs e )
        {
            DialogResult res = new DialogResult ( );

            res = MessageBox.Show ( "This action cannot be reversed. Are you sure you want to do this?", "Delete Record", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning );
            sb = new StringBuilder ( );
            //if (res == DialogResult.OK)
            //{
            //    using (var db = new NewGuestsEntities())
            //    {
            //        Guest update_record = db.Guests.Find(guestkey);                  // Find a specific record using Primary Key Values
            //        string tmps = BuildVisitKey(update_record, update_record.Visits);
            //        object[] vkeyv = new object[] { tmps };
            //        VisitData vd = db.VisitDatas.Find(vkeyv);
            //        //*db.Guests.Remove(delete_guest);
            //        //* Only delete the guest personal data if only 1 visit
            //        if (update_record.Visits == 1)
            //        {
            //            db.Guests.Remove(update_record);
            //        }
            //        else
            //        {
            //            update_record.Visits--;
            //        }
            //        db.VisitDatas.Remove(vd);
            //        db.SaveChanges();
            //        MessageBox.Show("Record successfully deleted");
            //        Close();
            //    }
            //}
            return;
        }

        #endregion Delete a visit

        #endregion Message and Event Handlers

        #region Common Routines

        //Open file in to a filestream and read data in a byte array.
        private byte [ ] ReadFile ( string sPath )
        {
            //Initialize byte array with a null value initially.
            byte [ ] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo ( sPath );
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream ( sPath, FileMode.Open, FileAccess.Read );

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader ( fStream );

            //When you use BinaryReader, you need to supply number of bytes
            //to read from file.
            //In this case we want to read entire file.
            //So supplying total number of bytes.
            data = br.ReadBytes ( ( int )numBytes );

            return data;
        }

        private void edit_ssn_textbox ( object sender, EventArgs e )
        {
            if (ssn_id_no_box.Text.Length < 1)
            {
                MessageBox.Show ( "SSN/W7 field may not be empty. Try Again." );
                return;
            }
            if (( ssn_id_no_box.Text.Length < 9 ) || ( ssn_id_no_box.Text.Contains ( "N/A" ) ))
            {
                MessageBox.Show ( "Incorrect information in field. Please try again" );
                this.ActiveControl = ssn_id_no_box;
                return;
            }
            if (!int.TryParse ( ssn_id_no_box.Text, out ssn_in ))
            {
                MessageBox.Show ( "Incorrect information in field. Please try again" );
                this.ActiveControl = ssn_id_no_box;
                return;
            }
        }

        #endregion Common Routines
    }
}
