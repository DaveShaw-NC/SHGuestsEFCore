using DataGridPrinter.DataGridPrinter;
using OfficeOpenXml;
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
using System.IO;

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
        public DateTime to_Date = DateTime.Today;
        public DataRow my_rows;
        public DataView my_view;
        public DataTableReader dtr;
        public string p_query = null, tableName = "Tables", filePath = string.Empty;
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


        #endregion Printing Area

        private void button_toExcelFile_Click ( object sender, EventArgs e )
        {
            DirectoryInfo di;
            DialogResult svfres;
            //string filePath = string.Empty;
            try
            {
                SaveFileDialog fDialog = new SaveFileDialog ( );
                fDialog.Filter = "Excel Workbook|*.xlsx|Excel (97-2003)|*.xls|Comma Separated Values|*.csv";
                fDialog.Title = "Save exported file";
                string initialdirectory = Environment.GetFolderPath ( Environment.SpecialFolder.MyDocuments ) + @"\Samaritan House SpreadSheets";
                if (Directory.Exists ( initialdirectory ))
                {
                    di = new DirectoryInfo ( initialdirectory );
                }
                else
                {
                    di = Directory.CreateDirectory ( initialdirectory );
                }
                fDialog.InitialDirectory = di.FullName;
                svfres = fDialog.ShowDialog ( );
                if (svfres != DialogResult.OK)
                {
                    return;
                }
                BuildtheExcelfile ( fDialog.FileName );
                MessageBox.Show ( $"Excel file {fDialog.FileName} successfully created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
            catch (Exception)
            {
                throw;
            }
            return;
        }
        #region Build an Excel Workbook

        public void BuildtheExcelfile ( string filePath )
        {
            FileInfo _finfo = new FileInfo ( filePath );
            using (var package = new ExcelPackage ( ))
            {
                ExcelWorksheet wksht = package.Workbook.Worksheets.Add ( $"{this.Text}" );
                BuildNoReturnsWorkSheet ( wksht );

                package.Workbook.Properties.LastModifiedBy = Environment.UserName;
                package.Workbook.Properties.Application = "SHGuests";
                package.Workbook.Properties.Company = "Samaritan House, Inc.";
                package.Workbook.Properties.Title = $"{this.Text}";
                package.SaveAs ( _finfo );
            }
            return;
        }
        private void BuildNoReturnsWorkSheet ( ExcelWorksheet wksht )
        {
            wksht.Cells ["A1"].LoadFromDataTable ( dt, true );
            wksht.Cells.Style.Font.Size = 9F;
            wksht.Cells.Style.Font.Name = "Calibri";

            using (var heading = wksht.Cells [1, 1, 1, dt.Columns.Count])
            {
                heading.Style.Font.Bold = true;
                heading.Style.Font.Size = 10F;
                heading.Style.Font.Name = "MS Sans Serif";
                heading.Style.Font.Color.SetColor ( System.Drawing.Color.IndianRed );
                heading.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.MediumGray;
                heading.Style.Fill.BackgroundColor.SetColor ( System.Drawing.Color.Gray );
                heading.Style.Border.BorderAround ( OfficeOpenXml.Style.ExcelBorderStyle.Medium );
                heading.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            IEnumerable<int> datecolumns = from DataColumn d in dt.Columns
                                           where d.DataType == typeof ( DateTime )
                                           select d.Ordinal + 1;
            int rowcount = dt.Rows.Count;
            foreach (int dc in datecolumns)
            {
                wksht.Cells [2, dc, rowcount + 1, dc].Style.Numberformat.Format = @"MM/dd/yyyy";
                wksht.Cells [2, dc, rowcount + 1, dc].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            IEnumerable<int> intcolumns = from DataColumn dc in dt.Columns
                                          where dc.DataType == typeof ( int )
                                          select dc.Ordinal + 1;
            rowcount = wksht.Dimension.End.Row;
            foreach (int dc in intcolumns)
            {
                wksht.Cells [2, dc, rowcount, dc].Style.Numberformat.Format = @"#,###";
                wksht.Cells [2, dc, rowcount, dc].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            }
            wksht.Cells [2, 1, rowcount, 1].Style.Font.Bold = true;
            IEnumerable<int> dblcolumns = from DataColumn dc in dt.Columns
                                          where dc.DataType == typeof ( double )
                                          select dc.Ordinal + 1;
            rowcount = wksht.Dimension.End.Row;
            foreach (int dc in dblcolumns)
            {
                wksht.Cells [2, dc, rowcount, dc].Style.Numberformat.Format = @"#,##0.00000";
                wksht.Cells [2, dc, rowcount, dc].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            }
            wksht.Cells.AutoFitColumns ( );
            wksht.HeaderFooter.OddHeader.CenteredText = $"&\"Calibri\"&B&14&K000000{this.Text} ";
            wksht.HeaderFooter.EvenHeader.CenteredText = $"&\"Calibri\"&B&14&K000000{this.Text} ";
            wksht.HeaderFooter.OddFooter.CenteredText = $"&\"Arial\"&B&12&KFF0000 Copyright © {DateTime.Today.Year} Samaritan House Inc. All Rights Reserved.";
            wksht.HeaderFooter.OddFooter.LeftAlignedText = $"{ExcelHeaderFooter.PageNumber} of {ExcelHeaderFooter.NumberOfPages}";
            wksht.HeaderFooter.OddFooter.RightAlignedText = $"{ExcelHeaderFooter.CurrentDate} at {ExcelHeaderFooter.CurrentTime}";
            wksht.HeaderFooter.EvenFooter.CenteredText = $"&\"Arial\"&B&12&KFF0000 Copyright © {DateTime.Today.Year} Samaritan House Inc. All Rights Reserved.";
            wksht.HeaderFooter.EvenFooter.LeftAlignedText = $"{ExcelHeaderFooter.CurrentDate} at {ExcelHeaderFooter.CurrentTime}";
            wksht.HeaderFooter.EvenFooter.RightAlignedText = $"{ExcelHeaderFooter.PageNumber} of {ExcelHeaderFooter.NumberOfPages}";
            wksht.PrinterSettings.LeftMargin = 0.25M;
            wksht.PrinterSettings.RightMargin = 0.25M;
            wksht.PrinterSettings.TopMargin = 0.75M;
            wksht.PrinterSettings.BottomMargin = 0.75M;
            wksht.PrinterSettings.HeaderMargin = 0.3M;
            wksht.PrinterSettings.FooterMargin = 0.3M;
            wksht.PrinterSettings.Orientation = eOrientation.Landscape;
            wksht.PrinterSettings.RepeatRows = new ExcelAddress ( $"1:1" );            //'{wksht.Name}'
            wksht.PrinterSettings.ShowGridLines = true;
            wksht.View.FreezePanes ( 2, dt.Columns.Count + 1 );
            return;
        }
        #endregion
        #endregion Event Handlers
    }
}
