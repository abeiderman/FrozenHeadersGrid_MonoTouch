using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace FrozenHeadersGrid
{
	public class GridHeaderColumnView : GridHeaderView
	{
		protected override void LayoutView(UIView view, int index)
		{
			view.Frame = new RectangleF(0, (CellSize.Height + Gridlines.Thickness) * index, CellSize.Width, CellSize.Height);
		}

		protected override void AdjustFrame()
		{
			Frame = new RectangleF(Frame.X, Frame.Y, CellSize.Width,
                                   (CellSize.Height * HeadersCount) + (Gridlines.Thickness * (HeadersCount - 1)));
		}
        
		protected override void DrawGradient(RectangleF rect, CGContext context)
		{
			context.DrawLinearGradient(new CGGradient(CGColorSpace.CreateDeviceRGB(), 
			                                          new CGColor[] {
				TintColor.CGColor,
				TintColor.Darken(TintDarkenFactor).CGColor
			}), 
			                           new PointF(rect.Left, 0.5f), 
			                           new PointF(rect.Right, 0.5f), 0);
		}
		
		protected override void DrawGridlines(RectangleF rect, CGContext context)
		{
			float maxY = rect.Size.Height;
			for (float y = CellSize.Height; y < maxY; y += (CellSize.Height + Gridlines.Thickness))
			{
				context.MoveTo(rect.Left, y + 0.5f);
				context.AddLineToPoint(rect.Right, y + 0.5f);
			}
			context.StrokePath();
		}
	}
}