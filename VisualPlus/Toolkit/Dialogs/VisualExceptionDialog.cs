#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualExceptionDialog.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:27 AM
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
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Structure;
using VisualPlus.Toolkit.VisualBase;

#endregion

namespace VisualPlus.Toolkit.Dialogs
{
    /// <summary>The <see cref="VisualExceptionDialog" />.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Load")]
    [DefaultProperty("Text")]
    [Description("The Visual Exception Dialog")]
    [DesignerCategory("Form")]
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public partial class VisualExceptionDialog : VisualDialog
    {
        #region Fields

        private readonly Exception _exception;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualExceptionDialog" /> class.</summary>
        /// <param name="e">The exception.</param>
        /// <param name="caption">The caption.</param>
        public VisualExceptionDialog(Exception e, string caption = "Exception Dialog") : this()
        {
            Text = caption;
            _exception = e;

            string _message = _exception?.Message ?? "The exception was null.";
            tbMessage.Text = _message;

            string _messageT = _exception?.GetType().ToString() ?? "The exception was null.";
            tbType.Text = _messageT;

            if (_exception != null)
            {
                tbStackTrace.Text = _exception.StackTrace;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="VisualExceptionDialog" /> class.</summary>
        public VisualExceptionDialog()
        {
            InitializeComponent();
            dialogImage.Image = SystemIcons.Error.ToBitmap();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Show the exception dialog.</summary>
        /// <param name="exception">The exception.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="dialogWindow">The dialog Window.</param>
        public static void Show(Exception exception, string caption = "Exception Dialog", bool dialogWindow = true)
        {
            BackgroundWorker _backgroundWorkerShow = new BackgroundWorker();
            _backgroundWorkerShow.DoWork += BackgroundWorker_DoShowWork(exception, caption, dialogWindow);
            _backgroundWorkerShow.RunWorkerAsync();
        }

        /// <summary>Copy the log to the clipboard.</summary>
        public void CopyLogToClipboard()
        {
            Clipboard.SetText(ConsoleEx.Generate(_exception));
        }

        /// <summary>Saves the log to a file.</summary>
        /// <param name="filePath">The file Path.</param>
        public void SaveLog(string filePath)
        {
            File.WriteAllText(filePath, ConsoleEx.Generate(_exception));
        }

        #endregion

        #region Methods

        /// <summary>Display the <see cref="VisualExceptionDialog" />.</summary>
        /// <param name="exception">The exception.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="dialogWindow">The dialog Window.</param>
        /// <returns>The <see cref="DoWorkEventHandler" />.</returns>
        private static DoWorkEventHandler BackgroundWorker_DoShowWork(Exception exception, string caption, bool dialogWindow)
        {
            VisualExceptionDialog _exceptionDialog = new VisualExceptionDialog(exception) { Text = caption };

            if (dialogWindow)
            {
                _exceptionDialog.ShowDialog();
            }
            else
            {
                _exceptionDialog.Show();
            }

            return null;
        }

        /// <summary>The Copy button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            CopyLogToClipboard();
        }

        /// <summary>The Copy button is clicked.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog _saveFileDialog = new SaveFileDialog { Title = @"Save exception log...", Filter = @"Text Files|*.log;*.txt|All Files|*.*" };

            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveLog(_saveFileDialog.FileName);
            }
        }

        #endregion
    }
}