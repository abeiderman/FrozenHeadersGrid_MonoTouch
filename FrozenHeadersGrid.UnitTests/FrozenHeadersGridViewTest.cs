using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using MonoTouch.UIKit;
using FrozenHeadersGrid.UnitTests.Mocks;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public partial class FrozenHeadersGridViewTest
	{
		[Test]
		public void MinimumColumnWidth_HasADefaultValue()
		{
			Assert.AreEqual(120, CreateFrozenHeadersGridView().MinimumColumnWidth);
		}

		[Test]
		public void RowHeight_HasADefaultValue()
		{
			Assert.AreEqual(55, CreateFrozenHeadersGridView().RowHeight);
		}

		[Test]
		public void HeaderColumnWith_HasADefaultValue()
		{
			Assert.AreEqual(60, CreateFrozenHeadersGridView().HeaderColumnWidth);
		}

		[Test]
		public void HeaderRowHeight_HasADefaultValue()
		{
			Assert.AreEqual(55, CreateFrozenHeadersGridView().HeaderRowHeight);
		}

		[Test]
		public void SetDelegate_UpdatesContent()
		{
			var updateCalled = false;
			var gridView = CreateFrozenHeadersGridViewMock();
			gridView.UpdateContentCalled += (sender, e) => updateCalled = true;

			gridView.Delegate = new FrozenHeadersGridViewDelegateMock();

			Assert.True(updateCalled);
		}

        #region TintColor
        [Test]
        public void TintColor_SetsHeaderRowTintColor()
        {
            var gridView = CreateFrozenHeadersGridView();

            gridView.TintColor = UIColor.Blue;

            Assert.AreEqual(UIColor.Blue, gridView.HeaderRowView.TintColor);
        }

        [Test]
        public void TintColor_SetsHeaderRowGridlinesColor()
        {
            var gridView = CreateFrozenHeadersGridView();
            
            gridView.TintColor = UIColor.Blue;
            
            Assert.AreEqual(UIColor.Blue, gridView.HeaderRowView.Gridlines.Color);
        }

        [Test]
        public void TintColor_SetsHeaderColumnTintColor()
        {
            var gridView = CreateFrozenHeadersGridView();
            
            gridView.TintColor = UIColor.Blue;
            
            Assert.AreEqual(UIColor.Blue, gridView.HeaderColumnView.TintColor);
        }
        
        [Test]
        public void TintColor_SetsHeaderColumnGridlinesColor()
        {
            var gridView = CreateFrozenHeadersGridView();
            
            gridView.TintColor = UIColor.Blue;
            
            Assert.AreEqual(UIColor.Blue, gridView.HeaderColumnView.Gridlines.Color);
        }
        #endregion

		[Test]
		public void UpdateContent_WhenDelegateReturnsItemView_AddsTheViewToTheGridContentView()
		{
			var gridView = CreateFrozenHeadersGridView();
			var itemView = new UIView();
			var mockDelegate = new FrozenHeadersGridViewDelegateMock { ColumnCount = 1, RowCount = 1};
			mockDelegate.ViewForCellDelegate = (view, cell) => {
				return itemView; };

			gridView.Delegate = mockDelegate;

			Assert.AreSame(itemView, gridView.ContentView[new Point(0, 0)]);
		}

		[Test]
		public void UpdateContent_AddsHeaderRowItemsForEachColumn()
		{
			var gridView = CreateFrozenHeadersGridViewWithDelegate();
            
			gridView.UpdateContent();

			Assert.AreEqual(3, gridView.HeaderRowView.Subviews.Length);
		}

		[Test]
		public void UpdateContent_SetsHeaderRowItemTextFromDelegate()
		{
			string[] titles = {"First title", "Second title"};
			var gridView = CreateFrozenHeadersGridViewWithDelegate(columns: 2);
            ((FrozenHeadersGridViewDelegateMock)gridView.Delegate)
                .TitleForColumnDelegate = (grid, column) => { return titles[column]; };

			gridView.UpdateContent();
            
			Assert.AreEqual("First title", ((GridHeaderItemView)gridView.HeaderRowView[0]).TextLabel.Text);
			Assert.AreEqual("Second title", ((GridHeaderItemView)gridView.HeaderRowView[1]).TextLabel.Text);
		}

		[Test]
		public void UpdateContent_AddsHeaderColumnItemsForEachRow()
		{
			var gridView = CreateFrozenHeadersGridViewMock();
			
			gridView.UpdateContent();
			
			Assert.AreEqual(4, gridView.HeaderColumnView.Subviews.Length);
		}

		[Test]
		public void UpdateContent_SetsHeaderColumnItemTextFromDelegate()
		{
			string[] titles = {"First title", "Second title"};
			var gridView = CreateFrozenHeadersGridViewWithDelegate(rows: 2);
            ((FrozenHeadersGridViewDelegateMock)gridView.Delegate)
                .TitleForRowDelegate = (grid, row) => { return titles[row]; };
			
			gridView.UpdateContent();
			
			Assert.AreEqual("First title", ((GridHeaderItemView)gridView.HeaderColumnView[0]).TextLabel.Text);
			Assert.AreEqual("Second title", ((GridHeaderItemView)gridView.HeaderColumnView[1]).TextLabel.Text);
		}

		FrozenHeadersGridView CreateFrozenHeadersGridView(float width = 100, float height = 100)
		{
			return new FrozenHeadersGridView(new RectangleF(0, 0, width, height));
		}

		FrozenHeadersGridView CreateFrozenHeadersGridViewWithDelegate(float width = 100, float height = 100, int columns = 3, int rows = 4)
		{
			var gridView = new FrozenHeadersGridView(new RectangleF(0, 0, width, height));
			var mockDelegate = new FrozenHeadersGridViewDelegateMock { ColumnCount = columns, RowCount = rows };
			gridView.Delegate = mockDelegate;
			return gridView;
		}

		FrozenHeadersGridViewMock CreateFrozenHeadersGridViewMock(int columns = 3, int rows = 4)
		{
			var mockDelegate = new FrozenHeadersGridViewDelegateMock { ColumnCount = columns, RowCount = rows };
			var gridView = new FrozenHeadersGridViewMock(new RectangleF(0, 0, 100, 100));
			gridView.Delegate = mockDelegate;

			return gridView;
		}
	}
}