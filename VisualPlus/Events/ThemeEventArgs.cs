﻿#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: ThemeEventArgs.cs
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

using VisualPlus.Models;

#endregion Namespace

namespace VisualPlus.Events
{
    public class ThemeEventArgs : EventArgs
    {
        #region Fields

        private Theme _theme;

        #endregion Fields

        #region Constructors and Destructors

        public ThemeEventArgs(Theme theme)
        {
            _theme = theme;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        public Theme Theme
        {
            get
            {
                return _theme;
            }

            set
            {
                _theme = value;
            }
        }

        #endregion Public Properties
    }
}