using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace FrozenHeadersGrid
{
	public abstract class GridHeaderView : IndexedView<int>
	{
		protected const float TintDarkenFactor = 0.35f;
		UIColor tintColor;
		SizeF cellSize;
		readonly GridlineStyle gridlines = new GridlineStyle();
		int headersCount;

		public GridHeaderView()
		{
			BackgroundColor = UIColor.Clear;
		}

		public UIColor TintColor
		{
			get { return tintColor; }
			set
			{
				tintColor = value;
				SetNeedsDisplay();
			}
		}

		public SizeF CellSize
		{ 
			get { return cellSize; }
			set
			{
				cellSize = value;
				AdjustFrame();
				SetNeedsLayout();
			}
		}

		public int HeadersCount
		{
			get { return headersCount; }
			set
			{
				headersCount = value;
				AdjustFrame();
				SetNeedsLayout();
			}
		}

		protected abstract void AdjustFrame();

		public GridlineStyle Gridlines { get { return gridlines; } }

		public override void Draw(RectangleF rect)
		{
			CGContext context = UIGraphics.GetCurrentContext();
			if (TintColor != null)
				DrawGradient(rect, context);
			
			if (Gridlines.Thickness > 0)
			{
				context.SetStrokeColor(Gridlines.Color.CGColor);
				context.SetLineDash(0, new float[0], 0);
				context.SetLineWidth(Gridlines.Thickness);
				DrawGridlines(rect, context);
			}
		}

		protected abstract void DrawGradient(RectangleF rect, CGContext context);

		protected abstract void DrawGridlines(RectangleF rect, CGContext context);
	}
}