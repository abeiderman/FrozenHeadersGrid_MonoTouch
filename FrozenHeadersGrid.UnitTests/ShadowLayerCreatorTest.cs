using System;
using NUnit.Framework;
using MonoTouch.CoreGraphics;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class ShadowLayerCreatorTest
	{
		#region Left to Right Shadow
		[Test]
		public void CreateLeftToRightShadow_StartAndEndPointsXCoordinateSpan()
		{
			var shadowCreator = new ShadowLayerCreator();

			var layer = shadowCreator.CreateLeftToRightShadow();

			Assert.AreEqual(0, layer.StartPoint.X);
			Assert.AreEqual(1, layer.EndPoint.X);
		}

		[Test]
		public void CreateLeftToRightShadow_StartAndEndPointsYCoordinateStaysAtMidpoint()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateLeftToRightShadow();
			
			Assert.AreEqual(0.5f, layer.StartPoint.Y);
			Assert.AreEqual(0.5f, layer.EndPoint.Y);
		}

		[Test]
		public void CreateLeftToRightShadow_FirstColorIsBlackWithAlpha()
		{
			var shadowCreator = new ShadowLayerCreator { Alpha = 0.4f };
			
			var layer = shadowCreator.CreateLeftToRightShadow();

			AssertColorIsBlack(layer.Colors[0]);
			Assert.AreEqual(0.4f, layer.Colors[0].Alpha);
		}

		[Test]
		public void CreateLeftToRightShadow_SecondColorHasZeroAlpha()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateLeftToRightShadow();
			
			Assert.AreEqual(0f, layer.Colors[1].Alpha);
		}
		#endregion

		#region Right to Left Shadow
		[Test]
		public void CreateRightToLeftShadow_StartAndEndPointsXCoordinateSpan()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateRightToLeftShadow();
			
			Assert.AreEqual(0, layer.StartPoint.X);
			Assert.AreEqual(1, layer.EndPoint.X);
		}
		
		[Test]
		public void CreateRightToLeftShadow_StartAndEndPointsYCoordinateStaysAtMidpoint()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateRightToLeftShadow();
			
			Assert.AreEqual(0.5f, layer.StartPoint.Y);
			Assert.AreEqual(0.5f, layer.EndPoint.Y);
		}
		
		[Test]
		public void CreateRightToLeftShadow_FirstColorHasZeroAlpha()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateRightToLeftShadow();
			
			Assert.AreEqual(0f, layer.Colors[0].Alpha);
		}

		[Test]
		public void CreateRightToLeftShadow_SecondColorIsBlackWithAlpha()
		{
			var shadowCreator = new ShadowLayerCreator { Alpha = 0.4f };
			
			var layer = shadowCreator.CreateRightToLeftShadow();
			
			AssertColorIsBlack(layer.Colors[1]);
			Assert.AreEqual(0.4f, layer.Colors[1].Alpha);
		}
		#endregion

		#region Top to Bottom Shadow
		[Test]
		public void CreateTopToBottomShadow_StartAndEndPointsYCoordinateSpan()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateTopToBottomShadow();
			
			Assert.AreEqual(0, layer.StartPoint.Y);
			Assert.AreEqual(1, layer.EndPoint.Y);
		}
		
		[Test]
		public void CreateTopToBottomShadow_StartAndEndPointsXCoordinateStaysAtMidpoint()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateTopToBottomShadow();
			
			Assert.AreEqual(0.5f, layer.StartPoint.X);
			Assert.AreEqual(0.5f, layer.EndPoint.X);
		}
		
		[Test]
		public void CreateTopToBottomShadow_FirstColorIsBlackWithAlpha()
		{
			var shadowCreator = new ShadowLayerCreator { Alpha = 0.4f };
			
			var layer = shadowCreator.CreateTopToBottomShadow();
			
			AssertColorIsBlack(layer.Colors[0]);
			Assert.AreEqual(0.4f, layer.Colors[0].Alpha);
		}
		
		[Test]
		public void CreateTopToBottomShadow_SecondColorHasZeroAlpha()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateTopToBottomShadow();
			
			Assert.AreEqual(0f, layer.Colors[1].Alpha);
		}
		#endregion
		
		#region Bottom to Top Shadow
		[Test]
		public void CreateBottomToTopShadow_StartAndEndPointsYCoordinateSpan()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateBottomToTopShadow();
			
			Assert.AreEqual(0, layer.StartPoint.Y);
			Assert.AreEqual(1, layer.EndPoint.Y);
		}
		
		[Test]
		public void CreateBottomToTopShadow_StartAndEndPointsXCoordinateStaysAtMidpoint()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateBottomToTopShadow();
			
			Assert.AreEqual(0.5f, layer.StartPoint.X);
			Assert.AreEqual(0.5f, layer.EndPoint.X);
		}
		
		[Test]
		public void CreateBottomToTopShadow_FirstColorHasZeroAlpha()
		{
			var shadowCreator = new ShadowLayerCreator();
			
			var layer = shadowCreator.CreateBottomToTopShadow();
			
			Assert.AreEqual(0f, layer.Colors[0].Alpha);
		}

		[Test]
		public void CreateBottomToTopShadow_SecondColorIsBlackWithAlpha()
		{
			var shadowCreator = new ShadowLayerCreator { Alpha = 0.4f };
			
			var layer = shadowCreator.CreateBottomToTopShadow();
			
			AssertColorIsBlack(layer.Colors[1]);
			Assert.AreEqual(0.4f, layer.Colors[1].Alpha);
		}
		#endregion
		
		private void AssertColorIsBlack(CGColor color)
		{
			for (var i = 0; i < 3; i++)
				Assert.AreEqual(0, color.Components[i]);
		}
	}
}
