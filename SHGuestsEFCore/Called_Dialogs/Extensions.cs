using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHGuestsEFCore.Called_Dialogs
{
    public static class Extensions
    {
        public static DataTable AsDataTable<T> ( this IEnumerable<T> list ) where T : class
        {
            DataTable dtOutput = new DataTable ( "tblOutput" );

            //if the list is empty, return empty data table
            if (list.Count ( ) == 0)
                return dtOutput;

            //get the list of  public properties and add them as columns to the
            //output table           
            PropertyInfo [ ] properties = list.FirstOrDefault ( ).GetType ( ).
                GetProperties ( BindingFlags.Public | BindingFlags.Instance );
            foreach (PropertyInfo propertyInfo in properties)
                dtOutput.Columns.Add ( propertyInfo.Name, propertyInfo.PropertyType );

            //populate rows
            DataRow dr;
            //iterate through all the objects in the list and add them
            //as rows to the table
            foreach (T t in list)
            {
                dr = dtOutput.NewRow ( );
                //iterate through all the properties of the current object
                //and set their values to data row
                foreach (PropertyInfo propertyInfo in properties)
                {
                    dr [propertyInfo.Name] = propertyInfo.GetValue ( t, null );
                }
                dtOutput.Rows.Add ( dr );
            }
            return dtOutput;
        }
        public static void FromListView ( DataTable table, ListView lvw )
        {
            table.Clear ( );
            var columns = lvw.Columns.Count;

            foreach (ColumnHeader column in lvw.Columns)
            {
                //*Type colType = column.GetType().DeclaringType.DeclaringType;
                table.Columns.Add ( column.Text );
            }
            foreach (ListViewItem item in lvw.Items)
            {
                var cells = new object [columns];
                for (var i = 0; i < columns; i++)
                {
                    Type cellType = item.SubItems [i].GetType ( );
                    cells [i] = item.SubItems [i].Text;
                }
                table.Rows.Add ( cells );
            }
        }
        public static void PrintToGraphics ( Graphics graphics, Rectangle bounds, ListView lv_in )
        {
            Bitmap bitmap = new Bitmap ( lv_in.Width, lv_in.Height );
            lv_in.DrawToBitmap ( bitmap, new Rectangle ( 0, 0, bitmap.Width, bitmap.Height ) );
            Rectangle target = new Rectangle ( 5, 50, bounds.Width, bounds.Height );
            double xScale = ( double )bitmap.Width / bounds.Width;
            double yScale = ( double )bitmap.Height / bounds.Height;

            if (xScale < yScale)
                target.Width = ( int )( xScale * target.Width / yScale );
            else
                target.Height = ( int )( yScale * target.Height / xScale );

            graphics.PageUnit = GraphicsUnit.Display;
            graphics.DrawImage ( bitmap, target );
            return;
        }
        public static DateTime GetAge ( DateTime date_In )
        {
            TimeSpan age = TimeSpan.MinValue;
            age = DateTime.Now - date_In;
            DateTime new_date = new DateTime ( age.Ticks );
            DateTime ret_Date = new DateTime ( new_date.Year - 1, new_date.Month - 1, new_date.Day - 1 );
            return ret_Date;
        }
        /// <summary>
        /// Adds sub-totals to a list of items, along with a grand total for the whole list.
        /// </summary>
        /// <param name="elements">Group and/or sort this yourself before calling WithRollup.</param>
        /// <param name="primaryKeyOfElement">Given a TElement, return the property that you want sub-totals for.</param>
        /// <param name="calculateSubTotalElement">Given a group of elements, return a TElement that represents the sub-total.</param>
        /// <param name="grandTotalElement">A TElement that represents the grand total.</param>
        public static List<TElement> WithRollup<TElement, TKey> ( this IEnumerable<TElement> elements,
            Func<TElement, TKey> primaryKeyOfElement,
            Func<IGrouping<TKey, TElement>, TElement> calculateSubTotalElement,
            TElement grandTotalElement )
        {
            // Create a new list the items, subtotals, and the grand total.
            List<TElement> results = new List<TElement> ( );
            var lookup = elements.ToLookup ( primaryKeyOfElement );
            foreach (var group in lookup)
            {
                // Add items in the current group
                results.AddRange ( group );
                // Add subTotal for current group
                results.Add ( calculateSubTotalElement ( group ) );
            }
            // Add grand total
            results.Add ( grandTotalElement );

            return results;
        }
    }
}
