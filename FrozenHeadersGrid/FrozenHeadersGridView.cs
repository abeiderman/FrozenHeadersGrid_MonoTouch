using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace FrozenHeadersGrid
{
	public class FrozenHeadersGridView : UIView
	{
		const float DefaultMinimumColumnWidth = 120;
		const float DefaultRowHeight = 55;
		const float DefaultHeaderColumnWidth = 60;
		const float DefaultHeaderRowHeight = 55;
		FrozenHeadersScrollView frozenHeadersScrollView;
		GridHeaderRowView gridHeaderRowView;
		GridHeaderColumnView gridHeaderColumnView;
		GridContentView gridContentView;
		int columnCount;
		int rowCount;
		IFrozenHeadersGridViewDelegate @delegate;

		public FrozenHeadersGridView(RectangleF frame) : base(frame)
		{
			MinimumColumnWidth = DefaultMinimumColumnWidth;
			RowHeight = DefaultRowHeight;
			Initialize();
		}

		public float MinimumColumnWidth { get; set; }

		public float RowHeight { get; set; }

		public float HeaderColumnWidth
		{
			get { return frozenHeadersScrollView.HeaderColumnWidth; } 
			set { frozenHeadersScrollView.HeaderColumnWidth = value; }
		}

		public float HeaderRowHeight
		{ 
			get { return frozenHeadersScrollView.HeaderRowHeight; }
			set { frozenHeadersScrollView.HeaderRowHeight = value; }
		}

		public IFrozenHeadersGridViewDelegate Delegate
		{
			get { return @delegate; }
			set
			{
				 @delegate = value;
				UpdateContent();
			}
		}
        
		public UIColor HeaderRowTintColor
		{
			get { return gridHeaderRowView.TintColor; }
			set { gridHeaderRowView.TintColor = value; }
		}

		public UIColor HeaderColumnTintColor
		{
			get { return gridHeaderColumnView.TintColor; }
			set { gridHeaderColumnView.TintColor = value; }
		}

		public GridContentView ContentView { get { return gridContentView; } }

		public GridHeaderView HeaderRowView { get { return gridHeaderRowView; } }

		public GridHeaderView HeaderColumnView { get { return gridHeaderColumnView; } }

		public UIView HeaderCornerView { get { return frozenHeadersScrollView.HeaderCornerView; } }
        
		void Initialize()
		{
			BackgroundColor = UIColor.Clear;
            
			frozenHeadersScrollView = new FrozenHeadersScrollView(Bounds);
			HeaderRowHeight = DefaultHeaderRowHeight;
			HeaderColumnWidth = DefaultHeaderColumnWidth;

			gridHeaderRowView = new GridHeaderRowView();
			gridHeaderRowView.Gridlines.Thickness = 1;
			frozenHeadersScrollView.HeaderRow.AddSubview(gridHeaderRowView);

			gridHeaderColumnView = new GridHeaderColumnView();
			gridHeaderColumnView.Gridlines.Thickness = 1;
			frozenHeadersScrollView.HeaderColumn.AddSubview(gridHeaderColumnView);
        
			gridContentView = new GridContentView();
			gridContentView.VerticalGridlines.Thickness = 1;
			gridContentView.HorizontalGridlines.Thickness = 1;
			frozenHeadersScrollView.ContentView.AddSubview(gridContentView);
                
			AddSubview(frozenHeadersScrollView);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			var columnWidth = CalcualteColumnWidth();

			ContentView.CellSize = new SizeF(columnWidth, RowHeight);
			ContentView.GridSize = new Size(columnCount, rowCount);
			ContentView.LayoutIfNeeded();

			frozenHeadersScrollView.Frame = Bounds;            
			frozenHeadersScrollView.HeaderColumn.ContentSize = new SizeF(HeaderColumnWidth, RowHeight * rowCount);
			frozenHeadersScrollView.HeaderRow.ContentSize = new SizeF(columnWidth * columnCount, HeaderRowHeight);
			frozenHeadersScrollView.ContentView.ContentSize = ContentView.Frame.Size;
			frozenHeadersScrollView.LayoutIfNeeded();

			gridHeaderRowView.CellSize = new SizeF(columnWidth, HeaderRowHeight);
			gridHeaderRowView.HeadersCount = columnCount;
			gridHeaderRowView.LayoutIfNeeded();

			gridHeaderColumnView.CellSize = new SizeF(HeaderColumnWidth, RowHeight);
			gridHeaderColumnView.HeadersCount = rowCount;
			gridHeaderColumnView.LayoutIfNeeded();
		}

		float CalcualteColumnWidth()
		{
			return columnCount > 0 ? (float)Math.Max(Math.Round((Bounds.Size.Width - HeaderColumnWidth) / columnCount), MinimumColumnWidth) : 0;
		}
        
		public virtual void UpdateContent()
		{
			Clear();
			columnCount = @delegate.GetColumnCount(this);
			rowCount = @delegate.GetRowCount(this);

			UpdateHeaderRow();
			UpdateHeaderColumn();
			UpdateContentViews();

			SetNeedsLayout();
		}

		void Clear()
		{
			gridHeaderRowView.RemoveAll();
			gridHeaderColumnView.RemoveAll();
			gridContentView.RemoveAll();
		}
        
		void UpdateHeaderRow()
		{
			gridHeaderRowView.HeadersCount = columnCount;

			for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
				AddHeaderRowItemView(columnIndex);
		}

		void AddHeaderRowItemView(int columnIndex)
		{
			var item = new GridHeaderItemView();
			item.TextLabel.Text = @delegate.GetTitleForHeaderRowItem(this, columnIndex);
			item.TextLabel.TextAlignment = UITextAlignment.Center;
			item.TextLabel.TextColor = UIColor.White;
			gridHeaderRowView[columnIndex] = item;
		}

		void UpdateHeaderColumn()
		{
			gridHeaderColumnView.HeadersCount = rowCount;

			for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
				AddHeaderColumnItemView(rowIndex);
		}

		void AddHeaderColumnItemView(int rowIndex)
		{
			var item = new GridHeaderItemView();
			item.TextLabel.Text = @delegate.GetTitleForHeaderColumnItem(this, rowIndex);
			item.TextLabel.TextAlignment = UITextAlignment.Left;
			item.TextLabel.TextColor = UIColor.White;
			gridHeaderColumnView[rowIndex] = item;
		}

		void UpdateContentViews()
		{
			ContentView.GridSize = new Size(columnCount, rowCount);
            
			for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
				UpdateContentViewRow(rowIndex);
		}

		void UpdateContentViewRow(int rowIndex)
		{
			for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
				ContentView[new Point(columnIndex, rowIndex)] = @delegate.GetViewForGrid(this, columnIndex, rowIndex);
		}
	}

	public interface IFrozenHeadersGridViewDelegate
	{
		int GetColumnCount(FrozenHeadersGridView gridView);

		int GetRowCount(FrozenHeadersGridView gridView);

		string GetTitleForHeaderRowItem(FrozenHeadersGridView gridView, int column);

		string GetTitleForHeaderColumnItem(FrozenHeadersGridView gridView, int row);

		UIView GetViewForGrid(FrozenHeadersGridView gridView, int column, int row);
	}
}