using System;
using System.Drawing;
using NUnit.Framework;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class FrozenHeadersScrollViewTest
	{
        #region Layout HorizontalGuide
		[Test]
		public void LayoutSubviews_WhenSettingHorizontalGuideWidth_AccountsForVerticalGuideWidth()
		{
			var scrollView = CreateFrozenHeadersScrollView(width: 100);
			scrollView.HeaderColumnWidth = 40;

			scrollView.LayoutSubviews();
            
			Assert.AreEqual(60, scrollView.HeaderRow.Frame.Width);
		}

		[Test]
		public void LayoutSubviews_WhenPositioningHorizontalGuide_AccountsForVerticalGuideWidth()
		{
			var scrollView = CreateFrozenHeadersScrollView(width: 100);
			scrollView.HeaderColumnWidth = 40;
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(40, scrollView.HeaderRow.Frame.Left);
		}

		[Test]
		public void LayoutSubviews_SetsHorizontalGuideHeight()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.HeaderRowHeight = 53;
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(53, scrollView.HeaderRow.Frame.Height);
		}

		[Test]
		public void LayoutSubviews_WhenContentIsScrolledDown_AdjustsHorizontalGuideTopDownwardsForBouncyEffect()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(0, -10);

			scrollView.LayoutSubviews();

			Assert.AreEqual(10, scrollView.HeaderRow.Frame.Top);
		}

		[Test]
		public void LayoutSubviews_WhenContentIsNotScrolled_AdjustsHorizontalGuideTopToZero()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(0, 0);
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(0, scrollView.HeaderRow.Frame.Top);
		}

		[Test]
		public void LayoutSubviews_WhenContentIsScrolledDown_AdjustsHorizontalGuideTopToZero()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(0, 10);
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(0, scrollView.HeaderRow.Frame.Top);
		}
        #endregion

        #region Layout VerticalGuide
		public void LayoutSubviews_WhenSettingVerticalGuideHeight_AccountsForHorizontalGuideHeight()
		{
			var scrollView = CreateFrozenHeadersScrollView(width: 100);
			scrollView.HeaderRowHeight = 40;
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(60, scrollView.HeaderColumn.Frame.Height);
		}

		[Test]
		public void LayoutSubviews_WhenPositioningVerticalGuide_AccountsForHorizontalGuideHeight()
		{
			var scrollView = CreateFrozenHeadersScrollView(width: 100);
			scrollView.HeaderRowHeight = 40;
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(40, scrollView.HeaderColumn.Frame.Top);
		}

		[Test]
		public void LayoutSubviews_SetsVerticalGuideWidth()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.HeaderColumnWidth = 53;
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(53, scrollView.HeaderColumn.Frame.Width);
		}

		[Test]
		public void LayoutSubviews_WhenContentIsScrolledRight_AdjustsVerticalGuideLeftRightwardsForBouncyEffect()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(-10, 0);
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(10, scrollView.HeaderColumn.Frame.Left);
		}
        
		[Test]
		public void LayoutSubviews_WhenContentIsNotScrolled_AdjustsVerticalGuideLeftToZero()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(0, 0);
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(0, scrollView.HeaderColumn.Frame.Left);
		}
        
		[Test]
		public void LayoutSubviews_WhenContentIsScrolledLeft_AdjustsVerticalGuideLeftToZero()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(0, 10);
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(0, scrollView.HeaderColumn.Frame.Left);
		}
        #endregion

		#region Layout ContentContainer
		[Test]
		public void LayoutSubviews_WhenPositioningContentContainer_AccountsForGuidesHeightAndWidth()
		{
			var scrollView = CreateFrozenHeadersScrollView(width: 100, height: 100);
			scrollView.HeaderColumnWidth = 30;
			scrollView.HeaderRowHeight = 20;

			scrollView.LayoutSubviews();
            
			Assert.AreEqual(30, scrollView.ContentView.Frame.Left);
			Assert.AreEqual(20, scrollView.ContentView.Frame.Top);
		}

		[Test]
		public void LayoutSubviews_WhenSizingContentContainer_AccountsForGuidesHeightAndWidth()
		{
			var scrollView = CreateFrozenHeadersScrollView(width: 100, height: 100);
			scrollView.HeaderColumnWidth = 30;
			scrollView.HeaderRowHeight = 20;

			scrollView.LayoutSubviews();
            
			Assert.AreEqual(70, scrollView.ContentView.Frame.Width);
			Assert.AreEqual(80, scrollView.ContentView.Frame.Height);
		}
		#endregion

		#region Layout TopLeftView
		[Test]
		public void LayoutSubviews_WhenPositioningTopLeftView_AlignsItsLeftWithTheVerticalGuide()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(-10, 0);

			scrollView.LayoutSubviews();

			Assert.AreEqual(10, scrollView.HeaderCornerView.Frame.Left);
		}

		[Test]
		public void LayoutSubviews_WhenPositioningTopLeftView_AlignsItsTopWithTheHorizontalGuide()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(0, -10);
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(10, scrollView.HeaderCornerView.Frame.Top);
		}

		[Test]
		public void LayoutSubviews_WhenSizingTopLeftView_MatchesGuidesSizes()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.HeaderColumnWidth = 20;
			scrollView.HeaderRowHeight = 30;

			scrollView.LayoutSubviews();

			Assert.AreEqual(20, scrollView.HeaderCornerView.Frame.Width);
			Assert.AreEqual(30, scrollView.HeaderCornerView.Frame.Height);
		}
		#endregion

        #region Outer Shadows
		[Test]
		public void LayoutSubviews_WhenOverscrollingRightwards_LeftShadowStarts()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(-10, 0);

			scrollView.LayoutSubviews();

			Assert.AreEqual(10, scrollView.LeftShadowStartX);
		}

		[Test]
		public void LayoutSubviews_WhenOverscrollingDownwards_TopShadowStarts()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(0, -10);
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(10, scrollView.TopShadowStartY);
		}

		[Test]
		public void LayoutSubviews_WhenOverscrollingLeft_RightShadowStarts()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.HeaderColumnWidth = 20;
			scrollView.ContentView.ContentSize = new SizeF(100, 100);
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(30, 0);
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(90, scrollView.RightShadowStartX);
		}

		[Test]
		public void LayoutSubviews_WhenOverscrollingUp_BottomShadowStarts()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.HeaderRowHeight = 20;
			scrollView.ContentView.ContentSize = new SizeF(100, 100);
			scrollView.LayoutSubviews();
			scrollView.ContentView.ContentOffset = new PointF(0, 30);
            
			scrollView.LayoutSubviews();
            
			Assert.AreEqual(90, scrollView.BottomShadowStartY);
		}
        #endregion

		#region Dimension changes
		[Test]
		public void HeaderColumnWidth_WhenSet_CallsSetNeedsLayout()
		{
			var needsLayoutCalled = false;
			var scrollViewMock = new FrozenHeadersScrollViewMock(new RectangleF(0, 0, 100, 100));
			scrollViewMock.SetNeedsLayoutDelegate = () => needsLayoutCalled = true;
			
			scrollViewMock.HeaderColumnWidth = 50;
			
			Assert.True(needsLayoutCalled);
		}
		#endregion

		#region Scrolling
		[Test]
		public void ScrollingDelegate_WhenScrolled_ItCallsSetNeedsLayout()
		{
			var needsLayoutCalled = false;
			var scrollViewMock = new FrozenHeadersScrollViewMock(new RectangleF(0, 0, 100, 100));
			scrollViewMock.SetNeedsLayoutDelegate = () => needsLayoutCalled = true;

			scrollViewMock.HeaderRow.Delegate.Scrolled(scrollViewMock.HeaderRow);

			Assert.True(needsLayoutCalled);
		}

		[Test]
		public void ScrollingDelegate_WhenHorizontalGuideScrolls_ItAdjustsTheContentContainer()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.HeaderRow.ContentOffset = new PointF(10, 10);

			scrollView.HeaderRow.Delegate.Scrolled(scrollView.HeaderRow);

			Assert.AreEqual(10, scrollView.ContentView.ContentOffset.X);
			Assert.AreEqual(0, scrollView.ContentView.ContentOffset.Y);
		}

		[Test]
		public void ScrollingDelegate_WhenVerticalGuideScrolls_ItAdjustsTheContentContainer()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.HeaderColumn.ContentOffset = new PointF(10, 10);
			
			scrollView.HeaderColumn.Delegate.Scrolled(scrollView.HeaderColumn);
			
			Assert.AreEqual(0, scrollView.ContentView.ContentOffset.X);
			Assert.AreEqual(10, scrollView.ContentView.ContentOffset.Y);
		}

		[Test]
		public void ScrollingDelegate_WhenContentContainerScrolls_ItAdjustsTheVerticalGuide()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.ContentView.ContentOffset = new PointF(10, 10);
			
			scrollView.ContentView.Delegate.Scrolled(scrollView.ContentView);
			
			Assert.AreEqual(0, scrollView.HeaderColumn.ContentOffset.X);
			Assert.AreEqual(10, scrollView.HeaderColumn.ContentOffset.Y);
		}

		[Test]
		public void ScrollingDelegate_WhenContentContainerScrolls_ItAdjustsTheHorizontalGuide()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			scrollView.ContentView.ContentOffset = new PointF(10, 10);
			
			scrollView.ContentView.Delegate.Scrolled(scrollView.ContentView);
			
			Assert.AreEqual(10, scrollView.HeaderRow.ContentOffset.X);
			Assert.AreEqual(0, scrollView.HeaderRow.ContentOffset.Y);
		}

		[Test]
		public void ShouldScrollToTop_WhenInvokedByVerticalGuide_ItReturnsFalse()
		{
			var scrollView = CreateFrozenHeadersScrollView();

			Assert.False(scrollView.HeaderColumn.Delegate.ShouldScrollToTop(scrollView.HeaderColumn));
		}

		[Test]
		public void ShouldScrollToTop_WhenInvokedByHorizontalGuide_ItReturnsFalse()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			
			Assert.False(scrollView.HeaderRow.Delegate.ShouldScrollToTop(scrollView.HeaderRow));
		}

		[Test]
		public void ShouldScrollToTop_WhenInvokedByContentContainer_ItReturnsTrue()
		{
			var scrollView = CreateFrozenHeadersScrollView();
			
			Assert.True(scrollView.ContentView.Delegate.ShouldScrollToTop(scrollView.ContentView));
		}
		#endregion

		FrozenHeadersScrollView CreateFrozenHeadersScrollView(float width = 100, float height = 100)
		{
			return new FrozenHeadersScrollView(new RectangleF(0, 0, width, height));
		}
	}
}
