namespace SHGuestsEFCore.Called_Dialogs
{
    partial class Add_CG
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

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.dob_picker = new System.Windows.Forms.DateTimePicker ( );
            this.last_name_box = new System.Windows.Forms.TextBox ( );
            this.first_name_box = new System.Windows.Forms.TextBox ( );
            this.admit_date_picker = new System.Windows.Forms.DateTimePicker ( );
            this.admit_reason_box = new System.Windows.Forms.TextBox ( );
            this.referrer_box = new System.Windows.Forms.TextBox ( );
            this.visit_num_box = new System.Windows.Forms.TextBox ( );
            this.add_guest_button = new System.Windows.Forms.Button ( );
            this.exit_button = new System.Windows.Forms.Button ( );
            this.label1 = new System.Windows.Forms.Label ( );
            this.label2 = new System.Windows.Forms.Label ( );
            this.label3 = new System.Windows.Forms.Label ( );
            this.label4 = new System.Windows.Forms.Label ( );
            this.label5 = new System.Windows.Forms.Label ( );
            this.label6 = new System.Windows.Forms.Label ( );
            this.label7 = new System.Windows.Forms.Label ( );
            this.label8 = new System.Windows.Forms.Label ( );
            this.gender_box = new System.Windows.Forms.TextBox ( );
            this.label9 = new System.Windows.Forms.Label ( );
            this.room_num_box = new System.Windows.Forms.TextBox ( );
            this.bed_num_box = new System.Windows.Forms.TextBox ( );
            this.label10 = new System.Windows.Forms.Label ( );
            this.label11 = new System.Windows.Forms.Label ( );
            this.referring_social_worker = new System.Windows.Forms.TextBox ( );
            this.label12 = new System.Windows.Forms.Label ( );
            this.ssn_id_box = new System.Windows.Forms.MaskedTextBox ( );
            this.SuspendLayout ( );
            // 
            // dob_picker
            // 
            this.dob_picker.CustomFormat = "MM-dd-yyyy";
            this.dob_picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dob_picker.Location = new System.Drawing.Point ( 12, 29 );
            this.dob_picker.Name = "dob_picker";
            this.dob_picker.Size = new System.Drawing.Size ( 200, 26 );
            this.dob_picker.TabIndex = 0;
            // 
            // last_name_box
            // 
            this.last_name_box.Location = new System.Drawing.Point ( 218, 29 );
            this.last_name_box.Name = "last_name_box";
            this.last_name_box.Size = new System.Drawing.Size ( 209, 26 );
            this.last_name_box.TabIndex = 1;
            // 
            // first_name_box
            // 
            this.first_name_box.Location = new System.Drawing.Point ( 433, 29 );
            this.first_name_box.Name = "first_name_box";
            this.first_name_box.Size = new System.Drawing.Size ( 192, 26 );
            this.first_name_box.TabIndex = 2;
            // 
            // admit_date_picker
            // 
            this.admit_date_picker.CustomFormat = "MM-dd-yyyy";
            this.admit_date_picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.admit_date_picker.Location = new System.Drawing.Point ( 12, 150 );
            this.admit_date_picker.Name = "admit_date_picker";
            this.admit_date_picker.Size = new System.Drawing.Size ( 200, 26 );
            this.admit_date_picker.TabIndex = 5;
            // 
            // admit_reason_box
            // 
            this.admit_reason_box.Location = new System.Drawing.Point ( 231, 150 );
            this.admit_reason_box.Name = "admit_reason_box";
            this.admit_reason_box.Size = new System.Drawing.Size ( 246, 26 );
            this.admit_reason_box.TabIndex = 6;
            // 
            // referrer_box
            // 
            this.referrer_box.Location = new System.Drawing.Point ( 15, 213 );
            this.referrer_box.Name = "referrer_box";
            this.referrer_box.Size = new System.Drawing.Size ( 192, 26 );
            this.referrer_box.TabIndex = 7;
            // 
            // visit_num_box
            // 
            this.visit_num_box.Location = new System.Drawing.Point ( 15, 264 );
            this.visit_num_box.Name = "visit_num_box";
            this.visit_num_box.Size = new System.Drawing.Size ( 31, 26 );
            this.visit_num_box.TabIndex = 9;
            // 
            // add_guest_button
            // 
            this.add_guest_button.Location = new System.Drawing.Point ( 123, 332 );
            this.add_guest_button.Name = "add_guest_button";
            this.add_guest_button.Size = new System.Drawing.Size ( 139, 35 );
            this.add_guest_button.TabIndex = 12;
            this.add_guest_button.Text = "Add this Guest";
            this.add_guest_button.UseVisualStyleBackColor = true;
            this.add_guest_button.Click += new System.EventHandler ( this.Add_guest_buttonClick );
            // 
            // exit_button
            // 
            this.exit_button.Location = new System.Drawing.Point ( 433, 332 );
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size ( 103, 35 );
            this.exit_button.TabIndex = 13;
            this.exit_button.Text = "Cancel/Exit";
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler ( this.Exit_buttonClick );
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point ( 12, 54 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 200, 23 );
            this.label1.TabIndex = 14;
            this.label1.Text = "Date of Birth";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point ( 218, 54 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size ( 209, 23 );
            this.label2.TabIndex = 15;
            this.label2.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point ( 433, 54 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size ( 192, 23 );
            this.label3.TabIndex = 16;
            this.label3.Text = "First Name";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point ( 12, 121 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size ( 169, 23 );
            this.label4.TabIndex = 18;
            this.label4.Text = "SSN/W7";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point ( 12, 175 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size ( 200, 23 );
            this.label5.TabIndex = 19;
            this.label5.Text = "Admission Date";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point ( 231, 175 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size ( 246, 23 );
            this.label6.TabIndex = 20;
            this.label6.Text = "Admission Reason";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point ( 12, 238 );
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size ( 192, 23 );
            this.label7.TabIndex = 21;
            this.label7.Text = "Referring Hospital";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point ( 12, 289 );
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size ( 100, 23 );
            this.label8.TabIndex = 23;
            this.label8.Text = "Visit Number";
            // 
            // gender_box
            // 
            this.gender_box.Location = new System.Drawing.Point ( 631, 29 );
            this.gender_box.Name = "gender_box";
            this.gender_box.Size = new System.Drawing.Size ( 44, 26 );
            this.gender_box.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point ( 631, 54 );
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size ( 65, 23 );
            this.label9.TabIndex = 17;
            this.label9.Text = "Gender";
            // 
            // room_num_box
            // 
            this.room_num_box.Location = new System.Drawing.Point ( 165, 264 );
            this.room_num_box.Name = "room_num_box";
            this.room_num_box.Size = new System.Drawing.Size ( 39, 26 );
            this.room_num_box.TabIndex = 10;
            // 
            // bed_num_box
            // 
            this.bed_num_box.Location = new System.Drawing.Point ( 299, 264 );
            this.bed_num_box.Name = "bed_num_box";
            this.bed_num_box.Size = new System.Drawing.Size ( 33, 26 );
            this.bed_num_box.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point ( 162, 289 );
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size ( 100, 23 );
            this.label10.TabIndex = 24;
            this.label10.Text = "Room Number";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point ( 299, 289 );
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size ( 100, 23 );
            this.label11.TabIndex = 25;
            this.label11.Text = "Bed Number";
            // 
            // referring_social_worker
            // 
            this.referring_social_worker.Location = new System.Drawing.Point ( 231, 213 );
            this.referring_social_worker.Name = "referring_social_worker";
            this.referring_social_worker.Size = new System.Drawing.Size ( 246, 26 );
            this.referring_social_worker.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point ( 231, 238 );
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size ( 188, 20 );
            this.label12.TabIndex = 22;
            this.label12.Text = "Referring Social Worker";
            // 
            // ssn_id_box
            // 
            this.ssn_id_box.Location = new System.Drawing.Point ( 12, 92 );
            this.ssn_id_box.Mask = "000-00-0000";
            this.ssn_id_box.Name = "ssn_id_box";
            this.ssn_id_box.Size = new System.Drawing.Size ( 192, 26 );
            this.ssn_id_box.TabIndex = 4;
            // 
            // Add_Current_Guest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 9F, 20F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 708, 395 );
            this.Controls.Add ( this.ssn_id_box );
            this.Controls.Add ( this.label12 );
            this.Controls.Add ( this.referring_social_worker );
            this.Controls.Add ( this.label11 );
            this.Controls.Add ( this.label10 );
            this.Controls.Add ( this.bed_num_box );
            this.Controls.Add ( this.room_num_box );
            this.Controls.Add ( this.label9 );
            this.Controls.Add ( this.gender_box );
            this.Controls.Add ( this.label8 );
            this.Controls.Add ( this.label7 );
            this.Controls.Add ( this.label6 );
            this.Controls.Add ( this.label5 );
            this.Controls.Add ( this.label4 );
            this.Controls.Add ( this.label3 );
            this.Controls.Add ( this.label2 );
            this.Controls.Add ( this.label1 );
            this.Controls.Add ( this.exit_button );
            this.Controls.Add ( this.add_guest_button );
            this.Controls.Add ( this.visit_num_box );
            this.Controls.Add ( this.referrer_box );
            this.Controls.Add ( this.admit_reason_box );
            this.Controls.Add ( this.admit_date_picker );
            this.Controls.Add ( this.first_name_box );
            this.Controls.Add ( this.last_name_box );
            this.Controls.Add ( this.dob_picker );
            this.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.Margin = new System.Windows.Forms.Padding ( 4 );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Add_Current_Guest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add an Active Guest";
            this.Load += new System.EventHandler ( this.Add_Current_Guest_Load );
            this.ResumeLayout ( false );
            this.PerformLayout ( );

        }
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox gender_box;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Button add_guest_button;
        private System.Windows.Forms.TextBox visit_num_box;
        private System.Windows.Forms.TextBox referrer_box;
        private System.Windows.Forms.TextBox admit_reason_box;
        private System.Windows.Forms.DateTimePicker admit_date_picker;
        private System.Windows.Forms.TextBox first_name_box;
        private System.Windows.Forms.TextBox last_name_box;
        private System.Windows.Forms.DateTimePicker dob_picker;
        private System.Windows.Forms.TextBox room_num_box;
        private System.Windows.Forms.TextBox bed_num_box;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox referring_social_worker;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MaskedTextBox ssn_id_box;
    }
}
