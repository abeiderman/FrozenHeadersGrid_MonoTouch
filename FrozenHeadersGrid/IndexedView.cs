using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace FrozenHeadersGrid
{
	public abstract class IndexedView<TIndex> : UIView
	{
		Dictionary<TIndex, WeakReference> views = new Dictionary<TIndex, WeakReference>();

		public UIView this[TIndex index]
		{
			get
			{
				WeakReference referenceToView = null;
				views.TryGetValue(index, out referenceToView);
				return referenceToView == null ? null : referenceToView.Target as UIView;
			}
			set
			{
				if (value == null)
					return;

				RemoveAt(index);
				AddSubview(value);               
				views.Add(index, new WeakReference(value));
				SetNeedsLayout();
			}
		}
        
		public void RemoveAt(TIndex index)
		{
			var view = this[index];
			if (view != null)
				view.RemoveFromSuperview();
			views.Remove(index);
		}

		public void RemoveAll()
		{
			foreach (var key in views.Keys)
			{
				var view = views[key].Target as UIView;
				if (view != null)
					view.RemoveFromSuperview();
			}
			views.Clear();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			foreach (var key in views.Keys)
			{
				var view = views[key].Target as UIView;
				if (view != null)
					LayoutView(view, key);
			}
		}
        
		protected abstract void LayoutView(UIView view, TIndex index);
	}
}

