namespace SHGuestsEFCore.Called_Dialogs
{
    partial class Current_Guest_Display
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if (disposing && ( components != null ))
            {
                components.Dispose ( );
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.components = new System.ComponentModel.Container ( );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "Current_Guest_Display";
            this.dob_datepicker = new System.Windows.Forms.DateTimePicker ( );
            this.last_name_box = new System.Windows.Forms.TextBox ( );
            this.first_name_box = new System.Windows.Forms.TextBox ( );
            this.ssn_id_no_box = new System.Windows.Forms.TextBox ( );
            this.admit_datepicker = new System.Windows.Forms.DateTimePicker ( );
            this.admit_reason_box = new System.Windows.Forms.TextBox ( );
            this.referring_hospital_box = new System.Windows.Forms.TextBox ( );
            this.visit_num_box = new System.Windows.Forms.TextBox ( );
            this.exit_display_button = new System.Windows.Forms.Button ( );
            this.label1 = new System.Windows.Forms.Label ( );
            this.label2 = new System.Windows.Forms.Label ( );
            this.label3 = new System.Windows.Forms.Label ( );
            this.label4 = new System.Windows.Forms.Label ( );
            this.days_here_label = new System.Windows.Forms.Label ( );
            this.label5 = new System.Windows.Forms.Label ( );
            this.label6 = new System.Windows.Forms.Label ( );
            this.label7 = new System.Windows.Forms.Label ( );
            this.label8 = new System.Windows.Forms.Label ( );
            this.discharge_guest_button = new System.Windows.Forms.Button ( );
            this.update_guest_button = new System.Windows.Forms.Button ( );
            this.gender_box = new System.Windows.Forms.TextBox ( );
            this.label9 = new System.Windows.Forms.Label ( );
            this.room_num_box = new System.Windows.Forms.TextBox ( );
            this.bed_num_box = new System.Windows.Forms.TextBox ( );
            this.label10 = new System.Windows.Forms.Label ( );
            this.label11 = new System.Windows.Forms.Label ( );
            this.calc_dischg_date = new System.Windows.Forms.Label ( );
            this.days_over_stay = new System.Windows.Forms.Label ( );
            this.referring_social_worker = new System.Windows.Forms.TextBox ( );
            this.label12 = new System.Windows.Forms.Label ( );
            this.modified_box = new System.Windows.Forms.TextBox ( );
            this.label14 = new System.Windows.Forms.Label ( );
            this.guest_personals = new System.Windows.Forms.Label ( );
            this.guest_admit_info = new System.Windows.Forms.Label ( );
            this.guest_discharge_info = new System.Windows.Forms.Label ( );
            this.sugg_dischargeLabel = new System.Windows.Forms.Label ( );
            this.textBox_GiD = new System.Windows.Forms.TextBox ( );
            this.textBox_ViD = new System.Windows.Forms.TextBox ( );
            this.label_GiD = new System.Windows.Forms.Label ( );
            this.label_ViD = new System.Windows.Forms.Label ( );
            this.pictureBox_GuestPicture = new System.Windows.Forms.PictureBox ( );
            this.label_pb = new System.Windows.Forms.Label ( );
            ( ( System.ComponentModel.ISupportInitialize )( this.pictureBox_GuestPicture ) ).BeginInit ( );
            this.SuspendLayout ( );
            // 
            // dob_datepicker
            // 
            this.dob_datepicker.CustomFormat = "MM-dd-yyyy";
            this.dob_datepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dob_datepicker.Location = new System.Drawing.Point ( 197, 64 );
            this.dob_datepicker.Name = "dob_datepicker";
            this.dob_datepicker.Size = new System.Drawing.Size ( 200, 24 );
            this.dob_datepicker.TabIndex = 0;
            // 
            // last_name_box
            // 
            this.last_name_box.Location = new System.Drawing.Point ( 197, 127 );
            this.last_name_box.Name = "last_name_box";
            this.last_name_box.Size = new System.Drawing.Size ( 210, 24 );
            this.last_name_box.TabIndex = 1;
            // 
            // first_name_box
            // 
            this.first_name_box.Location = new System.Drawing.Point ( 197, 186 );
            this.first_name_box.Name = "first_name_box";
            this.first_name_box.Size = new System.Drawing.Size ( 210, 24 );
            this.first_name_box.TabIndex = 2;
            // 
            // ssn_id_no_box
            // 
            this.ssn_id_no_box.Location = new System.Drawing.Point ( 260, 242 );
            this.ssn_id_no_box.Name = "ssn_id_no_box";
            this.ssn_id_no_box.Size = new System.Drawing.Size ( 105, 24 );
            this.ssn_id_no_box.TabIndex = 4;
            // 
            // admit_datepicker
            // 
            this.admit_datepicker.CustomFormat = "MM-dd-yyyy";
            this.admit_datepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.admit_datepicker.Location = new System.Drawing.Point ( 445, 64 );
            this.admit_datepicker.Name = "admit_datepicker";
            this.admit_datepicker.Size = new System.Drawing.Size ( 200, 24 );
            this.admit_datepicker.TabIndex = 5;
            // 
            // admit_reason_box
            // 
            this.admit_reason_box.Location = new System.Drawing.Point ( 445, 127 );
            this.admit_reason_box.Multiline = true;
            this.admit_reason_box.Name = "admit_reason_box";
            this.admit_reason_box.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.admit_reason_box.Size = new System.Drawing.Size ( 259, 24 );
            this.admit_reason_box.TabIndex = 6;
            // 
            // referring_hospital_box
            // 
            this.referring_hospital_box.Location = new System.Drawing.Point ( 445, 186 );
            this.referring_hospital_box.Name = "referring_hospital_box";
            this.referring_hospital_box.Size = new System.Drawing.Size ( 259, 24 );
            this.referring_hospital_box.TabIndex = 7;
            // 
            // visit_num_box
            // 
            this.visit_num_box.Location = new System.Drawing.Point ( 450, 275 );
            this.visit_num_box.Name = "visit_num_box";
            this.visit_num_box.Size = new System.Drawing.Size ( 33, 24 );
            this.visit_num_box.TabIndex = 9;
            // 
            // exit_display_button
            // 
            this.exit_display_button.Location = new System.Drawing.Point ( 830, 370 );
            this.exit_display_button.Name = "exit_display_button";
            this.exit_display_button.Size = new System.Drawing.Size ( 172, 37 );
            this.exit_display_button.TabIndex = 14;
            this.exit_display_button.Text = "Exit";
            this.exit_display_button.UseVisualStyleBackColor = true;
            this.exit_display_button.Click += new System.EventHandler ( this.Exit_display_buttonClick );
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point ( 197, 41 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 200, 21 );
            this.label1.TabIndex = 15;
            this.label1.Text = "Date of Birth";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point ( 199, 104 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size ( 208, 21 );
            this.label2.TabIndex = 16;
            this.label2.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point ( 193, 161 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size ( 196, 21 );
            this.label3.TabIndex = 17;
            this.label3.Text = "First Name";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point ( 256, 219 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size ( 167, 21 );
            this.label4.TabIndex = 19;
            this.label4.Text = "SSN/W7";
            // 
            // days_here_label
            // 
            this.days_here_label.Location = new System.Drawing.Point ( 727, 84 );
            this.days_here_label.Name = "days_here_label";
            this.days_here_label.Size = new System.Drawing.Size ( 275, 21 );
            this.days_here_label.TabIndex = 25;
            this.days_here_label.Text = "x3";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point ( 445, 41 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size ( 200, 21 );
            this.label5.TabIndex = 20;
            this.label5.Text = "Admission Date";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point ( 445, 104 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size ( 290, 21 );
            this.label6.TabIndex = 23;
            this.label6.Text = "Admission Reason";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point ( 445, 164 );
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size ( 195, 18 );
            this.label7.TabIndex = 24;
            this.label7.Text = "Referring Hospital";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point ( 448, 301 );
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size ( 87, 21 );
            this.label8.TabIndex = 26;
            this.label8.Text = "Visit";
            // 
            // discharge_guest_button
            // 
            this.discharge_guest_button.Location = new System.Drawing.Point ( 650, 370 );
            this.discharge_guest_button.Name = "discharge_guest_button";
            this.discharge_guest_button.Size = new System.Drawing.Size ( 172, 37 );
            this.discharge_guest_button.TabIndex = 13;
            this.discharge_guest_button.Text = "Discharge This Guest";
            this.discharge_guest_button.UseVisualStyleBackColor = true;
            this.discharge_guest_button.Click += new System.EventHandler ( this.Discharge_guest_buttonClick );
            // 
            // update_guest_button
            // 
            this.update_guest_button.Location = new System.Drawing.Point ( 470, 370 );
            this.update_guest_button.Name = "update_guest_button";
            this.update_guest_button.Size = new System.Drawing.Size ( 172, 37 );
            this.update_guest_button.TabIndex = 12;
            this.update_guest_button.Text = "Change/Update Guest";
            this.update_guest_button.UseVisualStyleBackColor = true;
            this.update_guest_button.Click += new System.EventHandler ( this.Update_guest_buttonClick );
            // 
            // gender_box
            // 
            this.gender_box.Location = new System.Drawing.Point ( 194, 242 );
            this.gender_box.Name = "gender_box";
            this.gender_box.Size = new System.Drawing.Size ( 36, 24 );
            this.gender_box.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point ( 186, 220 );
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size ( 73, 21 );
            this.label9.TabIndex = 18;
            this.label9.Text = "Gender";
            // 
            // room_num_box
            // 
            this.room_num_box.Location = new System.Drawing.Point ( 541, 275 );
            this.room_num_box.Name = "room_num_box";
            this.room_num_box.Size = new System.Drawing.Size ( 31, 24 );
            this.room_num_box.TabIndex = 10;
            // 
            // bed_num_box
            // 
            this.bed_num_box.Location = new System.Drawing.Point ( 618, 275 );
            this.bed_num_box.Name = "bed_num_box";
            this.bed_num_box.Size = new System.Drawing.Size ( 30, 24 );
            this.bed_num_box.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point ( 538, 301 );
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size ( 71, 21 );
            this.label10.TabIndex = 27;
            this.label10.Text = "Room";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point ( 615, 301 );
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size ( 100, 21 );
            this.label11.TabIndex = 28;
            this.label11.Text = "Bed";
            // 
            // calc_dischg_date
            // 
            this.calc_dischg_date.Location = new System.Drawing.Point ( 727, 41 );
            this.calc_dischg_date.Name = "calc_dischg_date";
            this.calc_dischg_date.Size = new System.Drawing.Size ( 275, 21 );
            this.calc_dischg_date.TabIndex = 21;
            this.calc_dischg_date.Text = "x1";
            // 
            // days_over_stay
            // 
            this.days_over_stay.Location = new System.Drawing.Point ( 727, 105 );
            this.days_over_stay.Name = "days_over_stay";
            this.days_over_stay.Size = new System.Drawing.Size ( 275, 21 );
            this.days_over_stay.TabIndex = 22;
            this.days_over_stay.Text = "x2";
            // 
            // referring_social_worker
            // 
            this.referring_social_worker.Location = new System.Drawing.Point ( 445, 246 );
            this.referring_social_worker.Name = "referring_social_worker";
            this.referring_social_worker.Size = new System.Drawing.Size ( 259, 24 );
            this.referring_social_worker.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point ( 445, 220 );
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size ( 167, 18 );
            this.label12.TabIndex = 25;
            this.label12.Text = "Referring Social Worker";
            // 
            // modified_box
            // 
            this.modified_box.Location = new System.Drawing.Point ( 727, 246 );
            this.modified_box.Name = "modified_box";
            this.modified_box.Size = new System.Drawing.Size ( 210, 24 );
            this.modified_box.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point ( 727, 222 );
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size ( 138, 18 );
            this.label14.TabIndex = 32;
            this.label14.Text = "Record Last Editted";
            // 
            // guest_personals
            // 
            this.guest_personals.AutoSize = true;
            this.guest_personals.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.guest_personals.Location = new System.Drawing.Point ( 192, 9 );
            this.guest_personals.Name = "guest_personals";
            this.guest_personals.Size = new System.Drawing.Size ( 247, 25 );
            this.guest_personals.TabIndex = 33;
            this.guest_personals.Text = "Guest Personal Information";
            // 
            // guest_admit_info
            // 
            this.guest_admit_info.AutoSize = true;
            this.guest_admit_info.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.guest_admit_info.Location = new System.Drawing.Point ( 445, 9 );
            this.guest_admit_info.Name = "guest_admit_info";
            this.guest_admit_info.Size = new System.Drawing.Size ( 261, 25 );
            this.guest_admit_info.TabIndex = 34;
            this.guest_admit_info.Text = "Guest Admission Information";
            // 
            // guest_discharge_info
            // 
            this.guest_discharge_info.AutoSize = true;
            this.guest_discharge_info.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.guest_discharge_info.Location = new System.Drawing.Point ( 727, 9 );
            this.guest_discharge_info.Name = "guest_discharge_info";
            this.guest_discharge_info.Size = new System.Drawing.Size ( 258, 25 );
            this.guest_discharge_info.TabIndex = 35;
            this.guest_discharge_info.Text = "Guest Discharge Information";
            // 
            // sugg_dischargeLabel
            // 
            this.sugg_dischargeLabel.Location = new System.Drawing.Point ( 727, 62 );
            this.sugg_dischargeLabel.Name = "sugg_dischargeLabel";
            this.sugg_dischargeLabel.Size = new System.Drawing.Size ( 275, 21 );
            this.sugg_dischargeLabel.TabIndex = 36;
            this.sugg_dischargeLabel.Text = "x1";
            // 
            // textBox_GiD
            // 
            this.textBox_GiD.Location = new System.Drawing.Point ( 190, 275 );
            this.textBox_GiD.Name = "textBox_GiD";
            this.textBox_GiD.ReadOnly = true;
            this.textBox_GiD.Size = new System.Drawing.Size ( 78, 24 );
            this.textBox_GiD.TabIndex = 37;
            this.textBox_GiD.TabStop = false;
            this.textBox_GiD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_ViD
            // 
            this.textBox_ViD.Location = new System.Drawing.Point ( 329, 275 );
            this.textBox_ViD.Name = "textBox_ViD";
            this.textBox_ViD.ReadOnly = true;
            this.textBox_ViD.Size = new System.Drawing.Size ( 78, 24 );
            this.textBox_ViD.TabIndex = 38;
            this.textBox_ViD.TabStop = false;
            this.textBox_ViD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_GiD
            // 
            this.label_GiD.AutoSize = true;
            this.label_GiD.Location = new System.Drawing.Point ( 186, 314 );
            this.label_GiD.Name = "label_GiD";
            this.label_GiD.Size = new System.Drawing.Size ( 74, 18 );
            this.label_GiD.TabIndex = 39;
            this.label_GiD.Text = "Guest ID#";
            // 
            // label_ViD
            // 
            this.label_ViD.AutoSize = true;
            this.label_ViD.Location = new System.Drawing.Point ( 325, 314 );
            this.label_ViD.Name = "label_ViD";
            this.label_ViD.Size = new System.Drawing.Size ( 61, 18 );
            this.label_ViD.TabIndex = 40;
            this.label_ViD.Text = "Visit ID#";
            // 
            // pictureBox_GuestPicture
            // 
            this.pictureBox_GuestPicture.Location = new System.Drawing.Point ( 4, 41 );
            this.pictureBox_GuestPicture.Name = "pictureBox_GuestPicture";
            this.pictureBox_GuestPicture.Size = new System.Drawing.Size ( 155, 155 );
            this.pictureBox_GuestPicture.TabIndex = 41;
            this.pictureBox_GuestPicture.TabStop = false;
            // 
            // label_pb
            // 
            this.label_pb.AutoSize = true;
            this.label_pb.Location = new System.Drawing.Point ( 0, 244 );
            this.label_pb.Name = "label_pb";
            this.label_pb.Size = new System.Drawing.Size ( 146, 18 );
            this.label_pb.TabIndex = 42;
            this.label_pb.Text = "Guest Picture on File";
            // 
            // current_guest_display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 9F, 18F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 1028, 443 );
            this.Controls.Add ( this.label_pb );
            this.Controls.Add ( this.pictureBox_GuestPicture );
            this.Controls.Add ( this.label_ViD );
            this.Controls.Add ( this.label_GiD );
            this.Controls.Add ( this.textBox_ViD );
            this.Controls.Add ( this.textBox_GiD );
            this.Controls.Add ( this.sugg_dischargeLabel );
            this.Controls.Add ( this.guest_discharge_info );
            this.Controls.Add ( this.guest_admit_info );
            this.Controls.Add ( this.guest_personals );
            this.Controls.Add ( this.label14 );
            this.Controls.Add ( this.modified_box );
            this.Controls.Add ( this.label12 );
            this.Controls.Add ( this.referring_social_worker );
            this.Controls.Add ( this.days_over_stay );
            this.Controls.Add ( this.calc_dischg_date );
            this.Controls.Add ( this.label11 );
            this.Controls.Add ( this.label10 );
            this.Controls.Add ( this.bed_num_box );
            this.Controls.Add ( this.room_num_box );
            this.Controls.Add ( this.label9 );
            this.Controls.Add ( this.gender_box );
            this.Controls.Add ( this.update_guest_button );
            this.Controls.Add ( this.discharge_guest_button );
            this.Controls.Add ( this.label8 );
            this.Controls.Add ( this.label7 );
            this.Controls.Add ( this.label6 );
            this.Controls.Add ( this.label5 );
            this.Controls.Add ( this.days_here_label );
            this.Controls.Add ( this.label4 );
            this.Controls.Add ( this.label3 );
            this.Controls.Add ( this.label2 );
            this.Controls.Add ( this.label1 );
            this.Controls.Add ( this.exit_display_button );
            this.Controls.Add ( this.visit_num_box );
            this.Controls.Add ( this.referring_hospital_box );
            this.Controls.Add ( this.admit_reason_box );
            this.Controls.Add ( this.admit_datepicker );
            this.Controls.Add ( this.ssn_id_no_box );
            this.Controls.Add ( this.first_name_box );
            this.Controls.Add ( this.last_name_box );
            this.Controls.Add ( this.dob_datepicker );
            this.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.Margin = new System.Windows.Forms.Padding ( 4 );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "current_guest_display";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Current Guest Information";
            this.Load += new System.EventHandler ( this.Current_guest_displayLoad );
            ( ( System.ComponentModel.ISupportInitialize )( this.pictureBox_GuestPicture ) ).EndInit ( );
            this.ResumeLayout ( false );
            this.PerformLayout ( );

        }
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox gender_box;
        private System.Windows.Forms.Button update_guest_button;
        private System.Windows.Forms.Button discharge_guest_button;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label days_here_label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exit_display_button;
        private System.Windows.Forms.TextBox visit_num_box;
        private System.Windows.Forms.TextBox referring_hospital_box;
        private System.Windows.Forms.TextBox admit_reason_box;
        private System.Windows.Forms.DateTimePicker admit_datepicker;
        private System.Windows.Forms.TextBox ssn_id_no_box;
        private System.Windows.Forms.TextBox first_name_box;
        private System.Windows.Forms.TextBox last_name_box;
        private System.Windows.Forms.DateTimePicker dob_datepicker;
        private System.Windows.Forms.TextBox room_num_box;
        private System.Windows.Forms.TextBox bed_num_box;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label calc_dischg_date;
        private System.Windows.Forms.Label days_over_stay;
        private System.Windows.Forms.TextBox referring_social_worker;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox modified_box;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label guest_personals;
        private System.Windows.Forms.Label guest_admit_info;
        private System.Windows.Forms.Label guest_discharge_info;
        private System.Windows.Forms.Label sugg_dischargeLabel;
        private System.Windows.Forms.TextBox textBox_GiD;
        private System.Windows.Forms.TextBox textBox_ViD;
        private System.Windows.Forms.Label label_GiD;
        private System.Windows.Forms.Label label_ViD;
        private System.Windows.Forms.PictureBox pictureBox_GuestPicture;
        private System.Windows.Forms.Label label_pb;
    }
}

        #endregion
