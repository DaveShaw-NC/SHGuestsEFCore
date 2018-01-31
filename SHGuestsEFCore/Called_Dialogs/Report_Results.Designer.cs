namespace SHGuestsEFCore.Called_Dialogs
{
    partial class Report_Results
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
            this.quit_the_query_button = new System.Windows.Forms.Button();
            this.printtheDocumentButton = new System.Windows.Forms.Button();
            this.button_toExcelFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // quit_the_query_button
            // 
            this.quit_the_query_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.quit_the_query_button.Location = new System.Drawing.Point(1247, 575);
            this.quit_the_query_button.Name = "quit_the_query_button";
            this.quit_the_query_button.Size = new System.Drawing.Size(105, 37);
            this.quit_the_query_button.TabIndex = 1;
            this.quit_the_query_button.Text = "Leave";
            this.quit_the_query_button.UseVisualStyleBackColor = true;
            this.quit_the_query_button.Click += new System.EventHandler(this.Quit_the_query_buttonClick);
            // 
            // printtheDocumentButton
            // 
            this.printtheDocumentButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.printtheDocumentButton.Location = new System.Drawing.Point(1112, 575);
            this.printtheDocumentButton.Name = "printtheDocumentButton";
            this.printtheDocumentButton.Size = new System.Drawing.Size(95, 37);
            this.printtheDocumentButton.TabIndex = 0;
            this.printtheDocumentButton.Text = "Print";
            this.printtheDocumentButton.UseVisualStyleBackColor = true;
            this.printtheDocumentButton.Click += new System.EventHandler(this.printtheDocumentButton_Click);
            // 
            // button_toExcelFile
            // 
            this.button_toExcelFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_toExcelFile.Location = new System.Drawing.Point(12, 575);
            this.button_toExcelFile.Name = "button_toExcelFile";
            this.button_toExcelFile.Size = new System.Drawing.Size(168, 37);
            this.button_toExcelFile.TabIndex = 2;
            this.button_toExcelFile.Text = "To Excel File";
            this.button_toExcelFile.UseVisualStyleBackColor = true;
            this.button_toExcelFile.Click += new System.EventHandler(this.button_toExcelFile_Click);
            // 
            // Report_Results
            // 
            this.AcceptButton = this.quit_the_query_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.quit_the_query_button;
            this.ClientSize = new System.Drawing.Size(1376, 624);
            this.Controls.Add(this.button_toExcelFile);
            this.Controls.Add(this.printtheDocumentButton);
            this.Controls.Add(this.quit_the_query_button);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Report_Results";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report_Results";
            this.Load += new System.EventHandler(this.Report_Results_Load);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Button quit_the_query_button;
        private System.Windows.Forms.Button printtheDocumentButton;
        private System.Windows.Forms.Button button_toExcelFile;
    }
}

        #endregion
 