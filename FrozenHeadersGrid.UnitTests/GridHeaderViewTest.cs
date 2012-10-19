using System;
using System.Drawing;
using System.Linq;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;
using NUnit.Framework;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class GridHeaderViewTest
	{
		[Test]
		public void HeadersCount_AdjustsFrame()
		{
			var adjustedFrame = false;
			var header = new TestGridHeaderView();
			header.FrameAdjusted += (sender, e) => adjustedFrame = true;

			header.HeadersCount = 5;

			Assert.True(adjustedFrame);
		}

	}

	class TestGridHeaderView : GridHeaderView
	{
		public event EventHandler<EventArgs> FrameAdjusted;
		public event EventHandler<EventArgs> SetNeedsLayoutCalled;

		protected override void LayoutView(MonoTouch.UIKit.UIView view, int index)
		{
		}

		protected override void AdjustFrame()
		{
			if (FrameAdjusted != null)
				FrameAdjusted(this, new EventArgs());
		}

		public override void SetNeedsLayout()
		{
			base.SetNeedsLayout();
			if (SetNeedsLayoutCalled != null)
				SetNeedsLayoutCalled(this, new EventArgs());
		}

		protected override void DrawGradient(RectangleF rect, CGContext context)
		{
		}

		protected override void DrawGridlines(RectangleF rect, CGContext context)
		{
		}
	}
}
