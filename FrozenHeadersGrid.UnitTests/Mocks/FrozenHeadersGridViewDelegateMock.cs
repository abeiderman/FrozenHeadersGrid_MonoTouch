using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests.Mocks
{
	internal class FrozenHeadersGridViewDelegateMock : FrozenHeadersGridViewDelegate
	{
		public int ColumnCount { get; set; }
		
		public int RowCount { get; set; }
		
		public Func<FrozenHeadersGridView, Point, UIView> ViewForCellDelegate { get; set; }
		
		public Func<FrozenHeadersGridView, int, string> TitleForColumnDelegate { get; set; }
		
		public Func<FrozenHeadersGridView, int, string> TitleForRowDelegate { get; set; }
		
		public override int NumberOfColumns(FrozenHeadersGridView gridView)
		{
			return ColumnCount;
		}
		
		public override int NumberOfRows(FrozenHeadersGridView gridView)
		{
			return RowCount;
		}
		
		public override string TitleForColumn(FrozenHeadersGridView gridView, int column)
		{
			if (TitleForColumnDelegate == null)
				return "";
			else
				return TitleForColumnDelegate(gridView, column);
		}
		
		public override string TitleForRow(FrozenHeadersGridView gridView, int row)
		{
			if (TitleForRowDelegate == null)
				return "";
			else
				return TitleForRowDelegate(gridView, row);
		}
		
		public override UIView ViewForCell(FrozenHeadersGridView gridView, Point cell)
		{
			if (ViewForCellDelegate == null)
				return null;
			else
				return ViewForCellDelegate(gridView, cell);
		}
	}
}

