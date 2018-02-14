using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using OfficeOpenXml;

namespace SHGuestsEFCore.Called_Dialogs
{
    public partial class PivotTable_Reports : Form
    {
        #region Public Variables

        public Boolean referring_switch = false;

        public int total_guests = 0, agency_row_total = 0, columns_in_listview = 0,
                   detail_rows = 0, total_detail_rows = 0, iLoop = 0;

        public DataTableReader in_reader, temp_rdr;
        public DataTable in_table = new DataTable ( ), tmp_table = new DataTable ( );
        public ListViewItem lv_item;
        public ListView my_ListView = new ListView ( );
        public Size my_ListSize = new Size ( );
        public Point my_ListLocation = new Point ( );
        public object [ ] row_values;
        public string [ ] list_items;
        public int [ ] agency_totals, column_totals, agency_row_totals;
        public string sv_agency = null;


        public SHGuestsEFCore.SHGuests.pivot_rpt_type report_type;

        //*public PrintableListView.PrintableListView pivot_tbl_listview ;
        private ListViewPrinterBase lvp;

        #endregion Public Variables

        #region Constructor

        public PivotTable_Reports ( DataTable table_in, DataTableReader reader_in )
        {
            //************************************************************************************
            //* Set up a local copy of the DataTable and DataTableReader for access and paranoia *
            //************************************************************************************
            in_reader = reader_in;
            tmp_table = table_in;
            InitializeComponent ( );
        }

        #endregion Constructor

        #region Form Loading

        private void PivotTableForm_Load ( object sender, EventArgs e )
        {
            //**************************************************************************************
            //* If this is a monthly report, we must eliminate the sorting column of month numbers *
            //* for report clarity.                                                                *
            //* Initialize all the arrays to their correct sizes                                   *
            //**************************************************************************************
            if (report_type == SHGuestsEFCore.SHGuests.pivot_rpt_type.MonthlyReport)
            {
                tmp_table.Columns.RemoveAt ( 0 );
                in_table = tmp_table;
            }
            else
            {
                in_table = tmp_table;
            }
            //********************************************************************************************
            //* We create a new DataTableReader here because we may have fiddled with the table columns. *
            //********************************************************************************************
            in_reader = in_table.CreateDataReader ( );
            list_items = new string [in_table.Columns.Count + 1];
            column_totals = new int [in_table.Columns.Count + 1];
            agency_totals = new int [in_table.Columns.Count + 1];
            agency_row_totals = new int [in_table.Columns.Count + 1];
            row_values = new object [in_table.Columns.Count];
            this.Font = new Font ( "Lucida Sans Unicode", 8, FontStyle.Regular, GraphicsUnit.Point );
            this.Controls.Remove ( pivot_tbl_listview );
            Size this_size = new Size ( );
            this_size = this.Size;
            pivot_tbl_listview = new ListView ( );
            pivot_tbl_listview.Location = new Point ( 10, 10 );
            pivot_tbl_listview.Size = new Size ( this_size.Width - 40, 420 );
            pivot_tbl_listview.MaximumSize = new Size ( this_size.Width - 40, 420 );
            pivot_tbl_listview.Margin = new Padding ( 1 );
            pivot_tbl_listview.GridLines = true;
            pivot_tbl_listview.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            pivot_tbl_listview.Font = new Font ( "Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point );
            pivot_tbl_listview.View = View.Details;
            pivot_tbl_listview.BorderStyle = BorderStyle.Fixed3D;
            pivot_tbl_listview.GridLines = true;
            Build_ListView_Columns ( );
            Process_the_Records ( );
            if (report_type == SHGuestsEFCore.SHGuests.pivot_rpt_type.WorkerDetail)
            {
                pivot_tbl_listview.AutoResizeColumns ( ColumnHeaderAutoResizeStyle.ColumnContent );
            }
            else
            {
                pivot_tbl_listview.AutoResizeColumns ( ColumnHeaderAutoResizeStyle.None );
            }
            this.Controls.Add ( pivot_tbl_listview );
            return;
        }

        #endregion Form Loading

        #region Main Processing of DataTable

        private void Process_the_Records ( )
        {
            while (in_reader.Read ( ))
            {
                int col_count = in_reader.GetValues ( row_values );
                if (!referring_switch)
                {
                    Check_for_Totals ( );
                }
                for (int ndx = 0; ndx < col_count; ndx++)
                {
                    string col_type = row_values [ndx].GetType ( ).ToString ( );
                    switch (col_type)
                    {
                        case "System.String":
                            list_items [ndx] = row_values [ndx].ToString ( );
                            break;

                        case "System.DateTime":
                            DateTime t_date = ( DateTime )row_values [ndx];
                            list_items [ndx] = t_date.ToString ( "MM/dd/yyyy" );
                            break;

                        case "System.Int32":
                            int t_int = ( int )row_values [ndx];
                            list_items [ndx] = t_int.ToString ( "##0" );
                            total_guests += t_int;
                            column_totals [ndx] += t_int;
                            agency_totals [ndx] += t_int;
                            break;

                        case "System.DBNull":
                            int nul_int = 0;
                            list_items [ndx] = nul_int.ToString ( "---" );
                            break;

                        default:
                            break;
                    }
                }
                if (referring_switch)
                {
                    agency_row_total = 0;
                    for (int j = 1; j < col_count; j++)
                    {
                        string col_type = row_values [j].GetType ( ).ToString ( );
                        if (col_type.Equals ( "System.Int32" ))
                        {
                            int a_int = ( int )row_values [j];
                            agency_row_total += a_int;
                        }
                    }
                    list_items [in_table.Columns.Count] = agency_row_total.ToString ( "#,##0" );
                    agency_row_totals [in_table.Columns.Count] += agency_row_total;
                }
                else
                {
                    list_items [in_table.Columns.Count] = total_guests.ToString ( "#,##0" );
                }
                lv_item = new ListViewItem ( list_items );
                lv_item.UseItemStyleForSubItems = false;
                for (int j = 0; j < lv_item.SubItems.Count; j++)
                {
                    if (lv_item.SubItems [j].Text.Equals ( "---" ))
                    {
                        lv_item.SubItems [j].ForeColor = Color.Red;
                    }
                }
                string col_typ = row_values [0].GetType ( ).ToString ( );
                if (col_typ != "System.DBNull")
                {
                    pivot_tbl_listview.Items.Add ( lv_item );
                }
                column_totals [in_table.Columns.Count] += total_guests;
                total_guests = 0;
                detail_rows++;
            }
            if (!referring_switch)
            {
                row_values [0] = 0xFFFFFF;                       //* We've hit end of the file, so trigger the totals to be output
                Check_for_Totals ( );
            }
            for (int j = 0; j < in_table.Columns.Count + 1; j++)
            {
                list_items [j] = " ";
            }
            lv_item = new ListViewItem ( list_items );
            pivot_tbl_listview.Items.Add ( lv_item );
            list_items [0] = "Grand Totals";
            if (!referring_switch && report_type != SHGuestsEFCore.SHGuests.pivot_rpt_type.WorkerDetail)
            {
                list_items [1] = $"{total_detail_rows:N0} Agency Referrers";
                for (int j = 2; j < columns_in_listview; j++)
                {
                    if (column_totals [j] > 0)
                    {
                        list_items [j] = column_totals [j].ToString ( "#,##0" );
                    }
                }
            }
            else
            {
                for (iLoop = 1; iLoop < in_table.Columns.Count; iLoop++)
                {
                    if (column_totals [iLoop] > 0)
                    {
                        list_items [iLoop] = column_totals [iLoop].ToString ( "#,##0" );
                    }
                }
                list_items [iLoop] = agency_row_totals [iLoop].ToString ( "#,##0" );
            }
            lv_item = new ListViewItem ( list_items );
            pivot_tbl_listview.Items.Add ( lv_item );
            return;
        }

        #endregion Main Processing of DataTable

        #region Listview Column Build

        private void Build_ListView_Columns ( )
        {
            int col_width = ( report_type == SHGuests.pivot_rpt_type.MonthlyReport ) ? 80 : 200;
            pivot_tbl_listview.Columns.Add ( in_table.Columns [0].ColumnName, col_width, HorizontalAlignment.Left );

            col_width = ( referring_switch ) ? 40 : 140;
            HorizontalAlignment col_align;
            switch (report_type)
            {
                case SHGuests.pivot_rpt_type.MonthlyReport:
                    col_align = HorizontalAlignment.Right;
                    pivot_tbl_listview.BackColor = Color.AntiqueWhite;
                    pivot_tbl_listview.ForeColor = Color.Black;
                    break;

                case SHGuests.pivot_rpt_type.Worker:
                    col_align = HorizontalAlignment.Left;
                    pivot_tbl_listview.BackColor = Color.AntiqueWhite;
                    pivot_tbl_listview.ForeColor = Color.DarkSlateBlue;
                    break;

                case SHGuests.pivot_rpt_type.WorkerDetail:
                    col_align = HorizontalAlignment.Left;
                    pivot_tbl_listview.BackColor = Color.AntiqueWhite;
                    pivot_tbl_listview.ForeColor = Color.DarkSlateBlue;
                    break;

                case SHGuests.pivot_rpt_type.Normal:
                default:
                    col_align = HorizontalAlignment.Right;
                    pivot_tbl_listview.BackColor = Color.AntiqueWhite;
                    pivot_tbl_listview.ForeColor = Color.Black;
                    break;
            }
            pivot_tbl_listview.Columns.Add ( in_table.Columns [1].ColumnName, col_width, col_align );
            column_totals [1] = 0;
            agency_totals [1] = 0;
            for (int j = 2; j < in_table.Columns.Count; j++)
            {
                if (report_type != SHGuests.pivot_rpt_type.WorkerDetail)
                {
                    pivot_tbl_listview.Columns.Add ( in_table.Columns [j].ColumnName, 45, HorizontalAlignment.Right );
                }
                else
                {
                    if (in_table.Columns [j].ColumnName.Equals ( "Days" ))
                    {
                        pivot_tbl_listview.Columns.Add ( in_table.Columns [j].ColumnName, 45, HorizontalAlignment.Right );
                    }
                    else
                    {
                        pivot_tbl_listview.Columns.Add ( in_table.Columns [j].ColumnName, 150, HorizontalAlignment.Left );
                    }
                }
                column_totals [j] = 0;
                agency_totals [j] = 0;
            }
            if (report_type != SHGuests.pivot_rpt_type.WorkerDetail)
                pivot_tbl_listview.Columns.Add ( "Totals", 50, HorizontalAlignment.Right );
            columns_in_listview = pivot_tbl_listview.Columns.Count;
            return;
        }

        #endregion Listview Column Build

        #region Process Totals

        private void Check_for_Totals ( )
        {
            string tmp_agency = ( string )row_values [0].ToString ( );
            if (String.IsNullOrWhiteSpace ( sv_agency ))
            {
                sv_agency = tmp_agency;
            }
            if (!sv_agency.Equals ( tmp_agency ))
            {
                if (report_type != SHGuests.pivot_rpt_type.WorkerDetail)
                {
                    agency_row_total = 0;
                    list_items [0] = $"Totals: {sv_agency}";
                    for (int j = 1; j < in_table.Columns.Count; j++)
                    {
                        list_items [j] = agency_totals [j].ToString ( "###" );
                        agency_row_total += agency_totals [j];
                        agency_totals [j] = 0;
                    }
                }
                //*
                //* Only do total line if more than 1 referral
                //*
                if (detail_rows > 1)
                {
                    if (report_type != SHGuests.pivot_rpt_type.WorkerDetail)
                    {
                        list_items [in_table.Columns.Count] = agency_row_total.ToString ( "#,##0" );
                        //*list_items[1] = $"Total Referrers:  {detail_rows:N0}";
                        lv_item = new ListViewItem ( list_items );
                        pivot_tbl_listview.Items.Add ( lv_item );
                    }
                    for (int j = 0; j < in_table.Columns.Count + 1; j++)
                    {
                        list_items [j] = " ";
                    }
                    lv_item = new ListViewItem ( list_items );
                    pivot_tbl_listview.Items.Add ( lv_item );
                }
                else
                {
                    if (report_type == SHGuests.pivot_rpt_type.WorkerDetail)
                    {
                        for (int j = 0; j < in_table.Columns.Count + 1; j++)
                        {
                            list_items [j] = " ";
                        }
                        lv_item = new ListViewItem ( list_items );
                        pivot_tbl_listview.Items.Add ( lv_item );
                    }
                }
                sv_agency = ( string )row_values [0].ToString ( );
                total_detail_rows += detail_rows;
                agency_row_total = 0;
                detail_rows = 0;
            }
            return;
        }

        #endregion Process Totals

        #region Event Handlers

        private void exit_report_Button_Click ( object sender, EventArgs e )
        {
            Close ( );
            return;
        }

        private void printtheViewButton_Click ( object sender, EventArgs e )
        {
            lvp = new ListViewPrinterBase ( pivot_tbl_listview );
            lvp.PrinterSettings.DefaultPageSettings.Landscape = true;
            lvp.Header = this.Text;
            //lvp.HeaderFormat = BlockFormat.Header();
            lvp.HeaderFormat.Font = new Font ( "Arial", 14F, FontStyle.Bold, GraphicsUnit.Point );
            lvp.IsShrinkToFit = true;
            lvp.ListFont = pivot_tbl_listview.Font;
            lvp.OriginAtMargins = false;
            lvp.DefaultPageSettings.Landscape = true;
            lvp.ListHeaderFormat.Font = pivot_tbl_listview.Font;
            lvp.PrintPreview ( );
            return;
        }
        private void button_createExcel_Click ( object sender, EventArgs e )
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
                BuildWorksheet ( wksht );

                package.Workbook.Properties.LastModifiedBy = Environment.UserName;
                package.Workbook.Properties.Application = "SHGuests";
                package.Workbook.Properties.Company = "Samaritan House, Inc.";
                package.Workbook.Properties.Title = $"{this.Text}";
                package.SaveAs ( _finfo );
            }
            return;
        }
        private void BuildWorksheet ( ExcelWorksheet wksht )
        {
            DataTable dt = new DataTable ( "Pivots" );
            dt = in_table.Copy ( );
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
            int rowcount = in_table.Rows.Count;
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