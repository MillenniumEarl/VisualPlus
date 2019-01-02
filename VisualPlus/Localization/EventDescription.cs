#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: EventDescription.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 01/01/2019 - 11:44 PM
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

namespace VisualPlus.Localization
{
    public sealed class EventDescription
    {
        #region Constants

        public const string ControlDragChanged = "Occours when the control is being dragged.";
        public const string ControlDragToggleChanged = "Occours when the control drag toggle has been changed.";
        public const string CursorChanged = "Occours when the cursor for the control has been changed.";
        public const string ForeColorDisabledChanged = "The foreground disabled color of this compoment, which is used to display the text.";
        public const string MouseStateChanged = "Occours when the state of the mouse on the control changes.";
        public const string PropertyEventChanged = "Occours when the controls property was triggered by an event that has caused a change.";
        public const string StyleManagerChanged = "Occours when the style manager has changed.";
        public const string TextChanged = "Occurs when the Control.Text property value changes.";
        public const string TextRenderingHintChanged = "Occours when the TextRenderingHint has changed.";
        public const string ToggleExpanderChanged = "Occours when the expander toggle has changed.";

        #endregion
    }
}