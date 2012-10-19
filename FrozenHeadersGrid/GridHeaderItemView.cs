using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid
{
	public class GridHeaderItemView : UIView
	{
		UILabel textLabel;

		public GridHeaderItemView(RectangleF frame) : base(frame)
		{
			Initialize();
		}

		public GridHeaderItemView()
		{
			Initialize();
		}

		void Initialize()
		{
			textLabel = new UILabel();
			textLabel.BackgroundColor = UIColor.Clear;
			textLabel.Frame = Bounds;
			AddSubview(textLabel);
		}

		public UILabel TextLabel
		{
			get { return textLabel; }
		}

		public override RectangleF Frame
		{
			get { return base.Frame; }
			set
			{
				base.Frame = value;
				if (textLabel != null)
					textLabel.Frame = Bounds;
			}
		}
	}
}

