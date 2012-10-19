using System;
using System.Linq;
using System.Drawing;
using NUnit.Framework;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class GridHeaderColumnViewTest
	{
		[Test]
		public void LayoutSubviews_ResizesViewToRowHeight()
		{
			var grid = CreateGridHeaderColumnView();
			var cell = new UIView(new RectangleF(0, 0, 100, 150));			
			grid[0] = cell;

			grid.LayoutSubviews();
			
			Assert.AreEqual(60, cell.Frame.Height);
		}

		[Test]
		public void LayoutSubviews_WithoutGridLines_PositionsViewVerticallyBasedOnRowHeight()
		{
			var grid = CreateGridHeaderColumnView();
			var cell = new UIView();			
			grid[1] = cell;
			
			grid.LayoutSubviews();

			Assert.AreEqual(60, cell.Frame.Y);
		}
		
		[Test]
		public void LayoutSubviews_WithGridLines_PositionsViewVerticallyBasedOnRowHeightAndGridLines()
		{
			var grid = CreateGridHeaderColumnView();
			grid.Gridlines.Thickness = 2;
			var cell = new UIView();			
			grid[1] = cell;
			
			grid.LayoutSubviews();

			Assert.AreEqual(62, cell.Frame.Y);
		}
		
		[Test]
		public void SetHeadersCount_AdjustsTheFrameHeightBasedOnRowHeight()
		{
			var grid = CreateGridHeaderColumnView();

			grid.HeadersCount = 3;
			
			Assert.AreEqual(180, grid.Frame.Size.Height);
		}
		
		[Test]
		public void SetHeadersCount_WhenAdjustingFrameHeight_AccountsForInnerGridLines()
		{
			var grid = CreateGridHeaderColumnView();
			grid.Gridlines.Thickness = 2;

			grid.HeadersCount = 3;
			
			Assert.AreEqual(184, grid.Frame.Size.Height);
		}

		GridHeaderColumnView CreateGridHeaderColumnView()
		{
			return new GridHeaderColumnView { CellSize = new SizeF(50, 60) };
		}
	}
}
