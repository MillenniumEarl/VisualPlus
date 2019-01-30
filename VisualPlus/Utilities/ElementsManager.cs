#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ElementsManager.cs
// 
// Copyright (c) 2019 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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
using System.Drawing;

#endregion

namespace VisualPlus.Utilities
{
    public sealed class ElementsManager
    {
        #region Public Methods and Operators

        /// <summary>Generates a 5 point star.</summary>
        /// <param name="originF"> The originF is the middle of the star.</param>
        /// <param name="outerRadius">Radius of the surrounding circle.</param>
        /// <param name="innerRadius">Radius of the circle for the "inner" points</param>
        /// <returns>The <see cref="PointF" />.</returns>
        public static PointF[] Generate5PointStar(PointF originF, float outerRadius, float innerRadius)
        {
            // Define some variables to avoid as much calculations as possible
            // conversions to radians
            const double Ang36 = Math.PI / 5.0; // 36Â° x PI/180
            const double Ang72 = 2.0 * Ang36; // 72Â° x PI/180

            // some sine and cosine values we need
            var sin36 = (float)Math.Sin(Ang36);
            var sin72 = (float)Math.Sin(Ang72);
            var cos36 = (float)Math.Cos(Ang36);
            var cos72 = (float)Math.Cos(Ang72);

            // Fill array with 10 originF points
            PointF[] pointsArray = { originF, originF, originF, originF, originF, originF, originF, originF, originF, originF };
            pointsArray[0].Y -= outerRadius; // top off the star, or on a clock this is 12:00 or 0:00 hours
            pointsArray[1].X += innerRadius * sin36;
            pointsArray[1].Y -= innerRadius * cos36; // 0:06 hours
            pointsArray[2].X += outerRadius * sin72;
            pointsArray[2].Y -= outerRadius * cos72; // 0:12 hours
            pointsArray[3].X += innerRadius * sin72;
            pointsArray[3].Y += innerRadius * cos72; // 0:18
            pointsArray[4].X += outerRadius * sin36;
            pointsArray[4].Y += outerRadius * cos36; // 0:24 

            // Phew! Glad I got that trig working.
            pointsArray[5].Y += innerRadius;

            // I use the symmetry of the star figure here
            pointsArray[6].X += pointsArray[6].X - pointsArray[4].X;
            pointsArray[6].Y = pointsArray[4].Y; // mirror point
            pointsArray[7].X += pointsArray[7].X - pointsArray[3].X;
            pointsArray[7].Y = pointsArray[3].Y; // mirror point
            pointsArray[8].X += pointsArray[8].X - pointsArray[2].X;
            pointsArray[8].Y = pointsArray[2].Y; // mirror point
            pointsArray[9].X += pointsArray[9].X - pointsArray[1].X;
            pointsArray[9].Y = pointsArray[1].Y; // mirror point

            return pointsArray;
        }

        /// <summary>Generate half of the detailed star polygon as a point[].</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="PointF" />.</returns>
        public static PointF[] GenerateDetailedSemiStar(RectangleF rectangle)
        {
            return new[]
                {
                    new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 0f)), new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 1f)), new PointF(rectangle.X + (rectangle.Width * 0.4f), rectangle.Y + (rectangle.Height * 0.73f)), new PointF(rectangle.X + (rectangle.Width * 0.17f), rectangle.Y + (rectangle.Height * 0.83f)), new PointF(rectangle.X + (rectangle.Width * 0.27f), rectangle.Y + (rectangle.Height * 0.6f)), new PointF(rectangle.X + (rectangle.Width * 0f), rectangle.Y + (rectangle.Height * 0.5f)),
                    new PointF(rectangle.X + (rectangle.Width * 0.27f), rectangle.Y + (rectangle.Height * 0.4f)), new PointF(rectangle.X + (rectangle.Width * 0.17f), rectangle.Y + (rectangle.Height * 0.17f)), new PointF(rectangle.X + (rectangle.Width * 0.4f), rectangle.Y + (rectangle.Height * 0.27f))
                };
        }

        /// <summary>Generate a detailed star polygon as a point[].</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="PointF" />.</returns>
        public static PointF[] GenerateDetailedStar(RectangleF rectangle)
        {
            return new[]
                {
                    new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 0f)), new PointF(rectangle.X + (rectangle.Width * 0.6f), rectangle.Y + (rectangle.Height * 0.27f)), new PointF(rectangle.X + (rectangle.Width * 0.83f), rectangle.Y + (rectangle.Height * 0.17f)), new PointF(rectangle.X + (rectangle.Width * 0.73f), rectangle.Y + (rectangle.Height * 0.4f)), new PointF(rectangle.X + (rectangle.Width * 1f), rectangle.Y + (rectangle.Height * 0.5f)), new PointF(rectangle.X + (rectangle.Width * 0.73f), rectangle.Y + (rectangle.Height * 0.6f)),
                    new PointF(rectangle.X + (rectangle.Width * 0.83f), rectangle.Y + (rectangle.Height * 0.83f)), new PointF(rectangle.X + (rectangle.Width * 0.6f), rectangle.Y + (rectangle.Height * 0.73f)), new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 1f)), new PointF(rectangle.X + (rectangle.Width * 0.4f), rectangle.Y + (rectangle.Height * 0.73f)), new PointF(rectangle.X + (rectangle.Width * 0.17f), rectangle.Y + (rectangle.Height * 0.83f)), new PointF(rectangle.X + (rectangle.Width * 0.27f), rectangle.Y + (rectangle.Height * 0.6f)),
                    new PointF(rectangle.X + (rectangle.Width * 0f), rectangle.Y + (rectangle.Height * 0.5f)), new PointF(rectangle.X + (rectangle.Width * 0.27f), rectangle.Y + (rectangle.Height * 0.4f)), new PointF(rectangle.X + (rectangle.Width * 0.17f), rectangle.Y + (rectangle.Height * 0.17f)), new PointF(rectangle.X + (rectangle.Width * 0.4f), rectangle.Y + (rectangle.Height * 0.27f))
                };
        }

        /// <summary>Generate half of a fat star polygon as a point[].</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="PointF" />.</returns>
        public static PointF[] GenerateFatSemiStar(RectangleF rectangle)
        {
            return new[] { new PointF(rectangle.X + (rectangle.Width * 0.31f), rectangle.Y + (rectangle.Height * 0.33f)), new PointF(rectangle.X + (rectangle.Width * 0f), rectangle.Y + (rectangle.Height * 0.37f)), new PointF(rectangle.X + (rectangle.Width * 0.25f), rectangle.Y + (rectangle.Height * 0.62f)), new PointF(rectangle.X + (rectangle.Width * 0.19f), rectangle.Y + (rectangle.Height * 1f)), new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 0.81f)), new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 0f)) };
        }

        /// <summary>Generate a fat star polygon as a point[].</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="PointF" />.</returns>
        public static PointF[] GenerateFatStar(RectangleF rectangle)
        {
            return new[]
                {
                    new PointF(rectangle.X + (rectangle.Width * 0.31f), rectangle.Y + (rectangle.Height * 0.33f)), new PointF(rectangle.X + (rectangle.Width * 0f), rectangle.Y + (rectangle.Height * 0.37f)), new PointF(rectangle.X + (rectangle.Width * 0.25f), rectangle.Y + (rectangle.Height * 0.62f)), new PointF(rectangle.X + (rectangle.Width * 0.19f), rectangle.Y + (rectangle.Height * 1f)), new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 0.81f)), new PointF(rectangle.X + (rectangle.Width * 0.81f), rectangle.Y + (rectangle.Height * 1f)),
                    new PointF(rectangle.X + (rectangle.Width * 0.75f), rectangle.Y + (rectangle.Height * 0.62f)), new PointF(rectangle.X + (rectangle.Width * 1f), rectangle.Y + (rectangle.Height * 0.37f)), new PointF(rectangle.X + (rectangle.Width * 0.69f), rectangle.Y + (rectangle.Height * 0.33f)), new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 0f))
                };
        }

        /// <summary>Generate half of a typical thin star polygon as a point[].</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="PointF" />.</returns>
        public static PointF[] GenerateNormalSemiStar(RectangleF rectangle)
        {
            return new[] { new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 0f)), new PointF(rectangle.X + (rectangle.Width * 0.38f), rectangle.Y + (rectangle.Height * 0.38f)), new PointF(rectangle.X + (rectangle.Width * 0f), rectangle.Y + (rectangle.Height * 0.38f)), new PointF(rectangle.X + (rectangle.Width * 0.31f), rectangle.Y + (rectangle.Height * 0.61f)), new PointF(rectangle.X + (rectangle.Width * 0.19f), rectangle.Y + (rectangle.Height * 1f)), new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 0.77f)) };
        }

        /// <summary>Generate a typical thin star polygon as a point[].</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="PointF" />.</returns>
        public static PointF[] GenerateNormalStar(RectangleF rectangle)
        {
            return new[]
                {
                    new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 0f)), new PointF(rectangle.X + (rectangle.Width * 0.38f), rectangle.Y + (rectangle.Height * 0.38f)), new PointF(rectangle.X + (rectangle.Width * 0f), rectangle.Y + (rectangle.Height * 0.38f)), new PointF(rectangle.X + (rectangle.Width * 0.31f), rectangle.Y + (rectangle.Height * 0.61f)), new PointF(rectangle.X + (rectangle.Width * 0.19f), rectangle.Y + (rectangle.Height * 1f)), new PointF(rectangle.X + (rectangle.Width * 0.5f), rectangle.Y + (rectangle.Height * 0.77f)),
                    new PointF(rectangle.X + (rectangle.Width * 0.8f), rectangle.Y + (rectangle.Height * 1f)), new PointF(rectangle.X + (rectangle.Width * 0.69f), rectangle.Y + (rectangle.Height * 0.61f)), new PointF(rectangle.X + (rectangle.Width * 1f), rectangle.Y + (rectangle.Height * 0.38f)), new PointF(rectangle.X + (rectangle.Width * 0.61f), rectangle.Y + (rectangle.Height * 0.38f))
                };
        }

        #endregion
    }
}