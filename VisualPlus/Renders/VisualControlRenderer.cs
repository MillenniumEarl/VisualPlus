#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualControlRenderer.cs
// 
// Copyright (c) 2016 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using VisualPlus.Enumerators;
using VisualPlus.Models;
using VisualPlus.Utilities;

#endregion

namespace VisualPlus.Renders
{
    public sealed class VisualControlRenderer
    {
        #region Public Methods and Operators

        /// <summary>Draws a button control.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        /// <param name="backColor">The BackColor of the button.</param>
        /// <param name="backgroundImage">The background image for the button.</param>
        /// <param name="border">The border.</param>
        /// <param name="mouseState">The mouse State.</param>
        /// <param name="text">The string to draw.</param>
        /// <param name="font">The font to use in the string.</param>
        /// <param name="foreColor">The color of the string.</param>
        /// <param name="image">The image to draw.</param>
        /// <param name="imageSize">The image Size.</param>
        /// <param name="textImageRelation">The text image relation.</param>
        public static void DrawButton(Graphics graphics, Rectangle rectangle, Color backColor, Image backgroundImage, Border border, MouseStates mouseState, string text, Font font, Color foreColor, Image image, Size imageSize, TextImageRelation textImageRelation)
        {
            GraphicsPath _controlGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(rectangle, border);
            VisualBackgroundRenderer.DrawBackground(graphics, backColor, backgroundImage, mouseState, rectangle, border);
            DrawContent(graphics, rectangle, text, font, foreColor, image, imageSize, textImageRelation);
            VisualBorderRenderer.DrawBorderStyle(graphics, border, _controlGraphicsPath, mouseState);
        }

        /// <summary>Draws the text and image content.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        /// <param name="text">The string to draw.</param>
        /// <param name="font">The font to use in the string.</param>
        /// <param name="foreColor">The color of the string.</param>
        /// <param name="image">The image to draw.</param>
        /// <param name="imageSize">The image Size.</param>
        /// <param name="textImageRelation">The text image relation.</param>
        public static void DrawContent(Graphics graphics, Rectangle rectangle, string text, Font font, Color foreColor, Image image, Size imageSize, TextImageRelation textImageRelation)
        {
            Rectangle _imageRectangle = new Rectangle(new Point(), imageSize);
            Point _imagePoint = RelationManager.GetTextImageRelationLocation(graphics, textImageRelation, _imageRectangle, text, font, rectangle, Relation.Image);
            Point _textPoint = RelationManager.GetTextImageRelationLocation(graphics, textImageRelation, _imageRectangle, text, font, rectangle, Relation.Text);

            graphics.DrawImage(image, new Rectangle(_imagePoint, imageSize));
            graphics.DrawString(text, font, new SolidBrush(foreColor), _textPoint);
        }

        #endregion
    }
}