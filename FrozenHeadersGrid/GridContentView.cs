using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace FrozenHeadersGrid
{
	public class GridContentView : IndexedView<Point>
	{
		GridlineStyle verticalGridlines = new GridlineStyle();
		GridlineStyle horizontalGridlines = new GridlineStyle();
		Size gridSize;
		SizeF cellSize;

		public GridContentView() : base()
		{
			ContentMode = UIViewContentMode.Redraw;
		}
		
		public Size GridSize
		{
			set
			{
				gridSize = value;
				AdjustFrame();
			}
		}

		public SizeF CellSize
		{ 
			get { return cellSize; }
			set
			{
				cellSize = value;
				AdjustFrame();
			}
		}

		void AdjustFrame()
		{
			Frame = new RectangleF(Frame.X, Frame.Y, 
                                   (gridSize.Width * CellSize.Width) + (verticalGridlines.Thickness * (gridSize.Width - 1)),
                                   (gridSize.Height * CellSize.Height) + (horizontalGridlines.Thickness * (gridSize.Height - 1)));
			SetNeedsLayout();
		}

		protected override void LayoutView(UIView view, Point gridLocation)
		{
			view.Frame = new RectangleF((CellSize.Width + verticalGridlines.Thickness) * gridLocation.X, 
                                        (CellSize.Height + horizontalGridlines.Thickness) * gridLocation.Y, 
                                        CellSize.Width, CellSize.Height);
		}

		public GridlineStyle VerticalGridlines { get { return verticalGridlines; } }

		public GridlineStyle HorizontalGridlines { get { return horizontalGridlines; } }

		public override void Draw(RectangleF rect)
		{
			CGContext context = UIGraphics.GetCurrentContext();
			context.SetLineDash(0, new float[0], 0);

			if (horizontalGridlines.Thickness > 0)
				DrawHorizontalGridLines(rect, context);

			if (verticalGridlines.Thickness > 0)
				DrawVerticalGridLines(rect, context);
		}

		void DrawHorizontalGridLines(RectangleF rect, CGContext context)
		{
			float maxY = rect.Size.Height;
			context.SetLineWidth(horizontalGridlines.Thickness);
			context.SetStrokeColor(horizontalGridlines.Color.CGColor);
			var rowHeight = CellSize.Height;
			for (float y = rowHeight; y < maxY; y += (rowHeight + horizontalGridlines.Thickness))
			{
				context.MoveTo(rect.Location.X, y + 0.5f);
				context.AddLineToPoint(rect.Size.Width, y + 0.5f);
			}
			context.StrokePath();
		}

		void DrawVerticalGridLines(RectangleF rect, CGContext context)
		{
			float maxX = rect.Size.Width;
			context.SetLineWidth(verticalGridlines.Thickness);
			context.SetStrokeColor(verticalGridlines.Color.CGColor);
			var columnWidth = CellSize.Width;
			for (float x = columnWidth; x < maxX; x += (columnWidth + verticalGridlines.Thickness))
			{
				context.MoveTo(x + 0.5f, rect.Location.Y);
				context.AddLineToPoint(x + 0.5f, rect.Size.Height);
			}
			context.StrokePath();
		}
	}
}