using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SHGuestsEFCore.Called_Dialogs
{
	/// <summary>
	/// Description of About_this_app.
	/// </summary>
	public partial class About_this_App : Form
	{
		public About_this_App()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.about_rtf_doc.LinkClicked += new LinkClickedEventHandler(About_this_app_LinkClicked);
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Exit_about_buttonClick(object sender, EventArgs e)
		{
			about_rtf_doc.Clear();
			Close();
		}
		
		void About_this_appLoad(object sender, EventArgs e)
		{
			about_rtf_doc.LoadFile("SHGuests.rtf", RichTextBoxStreamType.RichText);
		}
		private void About_this_app_LinkClicked(Object sender, LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}
	}
}
