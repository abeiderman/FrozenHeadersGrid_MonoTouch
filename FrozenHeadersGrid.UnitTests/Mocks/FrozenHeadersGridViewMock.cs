using System;
using System.Drawing;

namespace FrozenHeadersGrid.UnitTests.Mocks
{
	internal class FrozenHeadersGridViewMock : FrozenHeadersGridView
	{
		public FrozenHeadersGridViewMock(RectangleF frame) : base (frame)
		{
		}

        public event EventHandler UpdateContentCalled;

		public new FrozenHeadersGridViewDelegateMock Delegate
		{
			get { return (FrozenHeadersGridViewDelegateMock)base.Delegate; }
			set { base.Delegate = value; }
		}

        public override void UpdateContent()
        {
            if (UpdateContentCalled != null)
                UpdateContentCalled(this, new EventArgs());

            base.UpdateContent();
        }
	}
}