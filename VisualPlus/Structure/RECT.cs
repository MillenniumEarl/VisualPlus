#region Namespace

using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

#endregion

namespace VisualPlus.Structure
{
    [StructLayout(LayoutKind.Explicit)]
    public struct RECT
    {
        #region Fields

        [FieldOffset(12)]
        public int Bottom;

        [FieldOffset(0)]
        public int Left;

        [FieldOffset(8)]
        public int Right;

        [FieldOffset(4)]
        public int Top;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="RECT" /> struct.</summary>
        /// <param name="location">Sets the coordinates of the upper-left corner of this <see cref="RECT" /> structure.</param>
        /// <param name="size">Sets the size of this <see cref="RECT" />.</param>
        public RECT(Point location, Size size) : this(location.X, location.Y, location.X + size.Width, location.Y + size.Height)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="RECT" /> struct.</summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>Initializes a new instance of the <see cref="RECT" /> struct.</summary>
        /// <param name="rectangle">The rectangle.</param>
        public RECT(Rectangle rectangle) : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the height of this <see cref="RECT" /> structure.</summary>
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

        /// <summary>Gets a value indicating whether this <see cref="RECT" /> is empty.</summary>
        public bool IsEmpty
        {
            get
            {
                return (Left == 0) && (Top == 0) && (Right == 0) && (Bottom == 0);
            }
        }

        /// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="RECT" /> structure.</summary>
        public Point Location
        {
            get
            {
                return new Point(Left, Top);
            }

            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>Retrieves the <see cref="Rectangle" /> from the <see cref="RECT" />.</summary>
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Left, Top, Right, Bottom - 1);
            }
        }

        /// <summary>Gets or sets the size of this <see cref="RECT" />.</summary>
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

        /// <summary>Gets or sets the width of this <see cref="RECT" /> structure.</summary>
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

        /// <summary>Compares the two <see cref="RECT" /> structures for equality.</summary>
        /// <param name="rect1">The first rectangle to compare.</param>
        /// <param name="rect2">The second rectangle to compare.</param>
        /// <returns>
        ///     Returns true if the <see cref="RECT" /> structures have the same x, y, width, height property values;
        ///     otherwise, false.
        /// </returns>
        public static bool operator ==(RECT rect1, RECT rect2)
        {
            return rect1.Equals(rect2);
        }

        /// <summary>Converts the <see cref="RECT" /> to a <see cref="Rectangle" /> structure.</summary>
        /// <param name="rect">The <see cref="RECT" /> to convert.</param>
        public static implicit operator Rectangle(RECT rect)
        {
            return new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        /// <summary>Converts the <see cref="Rectangle" /> to a <see cref="RECT" /> structure.</summary>
        /// <param name="rectangle">The <see cref="Rectangle" /> to convert.</param>
        public static implicit operator RECT(Rectangle rectangle)
        {
            return new RECT(rectangle);
        }

        /// <summary>Compares the two <see cref="RECT" /> structures for inequality.</summary>
        /// <param name="rect1">The first rectangle to compare.</param>
        /// <param name="rect2">The second rectangle to compare.</param>
        /// <returns>
        ///     Returns true if the <see cref="RECT" /> structures do not have the same x, y, width, height property values;
        ///     otherwise, false.
        /// </returns>
        public static bool operator !=(RECT rect1, RECT rect2)
        {
            return !rect1.Equals(rect2);
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
            return (rect.Left == Left) && (rect.Top == Top) && (rect.Right == Right) && (rect.Bottom == Bottom);
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
            return ((Rectangle)this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }

        #endregion
    }
}