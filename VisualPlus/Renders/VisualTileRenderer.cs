#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualTileRenderer.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:24 AM
// 
// Copyright (c) 2016-2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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

using VisualPlus.Enumerators;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Controls.Layout;

#endregion

namespace VisualPlus.Renders
{
    public sealed class VisualTileRenderer
    {
        #region Public Methods and Operators

        /// <summary>Render the tile.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="type">The type to draw.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="image">The image to draw.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to  draw.</param>
        /// <param name="enabled">The enabled.</param>
        /// <param name="mouseState">The mouse State.</param>
        /// <param name="textStyle">The text Style.</param>
        /// <param name="offset">The location offset.</param>
        public static void RenderTile(Graphics graphics, VisualTile.TileType type, Rectangle clientRectangle, Image image, string text, Font font, bool enabled, MouseStates mouseState, TextStyle textStyle, Point offset = new Point())
        {
            switch (type)
            {
                case VisualTile.TileType.Image:
                    {
                        VisualImageRenderer.RenderImageCentered(graphics, clientRectangle, image, offset);
                        break;
                    }

                case VisualTile.TileType.Text:
                    {
                        VisualTextRenderer.RenderText(graphics, clientRectangle, text, font, enabled, mouseState, textStyle);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    }
            }
        }

        #endregion
    }
}