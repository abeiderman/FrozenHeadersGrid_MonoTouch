using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.CoreAnimation;

namespace FrozenHeadersGrid
{
	public class OuterShadow
	{
		readonly IDictionary<Edge, CAGradientLayer> shadows = new Dictionary<Edge, CAGradientLayer>();
		readonly ShadowLayerCreator shadowCreator = new ShadowLayerCreator();
		readonly CALayer targetLayer;

		public OuterShadow(CALayer targetLayer, SizeF frameSize, float shadowDepth)
		{
			this.targetLayer = targetLayer;
			FrameSize = frameSize;
			ShadowDepth = shadowDepth;
			shadows[Edge.Top] = shadows[Edge.Right] = shadows[Edge.Bottom] = shadows[Edge.Left] = null;
		}

		public SizeF FrameSize { get; set; }

		public float ShadowDepth { get; set; }

		public IOuterShadowDelegate Delegate { get; set; }

		public void LayoutShadows()
		{
			LayoutLeftShadow();
			LayoutTopShadow();
			LayoutRightShadow();
			LayoutBottomShadow();
		}

		void LayoutLeftShadow()
		{
			if (Delegate.LeftShadowStartX > 0)
			{
				CreateOrReInsertShadowLayer(Edge.Left, shadowCreator.CreateRightToLeftShadow);
				SetShadowLayerFrame(Edge.Left, new RectangleF(Delegate.LeftShadowStartX - ShadowDepth, 
                                                              0f, ShadowDepth, FrameSize.Height));
			}
			else
			{
				RemoveShadowLayer(Edge.Left);
			}
		}

		void LayoutTopShadow()
		{
			if (Delegate.TopShadowStartY > 0)
			{
				CreateOrReInsertShadowLayer(Edge.Top, shadowCreator.CreateBottomToTopShadow);
				SetShadowLayerFrame(Edge.Top, new RectangleF(0f, Delegate.TopShadowStartY - ShadowDepth, 
                                                             FrameSize.Width, ShadowDepth));
			}
			else
			{
				RemoveShadowLayer(Edge.Top);
			}
		}

		void LayoutRightShadow()
		{
			if (Delegate.RightShadowStartX < FrameSize.Width)
			{
				CreateOrReInsertShadowLayer(Edge.Right, shadowCreator.CreateLeftToRightShadow);
				SetShadowLayerFrame(Edge.Right, new RectangleF(Delegate.RightShadowStartX, 0f, ShadowDepth, FrameSize.Height));
			}
			else
			{
				RemoveShadowLayer(Edge.Right);
			}    
		}

		void LayoutBottomShadow()
		{
			if (Delegate.BottomShadowStartY < FrameSize.Height)
			{
				CreateOrReInsertShadowLayer(Edge.Bottom, shadowCreator.CreateTopToBottomShadow);
				SetShadowLayerFrame(Edge.Bottom, new RectangleF(0f, Delegate.BottomShadowStartY, FrameSize.Width, ShadowDepth));
			}
			else
			{
				RemoveShadowLayer(Edge.Bottom);
			}    
		}

		void CreateOrReInsertShadowLayer(Edge edge, Func<CAGradientLayer> creatorDelegate)
		{
			if (shadows[edge] == null)
				shadows[edge] = creatorDelegate();
			else
				shadows[edge].RemoveFromSuperLayer();
            
			targetLayer.InsertSublayer(shadows[edge], 0);
		}
        
		void SetShadowLayerFrame(Edge edge, RectangleF frame)
		{
			CATransaction.Begin();
			CATransaction.DisableActions = true;
			shadows[edge].Frame = frame;
			CATransaction.Commit();
		}
        
		void RemoveShadowLayer(Edge edge)
		{
			if (shadows[edge] == null)
				return;
            
			shadows[edge].RemoveFromSuperLayer();
			shadows[edge] = null;
		}

		enum Edge
		{
			Top,
			Right,
			Bottom,
			Left
		}
	}

	public interface IOuterShadowDelegate
	{
		float LeftShadowStartX { get; }
        
		float RightShadowStartX { get; }
        
		float TopShadowStartY { get; }
        
		float BottomShadowStartY { get; }
	}    
}