using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CommonRoutines
{
    public class Commons
    {
        public string integer_fmt = string.Empty;
        public bool stat_rpt = false;

        #region Grid View Layout

        public void Layout_GridView ( DataGridView dgv, DGVParms In_parms )
        {
            string my_Fontfamily = In_parms.font_family;
            integer_fmt = ( !string.IsNullOrEmpty ( In_parms.integer_fmt ) ) ? In_parms.integer_fmt : "###0";
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = true;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ShowCellToolTips = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.BackgroundColor = In_parms.backg_color; ;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = In_parms.hdr_back_color;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = In_parms.hdr_fore_color;
            dgv.DefaultCellStyle.ForeColor = In_parms.cell_fore_color;
            dgv.DefaultCellStyle.BackColor = In_parms.cell_back_color;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = In_parms.alt_fore_color;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = In_parms.alt_back_color;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = In_parms.hdr_alignment;
            dgv.ColumnHeadersDefaultCellStyle.Font = In_parms.hdr_font;
            dgv.DefaultCellStyle.Font = In_parms.cell_font;
            dgv.ScrollBars = In_parms.which_scroll;
            stat_rpt = In_parms.stat_rpt;
            return;
        }

        #endregion Grid View Layout

        #region Format DataGridView

        public void Format_the_View ( DataGridView dgv )
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.MinimumWidth = 50;
                col.FillWeight = 60;
                col.Resizable = DataGridViewTriState.False;
                switch (col.ValueType.ToString ( ))
                {
                    case "System.String":
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
                        col.MinimumWidth = 200;
                        col.Resizable = DataGridViewTriState.True;
                        if (col.Name.Equals ( "Return" ) || col.Name.Equals ( "Gender" ) || col.Name.Contains ( "Retn" ) || col.Name.Contains ( "Roster" ))
                        {
                            col.MinimumWidth = 90;
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
                            col.Resizable = DataGridViewTriState.False;
                        }
                        if (col.Name.Contains ( "Visit" ))
                        {
                            col.MinimumWidth = 60;
                            col.Resizable = DataGridViewTriState.False;
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                        }
                        break;

                    case "System.DateTime":
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                        col.DefaultCellStyle.Format = "MM/dd/yyyy";
                        col.MinimumWidth = 90;
                        break;

                    case "System.Int32":
                    case "System.Int":
                    case "System.uint":
                    case "System.Int64":
                        col.DefaultCellStyle.Format = ( col.Name.ToUpper ( ).Contains ( "SSN" ) ) ? "000-00-0000" : integer_fmt;
                        col.DefaultCellStyle.Alignment = ( col.Name.Contains ( "Visit" ) )
                                                         ? DataGridViewContentAlignment.BottomCenter
                                                         : DataGridViewContentAlignment.BottomRight;
                        col.MinimumWidth = 65;
                        col.Resizable = DataGridViewTriState.True;
                        break;

                    case "System.Single":
                    case "System.Double":
                    case "System.float":
                        col.DefaultCellStyle.Format = "N4";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                        col.MinimumWidth = 75;
                        break;

                    case "System.Decimal":
                        col.DefaultCellStyle.Format = "$###,###,##0";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                        col.MinimumWidth = 30;
                        break;

                    default:
                        break;
                }
            }
            dgv.ScrollBars = ScrollBars.Both;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            if (stat_rpt)
            {
                dgv.ScrollBars = ScrollBars.None;
            }
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgv.AutoResizeRows ( );
            dgv.AutoResizeColumns ( );
            dgv.Height = dgv.Rows.GetRowsHeight ( DataGridViewElementStates.None ) + dgv.ColumnHeadersHeight + 50;
            dgv.Width = dgv.Columns.GetColumnsWidth ( DataGridViewElementStates.None ) + dgv.RowHeadersWidth + 20;
            return;
        }

        #endregion Format DataGridView

        #region My Functions for LINQ

        /// <summary>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to">  </param>
        /// <returns> Integer days </returns>
        /// <example></example>
        public int CalcDays ( DateTime from, DateTime to )
        {
            TimeSpan ts = new TimeSpan ( 0, 0, 0 );
            ts = from.AddDays ( 1 ) - to;
            return ts.Days;
        }

        #endregion My Functions for LINQ

        #region SSN Validation Routines

        public bool ValidateSSN ( in string ssn_string )
        {
            Regex regex = new Regex ( @"^(?!\b(\d)\1+-(\d)\1+-(\d)\1+\b)(?!123-45-6789|219-09-9999|078-05-1120)(?!666|000|9\d{2})\d{3}-(?!00)\d{2}-(?!0{4})\d{4}$" );
            return regex.IsMatch ( ssn_string );
        }

        public bool RegValidateSSN ( in string ssn_string )
        {
            Regex regex = new Regex ( @"^(?!219-09-9999|078-05-1120)(?!666|000|9\d{2})\d{3}-(?!00)\d{2}-(?!0{4})\d{4}$" );
            return regex.IsMatch ( ssn_string );
        }

        #endregion

    }

    #region Class for DataGridView Layout parameters

    public class DGVParms
    {
        public string font_family { get; set; }
        public string integer_fmt { get; set; }
        public Color backg_color { get; set; }
        public Color hdr_back_color { get; set; }
        public Color hdr_fore_color { get; set; }
        public Color cell_back_color { get; set; }
        public Color cell_fore_color { get; set; }
        public Color alt_fore_color { get; set; }
        public Color alt_back_color { get; set; }
        public Font hdr_font { get; set; }
        public Font cell_font { get; set; }
        public DataGridViewContentAlignment hdr_alignment { get; set; }
        public DataGridViewContentAlignment cell_alignment { get; set; }
        public ScrollBars which_scroll { get; set; }
        public bool stat_rpt { get; set; }
    }

    #endregion Class for DataGridView Layout parameters
}