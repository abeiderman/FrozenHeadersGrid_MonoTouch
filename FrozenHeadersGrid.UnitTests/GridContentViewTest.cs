using System;
using System.Linq;
using System.Drawing;
using NUnit.Framework;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class GridContentViewTest
	{
        #region LayoutSubviews
		[Test]
		public void LayoutSubviews_ResizesSubviewToColumnWidth()
		{
			var grid = new GridContentView() { CellSize = new SizeF(50, 60) };
			var cell = new UIView(new RectangleF(0, 0, 100, 150));            
			grid[new Point(0, 0)] = cell;

			grid.LayoutSubviews();
            
			Assert.AreEqual(50, cell.Frame.Width);
		}

		[Test]
		public void LayoutSubviews_ResizesViewToRowHeight()
		{
			var grid = new GridContentView() { CellSize = new SizeF(50, 60) };
			var cell = new UIView(new RectangleF(0, 0, 150, 100));			
			grid[new Point(0, 0)] = cell;

			grid.LayoutSubviews();
			
			Assert.AreEqual(60, cell.Frame.Height);
		}

		[Test]
		public void LayoutSubviews_WithoutGridLines_PositionsViewHorizontallyBasedOnColumnWidth()
		{
			var grid = new GridContentView() { CellSize = new SizeF(50, 60) };
			var cell = new UIView();
			grid[new Point(1, 0)] = cell;

			grid.LayoutSubviews();

			Assert.AreEqual(50, cell.Frame.X);
		}

		[Test]
		public void LayoutSubviews_WithGridLines_PositionsViewHorizontallyBasedOnColumnWidthAndGridLines()
		{
			var grid = new GridContentView() { CellSize = new SizeF(50, 60) };
			grid.VerticalGridlines.Thickness = 2;
			var cell = new UIView();			
			grid[new Point(1, 0)] = cell;

			grid.LayoutSubviews();

			Assert.AreEqual(52, cell.Frame.X);
		}

		[Test]
		public void LayoutSubviews_WithoutGridLines_PositionsViewVerticallyBasedOnRowHeight()
		{
			var grid = new GridContentView() { CellSize = new SizeF(50, 60) };
			var cell = new UIView();			
			grid[new Point(0, 1)] = cell;
			
			grid.LayoutSubviews();

			Assert.AreEqual(60, cell.Frame.Y);
		}

		[Test]
		public void LayoutSubviews_WithGridLines_PositionsViewVerticallyBasedOnRowHeightAndGridLines()
		{
			var grid = new GridContentView() { CellSize = new SizeF(50, 60) };
			grid.HorizontalGridlines.Thickness = 2;
			var cell = new UIView();			
			grid[new Point(0, 1)] = cell;
			
			grid.LayoutSubviews();

			Assert.AreEqual(62, cell.Frame.Y);
		}
        #endregion

		[Test]
		public void SetGridSize_AdjustsTheFrameWidthBasedOnColumnWidth()
		{
			var grid = new GridContentView() { CellSize = new SizeF(50, 60) };

			grid.GridSize = new Size(3, 2);

			Assert.AreEqual(150, grid.Frame.Size.Width);
		}

		[Test]
		public void SetGridSize_AdjustsTheFrameHeightBasedOnRowHeight()
		{
			var grid = new GridContentView() { CellSize = new SizeF(50, 60) };
            
			grid.GridSize = new Size(2, 3);
            
			Assert.AreEqual(180, grid.Frame.Size.Height);
		}
        
		[Test]
		public void SetGridSize_WhenAdjustingFrameWidth_AccountsForInnerGridLines()
		{
			var grid = new GridContentView() { CellSize = new SizeF(50, 60) };
			grid.VerticalGridlines.Thickness = 2;
            
			grid.GridSize = new Size(3, 2);
            
			Assert.AreEqual(154, grid.Frame.Size.Width);
		}

		[Test]
		public void SetGridSize_WhenAdjustingFrameHeight_AccountsForInnerGridLines()
		{
			var grid = new GridContentView() { CellSize = new SizeF(50, 60) };
			grid.HorizontalGridlines.Thickness = 2;
            
			grid.GridSize = new Size(2, 3);
            
			Assert.AreEqual(184, grid.Frame.Size.Height);
		}

		[Test]
		public void SetGridSize_CallsSetNeedsLayout()
		{
			var calledSetNeedsLayout = false;
			var grid = new GridContentViewMock();
			grid.SetNeedsLayoutDelegate = () => calledSetNeedsLayout = true;

			grid.GridSize = new Size(2, 3);

			Assert.True(calledSetNeedsLayout);
		}

		[Test]
		public void SetCellSize_CallsSetNeedsLayout()
		{
			var calledSetNeedsLayout = false;
			var grid = new GridContentViewMock();
			grid.SetNeedsLayoutDelegate = () => calledSetNeedsLayout = true;
            
			grid.CellSize = new SizeF(50, 80);
            
			Assert.True(calledSetNeedsLayout);
		}

		[Test]
		public void SetCellSize_AdjustsFrameSize()
		{
			var grid = new GridContentView { GridSize = new Size(4, 3) };

			grid.CellSize = new SizeF(50, 80);
            
			Assert.AreEqual(200, grid.Frame.Width);
			Assert.AreEqual(240, grid.Frame.Height);
		}
	}
}