#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ComponentViewer.cs
// VisualThemeBuilder - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:28 AM
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
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus;
using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Events;
using VisualPlus.Extensibility;
using VisualPlus.Interfaces;
using VisualPlus.Managers;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Controls.DataManagement;
using VisualPlus.Toolkit.Controls.DataVisualization;
using VisualPlus.Toolkit.Controls.Editors;
using VisualPlus.Toolkit.Controls.Interactivity;
using VisualPlus.Toolkit.Dialogs;

#endregion

namespace VisualThemeBuilder.Controls
{
    /// <summary>The component viewer.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("ThemeChanged")]
    [DefaultProperty("Theme")]
    [Description("The Component Viewer")]
    [ToolboxItem(false)]
    public partial class ComponentViewer : UserControl
    {
        #region Fields

        private Control component;
        private string componentNamespace;
        private Type componentType;
        private Theme theme;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ComponentViewer" /> class.</summary>
        /// <param name="typeNamespace">The component type namespace to display.</param>
        public ComponentViewer(string typeNamespace) : this()
        {
            componentNamespace = typeNamespace;
            ComponentNamespace = componentNamespace;
        }

        /// <summary>Initializes a new instance of the <see cref="ComponentViewer" /> class.</summary>
        public ComponentViewer()
        {
            InitializeComponent();
            component = null;
            componentNamespace = string.Empty;
            componentType = null;
            theme = new Theme(Settings.DefaultValue.DefaultStyle);
        }

        #endregion

        #region Public Events

        [Description("Occurs when the component has changed.")]
        public event ControlEventHandler ComponentChanged;

        [Description("Occurs when the theme has changed.")]
        public event ThemeChangedEventHandler ThemeChanged;

        #endregion

        #region Public Properties

        /// <summary>The <see cref="Component" /> namespace to use for the viewer.</summary>
        [Browsable(true)]
        public string ComponentNamespace
        {
            get
            {
                return componentType.Namespace;
            }

            set
            {
                componentNamespace = value;
                OnComponentChanged(new ControlEventArgs(component));
            }
        }

        /// <summary>Determines if the <see cref="Component" /> is a Control.</summary>
        [Browsable(true)]
        [ReadOnly(true)]
        public bool IsControl
        {
            get
            {
                return ControlManager.IsControl(componentType);
            }
        }

        /// <summary>Determines if the <see cref="Component" /> is a Dialog.</summary>
        [Browsable(true)]
        [ReadOnly(true)]
        public bool IsDialog
        {
            get
            {
                return ControlManager.IsDialog(componentType);
            }
        }

        /// <summary>The <see cref="Theme" /> to use for the <see cref="Component" />.</summary>
        [Browsable(true)]
        public Theme Theme
        {
            get
            {
                return theme;
            }

            set
            {
                theme = value;
                OnThemeChanged(new ThemeEventArgs(theme));
            }
        }

        #endregion

        #region Methods

        /// <summary>Occurs when the component changed.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnComponentChanged(ControlEventArgs e)
        {
            CreateComponentInstance();
            ApplyTheme();
            ComponentChanged?.Invoke(this, e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (component != null)
            {
                component.ToCenter();
            }
        }

        /// <summary>Occurs when the theme changed.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnThemeChanged(ThemeEventArgs e)
        {
            ApplyTheme();
            ThemeChanged?.Invoke(e);
        }

        /// <summary>Applies the theme to the <see cref="Component" />.</summary>
        private void ApplyTheme()
        {
            if (component is IThemeSupport supportedControl)
            {
                supportedControl.UpdateTheme(theme);

                if (!IsDialog)
                {
                    component.BackColor = BackColor;
                }
            }
        }

        /// <summary>Create component instance.</summary>
        private void CreateComponentInstance()
        {
            if (componentNamespace == null)
            {
                return;
            }

            string visualPlusEntryPoint = SettingConstants.ProductName;

            componentType = Type.GetType(string.Concat(componentNamespace, ", ", visualPlusEntryPoint));
            component = (Control)Activator.CreateInstance(componentType);

            if (IsDialog)
            {
                if (component is VisualForm form)
                {
                    form.Text = @"VisualPlus";
                    form.Size = new Size(300, 250);
                    form.TopLevel = false;
                    form.AutoScroll = true;
                    form.UpdateTheme(theme);
                    form.Show();
                }
            }
            else
            {
                if (component is VisualComboBox comboBox)
                {
                    // Generate a sample items
                    for (var i = 0; i <= 7; i++)
                    {
                        comboBox.Items.Add("Item #" + i);
                    }

                    comboBox.SelectedIndex = 0;
                }
                else if (component is VisualDateTimePicker)
                {
                    // Do nothing. Doesn't like un-formatted Text.
                }
                else if (component is VisualGauge gauge)
                {
                    gauge.Value = 50;
                }
                else if (component is VisualListBox listBox)
                {
                    // Generate a sample items
                    for (var i = 0; i <= 7; i++)
                    {
                        listBox.Items.Add("Item #" + i);
                    }

                    listBox.SelectedIndex = 0;
                }
                else if (component is VisualListView listView)
                {
                    listView.Size = new Size(250, 200);
                    listView.Columns.Add("1", "Column 1", 100);
                    listView.Columns.Add("2", "Column 2", 100);

                    // Generate a sample items
                    for (var i = 0; i <= 7; i++)
                    {
                        VisualListViewItem item = new VisualListViewItem("Item #" + i);
                        VisualListViewSubItem subItem = new VisualListViewSubItem("SubItem #" + i);
                        item.SubItems.Add(subItem);
                        listView.Items.Add(item);
                    }

                    // listView.SelectedIndex = 0;
                }
                else if (component is VisualProgressBar progressBar)
                {
                    progressBar.Value = 50;
                }
                else if (component is VisualRadialProgress radialProgress)
                {
                    radialProgress.Value = 50;
                }
                else
                {
                    component.Text = @"VisualPlus";
                }
            }

            Controls.Clear();
            Controls.Add(component);
            component.ToCenter();
        }

        #endregion
    }
}