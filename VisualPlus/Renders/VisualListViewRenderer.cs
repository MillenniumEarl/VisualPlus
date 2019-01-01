#region Namespace

using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using VisualPlus.Constants;
using VisualPlus.Enumerators;
using VisualPlus.Managers;
using VisualPlus.Native;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Controls.DataManagement;
using VisualPlus.Toolkit.VisualBase;

using ContentAlignment = System.Drawing.ContentAlignment;

#endregion

namespace VisualPlus.Renders
{
    public static class VisualListViewRenderer
    {
        #region Methods

        /// <summary>Draw the contents of a cell, do not draw any background or associated things.</summary>
        /// <param name="graphicsCell">The graphics cell.</param>
        /// <param name="rectCell">The rectangle cell.</param>
        /// <param name="img">The image.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="cellPaddingSize">The Cell Padding Size.</param>
        /// <param name="listView">The list View.</param>
        /// <returns>Returns the area of the cell that is left for you to put anything else on.</returns>
        public static Rectangle DrawCellGraphic(Graphics graphicsCell, Rectangle rectCell, Image img, HorizontalAlignment alignment, int cellPaddingSize, VisualListView listView)
        {
            int _cellY;
            int _cellX;

            int _cellHeight = img.Height + (cellPaddingSize * 2);
            int _cellWidth = img.Width + (cellPaddingSize * 2);

            // This will only set if auto-size is true.
            listView.MaxHeight = _cellHeight;

            if ((_cellWidth > rectCell.Width) || (_cellHeight > rectCell.Height))
            {
                // Not enough room to draw the image, bail out.
                return rectCell;
            }

            if (alignment == HorizontalAlignment.Left)
            {
                _cellY = rectCell.Y + cellPaddingSize + ((rectCell.Height - _cellHeight) / 2);
                _cellX = rectCell.X + cellPaddingSize;

                graphicsCell.DrawImage(img, _cellX, _cellY);

                // Remove the width that we used for the graphic from the cell.
                rectCell.Width -= img.Width + (cellPaddingSize * 2);
                rectCell.X += _cellWidth;
            }
            else if (alignment == HorizontalAlignment.Center)
            {
                _cellY = rectCell.Y + cellPaddingSize + ((rectCell.Height - _cellHeight) / 2);
                _cellX = rectCell.X + cellPaddingSize + ((rectCell.Width - _cellWidth) / 2);

                graphicsCell.DrawImage(img, _cellX, _cellY);

                // remove the width that we used for the graphic from the cell
                // rectCell.Width -= (img.Width + (CellPaddingSize*2));
                // rectCell.X += (img.Width + (CellPaddingSize*2));
                rectCell.Width = 0;
            }
            else if (alignment == HorizontalAlignment.Right)
            {
                _cellY = rectCell.Y + cellPaddingSize + ((rectCell.Height - _cellHeight) / 2);
                _cellX = rectCell.Right - _cellWidth;

                graphicsCell.DrawImage(img, _cellX, _cellY);

                // Remove the width that we used for the graphic from the cell.
                rectCell.Width -= _cellWidth;
            }

            return rectCell;
        }

        /// <summary>Draw cell text is used by header and cell to draw properly aligned text in subitems.</summary>
        /// <param name="graphics">The graphics cell.</param>
        /// <param name="rectangle">The rectangle cell.</param>
        /// <param name="cellText">The text of the cell.</param>
        /// <param name="font">The font.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="textColor">The text color.</param>
        /// <param name="selected">The selected toggle.</param>
        /// <param name="listView">The list View.</param>
        public static void DrawCellText(Graphics graphics, Rectangle rectangle, string cellText, Font font, ContentAlignment alignment, Color textColor, bool selected, VisualListView listView)
        {
            int _interiorWidth = rectangle.Width - (listView.CellPaddingSize * 2);

            // int _interiorHeight = rectangle.Height - (cellPaddingSize * 2);

            // Convert property editor friendly alignment to an alignment we can use for strings
            StringFormat _stringFormat = new StringFormat
                {
                    Alignment = TextManager.ConvertContentAlignmentToStringAlignment(alignment, Orientation.Horizontal),
                    LineAlignment = TextManager.ConvertContentAlignmentToStringAlignment(alignment, Orientation.Vertical)
                };
            SizeF _measuredSize;
            if (listView.HeaderWordWrap)
            {
                // word wrapping is on by default for drawing
                _stringFormat.FormatFlags = 0;
                _measuredSize = graphics.MeasureString(cellText, font, new Point(listView.CellPaddingSize, listView.CellPaddingSize), _stringFormat);
            }
            else
            {
                // they aren't word wrapping so we need to put the ...'s where necessary
                _stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                _measuredSize = graphics.MeasureString(cellText, font, new Point(listView.CellPaddingSize, listView.CellPaddingSize), _stringFormat);
                if (_measuredSize.Width > _interiorWidth)
                {
                    // Don't truncate if we are doing word wrap
                    cellText = TextManager.TruncateString(cellText, font, _interiorWidth, graphics);
                }
            }

            // This will only set if auto - size is true
            listView.MaxHeight = (int)_measuredSize.Height + (listView.CellPaddingSize * 2);
            graphics.DrawString(cellText, font, new SolidBrush(textColor), rectangle /*rectCell.X+this.CellPaddingSize, rectCell.Y+this.CellPaddingSize*/, _stringFormat);
        }

        /// <summary>Draw a checkbox on the sub item.</summary>
        /// <param name="graphicsCell">The graphics cell.</param>
        /// <param name="rectCell">The rectangle cell.</param>
        /// <param name="checkToggle">The checked cell.</param>
        /// <param name="checkBoxSize">The check Box Size.</param>
        /// <param name="listView">The list View.</param>
        /// <returns>The <see cref="Rectangle" />.</returns>
        public static Rectangle DrawCheckBox(Graphics graphicsCell, Rectangle rectCell, bool checkToggle, int checkBoxSize, VisualListView listView)
        {
            int th = checkBoxSize + (listView.CellPaddingSize * 2);
            int tw = checkBoxSize + (listView.CellPaddingSize * 2);
            listView.MaxHeight = th; // this will only set if auto-size is true

            if ((tw > rectCell.Width) || (th > rectCell.Height))
            {
                return rectCell; // not enough room to draw the image, bail out
            }

            int ty = rectCell.Y + listView.CellPaddingSize + ((rectCell.Height - th) / 2);
            int tx = rectCell.X + listView.CellPaddingSize;

            Point _location = new Point(tx, ty);

            if (checkToggle)
            {
                // Checked
                // graphicsCell.DrawImage(listView.ImageList.Images[1], tx, ty);
                // graphicsCell.FillRectangle( Brushes.YellowGreen, tx, ty, checkBoxSize, checkBoxSize);
                CheckBoxRenderer.DrawCheckBox(graphicsCell, _location, CheckBoxState.CheckedNormal);
            }
            else
            {
                // Unchecked
                // graphicsCell.DrawImage(listView.ImageList.Images[0], tx, ty);
                // graphicsCell.FillRectangle( Brushes.Red, tx, ty, checkBoxSize, checkBoxSize);
                CheckBoxRenderer.DrawCheckBox(graphicsCell, _location, CheckBoxState.UncheckedNormal);
            }

            // Remove the width that we used for the graphic from the cell
            rectCell.Width -= checkBoxSize + (listView.CellPaddingSize * 2);
            rectCell.X += tw;

            return rectCell;
        }

        /// <summary>Draw the column header.</summary>
        /// <param name="graphicsColumn">The graphics column.</param>
        /// <param name="columnRectangle">The rectangle column.</param>
        /// <param name="column">The column.</param>
        /// <param name="theme">The theme.</param>
        /// <param name="listView">The list View.</param>
        public static void DrawColumnHeader(Graphics graphicsColumn, Rectangle columnRectangle, VisualListViewColumn column, IntPtr theme, VisualListView listView)
        {
            if (listView.ControlStyle == LVControlStyles.SuperFlat)
            {
                SolidBrush columnRectangleBrush;

                if (column.State == ColumnStates.None)
                {
                    columnRectangleBrush = new SolidBrush(listView.ColumnColorState.Enabled);
                    graphicsColumn.FillRectangle(columnRectangleBrush, columnRectangle);
                }
                else if (column.State == ColumnStates.Pressed)
                {
                    columnRectangleBrush = new SolidBrush(listView.ColumnColorState.Pressed);
                    graphicsColumn.FillRectangle(columnRectangleBrush, columnRectangle);
                }
                else if (column.State == ColumnStates.Hover)
                {
                    columnRectangleBrush = new SolidBrush(listView.ColumnColorState.Hover);
                    graphicsColumn.FillRectangle(columnRectangleBrush, columnRectangle);
                }
            }
            else if ((listView.ControlStyle == LVControlStyles.XP) && listView.ThemesAvailable)
            {
                // This is really the only thing we care about for theme right now inside the control.
                IntPtr hDC = graphicsColumn.GetHdc();

                RECT _columnRect = new RECT(columnRectangle.X, columnRectangle.Y, columnRectangle.Right, columnRectangle.Bottom);
                RECT _clipRect = new RECT(columnRectangle.X, columnRectangle.Y, columnRectangle.Right, columnRectangle.Bottom);

                if (column.State == ColumnStates.None)
                {
                    // Normal.
                    uxtheme.DrawThemeBackground(theme, hDC, 1, 1, ref _columnRect, ref _clipRect);
                }
                else if (column.State == ColumnStates.Pressed)
                {
                    // Pressed
                    uxtheme.DrawThemeBackground(theme, hDC, 1, 3, ref _columnRect, ref _clipRect);
                }
                else if (column.State == ColumnStates.Hover)
                {
                    // Hover
                    uxtheme.DrawThemeBackground(theme, hDC, 1, 2, ref _columnRect, ref _clipRect);
                }

                graphicsColumn.ReleaseHdc(hDC);
            }
            else
            {
                // Normal state.
                if (column.State != ColumnStates.Pressed)
                {
                    ControlPaint.DrawButton(graphicsColumn, columnRectangle, ButtonState.Normal);
                }
                else
                {
                    ControlPaint.DrawButton(graphicsColumn, columnRectangle, ButtonState.Pushed);
                }
            }

            // Check if we need checkboxes in this column.
            if (column.CheckBox)
            {
                columnRectangle = DrawCheckBox(graphicsColumn, columnRectangle, column.Checked, ListViewConstants.CHECKBOX_SIZE, listView);
            }

            // If there is an image, this routine will RETURN with exactly the space left for everything else after the image is drawn (or not drawn due to lack of space).
            if ((column.ImageIndex > -1) && (listView.ImageListColumns != null) && (column.ImageIndex < listView.ImageListColumns.Images.Count))
            {
                columnRectangle = DrawCellGraphic(graphicsColumn, columnRectangle, listView.ImageListColumns.Images[column.ImageIndex], HorizontalAlignment.Left, listView.CellPaddingSize, listView);
            }

            // Draw column header grid
            if (listView.GridLineStyle != GridLineStyle.None)
            {
                graphicsColumn.DrawLine(new Pen(listView.GridColor), new Point(columnRectangle.Right, 0), new Point(columnRectangle.Right, columnRectangle.Bottom));
            }

            DrawCellText(graphicsColumn, columnRectangle, column.Text, listView.Font, column.TextAlignment, listView.ForeColor, false, listView);
        }

        /// <summary>Draw the column headers.</summary>
        /// <param name="graphicHeader">The graphics header.</param>
        /// <param name="sizeHeader">The size header.</param>
        /// <param name="listView">The list View.</param>
        /// <param name="hPanelScrollBar">The h Panel Scroll Bar.</param>
        /// <param name="theme">The theme.</param>
        public static void DrawColumnHeaders(Graphics graphicHeader, Size sizeHeader, VisualListView listView, ManagedHScrollBar hPanelScrollBar, IntPtr theme)
        {
            // Draw the column header background.
            if (listView.ControlStyle == LVControlStyles.SuperFlat)
            {
                SolidBrush _headerRectangleBrush = new SolidBrush(listView.ColumnColorState.Enabled);
                graphicHeader.FillRectangle(_headerRectangleBrush, listView.HeaderRectangle);
                _headerRectangleBrush.Dispose();
            }
            else
            {
                graphicHeader.FillRectangle(SystemBrushes.Control, listView.HeaderRectangle);
            }

            if (listView.Columns.Count <= 0)
            {
                return;
            }

            // Draw vertical lines first then horizontal lines.
            int _currentX = -hPanelScrollBar.Value + listView.HeaderRectangle.X;
            foreach (VisualListViewColumn column in listView.Columns)
            {
                // Cull columns that won't be drawn first.
                if (_currentX + column.Width < 0)
                {
                    _currentX += column.Width;

                    // Skip this column, its not being drawn.
                    continue;
                }

                if (_currentX > listView.HeaderRectangle.Right)
                {
                    // Were past the end of the visible column, stop drawing.
                    return;
                }

                if (column.Width > 0)
                {
                    DrawColumnHeader(graphicHeader, new Rectangle(_currentX, listView.HeaderRectangle.Y, column.Width, listView.HeaderHeight), column, theme, listView);
                }

                // Move the parser.
                _currentX += column.Width;
            }
        }

        /// <summary>Draw grid lines in client area.</summary>
        /// <param name="rowsDC">Graphics dc.</param>
        /// <param name="vPanelScrollBar">The v Panel Scroll Bar.</param>
        /// <param name="hPanelScrollBar">The h Panel Scroll Bar.</param>
        /// <param name="listView">The list View.</param>
        public static void DrawGridLines(Graphics rowsDC, ManagedVScrollBar vPanelScrollBar, ManagedHScrollBar hPanelScrollBar, VisualListView listView)
        {
            int _yCursor = listView.RowsInnerClientRect.Y;
            using (Pen _gridPen = new Pen(listView.GridColor))
            {
                // Determine the grid line style.
                if (listView.GridLineStyle == GridLineStyle.Dashed)
                {
                    _gridPen.DashStyle = DashStyle.Dash;
                }
                else if (listView.GridLineStyle == GridLineStyle.Solid)
                {
                    _gridPen.DashStyle = DashStyle.Solid;
                }
                else
                {
                    _gridPen.DashStyle = DashStyle.Solid;
                }

                // Draw horizontal grid lines.
                if ((listView.GridLines == GridLines.Both) || (listView.GridLines == GridLines.Horizontal))
                {
                    int _rowsToDraw = listView.RowsVisible + 1;
                    if (listView.GridTypes == GridTypes.Exists)
                    {
                        if (listView.RowsVisible > listView.Count)
                        {
                            _rowsToDraw = listView.Count;
                        }
                    }

                    for (var _item = 0; _item < _rowsToDraw; _item++)
                    {
                        _yCursor += listView.ItemHeight;

                        // Draw horizontal line.
                        rowsDC.DrawLine(_gridPen, 0, _yCursor, listView.RowsInnerClientRect.Width, _yCursor);
                    }
                }

                // Draw vertical grid lines.
                if ((listView.GridLines == GridLines.Both) || (listView.GridLines == GridLines.Vertical))
                {
                    int _xCursor = -hPanelScrollBar.Value;
                    for (var column = 0; column < listView.Columns.Count; column++)
                    {
                        _xCursor += listView.Columns[column].Width;

                        // Draw vertical line.
                        rowsDC.DrawLine(_gridPen, _xCursor + 1, listView.RowsInnerClientRect.Y, _xCursor + 1, listView.RowsInnerClientRect.Bottom); // draw vertical line
                    }
                }
            }
        }

        /// <summary>Draw row at specified coordinates.</summary>
        /// <param name="graphicsRow">The graphics row.</param>
        /// <param name="rectRow">The rectangle row.</param>
        /// <param name="item">The item.</param>
        /// <param name="itemIndex">The item index.</param>
        /// <param name="listView">The list View.</param>
        /// <param name="hPanelScrollBar">The h Panel Scroll Bar.</param>
        /// <param name="_newLiveControls">The _new Live Controls.</param>
        /// <param name="_liveControls">The _live Controls.</param>
        /// <param name="checkBoxSize">The check Box Size.</param>
        public static void DrawRow(Graphics graphicsRow, Rectangle rectRow, VisualListViewItem item, int itemIndex, VisualListView listView, ManagedHScrollBar hPanelScrollBar, ArrayList _newLiveControls, ArrayList _liveControls, int checkBoxSize)
        {
            // If its selected, that trumps all, if not then see if we are using alternating colors, if not draw normal
            // this can all be overridden by the sub item background property make sure anything can even be selected before drawing selection rectangles
            if (item.Selected && listView.Selectable)
            {
                using (SolidBrush _brushBackground = new SolidBrush(Color.FromArgb(255, listView.ItemSelectedColor.R, listView.ItemSelectedColor.G, listView.ItemSelectedColor.B)))
                {
                    if (!listView.FullRowSelect)
                    {
                        // Calculate how far into the control it goes
                        int _widthDepth = -hPanelScrollBar.Value + listView.Columns.Width;
                        graphicsRow.FillRectangle(_brushBackground, listView.RowsInnerClientRect.X, rectRow.Y, _widthDepth, rectRow.Height);
                    }
                    else
                    {
                        graphicsRow.FillRectangle(_brushBackground, listView.RowsInnerClientRect.X, rectRow.Y, listView.RowsInnerClientRect.Width, rectRow.Height);
                    }
                }
            }
            else
            {
                // If the back color of the list doesn't match the back color of the item (AND) the back color isn't white, then override it
                if ((item.BackColor.ToArgb() != listView.BackColor.ToArgb()) && (item.BackColor != Color.White))
                {
                    using (SolidBrush _backgroundBrush = new SolidBrush(item.BackColor))
                    {
                        graphicsRow.FillRectangle(_backgroundBrush, listView.RowsInnerClientRect.X, rectRow.Y, listView.RowsInnerClientRect.Width, rectRow.Height);
                    }
                }
                else if (listView.AlternatingColors)
                {
                    // Check for full row alternate color alternating colors are only shown if the row isn't selected.
                    int _alternateItemIndex = listView.Items.FindItemIndex(item);
                    if (_alternateItemIndex % 2 > 0)
                    {
                        using (SolidBrush _backgroundBrush = new SolidBrush(listView.AlternateBackground))
                        {
                            if (!listView.FullRowSelect)
                            {
                                // Calculate how far into the control it goes
                                int _widthDepth = -hPanelScrollBar.Value + listView.Columns.Width;
                                graphicsRow.FillRectangle(_backgroundBrush, listView.RowsInnerClientRect.X, rectRow.Y, _widthDepth, rectRow.Height);
                            }
                            else
                            {
                                graphicsRow.FillRectangle(_backgroundBrush, listView.RowsInnerClientRect.X, rectRow.Y, listView.RowsInnerClientRect.Width, rectRow.Height);
                            }
                        }
                    }
                }
            }

            int _xCursor = -hPanelScrollBar.Value + listView.Border.Thickness;
            for (var subItemIndex = 0; subItemIndex < listView.Columns.Count; subItemIndex++)
            {
                Rectangle _subItemRectangle = new Rectangle(_xCursor, rectRow.Y, listView.Columns[subItemIndex].Width, rectRow.Height);

                // Avoid drawing items that are not in the visible region
                if ((_subItemRectangle.Right < 0) || (_subItemRectangle.Left > listView.RowsInnerClientRect.Right))
                {
                    Debug.Write(string.Empty);
                }
                else
                {
                    DrawSubItem(graphicsRow, _subItemRectangle, item, item.SubItems[subItemIndex], subItemIndex, listView.Font, listView, _newLiveControls, _liveControls, listView.CellPaddingSize, checkBoxSize);
                }

                _xCursor += listView.Columns[subItemIndex].Width;
            }

            // Post draw for focus rect and hot tracking
            if ((itemIndex == listView.HoverItemIndex) && listView.HoverItemTracking)
            {
                // handle hot tracking of items
                Color transparentColor = Color.FromArgb(75, listView.HoverTrackingColor.R, listView.HoverTrackingColor.G, listView.HoverTrackingColor.B); // 182, 189, 210 );
                using (Brush hotBrush = new SolidBrush(transparentColor))
                {
                    graphicsRow.FillRectangle(hotBrush, listView.RowsInnerClientRect.X, rectRow.Y, listView.RowsInnerClientRect.Width, rectRow.Height);
                }
            }

            // Draw row borders
            if (item.RowBorderSize > 0)
            {
                using (Pen _borderPen = new Pen(item.RowBorderColor, item.RowBorderSize) { Alignment = PenAlignment.Inset })
                {
                    graphicsRow.DrawRectangle(_borderPen, rectRow);
                }
            }

            // Make sure anything can even be selected before drawing selection rects
            if (listView.Selectable)
            {
                if (listView.ShowFocusRectangle && (listView.FocusedItem == item))
                {
                    // Deal with focus rect
                    ControlPaint.DrawFocusRectangle(graphicsRow, new Rectangle(listView.RowsInnerClientRect.X + 1, rectRow.Y, listView.RowsInnerClientRect.Width - 1, rectRow.Height));
                }
            }
        }

        /// <summary>Draw client rows of list control.</summary>
        /// <param name="graphicsRows">The graphics row.</param>
        /// <param name="listView">The list View.</param>
        /// <param name="backColor">The back Color.</param>
        /// <param name="vPanelScrollBar">The v Panel Scroll Bar.</param>
        /// <param name="hPanelScrollBar">The h Panel Scroll Bar.</param>
        /// <param name="_newLiveControls">The new Live Controls.</param>
        /// <param name="_liveControls">The live Controls.</param>
        /// <param name="checkBoxSize">The check Box Size.</param>
        public static void DrawRows(Graphics graphicsRows, VisualListView listView, Color backColor, ManagedVScrollBar vPanelScrollBar, ManagedHScrollBar hPanelScrollBar, ArrayList _newLiveControls, ArrayList _liveControls, int checkBoxSize)
        {
            using (SolidBrush _clientRowsViewBrush = new SolidBrush(backColor))
            {
                graphicsRows.FillRectangle(_clientRowsViewBrush, listView.RowsClientRectangle);
            }

            // Draw background image.
            if (listView.BackgroundImage != null)
            {
                if (listView.BackgroundStretchToFit)
                {
                    graphicsRows.DrawImage(listView.BackgroundImage, listView.RowsInnerClientRect.X, listView.RowsInnerClientRect.Y, listView.RowsInnerClientRect.Width, listView.RowsInnerClientRect.Height);
                }
                else
                {
                    graphicsRows.DrawImage(listView.BackgroundImage, listView.RowsInnerClientRect.X, listView.RowsInnerClientRect.Y);
                }
            }

            // Determine start item based on whether or not we have a vertical scrollbar present. Which item to start with in this visible view.
            int _startItem;
            if (vPanelScrollBar.Visible)
            {
                _startItem = vPanelScrollBar.Value;
            }
            else
            {
                _startItem = 0;
            }

            Rectangle _rectangleRow = listView.RowsRectangle;
            _rectangleRow.Height = listView.ItemHeight;

            // Draw rows.
            for (var item = 0; (item < listView.RowsVisible + 1) && (item + _startItem < listView.Items.Count); item++)
            {
                DrawRow(graphicsRows, _rectangleRow, listView.Items[item + _startItem], item + _startItem, listView, hPanelScrollBar, _newLiveControls, _liveControls, checkBoxSize);
                _rectangleRow.Y += listView.ItemHeight;
            }

            if (listView.GridLineStyle != GridLineStyle.None)
            {
                DrawGridLines(graphicsRows, vPanelScrollBar, hPanelScrollBar, listView);
            }

            // Draw hot tracking column overlay.
            if (listView.HoverColumnTracking && (listView.HoverColumnIndex != -1) && (listView.HoverColumnIndex < listView.Columns.Count))
            {
                int _xCursor = -hPanelScrollBar.Value;
                for (var _columnIndex = 0; _columnIndex < listView.HoverColumnIndex; _columnIndex++)
                {
                    _xCursor += listView.Columns[_columnIndex].Width;
                }

                using (Brush hotBrush = new SolidBrush(Color.FromArgb(75, listView.HoverTrackingColor.R, listView.HoverTrackingColor.G, listView.HoverTrackingColor.B)))
                {
                    graphicsRows.FillRectangle(hotBrush, _xCursor, listView.RowsInnerClientRect.Y, listView.Columns[listView.HoverColumnIndex].Width + 1, listView.RowsInnerClientRect.Height - 1);
                }
            }
        }

        /// <summary>Draw Sub Item (Cell) at location specified.</summary>
        /// <param name="graphicsSubItem">The graphics sub item.</param>
        /// <param name="rectSubItem">The rectangle sub item.</param>
        /// <param name="item">The item.</param>
        /// <param name="subItem">The sub item.</param>
        /// <param name="column">The column.</param>
        /// <param name="font">The font.</param>
        /// <param name="listView">The list View.</param>
        /// <param name="_newLiveControls">The _new Live Controls.</param>
        /// <param name="_liveControls">The _live Controls.</param>
        /// <param name="cellPaddingSize">The cell Padding Size.</param>
        /// <param name="checkBoxSize">The check Box Size.</param>
        public static void DrawSubItem(Graphics graphicsSubItem, Rectangle rectSubItem, VisualListViewItem item, VisualListViewSubItem subItem, int column, Font font, VisualListView listView, ArrayList _newLiveControls, ArrayList _liveControls, int cellPaddingSize, int checkBoxSize)
        {
            Rectangle _controlRectangle = new Rectangle(rectSubItem.X, rectSubItem.Y, rectSubItem.Width, rectSubItem.Height);

            if ((subItem.EmbeddedControl != null) && !subItem.ForceText)
            {
                Control _control = subItem.EmbeddedControl;

                if (_control.Parent != listView)
                {
                    _control.Parent = listView;
                }

                // Update the control BackColor to the current list view BackColorState.
                if (_control is VisualControlBase visualControlBase)
                {
                    visualControlBase.BackColor = ColorState.BackColorState(listView.BackColorState, listView.Enabled, listView.MouseState);
                }

                Rectangle _subItemRectangle = new Rectangle(_controlRectangle.X, _controlRectangle.Y + 1, _controlRectangle.Width, _controlRectangle.Height - 1);

                // Type _type = _control.GetType();
                PropertyInfo _propertyInfo = _control.GetType().GetProperty("PreferredHeight");
                if (_propertyInfo != null)
                {
                    var _preferredHeight = (int)_propertyInfo.GetValue(_control, null);

                    if ((_preferredHeight + (listView.CellPaddingSize * 2) > listView.ItemHeight) && listView.AutoHeight)
                    {
                        listView.ItemHeight = _preferredHeight + (listView.CellPaddingSize * 2);
                    }

                    _subItemRectangle.Y = _controlRectangle.Y + ((_controlRectangle.Height - _preferredHeight) / 2);
                }

                _newLiveControls.Add(_control);
                if (_liveControls.Contains(_control))
                {
                    _liveControls.Remove(_control);
                }

                if (_control.Bounds.ToString() != _subItemRectangle.ToString())
                {
                    // Forces to invalidate.
                    _control.Bounds = _subItemRectangle;
                }

                if (_control.Visible != true)
                {
                    _control.Visible = true;
                }
            }
            else
            {
                // If the sub item color is not the same as the back color fo the control, AND the item is not selected, then color this sub item background.
                if ((subItem.BackColor.ToArgb() != listView.BackColor.ToArgb()) && !item.Selected && (subItem.BackColor != Color.White))
                {
                    using (SolidBrush _backColorBrush = new SolidBrush(subItem.BackColor))
                    {
                        graphicsSubItem.FillRectangle(_backColorBrush, rectSubItem);
                    }
                }

                // Check if we need checkboxes in this column.
                if (listView.Columns[column].CheckBoxes && subItem.CheckBox)
                {
                    rectSubItem = DrawCheckBox(graphicsSubItem, rectSubItem, subItem.Checked, checkBoxSize, listView);
                }

                // If there is an image, this routine will RETURN with exactly the space left for everything else after the image is drawn (or not drawn due to lack of space).
                if ((subItem.ImageIndex > -1) && (listView.ImageListItems != null) && (subItem.ImageIndex < listView.ImageListItems.Images.Count))
                {
                    rectSubItem = DrawCellGraphic(graphicsSubItem, rectSubItem, listView.ImageListItems.Images[subItem.ImageIndex], subItem.ImageAlignment, cellPaddingSize, listView);
                }

                // Deal with text color in a box on whether it is selected or not.
                Color textColor;
                if (item.Selected && listView.Selectable)
                {
                    textColor = listView.ItemSelectedTextColor;
                }
                else
                {
                    textColor = listView.ForeColor;
                    if (item.ForeColor.ToArgb() != listView.ForeColor.ToArgb())
                    {
                        textColor = item.ForeColor;
                    }
                    else if (subItem.ForeColor.ToArgb() != listView.ForeColor.ToArgb())
                    {
                        textColor = subItem.ForeColor;
                    }
                }

                DrawCellText(graphicsSubItem, rectSubItem, subItem.Text, font, listView.Columns[column].TextAlignment, textColor, item.Selected, listView);
                subItem.LastCellRectangle = rectSubItem; // important to ONLY catch the area where the text is drawn
            }
        }

        #endregion
    }
}