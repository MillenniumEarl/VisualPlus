#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: MonitorManager.cs
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

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

#endregion

namespace VisualPlus.Structure
{
    [Description("The monitor manager.")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    public class MonitorManager
    {
        #region Fields

        public Rectangle Monitor;
        public Rectangle WorkingArea;

        private int _byteSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        private char[] _device;

        private int _dwordFlags;

        #endregion

        #region Constructors and Destructors

        public MonitorManager()
        {
            _byteSize = Marshal.SizeOf(typeof(MonitorManager));
            _dwordFlags = 0;
            Monitor = new Rectangle();
            WorkingArea = new Rectangle();
            _device = new char[32];
        }

        #endregion

        #region Public Properties

        public int ByteSize
        {
            get
            {
                return _byteSize;
            }

            set
            {
                _byteSize = value;
            }
        }

        public char[] Device
        {
            get
            {
                return _device;
            }

            set
            {
                _device = value;
            }
        }

        public int DWordFlags
        {
            get
            {
                return _dwordFlags;
            }

            set
            {
                _dwordFlags = value;
            }
        }

        #endregion
    }
}