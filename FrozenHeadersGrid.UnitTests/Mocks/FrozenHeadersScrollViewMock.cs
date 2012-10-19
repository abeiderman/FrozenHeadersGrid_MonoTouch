using System;
using System.Drawing;

namespace FrozenHeadersGrid.UnitTests
{
	public class FrozenHeadersScrollViewMock : FrozenHeadersScrollView
	{
		public Action SetNeedsLayoutDelegate { get; set; }

		public FrozenHeadersScrollViewMock () : base()
		{
		}

		public FrozenHeadersScrollViewMock (RectangleF frame) : base(frame)
		{
		}

		public override void SetNeedsLayout ()
		{
			if (SetNeedsLayoutDelegate != null)
				SetNeedsLayoutDelegate();

			base.SetNeedsLayout ();
		}
	}
}

