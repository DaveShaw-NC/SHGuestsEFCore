﻿using DataGridPrinter.DataGridPrinter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonRoutines;
using SHGuestsEFCore.Reporting_Modules;

namespace SHGuestsEFCore.Called_Dialogs
{
    public partial class Report_Results : Form
    {
        #region Variables and Constants

        public Commons cr = new Commons ( );
        public DGVParms dgvp = new DGVParms ( );
        public int NumberofRows { get; set; }
        public bool stat_rpt;
        public List<dynamic> list_to_view = new List<dynamic> ( );
        public DataGridView results_view1;
        public DataTable dt, view_table = new DataTable ( ), disp_tab2 = new DataTable ( );
        public DataSet dts;
        public DataRow my_rows;
        public DataView my_view;
        public DataTableReader dtr;
        public string p_query = null, tableName = "Tables";
        public string [ ] cols_array;
        public int num_rows_returned = 0, num_cols_returned = 0;

        public Font hdr_font = new Font ( "Times New Roman", 16F, FontStyle.Bold, GraphicsUnit.Pixel ),
               cell_font = new Font ( "TImes New Roman", 14F, FontStyle.Regular, GraphicsUnit.Pixel );

        private GridPrinter gp;

        #endregion Variables and Constants

        #region Entry Points 1 List, 1 DataTable

        public Report_Results ( DataTable dti, bool stat_rpt_in )
        {
            // // The InitializeComponent() call is required for Windows Forms designer support.
            stat_rpt = stat_rpt_in;
            NumberofRows = dti.Rows.Count;
            dt = dti;
            InitializeComponent ( );
            results_view1 = new DataGridView ( );
            results_view1.Location = new Point ( 10, 5 );
            Size this_size = new Size ( );
            this_size = this.Size;
            //*
            //* We must make sure the DataGridView fits into the Main Form
            //*
            results_view1.Size = new Size ( this_size.Width - 40, this_size.Height - 150 );
            results_view1.MaximumSize = new Size ( this_size.Width - 40, this_size.Height - 150 );
            stat_rpt = stat_rpt_in;
            results_view1.DataSource = dt;
            this.Controls.Add ( results_view1 );
            Layout_GridView ( results_view1 );
            cr.Format_the_View ( results_view1 );
            //results_view1.DataSource = dt;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            quit_the_query_button.ForeColor = Color.Red;
            return;
        }

        public Report_Results ( List<dynamic> dts, bool stat_rpt_in )
        {
            // // The InitializeComponent() call is required for Windows Forms designer support.
            stat_rpt = stat_rpt_in;
            NumberofRows = dts.Count;
            dt = Extensions.AsDataTable ( dts );
            list_to_view = dts;
            InitializeComponent ( );
            results_view1 = new DataGridView ( );
            results_view1.Location = new Point ( 10, 5 );
            Size this_size = new Size ( );
            this_size = this.Size;
            //*
            //* We must make sure the DataGridView fits into the Main Form
            //*
            results_view1.Size = new Size ( this_size.Width - 40, this_size.Height - 150 );
            results_view1.MaximumSize = new Size ( this_size.Width - 40, this_size.Height - 150 );
            stat_rpt = stat_rpt_in;
            results_view1.DataSource = dt;
            this.Controls.Add ( results_view1 );
            Layout_GridView ( results_view1 );
            cr.Format_the_View ( results_view1 );

            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            quit_the_query_button.ForeColor = Color.Red;
            return;
        }

        #endregion Entry Points 1 List, 1 DataTable

        #region Main Form Load

        private void Report_Results_Load ( object sender, EventArgs e )
        {
            NumberofRows = results_view1.RowCount;
            if (NumberofRows == 0)
            {
                MessageBox.Show ( "Nothing to display." );
                Close ( );
                return;
            }
            results_view1.Select ( );
            return;
        }

        #endregion Main Form Load

        #region Grid View Layout

        private void Layout_GridView ( DataGridView dgv )
        {
            dgvp.font_family = "MS Sans Serif";
            dgvp.hdr_font = new Font ( dgvp.font_family, 10F, FontStyle.Bold, GraphicsUnit.Point );
            dgvp.cell_font = new Font ( dgvp.font_family, 9F, FontStyle.Regular, GraphicsUnit.Point );
            dgvp.backg_color = SystemColors.ControlDark;
            dgvp.hdr_back_color = SystemColors.Window;
            dgvp.hdr_fore_color = Color.Black;
            dgvp.hdr_alignment = DataGridViewContentAlignment.BottomCenter;
            dgvp.alt_back_color = Color.LightBlue;
            dgvp.alt_fore_color = Color.Black;
            dgvp.cell_back_color = Color.White;
            dgvp.cell_fore_color = Color.Black;
            dgvp.which_scroll = ScrollBars.Both;
            dgvp.integer_fmt = "#,##0";
            dgvp.stat_rpt = stat_rpt;
            cr.Layout_GridView ( dgv, dgvp );
            return;
        }

        #endregion Grid View Layout

        #region Event Handlers
         
        #region Quit the Display

        private void Quit_the_query_buttonClick ( object sender, EventArgs e )
        {
            Close ( );
        }

        #endregion Quit the Display

        #region Printing Area

        private void printtheDocumentButton_Click ( object sender, EventArgs e )
        {
            if (gp == null)
            {
                gp = new GridPrinter ( results_view1 );
            }
            gp.HeaderText = this.Text;
            gp.HeaderHeightPercent = 5;
            gp.FooterHeightPercent = 5;
            gp.InterSectionSpacingPercent = 1;
            PrintDocument pd = new PrintDocument ( );
            pd.DefaultPageSettings.Landscape = true;
            pd.DefaultPageSettings.Margins = new Margins ( 2, 2, 2, 2 );
            pd.OriginAtMargins = true;
            PrintPreviewDialog ppDialog = new PrintPreviewDialog ( );
            gp.PrintDocument.PrinterSettings.DefaultPageSettings.Margins = new Margins ( 5, 5, 5, 5 );
            gp.PrintDocument.DefaultPageSettings.Landscape = true;
            gp.HeaderFont = new Font ( "MS Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point );
            gp.FooterFont = new Font ( "MS Sans Serif", 9F, FontStyle.Italic, GraphicsUnit.Point );
            Font printFont = new Font ( "Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point );
            gp.PrintFont = new Font ( "Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point );
            ppDialog.Document = gp.PrintDocument;
            ppDialog.AutoScaleMode = AutoScaleMode.Font;
            ppDialog.UseAntiAlias = true;
            DialogResult res = ppDialog.ShowDialog ( );
            if (res == DialogResult.OK)
            {
                gp.Print ( );
            }
            return;
        }
        //private void printtheDocumentButton_Click ( object sender, EventArgs e )
        //{
        //    DGVPrinter printer = new DGVPrinter ( );
        //    printer.Title = this.Text;
        //    printer.PageNumbers = true;
        //    printer.PageNumberInHeader = false;
        //    printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
        //    printer.TitleFont = new Font ( "MS Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point );
        //    printer.TitleColor = Color.Blue;
        //    printer.TitleBorder = new Pen ( Color.Black, 1.5F );
        //    printer.TitleSpacing = 50F;
        //    printer.SubTitle = string.Empty;
        //    printer.SubTitleSpacing = 1.0F;
        //    printer.FooterSpacing = 1.0F;
        //    printer.FooterFont = printer.TitleFont;
        //    printer.Footer = $"Copyright {DateTime.Today.Year.ToString ( )}, Samaritan House, Inc.";
        //    printer.FooterSpacing = 50F;
        //    printer.FooterAlignment = StringAlignment.Center;
        //    printer.printDocument.DefaultPageSettings.Landscape = ( stat_rpt ) ? false : true;
        //    //printer.printDocument.OriginAtMargins = true;
        //    printer.KeepRowsTogether = true;
        //    printer.KeepRowsTogetherTolerance = 50;
        //    printer.PrintPreviewZoom = 0.75F;
        //    printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
        //    printer.HeaderCellAlignment = StringAlignment.Near;
        //    printer.printDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins ( 10, 10, 20, 20 );
        //    printer.TableAlignment = DGVPrinter.Alignment.NotSet;

        //    if (DialogResult.OK == printer.DisplayPrintDialog ( ))
        //    {
        //        printer.PrintPreviewNoDisplay ( results_view1 );
        //    }
        //}

    }

    #endregion Printing Area

    #endregion Event Handlers
}
