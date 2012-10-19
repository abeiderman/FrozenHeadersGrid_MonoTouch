using System;
using System.Drawing;
using NUnit.Framework;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class GridHeaderItemViewTest
	{
		[Test]
		public void DefaultConstructor_InitializesTextLabelWithClearBackground()
		{
			var itemView = new GridHeaderItemView();

			Assert.AreEqual(UIColor.Clear, itemView.TextLabel.BackgroundColor);
		}

		[Test]
		public void DefaultConstructor_AddsTheLabelToTheSubviews()
		{
			var itemView = new GridHeaderItemView();
            
			Assert.True(itemView.Subviews[0] is UILabel);
		}

		[Test]
		public void ConstructorWithFrame_InitializesTextLabelWithClearBackground()
		{
			var itemView = new GridHeaderItemView(new RectangleF(50, 50, 100, 100));
            
			Assert.AreEqual(UIColor.Clear, itemView.TextLabel.BackgroundColor);
		}

		[Test]
		public void ConstructorWithFrame_InitializesTextLabelFrameToBounds()
		{
			var itemView = new GridHeaderItemView(new RectangleF(50, 50, 100, 100));
            
			Assert.AreEqual(new RectangleF(0, 0, 100, 100), itemView.TextLabel.Frame);
		}

		[Test]
		public void ConstructorWithFrame_AddsTheLabelToTheSubviews()
		{
			var itemView = new GridHeaderItemView(new RectangleF(50, 50, 100, 100));

			Assert.True(itemView.Subviews[0] is UILabel);
		}
        
		[Test]
		public void SetFrame_SetsTheTextLabelFrameToBounds()
		{
			var itemView = new GridHeaderItemView();
			itemView.Frame = new RectangleF(50, 50, 100, 200);

			Assert.AreEqual(new RectangleF(0, 0, 100, 200), itemView.TextLabel.Frame);
		}
	}
}
