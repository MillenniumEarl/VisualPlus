#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: EventCategory.cs
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

using VisualPlus.Constants;

#endregion

namespace VisualPlus.Localization
{
    public sealed class EventCategory
    {
#if DEBUG
        public const string Action = DefaultConstants.DefaultCategoryText;
        public const string Appearance = DefaultConstants.DefaultCategoryText;
        public const string Behavior = DefaultConstants.DefaultCategoryText;
        public const string Data = DefaultConstants.DefaultCategoryText;
        public const string DragDrop = DefaultConstants.DefaultCategoryText;
        public const string Focus = DefaultConstants.DefaultCategoryText;
        public const string Key = DefaultConstants.DefaultCategoryText;
        public const string Layout = DefaultConstants.DefaultCategoryText;
        public const string Misc = DefaultConstants.DefaultCategoryText;
        public const string Mouse = DefaultConstants.DefaultCategoryText;
        public const string PropertyChanged = DefaultConstants.DefaultCategoryText;
#else
            public const string Action = "Action";
            public const string Appearance = "Appearance";
            public const string Behavior = "Behavior";
            public const string Data = "Data";
            public const string DragDrop = "Drag Drop";
            public const string Focus = "Focus";
            public const string Key = "Key";
            public const string Layout = "Layout";
            public const string Misc = "Misc";
            public const string Mouse = "Mouse";
            public const string PropertyChanged = "Property Changed";
#endif
    }
}