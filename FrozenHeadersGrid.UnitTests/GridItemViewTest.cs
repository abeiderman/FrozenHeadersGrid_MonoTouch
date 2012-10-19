
using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class GridItemViewTest
	{
		[Test]
		public void Constructor_AddsTitleLabelToSubviews()
		{
			var gridView = new GridItemView();

			Assert.True(gridView.Subviews.Contains(gridView.TitleLabel));
		}

		[Test]
		public void Constructor_AddsSubtitleLabelToSubviews()
		{
			var gridView = new GridItemView();

			Assert.True(gridView.Subviews.Contains(gridView.SubtitleLabel));
		}

		[Test]
		public void Constructor_AddsGestureRecognizers()
		{
			var gridView = new GridItemView();

			var recognizer = gridView.GestureRecognizers.Single(
				gesture => ((UITapGestureRecognizer)gesture).NumberOfTapsRequired == 1);

			Assert.NotNull(recognizer);
		}

		[Test]
		public void HandleSingleTap_WhenEventHasDelegates_RaisesDidTouchUpInsideEvent()
		{
			var gridView = new GridItemView();
			var touchedUpInside = false;
			gridView.DidTouchUpInside += (item, sender) => touchedUpInside = (item == gridView);

			gridView.HandleSingleTap(new UITapGestureRecognizer());

			Assert.True(touchedUpInside);
		}
	}
}
