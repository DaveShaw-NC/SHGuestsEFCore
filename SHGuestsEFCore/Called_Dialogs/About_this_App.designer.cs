/*
 * Created by SharpDevelop.
 * User: DaveShaw
 * Date: 9/13/2014
 * Time: 11:36 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SHGuestsEFCore.Called_Dialogs
{
	partial class About_this_App
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.exit_about_button = new System.Windows.Forms.Button();
			this.about_rtf_doc = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// exit_about_button
			// 
			this.exit_about_button.Location = new System.Drawing.Point(288, 608);
			this.exit_about_button.Name = "exit_about_button";
			this.exit_about_button.Size = new System.Drawing.Size(136, 40);
			this.exit_about_button.TabIndex = 0;
			this.exit_about_button.Text = "Exit";
			this.exit_about_button.UseVisualStyleBackColor = true;
			this.exit_about_button.Click += new System.EventHandler(this.Exit_about_buttonClick);
			// 
			// about_rtf_doc
			// 
			this.about_rtf_doc.Location = new System.Drawing.Point(24, 24);
			this.about_rtf_doc.Name = "about_rtf_doc";
			this.about_rtf_doc.Size = new System.Drawing.Size(624, 552);
			this.about_rtf_doc.TabIndex = 1;
			this.about_rtf_doc.Text = "";
			// 
			// About_this_app
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(681, 659);
			this.Controls.Add(this.about_rtf_doc);
			this.Controls.Add(this.exit_about_button);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About_this_app";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About";
			this.Load += new System.EventHandler(this.About_this_appLoad);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.RichTextBox about_rtf_doc;
		private System.Windows.Forms.Button exit_about_button;
	}
}
