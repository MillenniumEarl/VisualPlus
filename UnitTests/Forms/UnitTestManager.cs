#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: UnitTestManager.cs
// UnitTests - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 01/01/2019 - 10:49 PM
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
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using UnitTests.Tests;

using VisualPlus.Attributes;
using VisualPlus.Constants;
using VisualPlus.Events;
using VisualPlus.Managers;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Dialogs;

#endregion

namespace UnitTests.Forms
{
    /// <summary>The test manager.</summary>
    public partial class UnitTestManager : VisualForm
    {
        #region Fields

        private UnitTests _unitTest;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="UnitTestManager" /> class.</summary>
        public UnitTestManager()
        {
            // Initialize variables
            InitializeComponent();
            HelpButtonClicked += HelpButton_Click;
        }

        #endregion

        #region Enums

        /// <summary>The different kind of unit tests.</summary>
        private enum UnitTests
        {
            /// <summary>The VisualForm.</summary>
            VisualForm = 0,

            /// <summary>The visual color dialog.</summary>
            VisualColorDialog = 1,

            /// <summary>The VisualControlBox.</summary>
            VisualControlBox = 2,

            /// <summary>The list view.</summary>
            VisualListView = 3,

            /// <summary>The visual exception dialog.</summary>
            VisualExceptionDialog = 4,

            /// <summary>The visual input box.</summary>
            VisualInputDialog = 5,

            /// <summary>The visual message box.</summary>
            VisualMessageBox = 6,

            /// <summary>The clipboard test.</summary>
            ClipboardTest = 7
        }

        #endregion

        #region Methods

        /// <summary>Creates a data entry.</summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        private static VisualListViewItem DataEntry(string name, string category)
        {
            VisualListViewItem dataEntry = new VisualListViewItem(name);
            VisualListViewSubItem subEntry = new VisualListViewSubItem(category);
            dataEntry.SubItems.Add(subEntry);

            return dataEntry;
        }

        /// <summary>Retrieve the reflection type name.</summary>
        /// <param name="data">The data type.</param>
        /// <returns>The <see cref="string" />.</returns>
        private static string RetrieveType(Type data)
        {
            string result;

            if (data.ReflectedType != null)
            {
                result = data.ReflectedType.Name;
            }
            else
            {
                result = "Unable to load reflected type.";
            }

            return result;
        }

        /// <summary>Occurs when the button to run tests was clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void BtnRunTest_Click(object sender, EventArgs e)
        {
            VisualForm _formToOpen;

            switch (_unitTest)
            {
                case UnitTests.VisualForm:
                    {
                        _formToOpen = new VisualForm($@"{nameof(VisualForm)} Test", FormStartPosition.CenterParent);
                        _formToOpen.ShowDialog();
                        break;
                    }

                case UnitTests.VisualControlBox:
                    {
                        _formToOpen = new VisualControlBoxTest();
                        _formToOpen.ShowDialog();
                        break;
                    }

                case UnitTests.VisualListView:
                    {
                        _formToOpen = new VisualListViewTest();
                        _formToOpen.ShowDialog();
                        break;
                    }

                case UnitTests.VisualMessageBox:
                    {
                        _formToOpen = new VisualMessageBoxTest();
                        _formToOpen.ShowDialog();
                        break;
                    }

                case UnitTests.VisualInputDialog:
                    {
                        VisualInputDialog inputDialog = new VisualInputDialog(@"Input Dialog Test");

                        if (inputDialog.ShowDialog() == DialogResult.OK)
                        {
                            ConsoleEx.WriteDebug(inputDialog.InputResult);
                        }

                        break;
                    }

                case UnitTests.VisualExceptionDialog:
                    {
                        VisualExceptionDialog.Show(new Exception("Your custom exception message."));
                        break;
                    }

                case UnitTests.VisualColorDialog:
                    {
                        VisualColorDialog colorDialog = new VisualColorDialog();

                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            ConsoleEx.WriteDebug(colorDialog.Color);
                        }

                        break;
                    }

                case UnitTests.ClipboardTest:
                    {
                        _formToOpen = new ClipboardTest();
                        _formToOpen.ShowDialog();
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
        }

        /// <summary>Generates the test statistics.</summary>
        /// <returns>The <see cref="string" />.</returns>
        private string GenerateTestStatistics()
        {
            StringBuilder stats = new StringBuilder();
            stats.AppendLine($@"Test Index: {visualListBoxTests.SelectedIndex}");
            stats.AppendLine($@"Total Tests: {visualListBoxTests.Items.Count}");
            return stats.ToString();
        }

        /// <summary>Occurs when the help button was clicked.</summary>
        /// <param name="e">The event args.</param>
        private void HelpButton_Click(ControlBoxEventArgs e)
        {
            DialogResult dialogResult = VisualMessageBox.Show(@"Would you like to visit the website?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Process.Start(SettingConstants.ProjectURL);
            }
        }

        /// <summary>Occurs when the unit test selection was changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ListBoxTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            _unitTest = (UnitTests)visualListBoxTests.SelectedIndex;
            visualLabelTestsStats.Text = GenerateTestStatistics();
        }

        /// <summary>Loads the flagged test attributes data.</summary>
        private void LoadFlaggedTests()
        {
            lvFlaggedTests.Columns.Add(null, "Name", 50);
            lvFlaggedTests.Columns.Add(null, "Namespace", 80);

            lvFlaggedTests.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            var constructors = ReflectionManager.LoadConstructors(AssemblyManager.VisualPlus(), typeof(Test));
            var events = ReflectionManager.LoadEvents(AssemblyManager.VisualPlus(), typeof(Test));
            var fields = ReflectionManager.LoadFields(AssemblyManager.VisualPlus(), typeof(Test));
            var members = ReflectionManager.LoadMembers(AssemblyManager.VisualPlus(), typeof(Test));
            var methods = ReflectionManager.LoadMethods(AssemblyManager.VisualPlus(), typeof(Test));
            var properties = ReflectionManager.LoadProperties(AssemblyManager.VisualPlus(), typeof(Test));

            foreach (ConstructorInfo data in constructors)
            {
                string typeName = RetrieveType(data.ReflectedType);
                VisualListViewItem item = DataEntry(data.Name, typeName);

                if (!lvFlaggedTests.Items.ContainsItem(item))
                {
                    lvFlaggedTests.Items.Add(item);
                }
            }

            foreach (EventInfo data in events)
            {
                string typeName = RetrieveType(data.ReflectedType);
                VisualListViewItem item = DataEntry(data.Name, typeName);
                if (!lvFlaggedTests.Items.ContainsItem(item))
                {
                    lvFlaggedTests.Items.Add(item);
                }
            }

            foreach (FieldInfo data in fields)
            {
                string typeName = RetrieveType(data.ReflectedType);
                VisualListViewItem item = DataEntry(data.Name, typeName);
                if (!lvFlaggedTests.Items.ContainsItem(item))
                {
                    lvFlaggedTests.Items.Add(item);
                }
            }

            foreach (MemberInfo data in members)
            {
                string typeName = RetrieveType(data.ReflectedType);
                VisualListViewItem item = DataEntry(data.Name, typeName);

                if (!lvFlaggedTests.Items.ContainsItem(item))
                {
                    lvFlaggedTests.Items.Add(item);
                }
            }

            foreach (MethodInfo data in methods)
            {
                string typeName = RetrieveType(data.ReflectedType);
                VisualListViewItem item = DataEntry(data.Name, typeName);
                if (!lvFlaggedTests.Items.ContainsItem(item))
                {
                    lvFlaggedTests.Items.Add(item);
                }
            }

            foreach (PropertyInfo data in properties)
            {
                string typeName = RetrieveType(data.ReflectedType);
                VisualListViewItem item = DataEntry(data.Name, typeName);
                if (!lvFlaggedTests.Items.ContainsItem(item))
                {
                    lvFlaggedTests.Items.Add(item);
                }
            }
        }

        /// <summary>Occurs when the form loads.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void TestManager_Load(object sender, EventArgs e)
        {
            _unitTest = UnitTests.VisualListView;

            Array _tests = typeof(UnitTests).GetEnumValues();
            visualLabelTestsStats.Text = GenerateTestStatistics();

            foreach (object test in _tests)
            {
                visualListBoxTests.Items.Add(test);
            }

            visualListBoxTests.SelectedIndex = 0;
            tabController.SelectedIndex = 0;

            LoadFlaggedTests();
        }

        #endregion
    }
}