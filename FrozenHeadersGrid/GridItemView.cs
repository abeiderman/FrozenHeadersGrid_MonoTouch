using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid
{
	public class GridItemView : UIView
	{
		UILabel titleLabel;
		UILabel subtitleLabel;

		public event DidTouchUpInside DidTouchUpInside;

		public GridItemView()
		{
			Padding = 2f;
			titleLabel = new UILabel();
			AddSubview(titleLabel);
			subtitleLabel = new UILabel();
			AddSubview(subtitleLabel);
			CreateGestureRecognizers();
		}

		void CreateGestureRecognizers()
		{
			UITapGestureRecognizer singleTap = new UITapGestureRecognizer(HandleSingleTap);
			singleTap.NumberOfTapsRequired = 1;
			AddGestureRecognizer(singleTap);
		}

		public void HandleSingleTap(UITapGestureRecognizer sender)
		{
			if (DidTouchUpInside != null)
				DidTouchUpInside(this, sender);
		}

		public UILabel TitleLabel
		{
			get { return titleLabel; }
		}

		public UILabel SubtitleLabel
		{
			get { return subtitleLabel; }
		}

		public float Padding { get; set; }
	}

	public delegate void DidTouchUpInside (GridItemView item,UIGestureRecognizer sender);
}