using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.WebUI;

namespace Vevo
{
    /// <summary>
    /// Summary description for GridViewHelper
    /// </summary>
    [Serializable]
    public class GridViewHelper
    {
        public enum Direction
        {
            ASC,
            DESC
        }

        private string _expression;
        //private string _direction = "ASC";
        private Direction _direction = Direction.ASC;


        private void ReverseDirection()
        {
            if (_direction == Direction.ASC)
                _direction = Direction.DESC;
            else
                _direction = Direction.ASC;
        }

        public GridViewHelper( GridView grid, string defaultSortExpression )
        {
            _expression = defaultSortExpression;
        }

        public GridViewHelper( GridView grid, string defaultSortExpression, Direction direction )
        {
            _expression = defaultSortExpression;
            _direction = direction;
        }

        public void SelectSorting( string sortExpression )
        {
            if (_expression == sortExpression)
            {
                ReverseDirection();
            }
            else
            {
                _direction = Direction.ASC;
            }
            _expression = sortExpression;
        }

        public void SetFullSortText( string fullSortText )
        {
            string[] text = fullSortText.Split( ' ' );

            _expression = text[0];
            if (text.Length > 1)
            {
                if (text[1].ToUpper() == Direction.ASC.ToString())
                    _direction = Direction.ASC;
                else
                    _direction = Direction.DESC;
            }
        }

        public void SelectSorting( string sortExpression, Direction direction )
        {
            _direction = direction;
            _expression = sortExpression;
        }

        public string GetFullSortText()
        {
            if (String.IsNullOrEmpty( _expression ))
                return String.Empty;
            else
                return _expression + " " + _direction.ToString() + " ";
        }

        public static void ShowGridAlways( GridView grid, DataTable table, string emptyMessage )
        {
            if (table.Rows.Count == 0)
            {
                table.Constraints.Clear();
                foreach (DataColumn column in table.Columns)
                    column.AllowDBNull = true;

                table.Columns[0].AllowDBNull = true;
                table.Rows.Add( table.NewRow() );

                grid.DataSource = table;
                grid.DataBind();

                int columnCount = grid.Rows[0].Cells.Count;

                grid.Rows[0].Cells.Clear();
                grid.Rows[0].Cells.Add( new TableCell() );
                grid.Rows[0].Cells[0].ColumnSpan = columnCount;
                grid.Rows[0].Cells[0].Text = emptyMessage;
            }
            else
            {
                grid.DataSource = table;
                grid.DataBind();
            }
        }

        public static void ShowGridAlways( GridView grid, IList<Order> orderLists, string emptyMessage )
        {
            if (orderLists.Count == 0)
            {
                DataTable table = new DataTable();
                table.Constraints.Clear();
                foreach (DataColumn column in table.Columns)
                    column.AllowDBNull = true;

                table.Columns[0].AllowDBNull = true;
                table.Rows.Add( table.NewRow() );

                grid.DataSource = table;
                grid.DataBind();

                int columnCount = grid.Rows[0].Cells.Count;

                grid.Rows[0].Cells.Clear();
                grid.Rows[0].Cells.Add( new TableCell() );
                grid.Rows[0].Cells[0].ColumnSpan = columnCount;
                grid.Rows[0].Cells[0].Text = emptyMessage;
            }
            else
            {
                grid.DataSource = orderLists;
                grid.DataBind();
            }
        }
    }
}
