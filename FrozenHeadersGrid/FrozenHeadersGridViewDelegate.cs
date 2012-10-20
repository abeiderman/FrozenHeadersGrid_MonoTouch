using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid
{
    public class FrozenHeadersGridViewDelegate : IFrozenHeadersGridViewDelegate
    {
        public virtual int NumberOfColumns(FrozenHeadersGridView gridView)
        {
            return 0;
        }

        public virtual int NumberOfRows(FrozenHeadersGridView gridView)
        {
            return 0;
        }

        public virtual string TitleForColumn(FrozenHeadersGridView gridView, int column)
        {
            return string.Empty;
        }

        public virtual string TitleForRow(FrozenHeadersGridView gridView, int row)
        {
            return string.Empty;
        }

        public virtual UIView ViewForCell(FrozenHeadersGridView gridView, Point cell)
        {
            return null;
        }
    }
}
