#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualControlBoxRenderer.cs
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

#endregion License

#region Namespace

using System;
using System.Drawing;

using VisualPlus.Constants;
using VisualPlus.Toolkit.Dialogs;

#endregion Namespace

namespace VisualPlus.Renders
{
    public sealed class VisualControlBoxRenderer
    {
        #region Public Methods and Operators

        /// <summary>Renders the control box icon to a bitmap.</summary>
        /// <param name="size">The bitmap size.</param>
        /// <param name="controlBoxIcons">The control Box Icon.</param>
        /// <param name="color">The color.</param>
        /// <param name="emSize">The em Size.</param>
        /// <returns>The <see cref="Bitmap" />.</returns>
        public static Bitmap RenderControlBoxIcon(Size size, VisualForm.ControlBoxIcons controlBoxIcons, Color color, float emSize = 12F)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            string text;

            switch (controlBoxIcons)
            {
                case VisualForm.ControlBoxIcons.Help:
                    {
                        text = ControlBoxConstants.HelpText;
                        break;
                    }

                case VisualForm.ControlBoxIcons.Minimize:
                    {
                        text = ControlBoxConstants.MinimizeText;
                        break;
                    }

                case VisualForm.ControlBoxIcons.Maximize:
                    {
                        text = ControlBoxConstants.MaximizeText;
                        break;
                    }

                case VisualForm.ControlBoxIcons.Restore:
                    {
                        text = ControlBoxConstants.RestoreText;
                        break;
                    }

                case VisualForm.ControlBoxIcons.Close:
                    {
                        text = ControlBoxConstants.CloseText;
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(controlBoxIcons), controlBoxIcons, null);
                    }
            }

            graphics.DrawString(text, new Font("Marlett", emSize), new SolidBrush(color), new PointF(0, 0));
            return bitmap;
        }

        #endregion Public Methods and Operators
    }
}