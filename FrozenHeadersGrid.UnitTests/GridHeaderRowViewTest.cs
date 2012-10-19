using System;
using System.Linq;
using System.Drawing;
using NUnit.Framework;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class GridHeaderRowViewTest
	{
		[Test]
		public void LayoutSubviews_ResizesViewToColumnWidth()
		{
			var grid = new GridHeaderRowView() { CellSize = new SizeF(50, 60) };
			var cell = new UIView(new RectangleF(0, 0, 100, 150));            
			grid[0] = cell;

			grid.LayoutSubviews();
            
			Assert.AreEqual(50, cell.Frame.Width);
		}

		[Test]
		public void LayoutSubviews_WithoutGridLines_PositionsViewHorizontallyBasedOnColumnWidth()
		{
			var grid = new GridHeaderRowView() { CellSize = new SizeF(50, 60) };
			var cell = new UIView();            
			grid[1] = cell;

			grid.LayoutSubviews();
            
			Assert.AreEqual(50, cell.Frame.X);
		}

		[Test]
		public void LayoutSubviews_WithGridLines_PositionsViewHorizontallyBasedOnColumnWidthAndGridLines()
		{
			var grid = new GridHeaderRowView() { CellSize = new SizeF(50, 60) };
			grid.Gridlines.Thickness = 2;
			var cell = new UIView();            
			grid[1] = cell;

			grid.LayoutSubviews();
            
			Assert.AreEqual(52, cell.Frame.X);
		}

		[Test]
		public void SetHeadersCount_AdjustsTheFrameWidthBasedOnColumnWidth()
		{
			var grid = new GridHeaderRowView() { CellSize = new SizeF(50, 60) };
            
			grid.HeadersCount = 3;
            
			Assert.AreEqual(150, grid.Frame.Size.Width);
		}
        
		[Test]
		public void SetHeadersCount_WhenAdjustingFrameWidth_AccountsForInnerGridLines()
		{
			var grid = new GridHeaderRowView() { CellSize = new SizeF(50, 60) };
			grid.Gridlines.Thickness = 2;
            
			grid.HeadersCount = 3;
            
			Assert.AreEqual(154, grid.Frame.Size.Width);
		}
	}
}
