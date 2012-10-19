using System;
using System.Drawing;
using NUnit.Framework;
using FrozenHeadersGrid.UnitTests.Mocks;

namespace FrozenHeadersGrid.UnitTests
{
    [TestFixture]
    public partial class FrozenHeadersGridViewTest
    {
        [Test]
        public void LayoutSubviews_WithFrameSmallerThanMinimumColumnWidth_SetsTheCellSizeToMinimumColumnWidth()
        {
            var gridView = new FrozenHeadersGridView(new RectangleF(0, 0, 100, 100));
            gridView.MinimumColumnWidth = 150;
            gridView.Delegate = new FrozenHeadersGridViewDelegateMock { ColumnCount = 1, RowCount = 1};
            
            gridView.LayoutSubviews();
            
            Assert.AreEqual(150, gridView.ContentView.CellSize.Width);
        }
        
        [Test]
        public void LayoutSubviews_WithFrameLargerThanMinimumColumnWidth_SetsTheCellSizeBasedOnFrameWidth()
        {
            var gridView = new FrozenHeadersGridView(new RectangleF(0, 0, 550, 100));
            gridView.MinimumColumnWidth = 150;
            gridView.HeaderColumnWidth = 50;
            gridView.Delegate = new FrozenHeadersGridViewDelegateMock { ColumnCount = 2, RowCount = 1};
            
            gridView.LayoutSubviews();
            
            Assert.AreEqual(250, gridView.ContentView.CellSize.Width, "View Width");
        }
        
        [Test]
        public void LayoutSubviews_WithFrameSmallerThanMinimumRowHeight_SetsTheCellSizeToMinimumRowHeight()
        {
            var gridView = new FrozenHeadersGridView(new RectangleF(0, 0, 100, 100));
            gridView.RowHeight = 80;
            gridView.Delegate = new FrozenHeadersGridViewDelegateMock { ColumnCount = 1, RowCount = 1};
            
            gridView.LayoutSubviews();
            
            Assert.AreEqual(80, gridView.ContentView.CellSize.Height, "View Height");
        }

        [Test]
        public void LayoutSubviews_HeaderRowItemHeight_IsSetToHeaderRowHeight()
        {
            var gridView = CreateFrozenHeadersGridViewWithDelegate();
            gridView.HeaderRowHeight = 55;
            
            gridView.LayoutSubviews();
            
            Assert.AreEqual(55, gridView.HeaderRowView.CellSize.Height);
        }
        
        [Test]
        public void LayoutSubviews_WithFrameSmallerThanMinimumColumnWidths_SetsHeaderRowItemWithToMinimumColumnWidth()
        {
            var gridView = CreateFrozenHeadersGridViewWithDelegate();
            gridView.MinimumColumnWidth = 80;
            
            gridView.LayoutSubviews();
            
            Assert.AreEqual(80, gridView.HeaderRowView.CellSize.Width);
        }
        
        [Test]
        public void LayoutSubviews_WithFrameLargerThanMinimumColumnWidths_SetsHeaderRowItemWithToMinimumColumnWidth()
        {
            var gridView = CreateFrozenHeadersGridViewWithDelegate(columns: 2);
            gridView.HeaderColumnWidth = 20;
            gridView.MinimumColumnWidth = 10;
            
            gridView.LayoutSubviews();
            
            Assert.AreEqual(40, gridView.HeaderRowView.CellSize.Width);
        }

        [Test]
        public void LayoutSubviews_HeaderColumnItemHeight_IsSetToRowHeight()
        {
            var gridView = CreateFrozenHeadersGridViewWithDelegate();
            gridView.RowHeight = 50;
            
            gridView.LayoutSubviews();
            
            Assert.AreEqual(50, gridView.HeaderColumnView.CellSize.Height);
        }
        
        [Test]
        public void LayoutSubviews_HeaderColumnItemWidth_IsSetToHeaderColumnWidth()
        {
            var gridView = CreateFrozenHeadersGridViewWithDelegate();
            gridView.HeaderColumnWidth = 64;
            
            gridView.LayoutSubviews();
            
            Assert.AreEqual(64, gridView.HeaderColumnView.CellSize.Width);
        }

        [Test]
        public void LayoutSubviews_CornerViewWidth_IsSetToHeaderColumnWidth()
        {
            var gridView = CreateFrozenHeadersGridView();
            gridView.Delegate = new FrozenHeadersGridViewDelegateMock { ColumnCount = 1, RowCount = 1};
            gridView.HeaderColumnWidth = 64;
            
            gridView.LayoutSubviews();
            
            Assert.AreEqual(64, gridView.HeaderCornerView.Frame.Width);
        }
        
        [Test]
        public void LayoutSubviews_CornerViewHeight_IsSetToHeaderRowHeight()
        {
            var gridView = CreateFrozenHeadersGridView();
            gridView.Delegate = new FrozenHeadersGridViewDelegateMock { ColumnCount = 1, RowCount = 1};
            gridView.HeaderRowHeight = 64;
            
            gridView.LayoutSubviews();
            
            Assert.AreEqual(64, gridView.HeaderCornerView.Frame.Height);
        }
    }
}
