namespace SHGuestsEFCore.Called_Dialogs
{
    partial class PivotTable_Reports
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
            this.pivot_tbl_listview = new System.Windows.Forms.ListView();
            this.exit_report_Button = new System.Windows.Forms.Button();
            this.printtheViewButton = new System.Windows.Forms.Button();
            this.button_createExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pivot_tbl_listview
            // 
            this.pivot_tbl_listview.GridLines = true;
            this.pivot_tbl_listview.Location = new System.Drawing.Point(36, 34);
            this.pivot_tbl_listview.Name = "pivot_tbl_listview";
            this.pivot_tbl_listview.Size = new System.Drawing.Size(1139, 414);
            this.pivot_tbl_listview.TabIndex = 0;
            this.pivot_tbl_listview.UseCompatibleStateImageBehavior = false;
            this.pivot_tbl_listview.View = System.Windows.Forms.View.Details;
            // 
            // exit_report_Button
            // 
            this.exit_report_Button.Location = new System.Drawing.Point(1063, 509);
            this.exit_report_Button.Name = "exit_report_Button";
            this.exit_report_Button.Size = new System.Drawing.Size(113, 35);
            this.exit_report_Button.TabIndex = 1;
            this.exit_report_Button.Text = "Exit";
            this.exit_report_Button.UseVisualStyleBackColor = true;
            this.exit_report_Button.Click += new System.EventHandler(this.exit_report_Button_Click);
            // 
            // printtheViewButton
            // 
            this.printtheViewButton.Location = new System.Drawing.Point(922, 509);
            this.printtheViewButton.Name = "printtheViewButton";
            this.printtheViewButton.Size = new System.Drawing.Size(113, 35);
            this.printtheViewButton.TabIndex = 2;
            this.printtheViewButton.Text = "Print";
            this.printtheViewButton.UseVisualStyleBackColor = true;
            this.printtheViewButton.Click += new System.EventHandler(this.printtheViewButton_Click);
            // 
            // button_createExcel
            // 
            this.button_createExcel.Location = new System.Drawing.Point(36, 509);
            this.button_createExcel.Name = "button_createExcel";
            this.button_createExcel.Size = new System.Drawing.Size(113, 35);
            this.button_createExcel.TabIndex = 3;
            this.button_createExcel.Text = "Create Excel File";
            this.button_createExcel.UseVisualStyleBackColor = true;
            this.button_createExcel.Click += new System.EventHandler(this.button_createExcel_Click);
            // 
            // PivotTable_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 573);
            this.Controls.Add(this.button_createExcel);
            this.Controls.Add(this.printtheViewButton);
            this.Controls.Add(this.exit_report_Button);
            this.Controls.Add(this.pivot_tbl_listview);
            this.Name = "PivotTable_Reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQLPivotTableForm";
            this.Load += new System.EventHandler(this.PivotTableForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView pivot_tbl_listview;
        private System.Windows.Forms.Button exit_report_Button;
        private System.Windows.Forms.Button printtheViewButton;
        private System.Windows.Forms.Button button_createExcel;
    }
}
