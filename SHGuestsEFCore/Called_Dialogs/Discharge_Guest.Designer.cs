namespace SHGuestsEFCore.Called_Dialogs
{
    partial class Discharge_Guest
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
            this.Text = "Discharge_Guest";
            this.dob_picker = new System.Windows.Forms.DateTimePicker ( );
            this.admit_date_picker = new System.Windows.Forms.DateTimePicker ( );
            this.discharge_date_picker = new System.Windows.Forms.DateTimePicker ( );
            this.last_name_box = new System.Windows.Forms.TextBox ( );
            this.first_name_box = new System.Windows.Forms.TextBox ( );
            this.ssn_id_box = new System.Windows.Forms.TextBox ( );
            this.admit_reason_box = new System.Windows.Forms.TextBox ( );
            this.discharge_reason_box = new System.Windows.Forms.TextBox ( );
            this.can_return_check = new System.Windows.Forms.CheckBox ( );
            this.deceased_check = new System.Windows.Forms.CheckBox ( );
            this.visit_num_box = new System.Windows.Forms.TextBox ( );
            this.discharge_button = new System.Windows.Forms.Button ( );
            this.cancel_discharge_button = new System.Windows.Forms.Button ( );
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
            this.label10 = new System.Windows.Forms.Label ( );
            this.referring_social_worker = new System.Windows.Forms.TextBox ( );
            this.label11 = new System.Windows.Forms.Label ( );
            this.admit_from = new System.Windows.Forms.TextBox ( );
            this.label12 = new System.Windows.Forms.Label ( );
            this.SuspendLayout ( );
            // 
            // dob_picker
            // 
            this.dob_picker.CustomFormat = "MM-dd-yyyy";
            this.dob_picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dob_picker.Location = new System.Drawing.Point ( 16, 15 );
            this.dob_picker.Margin = new System.Windows.Forms.Padding ( 4 );
            this.dob_picker.Name = "dob_picker";
            this.dob_picker.Size = new System.Drawing.Size ( 183, 22 );
            this.dob_picker.TabIndex = 0;
            // 
            // admit_date_picker
            // 
            this.admit_date_picker.CustomFormat = "MM-dd-yyyy";
            this.admit_date_picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.admit_date_picker.Location = new System.Drawing.Point ( 217, 15 );
            this.admit_date_picker.Name = "admit_date_picker";
            this.admit_date_picker.Size = new System.Drawing.Size ( 200, 22 );
            this.admit_date_picker.TabIndex = 1;
            // 
            // discharge_date_picker
            // 
            this.discharge_date_picker.CustomFormat = "MM-dd-yyyy";
            this.discharge_date_picker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.discharge_date_picker.Location = new System.Drawing.Point ( 444, 15 );
            this.discharge_date_picker.Name = "discharge_date_picker";
            this.discharge_date_picker.Size = new System.Drawing.Size ( 200, 22 );
            this.discharge_date_picker.TabIndex = 2;
            // 
            // last_name_box
            // 
            this.last_name_box.Location = new System.Drawing.Point ( 16, 74 );
            this.last_name_box.Name = "last_name_box";
            this.last_name_box.Size = new System.Drawing.Size ( 246, 22 );
            this.last_name_box.TabIndex = 3;
            // 
            // first_name_box
            // 
            this.first_name_box.Location = new System.Drawing.Point ( 303, 74 );
            this.first_name_box.Name = "first_name_box";
            this.first_name_box.Size = new System.Drawing.Size ( 210, 22 );
            this.first_name_box.TabIndex = 4;
            // 
            // ssn_id_box
            // 
            this.ssn_id_box.Location = new System.Drawing.Point ( 16, 125 );
            this.ssn_id_box.Name = "ssn_id_box";
            this.ssn_id_box.Size = new System.Drawing.Size ( 159, 22 );
            this.ssn_id_box.TabIndex = 6;
            // 
            // admit_reason_box
            // 
            this.admit_reason_box.Location = new System.Drawing.Point ( 16, 176 );
            this.admit_reason_box.Name = "admit_reason_box";
            this.admit_reason_box.Size = new System.Drawing.Size ( 368, 22 );
            this.admit_reason_box.TabIndex = 8;
            // 
            // discharge_reason_box
            // 
            this.discharge_reason_box.Location = new System.Drawing.Point ( 16, 227 );
            this.discharge_reason_box.Name = "discharge_reason_box";
            this.discharge_reason_box.Size = new System.Drawing.Size ( 368, 22 );
            this.discharge_reason_box.TabIndex = 10;
            // 
            // can_return_check
            // 
            this.can_return_check.Location = new System.Drawing.Point ( 12, 278 );
            this.can_return_check.Name = "can_return_check";
            this.can_return_check.Size = new System.Drawing.Size ( 183, 24 );
            this.can_return_check.TabIndex = 11;
            this.can_return_check.Text = "Eligible for Return?";
            this.can_return_check.UseVisualStyleBackColor = true;
            // 
            // deceased_check
            // 
            this.deceased_check.Location = new System.Drawing.Point ( 236, 278 );
            this.deceased_check.Name = "deceased_check";
            this.deceased_check.Size = new System.Drawing.Size ( 148, 24 );
            this.deceased_check.TabIndex = 12;
            this.deceased_check.Text = "Deceased?";
            this.deceased_check.UseVisualStyleBackColor = true;
            // 
            // visit_num_box
            // 
            this.visit_num_box.Location = new System.Drawing.Point ( 461, 278 );
            this.visit_num_box.Name = "visit_num_box";
            this.visit_num_box.Size = new System.Drawing.Size ( 26, 22 );
            this.visit_num_box.TabIndex = 13;
            // 
            // discharge_button
            // 
            this.discharge_button.Location = new System.Drawing.Point ( 236, 324 );
            this.discharge_button.Name = "discharge_button";
            this.discharge_button.Size = new System.Drawing.Size ( 114, 34 );
            this.discharge_button.TabIndex = 14;
            this.discharge_button.Text = "Do Discharge";
            this.discharge_button.UseVisualStyleBackColor = true;
            this.discharge_button.Click += new System.EventHandler ( this.Discharge_buttonClick );
            // 
            // cancel_discharge_button
            // 
            this.cancel_discharge_button.Location = new System.Drawing.Point ( 461, 324 );
            this.cancel_discharge_button.Name = "cancel_discharge_button";
            this.cancel_discharge_button.Size = new System.Drawing.Size ( 121, 34 );
            this.cancel_discharge_button.TabIndex = 15;
            this.cancel_discharge_button.Text = "Cancel";
            this.cancel_discharge_button.UseVisualStyleBackColor = true;
            this.cancel_discharge_button.Click += new System.EventHandler ( this.Cancel_discharge_buttonClick );
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point ( 16, 41 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 183, 23 );
            this.label1.TabIndex = 16;
            this.label1.Text = "Date of Birth";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point ( 217, 40 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size ( 200, 23 );
            this.label2.TabIndex = 17;
            this.label2.Text = "Admission Date";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point ( 444, 41 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size ( 200, 23 );
            this.label3.TabIndex = 18;
            this.label3.Text = "Discharge Date";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point ( 16, 99 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size ( 100, 23 );
            this.label4.TabIndex = 19;
            this.label4.Text = "Last Name";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point ( 303, 99 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size ( 100, 23 );
            this.label5.TabIndex = 20;
            this.label5.Text = "First Name";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point ( 16, 150 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size ( 100, 23 );
            this.label6.TabIndex = 22;
            this.label6.Text = "SSN/W7";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point ( 16, 201 );
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size ( 368, 23 );
            this.label7.TabIndex = 24;
            this.label7.Text = "Admission Reason";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point ( 16, 252 );
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size ( 179, 23 );
            this.label8.TabIndex = 26;
            this.label8.Text = "Discharge Reason";
            // 
            // gender_box
            // 
            this.gender_box.Location = new System.Drawing.Point ( 540, 74 );
            this.gender_box.Name = "gender_box";
            this.gender_box.Size = new System.Drawing.Size ( 23, 22 );
            this.gender_box.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point ( 540, 99 );
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size ( 100, 23 );
            this.label9.TabIndex = 21;
            this.label9.Text = "Gender";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point ( 493, 281 );
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size ( 100, 23 );
            this.label10.TabIndex = 27;
            this.label10.Text = "Visit Number";
            // 
            // referring_social_worker
            // 
            this.referring_social_worker.Location = new System.Drawing.Point ( 390, 176 );
            this.referring_social_worker.Name = "referring_social_worker";
            this.referring_social_worker.Size = new System.Drawing.Size ( 254, 22 );
            this.referring_social_worker.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point ( 390, 208 );
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size ( 151, 16 );
            this.label11.TabIndex = 25;
            this.label11.Text = "Referring Social Worker";
            // 
            // admit_from
            // 
            this.admit_from.Location = new System.Drawing.Point ( 390, 125 );
            this.admit_from.Name = "admit_from";
            this.admit_from.Size = new System.Drawing.Size ( 254, 22 );
            this.admit_from.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point ( 387, 150 );
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size ( 116, 16 );
            this.label12.TabIndex = 23;
            this.label12.Text = "Referring Hospital";
            // 
            // discharge_guest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 8F, 16F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 689, 370 );
            this.Controls.Add ( this.label12 );
            this.Controls.Add ( this.admit_from );
            this.Controls.Add ( this.label11 );
            this.Controls.Add ( this.referring_social_worker );
            this.Controls.Add ( this.label10 );
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
            this.Controls.Add ( this.cancel_discharge_button );
            this.Controls.Add ( this.discharge_button );
            this.Controls.Add ( this.visit_num_box );
            this.Controls.Add ( this.deceased_check );
            this.Controls.Add ( this.can_return_check );
            this.Controls.Add ( this.discharge_reason_box );
            this.Controls.Add ( this.admit_reason_box );
            this.Controls.Add ( this.ssn_id_box );
            this.Controls.Add ( this.first_name_box );
            this.Controls.Add ( this.last_name_box );
            this.Controls.Add ( this.discharge_date_picker );
            this.Controls.Add ( this.admit_date_picker );
            this.Controls.Add ( this.dob_picker );
            this.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding ( 4 );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "discharge_guest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guest Discharge Update";
            this.Load += new System.EventHandler ( this.Discharge_GuestLoad );
            this.ResumeLayout ( false );
            this.PerformLayout ( );

        }
        private System.Windows.Forms.Label label10;
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
        private System.Windows.Forms.Button cancel_discharge_button;
        private System.Windows.Forms.Button discharge_button;
        private System.Windows.Forms.TextBox visit_num_box;
        private System.Windows.Forms.CheckBox deceased_check;
        private System.Windows.Forms.CheckBox can_return_check;
        private System.Windows.Forms.TextBox discharge_reason_box;
        private System.Windows.Forms.TextBox admit_reason_box;
        private System.Windows.Forms.TextBox ssn_id_box;
        private System.Windows.Forms.TextBox first_name_box;
        private System.Windows.Forms.TextBox last_name_box;
        private System.Windows.Forms.DateTimePicker discharge_date_picker;
        private System.Windows.Forms.DateTimePicker admit_date_picker;
        private System.Windows.Forms.DateTimePicker dob_picker;
        private System.Windows.Forms.TextBox referring_social_worker;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox admit_from;
        private System.Windows.Forms.Label label12;

        #endregion
    }
}