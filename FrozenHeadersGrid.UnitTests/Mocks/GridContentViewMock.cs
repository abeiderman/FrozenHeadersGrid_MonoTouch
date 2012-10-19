using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests
{
	public class GridContentViewMock : GridContentView
	{
        public Action SetNeedsLayoutDelegate { get; set; }

        public override void SetNeedsLayout()
        {
            base.SetNeedsLayout();
            if (SetNeedsLayoutDelegate != null)
                SetNeedsLayoutDelegate();
        }
	}
}

