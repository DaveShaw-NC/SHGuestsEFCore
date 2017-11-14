namespace SHGuestsEFCore.Called_Dialogs
{
    partial class Former_Guest_Add
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
            this.Text = "Former_Guest_Add";
            this.add_guest_button = new System.Windows.Forms.Button ( );
            this.dob_date_picker = new System.Windows.Forms.DateTimePicker ( );
            this.lastname_entry = new System.Windows.Forms.TextBox ( );
            this.firstname_entry = new System.Windows.Forms.TextBox ( );
            this.canreturn_checkBox = new System.Windows.Forms.CheckBox ( );
            this.numvisits_textbox = new System.Windows.Forms.TextBox ( );
            this.lastvisit_datepicker = new System.Windows.Forms.DateTimePicker ( );
            this.nonreturn_reason_text = new System.Windows.Forms.TextBox ( );
            this.label1 = new System.Windows.Forms.Label ( );
            this.label2 = new System.Windows.Forms.Label ( );
            this.label3 = new System.Windows.Forms.Label ( );
            this.label4 = new System.Windows.Forms.Label ( );
            this.label5 = new System.Windows.Forms.Label ( );
            this.label6 = new System.Windows.Forms.Label ( );
            this.deceased_checkbox = new System.Windows.Forms.CheckBox ( );
            this.admit_reason_box = new System.Windows.Forms.TextBox ( );
            this.label7 = new System.Windows.Forms.Label ( );
            this.label8 = new System.Windows.Forms.Label ( );
            this.cancel_add_button = new System.Windows.Forms.Button ( );
            this.admit_date_picker = new System.Windows.Forms.DateTimePicker ( );
            this.label9 = new System.Windows.Forms.Label ( );
            this.gender_box = new System.Windows.Forms.TextBox ( );
            this.label10 = new System.Windows.Forms.Label ( );
            this.referring_sw = new System.Windows.Forms.TextBox ( );
            this.label11 = new System.Windows.Forms.Label ( );
            this.referring_hospital = new System.Windows.Forms.TextBox ( );
            this.label12 = new System.Windows.Forms.Label ( );
            this.ssn_id_no_box = new System.Windows.Forms.MaskedTextBox ( );
            this.SuspendLayout ( );
            // 
            // add_guest_button
            // 
            this.add_guest_button.Location = new System.Drawing.Point ( 300, 346 );
            this.add_guest_button.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.add_guest_button.Name = "add_guest_button";
            this.add_guest_button.Size = new System.Drawing.Size ( 127, 29 );
            this.add_guest_button.TabIndex = 15;
            this.add_guest_button.Text = "Add This Guest";
            this.add_guest_button.UseVisualStyleBackColor = true;
            this.add_guest_button.Click += new System.EventHandler ( this.Add_guest_buttonClick );
            // 
            // dob_date_picker
            // 
            this.dob_date_picker.CustomFormat = "MM/dd/yyyy";
            this.dob_date_picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dob_date_picker.Location = new System.Drawing.Point ( 35, 29 );
            this.dob_date_picker.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.dob_date_picker.Name = "dob_date_picker";
            this.dob_date_picker.Size = new System.Drawing.Size ( 228, 24 );
            this.dob_date_picker.TabIndex = 1;
            // 
            // lastname_entry
            // 
            this.lastname_entry.Location = new System.Drawing.Point ( 35, 76 );
            this.lastname_entry.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.lastname_entry.Name = "lastname_entry";
            this.lastname_entry.Size = new System.Drawing.Size ( 228, 24 );
            this.lastname_entry.TabIndex = 3;
            // 
            // firstname_entry
            // 
            this.firstname_entry.Location = new System.Drawing.Point ( 300, 76 );
            this.firstname_entry.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.firstname_entry.Name = "firstname_entry";
            this.firstname_entry.Size = new System.Drawing.Size ( 212, 24 );
            this.firstname_entry.TabIndex = 4;
            // 
            // canreturn_checkBox
            // 
            this.canreturn_checkBox.Location = new System.Drawing.Point ( 35, 289 );
            this.canreturn_checkBox.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.canreturn_checkBox.Name = "canreturn_checkBox";
            this.canreturn_checkBox.Size = new System.Drawing.Size ( 228, 24 );
            this.canreturn_checkBox.TabIndex = 12;
            this.canreturn_checkBox.Text = "Can Return? (Check for Yes)";
            this.canreturn_checkBox.UseVisualStyleBackColor = true;
            // 
            // numvisits_textbox
            // 
            this.numvisits_textbox.Location = new System.Drawing.Point ( 35, 319 );
            this.numvisits_textbox.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.numvisits_textbox.Name = "numvisits_textbox";
            this.numvisits_textbox.Size = new System.Drawing.Size ( 30, 24 );
            this.numvisits_textbox.TabIndex = 14;
            // 
            // lastvisit_datepicker
            // 
            this.lastvisit_datepicker.CustomFormat = "MM/dd/yyyy";
            this.lastvisit_datepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.lastvisit_datepicker.Location = new System.Drawing.Point ( 300, 29 );
            this.lastvisit_datepicker.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.lastvisit_datepicker.Name = "lastvisit_datepicker";
            this.lastvisit_datepicker.Size = new System.Drawing.Size ( 228, 24 );
            this.lastvisit_datepicker.TabIndex = 2;
            // 
            // nonreturn_reason_text
            // 
            this.nonreturn_reason_text.Location = new System.Drawing.Point ( 300, 184 );
            this.nonreturn_reason_text.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.nonreturn_reason_text.Name = "nonreturn_reason_text";
            this.nonreturn_reason_text.Size = new System.Drawing.Size ( 248, 24 );
            this.nonreturn_reason_text.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point ( 35, 57 );
            this.label1.Margin = new System.Windows.Forms.Padding ( 4, 0, 4, 0 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 228, 16 );
            this.label1.TabIndex = 17;
            this.label1.Text = "Guest Date of Birth";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point ( 35, 105 );
            this.label2.Margin = new System.Windows.Forms.Padding ( 4, 0, 4, 0 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size ( 228, 22 );
            this.label2.TabIndex = 19;
            this.label2.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point ( 297, 105 );
            this.label3.Margin = new System.Windows.Forms.Padding ( 4, 0, 4, 0 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size ( 212, 22 );
            this.label3.TabIndex = 20;
            this.label3.Text = "First Name";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point ( 32, 351 );
            this.label4.Margin = new System.Windows.Forms.Padding ( 4, 0, 4, 0 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size ( 115, 22 );
            this.label4.TabIndex = 27;
            this.label4.Text = "Visits";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point ( 300, 54 );
            this.label5.Margin = new System.Windows.Forms.Padding ( 4, 0, 4, 0 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size ( 228, 22 );
            this.label5.TabIndex = 18;
            this.label5.Text = "Discharge Date";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point ( 305, 214 );
            this.label6.Margin = new System.Windows.Forms.Padding ( 4, 0, 4, 0 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size ( 299, 22 );
            this.label6.TabIndex = 27;
            this.label6.Text = "Discharge Reason";
            // 
            // deceased_checkbox
            // 
            this.deceased_checkbox.Location = new System.Drawing.Point ( 308, 289 );
            this.deceased_checkbox.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.deceased_checkbox.Name = "deceased_checkbox";
            this.deceased_checkbox.Size = new System.Drawing.Size ( 228, 24 );
            this.deceased_checkbox.TabIndex = 13;
            this.deceased_checkbox.Text = "Deceased?";
            this.deceased_checkbox.UseVisualStyleBackColor = true;
            // 
            // admit_reason_box
            // 
            this.admit_reason_box.Location = new System.Drawing.Point ( 33, 238 );
            this.admit_reason_box.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.admit_reason_box.Name = "admit_reason_box";
            this.admit_reason_box.Size = new System.Drawing.Size ( 250, 24 );
            this.admit_reason_box.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point ( 35, 214 );
            this.label7.Margin = new System.Windows.Forms.Padding ( 4, 0, 4, 0 );
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size ( 261, 22 );
            this.label7.TabIndex = 24;
            this.label7.Text = "Reason Sent to Samaritan House";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point ( 35, 159 );
            this.label8.Margin = new System.Windows.Forms.Padding ( 4, 0, 4, 0 );
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size ( 128, 22 );
            this.label8.TabIndex = 22;
            this.label8.Text = "SSN/W7 ID Number";
            // 
            // cancel_add_button
            // 
            this.cancel_add_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_add_button.Location = new System.Drawing.Point ( 480, 346 );
            this.cancel_add_button.Margin = new System.Windows.Forms.Padding ( 3, 2, 3, 2 );
            this.cancel_add_button.Name = "cancel_add_button";
            this.cancel_add_button.Size = new System.Drawing.Size ( 119, 29 );
            this.cancel_add_button.TabIndex = 16;
            this.cancel_add_button.Text = "Cancel Add";
            this.cancel_add_button.UseVisualStyleBackColor = true;
            this.cancel_add_button.Click += new System.EventHandler ( this.Button1Click );
            // 
            // admit_date_picker
            // 
            this.admit_date_picker.CustomFormat = "MM/dd/yyyy";
            this.admit_date_picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.admit_date_picker.Location = new System.Drawing.Point ( 300, 130 );
            this.admit_date_picker.Margin = new System.Windows.Forms.Padding ( 3, 2, 3, 2 );
            this.admit_date_picker.Name = "admit_date_picker";
            this.admit_date_picker.Size = new System.Drawing.Size ( 228, 24 );
            this.admit_date_picker.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point ( 297, 161 );
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size ( 228, 20 );
            this.label9.TabIndex = 23;
            this.label9.Text = "Admission Date";
            // 
            // gender_box
            // 
            this.gender_box.Location = new System.Drawing.Point ( 560, 76 );
            this.gender_box.Margin = new System.Windows.Forms.Padding ( 3, 2, 3, 2 );
            this.gender_box.Name = "gender_box";
            this.gender_box.Size = new System.Drawing.Size ( 40, 24 );
            this.gender_box.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point ( 557, 107 );
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size ( 71, 20 );
            this.label10.TabIndex = 21;
            this.label10.Text = "Gender";
            // 
            // referring_sw
            // 
            this.referring_sw.Location = new System.Drawing.Point ( 300, 238 );
            this.referring_sw.Margin = new System.Windows.Forms.Padding ( 3, 2, 3, 2 );
            this.referring_sw.Name = "referring_sw";
            this.referring_sw.Size = new System.Drawing.Size ( 250, 24 );
            this.referring_sw.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point ( 305, 267 );
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size ( 167, 18 );
            this.label11.TabIndex = 25;
            this.label11.Text = "Referring Social Worker";
            // 
            // referring_hospital
            // 
            this.referring_hospital.Location = new System.Drawing.Point ( 35, 183 );
            this.referring_hospital.Margin = new System.Windows.Forms.Padding ( 3, 2, 3, 2 );
            this.referring_hospital.Name = "referring_hospital";
            this.referring_hospital.Size = new System.Drawing.Size ( 248, 24 );
            this.referring_hospital.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point ( 35, 267 );
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size ( 126, 18 );
            this.label12.TabIndex = 26;
            this.label12.Text = "Referring Hospital";
            // 
            // ssn_id_no_box
            // 
            this.ssn_id_no_box.Location = new System.Drawing.Point ( 35, 130 );
            this.ssn_id_no_box.Margin = new System.Windows.Forms.Padding ( 3, 2, 3, 2 );
            this.ssn_id_no_box.Mask = "000-00-0000";
            this.ssn_id_no_box.Name = "ssn_id_no_box";
            this.ssn_id_no_box.Size = new System.Drawing.Size ( 168, 24 );
            this.ssn_id_no_box.TabIndex = 6;
            // 
            // SHGuestAdd
            // 
            this.AcceptButton = this.add_guest_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 9F, 18F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancel_add_button;
            this.ClientSize = new System.Drawing.Size ( 656, 387 );
            this.Controls.Add ( this.ssn_id_no_box );
            this.Controls.Add ( this.label12 );
            this.Controls.Add ( this.referring_hospital );
            this.Controls.Add ( this.label11 );
            this.Controls.Add ( this.referring_sw );
            this.Controls.Add ( this.label10 );
            this.Controls.Add ( this.gender_box );
            this.Controls.Add ( this.label9 );
            this.Controls.Add ( this.admit_date_picker );
            this.Controls.Add ( this.cancel_add_button );
            this.Controls.Add ( this.label8 );
            this.Controls.Add ( this.label7 );
            this.Controls.Add ( this.admit_reason_box );
            this.Controls.Add ( this.deceased_checkbox );
            this.Controls.Add ( this.label6 );
            this.Controls.Add ( this.label5 );
            this.Controls.Add ( this.label4 );
            this.Controls.Add ( this.label3 );
            this.Controls.Add ( this.label2 );
            this.Controls.Add ( this.label1 );
            this.Controls.Add ( this.nonreturn_reason_text );
            this.Controls.Add ( this.lastvisit_datepicker );
            this.Controls.Add ( this.numvisits_textbox );
            this.Controls.Add ( this.canreturn_checkBox );
            this.Controls.Add ( this.firstname_entry );
            this.Controls.Add ( this.lastname_entry );
            this.Controls.Add ( this.dob_date_picker );
            this.Controls.Add ( this.add_guest_button );
            this.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            this.Margin = new System.Windows.Forms.Padding ( 4, 3, 4, 3 );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SHGuestAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SHGuestAdd";
            this.ResumeLayout ( false );
            this.PerformLayout ( );

        }
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox gender_box;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker admit_date_picker;
        private System.Windows.Forms.Button cancel_add_button;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox admit_reason_box;
        private System.Windows.Forms.CheckBox deceased_checkbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nonreturn_reason_text;
        private System.Windows.Forms.DateTimePicker lastvisit_datepicker;
        private System.Windows.Forms.TextBox numvisits_textbox;
        private System.Windows.Forms.CheckBox canreturn_checkBox;
        private System.Windows.Forms.TextBox firstname_entry;
        private System.Windows.Forms.TextBox lastname_entry;
        private System.Windows.Forms.DateTimePicker dob_date_picker;
        private System.Windows.Forms.Button add_guest_button;
        private System.Windows.Forms.TextBox referring_sw;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox referring_hospital;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MaskedTextBox ssn_id_no_box;
    }
#endregion
}
