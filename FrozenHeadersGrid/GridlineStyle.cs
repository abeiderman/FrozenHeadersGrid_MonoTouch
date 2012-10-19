using System;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid
{
	public class GridlineStyle
	{
		public GridlineStyle()
		{
			Color = UIColor.Clear;
		}

		public float Thickness { get; set; }

		public UIColor Color { get; set; }
	}
}