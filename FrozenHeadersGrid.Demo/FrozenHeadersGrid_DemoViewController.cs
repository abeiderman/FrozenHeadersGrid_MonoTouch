using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace FrozenHeadersGrid.Demo
{
	public partial class FrozenHeadersGrid_DemoViewController : UIViewController, IFrozenHeadersGridViewDelegate
	{
        readonly static string[] ColumnTitles = new [] {"A", "B", "C", "D", "E", "F", "G", "H"};
        readonly static UIColor TintColor = UIColor.FromRGB(71.0f / 255.0f, 141.0f / 255.0f, 18.0f / 255.0f);
		FrozenHeadersGridView gridView;
		CornerView cornerView = new CornerView();

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public FrozenHeadersGrid_DemoViewController ()
			: base (UserInterfaceIdiomIsPhone ? "FrozenHeadersGrid_DemoViewController_iPhone" : "FrozenHeadersGrid_DemoViewController_iPad", null)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            View.Frame = UIScreen.MainScreen.Bounds;
			gridView = new FrozenHeadersGridView(View.Bounds);
            gridView.Delegate = this;
			gridView.ContentView.HorizontalGridlines.Color = UIColor.LightGray;
			gridView.ContentView.VerticalGridlines.Color = UIColor.LightGray;
            gridView.HeaderRowView.Gridlines.Color = TintColor;
            gridView.HeaderColumnView.Gridlines.Color = TintColor;
            gridView.HeaderRowTintColor = TintColor;
            gridView.HeaderColumnTintColor = TintColor;

			cornerView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			gridView.HeaderCornerView.AddSubview(cornerView);

            View.AddSubview(gridView);
        }

		[Obsolete]
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			if (UserInterfaceIdiomIsPhone) {
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} else {
				return true;
			}
		}

		public override bool ShouldAutorotate ()
		{
			if (UserInterfaceIdiomIsPhone)
				return UIDevice.CurrentDevice.Orientation != UIDeviceOrientation.PortraitUpsideDown;
			else
				return true;
		}

		#region FrozenHeadersGridViewDelegate implementation
		public int GetColumnCount (FrozenHeadersGridView gridView)
		{
			return ColumnTitles.Length;
		}

		public int GetRowCount (FrozenHeadersGridView gridView)
		{
			return 25;
		}

		public string GetTitleForHeaderRowItem (FrozenHeadersGridView gridView, int column)
		{
            return string.Format("Column {0}", ColumnTitles[column]);
		}

		public string GetTitleForHeaderColumnItem (FrozenHeadersGridView gridView, int row)
		{
			return string.Format("Row {0}", row + 1);
		}

		public UIView GetViewForGrid (FrozenHeadersGridView gridView, int column, int row)
		{
			var view = new GridItemView();
			view.BackgroundColor = UIColor.White;
			var label = new UILabel(view.Bounds);
			label.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			label.TextAlignment = UITextAlignment.Center;
			label.Text = String.Format("Cell {0}{1}", ColumnTitles[column], row + 1);
            label.TextColor = UIColor.DarkTextColor;
			view.AddSubview(label);
			return view;
		}
		#endregion

		class CornerView : UIView
		{
			public override void Draw (RectangleF rect)
			{
				CGContext context = UIGraphics.GetCurrentContext ();
				context.DrawLinearGradient(new CGGradient(CGColorSpace.CreateDeviceRGB(), 
			                                          new CGColor[] { TintColor.CGColor, TintColor.Darken(0.35f).CGColor }), 
			                           new PointF(0.5f, rect.Top), 
			                           new PointF(0.5f, rect.Bottom), 0);

                context.SetStrokeColor(TintColor.CGColor);
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
}

