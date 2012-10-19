using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace FrozenHeadersGrid
{
	public static class UIColorExtensions
	{
		public static UIColor Darken(this UIColor color, float factor)
		{
			float hue, saturation, brightness, alpha;
			color.GetHSBA(out hue, out saturation, out brightness, out alpha);
			return UIColor.FromHSBA(hue, saturation, Math.Max(0, brightness * (1 - factor)), alpha);
		}

		public static UIColor Brighten(this UIColor color, float factor)
		{
			float hue, saturation, brightness, alpha;
			color.GetHSBA(out hue, out saturation, out brightness, out alpha);
			return UIColor.FromHSBA(hue, saturation, Math.Min(1, brightness * (1 + factor)), alpha);
		}
	}
}