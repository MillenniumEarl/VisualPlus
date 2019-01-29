#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: FormManager.cs
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
using System.Windows.Forms;

using VisualPlus.Renders;
using VisualPlus.Toolkit.Dialogs;

#endregion

namespace VisualPlus.Managers
{
    public sealed class FormManager
    {
        #region Public Methods and Operators

        /// <summary>Prepares the form for moving.</summary>
        /// <param name="form">The form.</param>
        public static void Move(Form form)
        {
            var defaultWindowHeight = 15;
            Point center = new Point(form.Width / 2, defaultWindowHeight);

            if (form is VisualForm visualForm)
            {
                if (visualForm.Moveable)
                {
                    center = new Point(visualForm.Width / 2, visualForm.WindowBarHeight / 2);
                }
                else
                {
                    return;
                }
            }

            Cursor.Position = form.PointToScreen(center);
        }

        /// <summary>Move the cursor to the form edge for sizing.</summary>
        /// <param name="form">The form.</param>
        public static void Sizing(Form form)
        {
            if (form.FormBorderStyle == FormBorderStyle.Fixed3D)
            {
                return;
            }

            if (form.FormBorderStyle == FormBorderStyle.FixedDialog)
            {
                return;
            }

            if (form.FormBorderStyle == FormBorderStyle.FixedToolWindow)
            {
                return;
            }

            if (form is VisualForm visualForm)
            {
                if (!visualForm.Sizable)
                {
                    return;
                }
            }

            Cursor.Position = form.PointToScreen(new Point(form.Width - 1, form.Height - 1));
        }

        /// <summary>Creates a default window context menu.</summary>
        /// <param name="form">The form.</param>
        /// <returns>The <see cref="ContextMenuStrip" />.</returns>
        public static ContextMenuStrip WindowContextMenu(VisualForm form)
        {
            ContextMenuStrip _contextMenu = new ContextMenuStrip { Name = Application.ProductName };

            ToolStripMenuItem _restore = new ToolStripMenuItem("Restore", null, (sender, args) => form.ControlBox.RestoreFormWindow(form));

            if ((form.WindowState == FormWindowState.Maximized) && form.MaximizeBox)
            {
                _restore.Enabled = true;
            }
            else
            {
                _restore.Enabled = false;
            }

            _restore.Image = VisualControlBoxRenderer.RenderControlBoxIcon(new Size(20, 20), VisualForm.ControlBoxIcons.Restore, Color.Black);

            ToolStripMenuItem _move = new ToolStripMenuItem("Move", null, (sender, args) => Move(form)) { Enabled = form.Moveable };

            ToolStripMenuItem _size = new ToolStripMenuItem("Size", null, (sender, args) => Sizing(form)) { Enabled = form.Sizable };

            ToolStripMenuItem _minimize = new ToolStripMenuItem("Minimize", null, (sender, args) => form.ControlBox.MinimizeForm(form));

            if ((form.WindowState != FormWindowState.Minimized) && form.MinimizeBox)
            {
                _minimize.Enabled = true;
            }
            else
            {
                _minimize.Enabled = false;
            }

            _minimize.Image = VisualControlBoxRenderer.RenderControlBoxIcon(new Size(20, 20), VisualForm.ControlBoxIcons.Minimize, Color.Black);

            ToolStripMenuItem _maximize = new ToolStripMenuItem("Maximize", null, (sender, args) => form.ControlBox.MaximizeForm(form));

            if ((form.WindowState == FormWindowState.Normal) && form.MaximizeBox)
            {
                _maximize.Enabled = true;
            }
            else
            {
                _maximize.Enabled = false;
            }

            _maximize.Image = VisualControlBoxRenderer.RenderControlBoxIcon(new Size(20, 20), VisualForm.ControlBoxIcons.Maximize, Color.Black);

            ToolStripSeparator _separator = new ToolStripSeparator();

            ToolStripMenuItem _close = new ToolStripMenuItem("Close", null, (sender, args) => form.ControlBox.CloseForm(form)) { Image = VisualControlBoxRenderer.RenderControlBoxIcon(new Size(20, 20), VisualForm.ControlBoxIcons.Close, Color.Black), ShortcutKeys = Keys.Alt | Keys.F4 };

            _contextMenu.Items.Add(_restore);
            _contextMenu.Items.Add(_move);
            _contextMenu.Items.Add(_size);
            _contextMenu.Items.Add(_minimize);
            _contextMenu.Items.Add(_maximize);
            _contextMenu.Items.Add(_separator);
            _contextMenu.Items.Add(_close);

            return _contextMenu;
        }

        #endregion
    }
}