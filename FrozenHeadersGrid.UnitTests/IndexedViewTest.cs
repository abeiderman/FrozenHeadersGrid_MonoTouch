using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class IndexedViewTest
	{
		[Test]
		public void SetIndex_WhenAddingIntoExistingIndex_ReplacesPreviousView()
		{
			var header = new TestIndexedView();
			var firstCell = new UIView();
			var secondCell = new UIView();
			header[1] = firstCell;
            
			header[1] = secondCell;
            
			Assert.True(header.Subviews.Contains(secondCell));
			Assert.False(header.Subviews.Contains(firstCell));
		}
        
		[Test]
		public void SetIndex_AddsViewToSubviews()
		{
			var header = new TestIndexedView();
			var view = new UIView();
            
			header[2] = view;
            
			Assert.True(header.Subviews.Contains(view));
		}
        
		[Test]
		public void SetIndex_CallsSetNeedsLayout()
		{
			var setNeedsLayoutCalled = false;
			var header = new TestIndexedView();
			header.SetNeedsLayoutCalled += (sender, e) => setNeedsLayoutCalled = true;
			var view = new UIView();
            
			header[2] = view;
            
			Assert.True(setNeedsLayoutCalled);
		}

		[Test]
		public void SetIndex_WithNull_NothingIsAdded()
		{
			var header = new TestIndexedView();
			var subviewsCount = header.Subviews.Length;

			header[2] = null;
            
			Assert.AreEqual(subviewsCount, header.Subviews.Length);
		}

		[Test]
		public void GetIndex_WhenViewExistsAtSpecifiedIndex_ReturnsView()
		{
			var header = new TestIndexedView();
			var view = new UIView();
			header[1] = view;
            
			var returnedView = header[1];
            
			Assert.AreSame(view, returnedView);
		}
        
		[Test]
		public void GetIndex_WhenViewDoesNotExist_ReturnsNull()
		{
			var header = new TestIndexedView();
            
			var returnedView = header[1];
            
			Assert.Null(returnedView);
		}
        
		[Test]
		public void RemoveAt_WhenIndexExists_RemovesViewAtIndex()
		{
			var header = new TestIndexedView();
			var firstView = new UIView();
			var secondView = new UIView();
			header[0] = firstView;
			header[1] = secondView;
            
			header.RemoveAt(1);
            
			Assert.AreSame(firstView, header[0]);
			Assert.Null(header[1]);
		}
        
		[Test]
		public void RemoveAt_WhenIndexDoesNotExists_NothingIsRemoved()
		{
			var header = new TestIndexedView();
			var firstView = new UIView();
			var secondView = new UIView();
			header[0] = firstView;
			header[1] = secondView;
            
			header.RemoveAt(2);
            
			Assert.AreSame(firstView, header[0]);
			Assert.AreSame(secondView, header[1]);
		}

		[Test]
		public void RemoveAll_AllIndexedViewsAreCleared()
		{
			var header = new TestIndexedView();
			header[0] = new UIView();
			header[1] = new UIView();

			header.RemoveAll();

			Assert.Null(header[0]);
			Assert.Null(header[1]);
		}

		[Test]
		public void RemoveAll_AllIndexedViewsAreRemovedFromTheirSuperview()
		{
			var header = new TestIndexedView();
			var firstView = new UIView();
			header[0] = firstView;
			var secondView = new UIView();
			header[1] = secondView;
			
			header.RemoveAll();
			
			Assert.False(header.Subviews.Contains(firstView));
			Assert.False(header.Subviews.Contains(secondView));
		}
		
		[Test]
		public void LayoutSubviews_CallsLayoutViewForEachView()
		{
			var layoutViewCalls = new Dictionary<int, UIView>();
			var header = new TestIndexedView();
			header.LayoutViewDelegate = (view, index) => layoutViewCalls.Add(index, view);
			var firstView = new UIView();
			var secondView = new UIView();
			header[0] = firstView;
			header[1] = secondView;

			header.LayoutSubviews();

			Assert.AreEqual(2, layoutViewCalls.Count);
			Assert.AreSame(firstView, layoutViewCalls[0]);
			Assert.AreSame(secondView, layoutViewCalls[1]);
		}
	}

	class TestIndexedView : IndexedView<int>
	{
		public event EventHandler<EventArgs> SetNeedsLayoutCalled;

		public Action<UIView, int> LayoutViewDelegate { get; set; }
        
		protected override void LayoutView(UIView view, int index)
		{
			if (LayoutViewDelegate != null)
				LayoutViewDelegate(view, index);
		}
        
		public override void SetNeedsLayout()
		{
			base.SetNeedsLayout();
			if (SetNeedsLayoutCalled != null)
				SetNeedsLayoutCalled(this, new EventArgs());
		}        
	}

}
