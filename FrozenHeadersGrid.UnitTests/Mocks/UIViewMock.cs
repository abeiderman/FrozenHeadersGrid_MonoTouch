using System;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests.Mocks
{
	internal class UIViewMock : UIView
	{
		public Action<System.Drawing.RectangleF> SetFrameDelegate { get; set; }
		
		public override System.Drawing.RectangleF Frame
		{
			get
			{
				return base.Frame;
			}
			set
			{
				base.Frame = value;
				if (SetFrameDelegate != null)
					SetFrameDelegate.Invoke(value);
			}
		}
	}
}