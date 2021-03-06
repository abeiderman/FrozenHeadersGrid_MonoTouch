FrozenHeadersGrid for [MonoTouch](http://www.xamarin.com/monotouch)
===============================

FrozenHeadersGrid is a bare-bones grid view that allows scrolling the contents of the grid while maintaining the headers visible.

![Frozen Headers Grid on iPhone](https://github.com/abeiderman/FrozenHeadersGrid_MonoTouch/raw/master/Screenshot.png)

How to Use FrozenHeadersGrid
----------------------------

Instantiate the grid view with a rectangle.  For instance, in your view controller's ViewDidLoad method:

```csharp
var gridView = new FrozenHeadersGridView(View.Bounds);
```

Either implement the IFrozenHeadersGridViewDelegate interface or extend the FrozenHeadersGridViewDelegate class:

```csharp
class GridDelegate : FrozenHeadersGridViewDelegate
{
	public int NumberOfColumns(FrozenHeadersGridView gridView)
	{
	    return 5;
	}
	
	public int NumberOfRows(FrozenHeadersGridView gridView)
	{
	    return 5;
	}
	
	public string TitleForColumn(FrozenHeadersGridView gridView, int column)
	{
	    return string.Format("Column {0}", column + 1);
	}
	
	public string TitleForRow(FrozenHeadersGridView gridView, int row)
	{
	    return string.Format("Row {0}", row + 1);
	}
	
	public UIView ViewForCell(FrozenHeadersGridView gridView, Point cell)
	{
	    var view = new UIView();
	    var label = new UILabel(view.Bounds);
	    label.AutoresizingMask = UIViewAutoresizing.FlexibleWidth |
	    							UIViewAutoresizing.FlexibleHeight;
	    label.Text = String.Format("Cell {0},{1}", cell.X + 1, cell.Y + 1);
	    view.AddSubview(label);
	    return view;
	}
}
```
        
Set the Delegate property of your grid view to an instance of the delegate you implemented:

```csharp
var gridView = new FrozenHeadersGridView(View.Bounds);
gridView.Delegate = new GridDelegate();
```
