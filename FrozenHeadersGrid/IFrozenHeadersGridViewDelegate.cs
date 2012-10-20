using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid
{

	public interface IFrozenHeadersGridViewDelegate
	{
		int NumberOfColumns(FrozenHeadersGridView gridView);

		int NumberOfRows(FrozenHeadersGridView gridView);

		string TitleForColumn(FrozenHeadersGridView gridView, int column);

		string TitleForRow(FrozenHeadersGridView gridView, int row);

		UIView ViewForCell(FrozenHeadersGridView gridView, Point cell);
	}
}