using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace FrozenHeadersGrid
{
	public class FrozenHeadersScrollView : UIView, IOuterShadowDelegate
	{
		const float DefaultShadowDepth = 15.0f;
		const float DefaultShadowAlpha = 0.3f;
		UIScrollView headerRowView;
		UIScrollView headerColumnView;
		UIScrollView contentView;
		UIView cornerView;
		OuterShadow shadows;
		ScrollViewDelegate scrollDelegate;
		float headerColumnWidth;

		public FrozenHeadersScrollView()
		{
			Initialize();
		}
		
		public FrozenHeadersScrollView(RectangleF frame) : base(frame)
		{
			Initialize();
		}
		
		void Initialize()
		{
			shadows = new OuterShadow(Layer, Frame.Size, DefaultShadowDepth);
			shadows.Delegate = this;
			scrollDelegate = new ScrollViewDelegate(this);			
			BackgroundColor = UIColor.Clear;

			InitializeHeaderRow();
			InitializeHeaderColumn();
			InitializeContentView();
			InitializeCornerView();
		}

		void InitializeHeaderRow()
		{
			headerRowView = new UIScrollView { BackgroundColor = UIColor.Clear, 
				ShowsHorizontalScrollIndicator = true,
				ShowsVerticalScrollIndicator = false,
				ScrollsToTop = false,
				Bounces = true,
				Delegate = scrollDelegate };
			AddSubview(headerRowView);
		}

		void InitializeHeaderColumn()
		{
			headerColumnView = new UIScrollView { BackgroundColor = UIColor.Clear,
				ShowsHorizontalScrollIndicator = false,
				ShowsVerticalScrollIndicator = true,
				ScrollsToTop = false,
				Bounces = true,
				Delegate = scrollDelegate };
			AddSubview(headerColumnView);
		}

		void InitializeContentView()
		{
			contentView = new UIScrollView { BackgroundColor = UIColor.Clear,
				ScrollsToTop = true,
				ShowsHorizontalScrollIndicator = true,
				ShowsVerticalScrollIndicator = true,
				DirectionalLockEnabled = true,
				DecelerationRate = UIScrollView.DecelerationRateFast,
				Bounces = true,
				Delegate = scrollDelegate };
			AddSubview(contentView);
		}

		void InitializeCornerView()
		{
			cornerView = new UIView { BackgroundColor = UIColor.Clear };
			AddSubview(cornerView);
		}

		public float HeaderColumnWidth
		{ 
			get { return headerColumnWidth; }
			set
			{
				headerColumnWidth = value;
				SetNeedsLayout();
			}
		}

		public float HeaderRowHeight { get; set; }

		public UIScrollView HeaderRow { get { return headerRowView; } }

		public UIScrollView HeaderColumn { get { return headerColumnView; } }

		public UIScrollView ContentView { get { return contentView; } }

		public UIView HeaderCornerView { get { return cornerView; } }

		public override RectangleF Frame
		{
			get { return base.Frame; }
			set
			{
				base.Frame = value;
				if (shadows != null)
					shadows.FrameSize = value.Size;
			}
		}
		
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			LayoutContentView();
			LayoutHeaderRow();
			LayoutHeaderColumn();
			LayoutCornerView();
			shadows.LayoutShadows();
		}

		void LayoutContentView()
		{
			SetFrameIfNeeded(contentView, new RectangleF(HeaderColumnWidth,
                                                                HeaderRowHeight,
                                                                Bounds.Size.Width - HeaderColumnWidth,
                                                                Bounds.Size.Height - HeaderRowHeight));
		}

		void LayoutHeaderRow()
		{
			float top = contentView.ContentOffset.Y < 0 ? -contentView.ContentOffset.Y : 0;
			SetFrameIfNeeded(headerRowView, new RectangleF(HeaderColumnWidth, 
                                                               top, 
                                                               Bounds.Size.Width - HeaderColumnWidth, 
                                                               HeaderRowHeight));
		}

		void LayoutHeaderColumn()
		{
			float left = contentView.ContentOffset.X < 0 ? -contentView.ContentOffset.X : 0;
			SetFrameIfNeeded(headerColumnView, new RectangleF(left, 
                                                             HeaderRowHeight, 
                                                             HeaderColumnWidth, 
                                                             Bounds.Size.Height - HeaderRowHeight));
		}

		void LayoutCornerView()
		{
			SetFrameIfNeeded(cornerView, new RectangleF(headerColumnView.Frame.Location.X, headerRowView.Frame.Location.Y, headerColumnView.Frame.Size.Width,
                                                           headerRowView.Frame.Size.Height));
		}

		public float LeftShadowStartX
		{
			get { return headerColumnView.Frame.Location.X; }
		}

		public float RightShadowStartX
		{
			get
			{
				float rightOffset = contentView.ContentOffset.X + contentView.Frame.Size.Width - contentView.ContentSize.Width;
				return contentView.ContentSize.Width > 0 ? Frame.Size.Width - rightOffset : Frame.Size.Width;
			}
		}

		public float TopShadowStartY
		{
			get { return headerRowView.Frame.Location.Y; }
		}

		public float BottomShadowStartY
		{
			get
			{
				float bottomOffset = contentView.ContentOffset.Y + contentView.Frame.Size.Height - contentView.ContentSize.Height;
				return contentView.ContentSize.Height > 0 ? Frame.Size.Height - bottomOffset : Frame.Size.Height;
			}
		}
		
		void SetFrameIfNeeded(UIView view, RectangleF newFrame)
		{
			if (view != null && !view.Frame.Equals(newFrame))
				view.Frame = newFrame;
		}

		protected override void Dispose(bool disposing)
		{
			contentView.Delegate = null;
			headerColumnView.Delegate = null;
			headerRowView.Delegate = null;
			scrollDelegate = null;
			base.Dispose(disposing);
		}
		
		class ScrollViewDelegate : UIScrollViewDelegate
		{
			
			FrozenHeadersScrollView parent;
			
			public ScrollViewDelegate(FrozenHeadersScrollView parentView) : base()
			{
				parent = parentView;
			}
			
			public override void Scrolled(UIScrollView scrollView)
			{
				if (scrollView == parent.headerRowView)
				{
					parent.contentView.ContentOffset = new PointF(parent.headerRowView.ContentOffset.X, 
					                                                    parent.contentView.ContentOffset.Y);
				}
				else if (scrollView == parent.contentView)
				{
					parent.headerRowView.ContentOffset = new PointF(parent.contentView.ContentOffset.X, 
					                                                   parent.headerRowView.ContentOffset.Y);
					parent.headerColumnView.ContentOffset = new PointF(parent.headerColumnView.ContentOffset.X, 
					                                                 parent.contentView.ContentOffset.Y);
				}
				else if (scrollView == parent.headerColumnView)
				{
					parent.contentView.ContentOffset = new PointF(parent.contentView.ContentOffset.X, 
					                                                    parent.headerColumnView.ContentOffset.Y);
				}
				parent.SetNeedsLayout();
			}
			
			public override bool ShouldScrollToTop(UIScrollView scrollView)
			{
				return (scrollView == parent.contentView);
			}
		}
	}
}