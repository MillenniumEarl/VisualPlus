#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: XMLManager.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 01/01/2019 - 11:49 PM
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
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using VisualPlus.Constants;
using VisualPlus.Extensibility;
using VisualPlus.Structure;

#endregion

namespace VisualPlus.Managers
{
    public class XMLManager
    {
        #region Public Methods and Operators

        /// <summary>Verify the element node is empty.</summary>
        /// <param name="container">The theme container.</param>
        /// <param name="elementPath">The element path.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool NodeEmpty(XContainer container, string elementPath)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (string.IsNullOrEmpty(elementPath))
            {
                throw new ArgumentNullException(nameof(elementPath));
            }

            bool nodeEmpty;
            if (NodeExists(container, elementPath))
            {
                XElement node = container.GetNode(elementPath);
                nodeEmpty = string.IsNullOrEmpty(node.Value);
            }
            else
            {
                throw new ArgumentNullException($@"The node doesn't exist. Element Path: {elementPath}");
            }

            return nodeEmpty;
        }

        /// <summary>Verify the element node exists.</summary>
        /// <param name="container">The theme container.</param>
        /// <param name="elementPath">The element path.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool NodeExists(XContainer container, string elementPath)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (string.IsNullOrEmpty(elementPath))
            {
                throw new ArgumentNullException(nameof(elementPath));
            }

            XElement node = container.GetNode(elementPath);
            bool nodeExists = node != null;
            return nodeExists;
        }

        /// <summary>Reads the xml container element to string.</summary>
        /// <param name="container">The xml container.</param>
        /// <param name="elementPath">The element path.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ReadElement(XContainer container, string elementPath)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (string.IsNullOrEmpty(elementPath))
            {
                throw new ArgumentNullException(nameof(elementPath));
            }

            string element;

            if (NodeExists(container, elementPath))
            {
                if (NodeEmpty(container, elementPath))
                {
                    // Empty node return a default or null value;
                    element = null;
                }
                else
                {
                    // TODO: Create method to deserialize various color input data. Like Red. Currently only support HTML code (hex).
                    // Read node value.
                    element = container.GetNode(elementPath).Value;
                }
            }
            else
            {
                element = null;

                // Node not found logging to trace listener.
                ConsoleEx.WriteDebug($@"Unable to read the xml node. Path: {elementPath}");
            }

            return element;
        }

        /// <summary>Write the element string to xml.</summary>
        /// <param name="xmlWriter">The xml writer.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void WriteElement(XmlWriter xmlWriter, string name, string value)
        {
            if (xmlWriter == null)
            {
                throw new ArgumentNullException(nameof(xmlWriter));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($@"The element doesnt contain a name. Value: {value}");
            }

            if (string.IsNullOrEmpty(value))
            {
                value = ResetToDefault(name, value);

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException($@"The element doesnt contain a value: {name}");
                }
            }

            xmlWriter.WriteElementString(name, value);
        }

        /// <summary>Write the element color to xml.</summary>
        /// <param name="xmlWriter">The xml writer.</param>
        /// <param name="name">The name.</param>
        /// <param name="color">The color.</param>
        public static void WriteElement(XmlWriter xmlWriter, string name, Color color)
        {
            if (color == Color.Empty)
            {
                throw new ArgumentNullException($@"The color is empty for the element: {name}");
            }

            // TODO: Attach color encoder to allow writing various colors but also need to be able to deserialize them.
            string encodedHTML = color.ToHTML();

            WriteElement(xmlWriter, name, encodedHTML);
        }

        /// <summary>Write the element group to xml.</summary>
        /// <param name="xmlWriter">The xml writer.</param>
        /// <param name="groupName">The group name.</param>
        /// <param name="colorTable">The element color table.</param>
        public static void WriteElementGroup(XmlWriter xmlWriter, string groupName, Dictionary<string, Color> colorTable)
        {
            if (xmlWriter == null)
            {
                throw new ArgumentNullException(nameof(xmlWriter));
            }

            xmlWriter.WriteStartElement(groupName);

            foreach (var element in colorTable)
            {
                WriteElement(xmlWriter, element.Key, element.Value);
            }

            xmlWriter.WriteEndElement();
        }

        /// <summary>Write the element group to xml.</summary>
        /// <param name="xmlWriter">The xml writer.</param>
        /// <param name="groupName">The group name.</param>
        /// <param name="dataTable">The data Table.</param>
        public static void WriteElementGroup(XmlWriter xmlWriter, string groupName, Dictionary<string, string> dataTable)
        {
            if (xmlWriter == null)
            {
                throw new ArgumentNullException(nameof(xmlWriter));
            }

            xmlWriter.WriteStartElement(groupName);

            foreach (var element in dataTable)
            {
                WriteElement(xmlWriter, element.Key, element.Value);
            }

            xmlWriter.WriteEndElement();
        }

        /// <summary>Retrieves the writer settings.</summary>
        /// <returns>The <see cref="XmlWriterSettings" />.</returns>
        public static XmlWriterSettings WriterSettings()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true, Encoding = new UTF8Encoding(false), NewLineHandling = NewLineHandling.None, NewLineChars = "\n" };

            return xmlWriterSettings;
        }

        #endregion

        #region Methods

        /// <summary>Resets the empty <see cref="ThemeInformation" /> element to the defaults.</summary>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        /// <returns>The <see cref="string" />.</returns>
        private static string ResetToDefault(string name, string value)
        {
            if ((name == "Name") && string.IsNullOrEmpty(value))
            {
                value = SettingConstants.ThemeName;
            }
            else if ((name == "Author") && string.IsNullOrEmpty(value))
            {
                value = SettingConstants.ThemeAuthor;
            }

            return value;
        }

        #endregion
    }
}