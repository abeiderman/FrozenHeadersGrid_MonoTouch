using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace FrozenHeadersGrid.Demo
{
	public class CornerView : UIView
	{
		UIColor color;
		
		public CornerView(UIColor color)
		{
			this.color = color;
		}

		public override void Draw(RectangleF rect)
		{
			CGContext context = UIGraphics.GetCurrentContext();
			context.DrawLinearGradient(new CGGradient(CGColorSpace.CreateDeviceRGB(), 
			                                          new CGColor[] { color.CGColor,
				color.Darken(0.35f).CGColor }), 
			                           new PointF(0.5f, rect.Top),
			                           new PointF(0.5f, rect.Bottom), 0);
			
			context.SetStrokeColor(color.CGColor);
			context.SetLineDash(0, new float[0], 0);
			context.SetLineWidth(1);
			context.MoveTo(rect.Left, rect.Bottom - 0.5f);
			context.AddLineToPoint(rect.Right, rect.Bottom - 0.5f);
			context.MoveTo(rect.Right - 0.5f, rect.Top);
			context.AddLineToPoint(rect.Right - 0.5f, rect.Bottom);
			context.StrokePath();
		}
	}

}
