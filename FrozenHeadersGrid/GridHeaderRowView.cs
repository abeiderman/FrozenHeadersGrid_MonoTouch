using System;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid
{
	public class GridHeaderRowView : GridHeaderView
	{
		protected override void LayoutView(UIView view, int index)
		{
			view.Frame = new RectangleF((CellSize.Width + Gridlines.Thickness) * index, 0, CellSize.Width, CellSize.Height);
		}

		protected override void AdjustFrame()
		{
			Frame = new RectangleF(Frame.Location.X, Frame.Location.Y, 
                                   (HeadersCount * CellSize.Width) + (Gridlines.Thickness * (HeadersCount - 1)), 
                                   CellSize.Height);
		}

		protected override void DrawGradient(RectangleF rect, CGContext context)
		{
			context.DrawLinearGradient(new CGGradient(CGColorSpace.CreateDeviceRGB(), 
			                                          new CGColor[] {
				TintColor.CGColor,
				TintColor.Darken(TintDarkenFactor).CGColor
			}), 
			                           new PointF(0.5f, rect.Top), 
			                           new PointF(0.5f, rect.Bottom), 0);
		}

		protected override void DrawGridlines(RectangleF rect, CGContext context)
		{
			float maxX = rect.Size.Width;
			float cellWidth = CellSize.Width;
			for (float x = cellWidth; x < maxX; x += (cellWidth + Gridlines.Thickness))
			{
				context.MoveTo(x + 0.5f, rect.Location.Y);
				context.AddLineToPoint(x + 0.5f, rect.Size.Height);
			}
			context.StrokePath();
		}
	}
}