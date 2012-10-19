using System;
using NUnit.Framework;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class GridlineStyleTest
	{
		[Test]
		public void Constructor_InitializesColorToClear()
		{
			var style = new GridlineStyle();

			Assert.AreEqual(UIColor.Clear, style.Color);
		}
	}
}
