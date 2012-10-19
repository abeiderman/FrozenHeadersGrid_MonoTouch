using System;
using NUnit.Framework;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid.UnitTests
{
	[TestFixture]
	public class UIColorExtensionsTest
	{
		const float ERROR_MARGIN = 0.00001f;

		[Test]
		public void Darken_ReducesColorBrightnessByFactor()
		{
			var color = UIColor.FromHSBA(0.1f, 0.8f, 0.5f, 0.9f);

			var darkerColor = color.Darken(0.2f);

			float hue, saturation, brightness, alpha;
			darkerColor.GetHSBA(out hue, out saturation, out brightness, out alpha);
			Assert.AreEqual(0.1f, hue, ERROR_MARGIN, "Hue");
			Assert.AreEqual(0.8f, saturation, ERROR_MARGIN, "Saturation");
			Assert.AreEqual(0.9f, alpha, ERROR_MARGIN, "Alpha");
			Assert.AreEqual(0.4f, brightness, ERROR_MARGIN, "Brightness");
		}

		[Test]
		public void Darken_WhenFactorIsGreaterThanOne_ReturnsBlackColorWithPreservedAlpha()
		{
			var color = UIColor.FromHSBA(0.1f, 0.8f, 0.5f, 0.8f);
            
			var darkerColor = color.Darken(1.1f);
            
			float hue, saturation, brightness, alpha;
			darkerColor.GetHSBA(out hue, out saturation, out brightness, out alpha);
			Assert.AreEqual(0, hue, ERROR_MARGIN, "Hue");
			Assert.AreEqual(0, saturation, ERROR_MARGIN, "Saturation");
			Assert.AreEqual(0.8f, alpha, ERROR_MARGIN, "Alpha");
			Assert.AreEqual(0, brightness, ERROR_MARGIN, "Brightness");
		}

		[Test]
		public void Brighten_IncreasesColorBrightnessByFactor()
		{
			var color = UIColor.FromHSBA(0.1f, 0.8f, 0.5f, 0.9f);
            
			var darkerColor = color.Brighten(0.2f);
            
			float hue, saturation, brightness, alpha;
			darkerColor.GetHSBA(out hue, out saturation, out brightness, out alpha);
			Assert.AreEqual(0.1f, hue, ERROR_MARGIN, "Hue");
			Assert.AreEqual(0.8f, saturation, ERROR_MARGIN, "Saturation");
			Assert.AreEqual(0.9f, alpha, ERROR_MARGIN, "Alpha");
			Assert.AreEqual(0.6f, brightness, ERROR_MARGIN, "Brightness");
		}

		[Test]
		public void Brighten_WhenFactorIsGreaterThanOne_SetsBrighnessToOne()
		{
			var color = UIColor.FromHSBA(0.1f, 0.8f, 0.5f, 0.8f);
            
			var darkerColor = color.Brighten(1.1f);
            
			float hue, saturation, brightness, alpha;
			darkerColor.GetHSBA(out hue, out saturation, out brightness, out alpha);
			Assert.AreEqual(0.1f, hue, ERROR_MARGIN, "Hue");
			Assert.AreEqual(0.8f, saturation, ERROR_MARGIN, "Saturation");
			Assert.AreEqual(0.8f, alpha, ERROR_MARGIN, "Alpha");
			Assert.AreEqual(1, brightness, ERROR_MARGIN, "Brightness");
		}        
	}
}
