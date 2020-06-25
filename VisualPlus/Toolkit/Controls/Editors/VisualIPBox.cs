#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualIPBox.cs
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

using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;

using VisualPlus.Designer;
using VisualPlus.Toolkit.VisualBase;

#endregion Namespace

namespace VisualPlus.Toolkit.Controls.Editors
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    [Description("The Visual IPBox")]
    [Designer(typeof(VisualIPBoxDesigner))]
    [ToolboxBitmap(typeof(VisualIPBox), "VisualIPBox.bmp")]
    [ToolboxItem(false)]
    public class VisualIPBox : VisualStyleBase
    {
        #region Fields

        private int boxSpacing;
        private IPAddress ipAddress;

        #endregion Fields

        #region Constructors and Destructors

        public VisualIPBox()
        {
            // Variables
            boxSpacing = 2;
            ipAddress = IPAddress.Parse("127.0.0.1");
            Size = new Size(135, 25);

            // TODO: Place box location automatically and resize handle
        }

        #endregion Constructors and Destructors

        #region Public Properties

        public int BoxSpacing
        {
            get
            {
                return boxSpacing;
            }

            set
            {
                boxSpacing = value;
            }
        }

        /// <summary> Gets or set the IP address.</summary>
        public IPAddress IPAddress
        {
            get
            {
                return ipAddress;
            }

            set
            {
                // TODO: Update numeric boxes
                ipAddress = value;
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public override string ToString()
        {
            return nameof(VisualIPBox) + ", Value = " + ipAddress;
        }

        #endregion Public Methods and Operators
    }
}