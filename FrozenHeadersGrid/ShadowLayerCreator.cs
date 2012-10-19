using System;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid
{
	public class ShadowLayerCreator
	{
		const float DefaultAlpha = 0.3f;

		public ShadowLayerCreator()
		{
			Alpha = DefaultAlpha;
		}

		public float Alpha { get; set; }

		public CAGradientLayer CreateLeftToRightShadow()
		{
			return CreateHorizontalLayer(new CGColor[] {
				CreateAlphaBlack(Alpha),
				UIColor.Clear.CGColor
			});
		}

		public CAGradientLayer CreateRightToLeftShadow()
		{
			return CreateHorizontalLayer(new CGColor[] {
				UIColor.Clear.CGColor,
				CreateAlphaBlack(Alpha)
			});
		}

		public CAGradientLayer CreateTopToBottomShadow()
		{
			return CreateVerticalLayer(new CGColor[] {
				CreateAlphaBlack(Alpha),
				UIColor.Clear.CGColor
			});
		}

		public CAGradientLayer CreateBottomToTopShadow()
		{
			return CreateVerticalLayer(new CGColor[] {
				UIColor.Clear.CGColor,
				CreateAlphaBlack(Alpha)
			});
		}

		CAGradientLayer CreateHorizontalLayer(CGColor[] colors)
		{
			return new CAGradientLayer {
				StartPoint = new PointF(0, 0.5f),
				EndPoint = new PointF(1, 0.5f),
				Colors = colors
			};
		}

		CAGradientLayer CreateVerticalLayer(CGColor[] colors)
		{
			return new CAGradientLayer {
				StartPoint = new PointF(0.5f, 0),
				EndPoint = new PointF(0.5f, 1),
				Colors = colors
			};
		}

		CGColor CreateAlphaBlack(float alpha)
		{
			return UIColor.FromRGBA(0, 0, 0, alpha).CGColor;
		}		
	}
}