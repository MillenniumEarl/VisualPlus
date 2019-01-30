#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: RECT.cs
// 
// Copyright (c) 2018 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
// All Rights Reserved.
// 
// -----------------------------------------------------------------------------------------------------------
// 
// GNU General Public License v3.0 (GPL-3.0)
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF
// MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  
// This file is subject to the terms and conditions defined in the file 
// 'LICENSE.md', which should be in the root directory of the source code package.
// 
// -----------------------------------------------------------------------------------------------------------

#endregion

#region Namespace

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Structure
{
    /// <summary>
    ///     The <see cref="RECT" /> structure defines the coordinates of the upper-left and lower-right corners of a
    ///     <see cref="RECT" />.
    /// </summary>
    [ComVisible(true)]
    [DebuggerDisplay("X = {X} Y = {Y} Width = {Width} Height = {Height}")]
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    [TypeConverter(typeof(RectConverter))]
    public struct RECT : IDisposable, IFormattable
    {
        #region Fields

        /// <summary>Gets the y-axis value of the bottom of the <see cref="RECT" />.</summary>
        [Browsable(false)]
        [FieldOffset(12)]
        public int Bottom;

        /// <summary>Gets the x-axis value of the left side of the <see cref="RECT" />.</summary>
        [Browsable(false)]
        [FieldOffset(0)]
        public int Left;

        /// <summary>Gets the x-axis value of the right side of the <see cref="RECT" />.</summary>
        [Browsable(false)]
        [FieldOffset(8)]
        public int Right;

        /// <summary>Gets the y-axis position of the top of the <see cref="RECT" />.</summary>
        [Browsable(false)]
        [FieldOffset(4)]
        public int Top;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="RECT" /> struct.</summary>
        /// <param name="size">A <see cref="Size" /> structure that specifies the width and height of the <see cref="RECT" />.</param>
        public RECT(Size size) : this(new Point(), size)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="RECT" /> struct.</summary>
        /// <param name="point1">The first point that the new <see cref="RECT" /> must contain.</param>
        /// <param name="point2">The second point that the new <see cref="RECT" /> must contain.</param>
        public RECT(Point point1, Point point2) : this(point1, new Size(point2.X, point2.Y))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="RECT" /> struct.</summary>
        /// <param name="location">Sets the coordinates of the upper-left corner of this <see cref="RECT" /> structure.</param>
        /// <param name="size">Sets the size of this <see cref="RECT" />.</param>
        public RECT(Point location, Size size) : this(location.X, location.Y, location.X + size.Width, location.Y + size.Height)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="RECT" /> struct.</summary>
        /// <param name="x">The x-coordinate of the top-left corner of the <see cref="RECT" />.</param>
        /// <param name="y">The y-coordinate of the top-left corner of the <see cref="RECT" />.</param>
        /// <param name="width">The width of the <see cref="RECT" />.</param>
        /// <param name="height">The height of the <see cref="RECT" />.</param>
        public RECT(int x, int y, int width, int height)
        {
            // Exceptions Check
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(width)} must be equal to or greater than zero.");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(height)} must be equal to or greater than zero.");
            }

            // Update property values
            Left = x;
            Top = y;
            Right = x + width;
            Bottom = y + height;
        }

        /// <summary>Initializes a new instance of the <see cref="RECT" /> struct.</summary>
        /// <param name="rectangle">The rectangle.</param>
        public RECT(Rectangle rectangle) : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>Represents a <see cref="RECT" /> structure with its properties left uninitialized.</summary>
        public static RECT Empty { get; }

        /// <summary>Gets the position of the bottom-left corner of the <see cref="RECT" />.</summary>
        [Browsable(false)]
        public Point BottomLeft
        {
            get
            {
                return new Point(Left, Bottom);
            }
        }

        /// <summary>Gets the position of the bottom-right corner of the <see cref="RECT" />.</summary>
        [Browsable(false)]
        public Point BottomRight
        {
            get
            {
                return new Point(Right, Bottom);
            }
        }

        /// <summary>Gets or sets the height of this <see cref="RECT" /> structure.</summary>
        [Browsable(false)]
        public int Height
        {
            get
            {
                return Bottom - Top;
            }

            set
            {
                Bottom = value + Top;
            }
        }

        /// <summary>Gets a value that indicates whether the <see cref="RECT" /> is the <see cref="Empty" /> <see cref="RECT" />.</summary>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                // Variable
                bool empty;

                // Indicates whether the properties are at zero value
                if ((Left == 0) && (Top == 0) && (Right == 0) && (Bottom == 0))
                {
                    empty = true;
                }
                else
                {
                    empty = false;
                }

                return empty;
            }
        }

        /// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="RECT" /> structure.</summary>
        [Browsable(false)]
        public Point Location
        {
            get
            {
                return new Point(X, Y);
            }

            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>Gets or sets the size of this <see cref="RECT" />.</summary>
        [Browsable(false)]
        public Size Size
        {
            get
            {
                return new Size(Width, Height);
            }

            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// <summary>Gets the position of the top-Left corner of the <see cref="RECT" />.</summary>
        [Browsable(false)]
        public Point TopLeft
        {
            get
            {
                return new Point(Left, Top);
            }
        }

        /// <summary>Gets the position of the top-right corner of the <see cref="RECT" />.</summary>
        [Browsable(false)]
        public Point TopRight
        {
            get
            {
                return new Point(X + Width, Top);
            }
        }

        /// <summary>Retrieves the <see cref="ToRectangle" /> from the <see cref="RECT" />.</summary>
        [Browsable(false)]
        public Rectangle ToRectangle
        {
            get
            {
                return new Rectangle(Left, Top, Right, Bottom);
            }
        }

        /// <summary>Gets or sets the width of this <see cref="RECT" /> structure.</summary>
        [Browsable(false)]
        public int Width
        {
            get
            {
                return Right - Left;
            }

            set
            {
                Right = value + Left;
            }
        }

        /// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="RECT" /> structure.</summary>
        [Browsable(false)]
        public int X
        {
            get
            {
                return Left;
            }

            set
            {
                Right -= Left - value;
                Left = value;
            }
        }

        /// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="RECT" /> structure.</summary>
        [Browsable(false)]
        public int Y
        {
            get
            {
                return Top;
            }

            set
            {
                Bottom -= Top - value;
                Top = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Converts the specified <see cref="RectangleF" /> structure to a <see cref="RECT" /> structure by rounding the
        ///     <see cref="RectangleF" /> values to the next higher integer values.
        /// </summary>
        /// <param name="value">The <see cref="RectangleF" /> structure to be converted.</param>
        /// <returns>The <see cref="RECT" />.</returns>
        public static RECT Ceiling(RectangleF value)
        {
            return new RECT((int)Math.Ceiling(value.X), (int)Math.Ceiling(value.Y), (int)Math.Ceiling(value.Width), (int)Math.Ceiling(value.Height));
        }

        /// <summary>Creates a <see cref="RECT" /> structure with the specified edge locations.</summary>
        /// <param name="left">The x-coordinate of the upper-left corner of this <see cref="RECT" /> structure.</param>
        /// <param name="top">The y-coordinate of the upper-left corner of this <see cref="RECT" /> structure</param>
        /// <param name="right">The x-coordinate of the lower-right corner of this <see cref="RECT" /> structure.</param>
        /// <param name="bottom">The y-coordinate of the lower-right corner of this <see cref="RECT" /> structure.</param>
        /// <returns>The <see cref="RECT" />.</returns>
        public static RECT FromLTRB(int left, int top, int right, int bottom)
        {
            return new RECT(left, top, right - left, bottom - top);
        }

        /// <summary>
        ///     Retrieves the numeric list separator for a given <see cref="IFormatProvider" />. Separator is a comma [,] if
        ///     the decimal separator is not a comma, or a semicolon [;] otherwise.
        /// </summary>
        /// <param name="provider">The format provider.</param>
        /// <returns>The <see cref="char" />.</returns>
        public static char GetNumericListSeparator(IFormatProvider provider)
        {
            // Variable
            var numericSeparator = ',';

            // Get the NumberFormatInfo out of the provider, if possible the IFormatProvider doesn't not contain a NumberFormatInfo, then this method returns the current culture's NumberFormatInfo.
            NumberFormatInfo numberFormat = NumberFormatInfo.GetInstance(provider);

            // Debug.Assert(null != numberFormat);

            // Is the decimal separator is the same as the list separator? If so, we use the ";".
            if ((numberFormat.NumberDecimalSeparator.Length > 0) && (numericSeparator == numberFormat.NumberDecimalSeparator[0]))
            {
                numericSeparator = ';';
            }

            return numericSeparator;
        }

        /// <summary>
        ///     Creates a rectangle that results from expanding or shrinking the specified rectangle by the specified width
        ///     and height amounts, in all directions.
        /// </summary>
        /// <param name="rect">The Rect structure to modify.</param>
        /// <param name="width">The amount by which to expand or shrink the left and right sides of the rectangle.</param>
        /// <param name="height">The amount by which to expand or shrink the top and bottom sides of the rectangle.</param>
        /// <returns>The <see cref="RECT" />.</returns>
        public static RECT Inflate(RECT rect, int width, int height)
        {
            rect.Inflate(width, height);
            return rect;
        }

        /// <summary>
        ///     Returns the <see cref="RECT" /> that results from expanding the specified <see cref="RECT" /> by the specified
        ///     <see cref="Size" />, in all directions.
        /// </summary>
        /// <param name="rect">The <see cref="RECT" /> structure to modify.</param>
        /// <param name="size">
        ///     Specifies the amount to expand the <see cref="RECT" />. The Size structure's Width property
        ///     specifies the amount to increase the <see cref="RECT" />'s Left and Right properties. The Size structure's Height
        ///     property specifies the amount to increase the <see cref="RECT" />'s Top and Bottom properties.
        /// </param>
        /// <returns>The <see cref="RECT" />.</returns>
        public static RECT Inflate(RECT rect, Size size)
        {
            rect.Inflate(size.Width, size.Height);
            return rect;
        }

        /// <summary>
        ///     Returns a third <see cref="RECT" /> structure that represents the intersection of two other
        ///     <see cref="RECT" /> structures. If there is no intersection, an empty <see cref="RECT" /> is returned.
        /// </summary>
        /// <param name="a">The first rectangle to intersect.</param>
        /// <param name="b">The second rectangle to intersect.</param>
        /// <returns>A <see cref="RECT" /> that represents the intersection of <paramref name="a" /> and <paramref name="b" />.</returns>
        public static RECT Intersect(RECT a, RECT b)
        {
            int x = Math.Max(a.X, b.X);
            int num1 = Math.Min(a.X + a.Width, b.X + b.Width);
            int y = Math.Max(a.Y, b.Y);
            int num2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

            if ((num1 >= x) && (num2 >= y))
            {
                return new RECT(x, y, num1 - x, num2 - y);
            }

            return Empty;
        }

        /// <summary>Adjusts the location of this <see cref="RECT" /> by the specified amount.</summary>
        /// <param name="rect">The <see cref="RECT" /> to offset.</param>
        /// <param name="offsetX">The offset X.</param>
        /// <param name="offsetY">The offset Y.</param>
        /// <returns>The <see cref="RECT" />.</returns>
        public static RECT Offset(RECT rect, int offsetX, int offsetY)
        {
            rect.Offset(offsetX, offsetY);
            return rect;
        }

        /// <summary>Compares the two <see cref="RECT" /> structures for equality.</summary>
        /// <param name="left">The first rectangle to compare.</param>
        /// <param name="right">The second rectangle to compare.</param>
        /// <returns>
        ///     Returns true if the <see cref="RECT" /> structures have the same x, y, width, height property values;
        ///     otherwise, false.
        /// </returns>
        public static bool operator ==(RECT left, RECT right)
        {
            return left.Equals(right);
        }

        /// <summary>Converts the <see cref="RECT" /> to a <see cref="ToRectangle" /> structure.</summary>
        /// <param name="rect">The <see cref="RECT" /> to convert.</param>
        public static implicit operator Rectangle(RECT rect)
        {
            return new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        /// <summary>Converts the <see cref="ToRectangle" /> to a <see cref="RECT" /> structure.</summary>
        /// <param name="rectangle">The <see cref="ToRectangle" /> to convert.</param>
        public static implicit operator RECT(Rectangle rectangle)
        {
            return new RECT(rectangle);
        }

        /// <summary>Compares the two <see cref="RECT" /> structures for inequality.</summary>
        /// <param name="left">The first rectangle to compare.</param>
        /// <param name="right">The second rectangle to compare.</param>
        /// <returns>
        ///     Returns true if the <see cref="RECT" /> structures do not have the same x, y, width, height property values;
        ///     otherwise, false.
        /// </returns>
        public static bool operator !=(RECT left, RECT right)
        {
            return !left.Equals(right);
        }

        /// <summary>Creates a new <see cref="RECT" /> from the specified string representation.</summary>
        /// <param name="source">The string representation of the <see cref="RECT" />, in the form "x, y, width, height".</param>
        /// <returns>The <see cref="RECT" />.</returns>
        public static RECT Parse(string source)
        {
            // Variable
            RECT rect = Empty;

            // Safety check
            if (!string.IsNullOrEmpty(source))
            {
                try
                {
                    // Parses the regular expression to extract the dimension from the ToString() value
                    Match regexMatches = Regex.Match(source, @"\D*(\d+)\D*(\d+)\D*(\d+)\D*(\d+)");

                    // Initializes the type using the matches
                    rect = new RECT(int.Parse(regexMatches.Groups[1].Value), int.Parse(regexMatches.Groups[2].Value), int.Parse(regexMatches.Groups[3].Value), int.Parse(regexMatches.Groups[4].Value));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                // Return empty for parsing no source data
                rect = Empty;
            }

            return rect;
        }

        /// <summary>
        ///     Converts the specified <see cref="RectangleF" /> to a <see cref="RECT" /> by rounding the
        ///     <see cref="RectangleF" /> values to the nearest integer values.
        /// </summary>
        /// <param name="value">The <see cref="RectangleF" /> to be converted.</param>
        /// <returns>The rounded integer value of the <see cref="RECT" />.</returns>
        public static RECT Round(RectangleF value)
        {
            return new RECT((int)Math.Round(value.X), (int)Math.Round(value.Y), (int)Math.Round(value.Width), (int)Math.Round(value.Height));
        }

        /// <summary>
        ///     Converts the specified <see cref="RectangleF" /> to a <see cref="RECT" /> by truncating the
        ///     <see cref="RectangleF" /> values.
        /// </summary>
        /// <param name="value">The <see cref="RectangleF" /> to be converted.</param>
        /// <returns>The truncated value of the  <see cref="RECT" />.</returns>
        public static RECT Truncate(RectangleF value)
        {
            return new RECT((int)value.X, (int)value.Y, (int)value.Width, (int)value.Height);
        }

        /// <summary>Gets a <see cref="RECT" /> structure that contains the union of two <see cref="RECT" /> structures.</summary>
        /// <param name="rect1">The first <see cref="RECT" /> to union.</param>
        /// <param name="rect2">The second <see cref="RECT" /> to union.</param>
        /// <returns>A <see cref="RECT" /> structure that bounds the union of the two <see cref="RECT" /> structures.</returns>
        public static RECT Union(RECT rect1, RECT rect2)
        {
            rect1.Union(rect2);
            return rect1;
        }

        /// <summary>
        ///     Creates a <see cref="RECT" /> that is exactly large enough to include the specified <see cref="RECT" /> and
        ///     the specified point.
        /// </summary>
        /// <param name="rect">The rectangle to include.</param>
        /// <param name="point">The point to include.</param>
        /// <returns>
        ///     A <see cref="RECT" /> that is exactly large enough to contain the specified <see cref="RECT" /> and the
        ///     specified point.
        /// </returns>
        public static RECT Union(RECT rect, Point point)
        {
            rect.Union(new RECT(point, point));
            return rect;
        }

        /// <summary>Determines if the specified point is contained within this <see cref="RECT" /> structure.</summary>
        /// <param name="x">The x-coordinate of the point to test.</param>
        /// <param name="y">The y-coordinate of the point to test.</param>
        /// <returns>
        ///     This method returns <see langword="true" /> if the point defined by <paramref name="x" /> and
        ///     <paramref name="y" /> is contained within this <see cref="RECT" /> structure; otherwise <see langword="false" />.
        /// </returns>
        public bool Contains(int x, int y)
        {
            if ((X <= x) && (x < X + Width) && (Y <= y))
            {
                return y < Y + Height;
            }

            return false;
        }

        /// <summary>Determines if the specified point is contained within this <see cref="RECT" /> structure.</summary>
        /// <param name="pt">The <see cref="Point" /> to test. </param>
        /// <returns>
        ///     This method returns <see langword="true" /> if the point represented by <paramref name="pt" /> is contained
        ///     within this <see cref="RECT" /> structure; otherwise <see langword="false" />.
        /// </returns>
        public bool Contains(Point pt)
        {
            return Contains(pt.X, pt.Y);
        }

        /// <summary>
        ///     Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this
        ///     <see cref="RECT" /> structure.
        /// </summary>
        /// <param name="rect">The <see cref="RECT" /> to test. </param>
        /// <returns>
        ///     This method returns <see langword="true" /> if the rectangular region represented by <paramref name="rect" />
        ///     is entirely contained within this <see cref="RECT" /> structure; otherwise <see langword="false" />.
        /// </returns>
        public bool Contains(RECT rect)
        {
            if ((X <= rect.X) && (rect.X + rect.Width <= X + Width) && (Y <= rect.Y))
            {
                return rect.Y + rect.Height <= Y + Height;
            }

            return false;
        }

        public void Dispose()
        {
            // Dispose of structure
            this = Empty;
        }

        /// <summary>
        ///     Tests whether the <see cref="RECT" /> structure with the same location and size of this <see cref="RECT" />
        ///     structure.
        /// </summary>
        /// <param name="rect">The <see cref="RECT" /> structure to test.</param>
        /// <returns>
        ///     This method returns true if <see cref="RECT" /> is a <see cref="RECT" /> structure and its X, Y, Width, and
        ///     Height properties are equal to the corresponding properties of this <see cref="RECT" /> structure; otherwise,
        ///     false.
        /// </returns>
        public bool Equals(RECT rect)
        {
            // Variable
            bool equals;

            // Determines whether all the values are equal
            if (rect.Left.Equals(Left) && rect.Top.Equals(Top) && rect.Right.Equals(Right) && rect.Bottom.Equals(Bottom))
            {
                equals = true;
            }
            else
            {
                equals = false;
            }

            return equals;
        }

        public override bool Equals(object obj)
        {
            // Attempt to return a RECT object.
            switch (obj)
            {
                case RECT rect:
                    {
                        return Equals(rect);
                    }

                case Rectangle rectangle:
                    {
                        return Equals(new RECT(rectangle));
                    }
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (IsEmpty)
            {
                return 0;
            }
            else
            {
                // Perform field-by-field XOR of HashCodes
                return X.GetHashCode() ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
            }
        }

        /// <summary>Enlarges this <see cref="RECT" /> by the specified amount.</summary>
        /// <param name="width">The amount to inflate this <see cref="RECT" /> horizontally. </param>
        /// <param name="height">The amount to inflate this <see cref="RECT" /> vertically. </param>
        public void Inflate(int width, int height)
        {
            // Safety check
            if (IsEmpty)
            {
                throw new InvalidOperationException("The " + nameof(RECT) + " is empty.");
            }

            // Calculate math inflation
            X -= width;
            Y -= height;

            // Do two additions rather than multiplication by 2 to avoid spurious overflow
            // That is: (A + 2 * B) != ((A + B) + B) if 2*B overflows.
            // Note that multiplication by 2 might work in this case because A should start
            // positive & be "clamped" to positive after, but consider A = Inf & B = -MAX.
            Width += width;
            Width += width;
            Height += height;
            Height += height;

            // We catch the case of inflation by less than -width/2 or -height/2 here.  This also
            // maintains the invariant that either the Rect is Empty or _width and _height are
            // non-negative, even if the user parameters were NaN, though this isn't strictly maintained
            // by other methods.
            if (!((Width >= 0) && (Height >= 0)))
            {
                this = Empty;
            }
        }

        /// <summary>Enlarges this <see cref="RECT" /> by the specified amount.</summary>
        /// <param name="size">The amount to inflate this <see cref="RECT" />. </param>
        public void Inflate(Size size)
        {
            Inflate(size.Width, size.Height);
        }

        /// <summary>Replaces this <see cref="RECT" /> with the intersection of itself and the specified <see cref="RECT" />.</summary>
        /// <param name="rect">The <see cref="RECT" /> with which to intersect. </param>
        public void Intersect(RECT rect)
        {
            RECT rectangle = Intersect(rect, this);
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
        }

        /// <summary>Determines if this <see cref="RECT" /> intersects with <paramref name="rect" />.</summary>
        /// <param name="rect">The <see cref="RECT" /> to test. </param>
        /// <returns>This method returns <see langword="true" /> if there is any intersection, otherwise <see langword="false" />.</returns>
        public bool IntersectsWith(RECT rect)
        {
            // Validate intersection
            if ((rect.X < X + Width) && (X < rect.X + rect.Width) && (rect.Y < Y + Height))
            {
                return Y < rect.Y + rect.Height;
            }

            return false;
        }

        /// <summary>Adjusts the location of this <see cref="RECT" /> by the specified amount.</summary>
        /// <param name="pos">Amount to offset the location. </param>
        public void Offset(Point pos)
        {
            Offset(pos.X, pos.Y);
        }

        /// <summary>Adjusts the location of this <see cref="RECT" /> by the specified amount.</summary>
        /// <param name="x">The horizontal offset. </param>
        /// <param name="y">The vertical offset. </param>
        public void Offset(int x, int y)
        {
            X += x;
            Y += y;
        }

        /// <summary>Multiplies the size of the current <see cref="RECT" /> by the specified x and y values.</summary>
        /// <param name="scaleX">The scale factor in the x-direction.</param>
        /// <param name="scaleY">The scale factor in the y-direction.</param>
        public void Scale(int scaleX, int scaleY)
        {
            // Safety check
            if (IsEmpty)
            {
                return;
            }

            // Multiply the current position and size by the scale
            X *= scaleX;
            Y *= scaleY;
            Width *= scaleX;
            Height *= scaleY;

            // If the scale in the X dimension is negative, we need to normalize X and Width
            if (scaleX < 0)
            {
                // Make X the left-most edge again
                X += Width;

                // And make Width positive
                Width *= -1;
            }

            // Do the same for the Y dimension
            if (scaleY < 0)
            {
                // Make Y the top-most edge again
                Y += Height;

                // And make Height positive
                Height *= -1;
            }
        }

        /// <summary>Formats the value of the current instance using the specified format.</summary>
        /// <param name="format">The format to use.</param>
        /// <returns>The <see cref="string" />.</returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>Formats the value of the current instance using the specified format.</summary>
        /// <param name="format">The format to use.</param>
        /// <param name="provider">The provider to use to format the value.</param>
        /// <returns>The <see cref="string" />.</returns>
        public string ToString(string format, IFormatProvider provider)
        {
            // Safety check
            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }

            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(format, provider);
        }

        public override string ToString()
        {
            // Delegate to the internal method which implements all ToString calls.
            return ConvertToString(null, null);
        }

        /// <summary>Expands the current <see cref="RECT" /> exactly enough to contain the specified <see cref="RECT" />.</summary>
        /// <param name="rect">The <see cref="RECT" /> to include.</param>
        public void Union(RECT rect)
        {
            // Safety check
            if (IsEmpty)
            {
                this = rect;
            }
            else if (!rect.IsEmpty)
            {
                // Variables
                int left = Math.Min(Left, rect.Left);
                int top = Math.Min(Top, rect.Top);

                // Check so that the math does not result in NaN
                if ((rect.Width == Convert.ToInt32(double.PositiveInfinity)) || (Width == Convert.ToInt32(double.PositiveInfinity)))
                {
                    Width = Convert.ToInt32(double.PositiveInfinity);
                }
                else
                {
                    // Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)                    
                    int maxRight = Math.Max(Right, rect.Right);
                    Width = Math.Max(maxRight - left, 0);
                }

                // Check so that the math does not result in NaN
                if ((rect.Height == Convert.ToInt32(double.PositiveInfinity)) || (Height == Convert.ToInt32(double.PositiveInfinity)))
                {
                    Height = Convert.ToInt32(double.PositiveInfinity);
                }
                else
                {
                    // Max with 0 to prevent double weirdness from causing us to be (-epsilon..0)
                    int maxBottom = Math.Max(Bottom, rect.Bottom);
                    Height = Math.Max(maxBottom - top, 0);
                }

                X = left;
                Y = top;
            }
        }

        /// <summary>Expands the current <see cref="RECT" /> exactly enough to contain the specified point.</summary>
        /// <param name="point">The point to include.</param>
        public void Union(Point point)
        {
            Union(new RECT(point, point));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates a string representation of this object based on the format string and <see cref="IFormatProvider" />
        ///     passed in. If the provider is null, the CurrentCulture is used. See the documentation for
        ///     <see cref="IFormattable" /> for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider to use to format the value.</param>
        /// <returns>The <see cref="string" />.</returns>
        private string ConvertToString(string format, IFormatProvider provider)
        {
            // Safety checks
            if (IsEmpty)
            {
                return base.ToString();
            }

            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }

            // Get the numeric list separator for a given culture
            char separator = GetNumericListSeparator(provider);

            // Returns the formatted struct text
            return string.Format(provider, "{{X={1}" + format + "{0}Y={2}" + format + "{0}Width={3}" + format + "{0}Height={4}" + format + "}}", separator, X, Y, Width, Height);
        }

        #endregion
    }
}