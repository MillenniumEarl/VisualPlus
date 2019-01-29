#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Main.cs
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

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using VisualPlus;
using VisualPlus.Constants;
using VisualPlus.Events;
using VisualPlus.Managers;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Dialogs;

using VisualThemeBuilder.Controls;

#endregion

namespace VisualThemeBuilder.Forms
{
    /// <summary>The main.</summary>
    public partial class Main : VisualForm
    {
        #region Fields

        private readonly Theme theme;
        private ComponentViewer componentViewer;
        private bool saved;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Main" /> class.</summary>
        public Main()
        {
            InitializeComponent();

            foreach (Type controlType in ControlManager.ThemeSupportedTypes())
            {
                cbControls.Items.Add(controlType.FullName);
            }

            componentViewer = new ComponentViewer { BackColor = componentPanel.BackColor, Dock = DockStyle.Fill };

            theme = new Theme(DefaultConstants.DefaultStyle);
            LoadTheme(theme);

            tbName.Text = "UnnamedTheme";
            tbAuthor.Text = "Unknown";

            saved = true;

            cbControls.SelectedIndex = 0;
            tabController.SelectedIndex = 0;

            componentPanel.Controls.Add(componentViewer);

            UpdateSelection();
        }

        #endregion

        #region Public Properties

        /// <summary>Retrieves the selected property color.</summary>
        [Browsable(false)]
        public Color SelectedColor
        {
            get
            {
                Color selectedItemColor = (Color)palettePropertyGrid.SelectedGridItem.Value;
                return selectedItemColor;
            }
        }

        #endregion

        #region Methods

        /// <summary>Occurs when the control combo box selection index changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void CbControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            componentViewer.ComponentNamespace = (string)cbControls.SelectedItem;
        }

        /// <summary>Occurs when the exit button has been clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>Loads the theme settings.</summary>
        /// <param name="newTheme">The theme to update with.</param>
        private void LoadTheme(Theme newTheme)
        {
            if (newTheme == null)
            {
                throw new NoNullAllowedException(nameof(newTheme));
            }

            theme.UpdateTheme(newTheme.Information, newTheme.ColorPalette);
            rawText.Text = newTheme.RawTheme;

            palettePropertyGrid.SelectedObject = newTheme.ColorPalette;
        }

        /// <summary>Occurs when the help button has been clicked.</summary>
        /// <param name="e">The event args.</param>
        private void Main_HelpButtonClicked(ControlBoxEventArgs e)
        {
            DialogResult dialogResult = VisualMessageBox.Show(@"Would you like to visit the VisualPlus website?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Process.Start(Library.ProjectURL);
            }
        }

        /// <summary>Occurs when the form loads.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void Main_Load(object sender, EventArgs e)
        {
        }

        /// <summary>Create new theme.</summary>
        private void NewTheme()
        {
            tbPath.Text = string.Empty;

            Theme newTheme = new Theme(DefaultConstants.DefaultStyle) { Information = { Author = "Unknown", Name = "UnnamedTheme" } };

            LoadTheme(newTheme);
            saved = false;
        }

        /// <summary>Occurs when the new button has been clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                DialogResult result = MessageBox.Show(@"Would you like to save the unsaved changed?", Application.ProductName, MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem.PerformClick();
                }
                else if (result == DialogResult.No)
                {
                    NewTheme();
                    return;
                }
                else
                {
                    // Cancel
                }
            }

            NewTheme();
        }

        /// <summary>Occurs when the open directory button has been clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OpenDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tbPath.TextLength > 0)
            {
                if (File.Exists(tbPath.Text))
                {
                    string directory = Path.GetDirectoryName(tbPath.Text);
                    Process.Start(directory);
                }
            }
        }

        /// <summary>Occurs when the open templates button has been clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OpenTemplatesDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = DefaultConstants.TemplatesFolder;

            if (path.Length <= 0)
            {
                return;
            }

            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
        }

        /// <summary>Occurs when the open button has been clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = @"Theme File|*.xml";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Theme openTheme = new Theme(openFileDialog.FileName);

                    if (openTheme.Information.IsNull)
                    {
                        // Unable to read theme information maybe corrupted.
                        MessageBox.Show($@"Unable to load the theme file.{Environment.NewLine}Detected invalid header.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        tbPath.Text = openFileDialog.FileName;
                    }

                    LoadTheme(openTheme);
                }
            }
        }

        /// <summary>Occurs when the palette property value changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void PalettePropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            Color changedItemValue = (Color)e.ChangedItem.Value;

            if (changedItemValue == Color.Empty)
            {
                return;
            }

            UpdateThemeContents();
            UpdateSelection();
        }

        /// <summary>Occurs when the palette selected item changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void PalettePropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            UpdateSelection();
        }

        /// <summary>Saves the theme to file.</summary>
        private void Save()
        {
            using (StreamWriter _streamWriter = new StreamWriter(tbPath.Text, false))
            {
                _streamWriter.WriteLine(rawText.Text);
                saved = false;
            }
        }

        /// <summary>Occurs when the save as button has been clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = @"Theme File|*.xml";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tbPath.Text = saveFileDialog.FileName;
                    Save();
                }
            }
        }

        /// <summary>Occurs when the save button has been clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                return;
            }

            if (string.IsNullOrEmpty(tbPath.Text))
            {
                saveAsToolStripMenuItem.PerformClick();
            }
            else
            {
                Save();
            }
        }

        /// <summary>Occurs when the theme information text was changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void TbThemeInformation_TextChanged(object sender, EventArgs e)
        {
            UpdateThemeContents();
        }

        /// <summary>Update the color information based on the selection in the properties dialog.</summary>
        private void UpdateSelection()
        {
            if (palettePropertyGrid.SelectedGridItem != null)
            {
                object selectedGridItem = palettePropertyGrid.SelectedGridItem.Value;

                if (selectedGridItem is Color)
                {
                    tbSelectedColor.Image = ImageManager.CreateBitmap(SelectedColor, tbSelectedColor.ImageSize);
                    tbSelectedColor.ForeColor = ((ColorPalette)palettePropertyGrid.SelectedObject).TextEnabled;
                    tbSelectedColor.Text = palettePropertyGrid.SelectedGridItem.Label;
                }
            }

            componentViewer.Theme = theme;
        }

        /// <summary>Update the theme contents.</summary>
        private void UpdateThemeContents()
        {
            ThemeInformation themeInformation = new ThemeInformation { Author = tbAuthor.Text, Name = tbName.Text };

            rawText.Text = new Theme(themeInformation, theme.ColorPalette).RawTheme;
            saved = false;
        }

        #endregion
    }
}