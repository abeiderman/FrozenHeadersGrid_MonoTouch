using System;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests.Mocks
{
	internal class FrozenHeadersGridViewDelegateMock : IFrozenHeadersGridViewDelegate
	{
		public int ColumnCount { get; set; }
		
		public int RowCount { get; set; }
		
		public Func<FrozenHeadersGridView, int, int, UIView> ViewForGridDelegate { get; set; }
		
		public Func<FrozenHeadersGridView, int, string> TitleForHeaderRowItemDelegate { get; set; }
		
		public Func<FrozenHeadersGridView, int, string> TitleForHeaderColumnItemDelegate { get; set; }
		
		public int GetColumnCount(FrozenHeadersGridView gridView)
		{
			return ColumnCount;
		}
		
		public int GetRowCount(FrozenHeadersGridView gridView)
		{
			return RowCount;
		}
		
		public string GetTitleForHeaderRowItem(FrozenHeadersGridView gridView, int column)
		{
			if (TitleForHeaderRowItemDelegate == null)
				return "";
			else
				return TitleForHeaderRowItemDelegate(gridView, column);
		}
		
		public string GetTitleForHeaderColumnItem(FrozenHeadersGridView gridView, int row)
		{
			if (TitleForHeaderColumnItemDelegate == null)
				return "";
			else
				return TitleForHeaderColumnItemDelegate(gridView, row);
		}
		
		public UIView GetViewForGrid(FrozenHeadersGridView gridView, int column, int row)
		{
			if (ViewForGridDelegate == null)
				return null;
			else
				return ViewForGridDelegate(gridView, column, row);
		}
	}
}

