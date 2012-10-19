using System;
using System.Drawing;
using NUnit.Framework;
using MonoTouch.CoreAnimation;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class OuterShadowTest
	{
		const float FrameWidth = 100;
		const float FrameHeight = 200;
		private CALayer layer;
		private OuterShadowDelegateMock @delegate;

		[Test]
		public void LayoutShadows_WhenShadowStartingPotisionsAreAtFrameBoundaries_ItHasNoShadowLayers()
		{
			var shadow = CreateOuterShadow();

			shadow.LayoutShadows();
            
			Assert.Null(layer.Sublayers);
		}
        
		[Test]
		public void LayoutShadows_WhenLeftShadowStartXIsLargerThanZero_ItInsertsLeftShadowLayer()
		{
			var shadow = CreateOuterShadow();
			@delegate.LeftShadowStartXDelegate = () => 1;

			shadow.LayoutShadows();

			var leftShadow = layer.Sublayers[0];
			Assert.AreEqual(0, leftShadow.Frame.Top);
			Assert.AreEqual(-9, leftShadow.Frame.Left);
			Assert.AreEqual(10, leftShadow.Frame.Width);
			Assert.AreEqual(FrameHeight, leftShadow.Frame.Height);
		}

		[Test]
		public void LayoutShadows_WhenTopShadowStartXIsLargerThanZero_ItInsertsTopShadowLayer()
		{
			var shadow = CreateOuterShadow();
			@delegate.TopShadowStartYDelegate = () => 1;
            
			shadow.LayoutShadows();
            
			var leftShadow = layer.Sublayers[0];
			Assert.AreEqual(-9, leftShadow.Frame.Top);
			Assert.AreEqual(0, leftShadow.Frame.Left);
			Assert.AreEqual(FrameWidth, leftShadow.Frame.Width);
			Assert.AreEqual(10, leftShadow.Frame.Height);
		}

		[Test]
		public void LayoutShadows_WhenRightShadowStartXIsLessThanWidth_ItInsertsRightShadowLayer()
		{
			var shadow = CreateOuterShadow();
			@delegate.RightShadowStartXDelegate = () => 99;
            
			shadow.LayoutShadows();
            
			var leftShadow = layer.Sublayers[0];
			Assert.AreEqual(0, leftShadow.Frame.Top);
			Assert.AreEqual(99, leftShadow.Frame.Left);
			Assert.AreEqual(10, leftShadow.Frame.Width);
			Assert.AreEqual(FrameHeight, leftShadow.Frame.Height);
		}

		[Test]
		public void LayoutShadows_WhenBottomShadowStartYIsLessThanHeight_ItInsertsBottomShadowLayer()
		{
			var shadow = CreateOuterShadow();
			@delegate.BottomShadowStartYDelegate = () => 199;
            
			shadow.LayoutShadows();
            
			var leftShadow = layer.Sublayers[0];
			Assert.AreEqual(199, leftShadow.Frame.Top);
			Assert.AreEqual(0, leftShadow.Frame.Left);
			Assert.AreEqual(FrameWidth, leftShadow.Frame.Width);
			Assert.AreEqual(10, leftShadow.Frame.Height);
		}

		private OuterShadow CreateOuterShadow(float width=FrameWidth, float height=FrameHeight, float depth=10)
		{
			layer = new CALayer();
			@delegate = new OuterShadowDelegateMock();
			@delegate.LeftShadowStartXDelegate = () => 0;
			@delegate.RightShadowStartXDelegate = () => width;
			@delegate.TopShadowStartYDelegate = () => 0;
			@delegate.BottomShadowStartYDelegate = () => height;
			var shadow = new OuterShadow(layer, new SizeF(width, height), depth);
			shadow.Delegate = @delegate;
			return shadow;
		}
	}
}
