#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: XmlExtension.cs
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

using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

#endregion Namespace

namespace VisualPlus.Extensibility
{
    /// <summary>The collection of the <see cref="XmlExtension" /> class.</summary>
    public static class XmlExtension
    {
        #region Public Methods and Operators

        public static XmlNode AppendElement(this XmlNode parent, string tagName)
        {
            return parent.AppendElement(tagName, null, false);
        }

        public static XmlNode AppendElement(this XmlNode parent, string tagName, string textContent, bool checkTextContent = true)
        {
            if (!checkTextContent || !string.IsNullOrEmpty(textContent))
            {
                XmlDocument xd;

                if (parent is XmlDocument)
                {
                    xd = (XmlDocument)parent;
                }
                else
                {
                    xd = parent.OwnerDocument;
                }

                XmlNode node = xd.CreateElement(tagName);
                parent.AppendChild(node);

                if (textContent != null)
                {
                    XmlNode content = xd.CreateTextNode(textContent);
                    node.AppendChild(content);
                }

                return node;
            }

            return null;
        }

        public static string GetAttributeFirstValue(this XElement xe, params string[] names)
        {
            string value;
            foreach (string name in names)
            {
                value = xe.GetAttributeValue(name);
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }

            return string.Empty;
        }

        public static string GetAttributeValue(this XElement xe, string name)
        {
            if (xe != null)
            {
                XAttribute xaItem = xe.Attribute(name);
                if (xaItem != null)
                {
                    return xaItem.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>Retrieves the descendant content.</summary>
        /// <param name="container">The container.</param>
        /// <param name="name">The name.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetDescendantContent(this XContainer container, string name)
        {
            return string.Concat(container.Descendants(name));
        }

        /// <summary>Retrieves the descendants content.</summary>
        /// <param name="container">The container.</param>
        /// <param name="name">The name.</param>
        /// <returns>The <see cref="XElement" />.</returns>
        public static IEnumerable<XElement> GetDescendants(this XContainer container, string name)
        {
            return container.Descendants(name);
        }

        public static XElement GetElement(this XElement xe, params string[] elements)
        {
            XElement result = null;

            if ((xe != null) && (elements != null) && (elements.Length > 0))
            {
                result = xe;

                foreach (string element in elements)
                {
                    result = result.Element(element);
                    if (result == null)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        public static XElement GetElement(this XDocument xd, params string[] elements)
        {
            if ((xd != null) && (elements != null) && (elements.Length > 0))
            {
                XElement result = xd.Root;

                if (result.Name == elements[0])
                {
                    for (var i = 1; i < elements.Length; i++)
                    {
                        result = result.Element(elements[i]);
                        if (result == null)
                        {
                            break;
                        }
                    }

                    return result;
                }
            }

            return null;
        }

        public static string GetElementValue(this XElement xe, XName name)
        {
            if (xe != null)
            {
                XElement xeItem = xe.Element(name);
                if (xeItem != null)
                {
                    return xeItem.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>Retrieves the node from the path.</summary>
        /// <param name="container">The container.</param>
        /// <param name="path">The full node path.</param>
        /// <returns>The <see cref="XElement" />.</returns>
        public static XElement GetNode(this XContainer container, string path)
        {
            path = path.Trim().Trim('/');

            if ((container == null) || string.IsNullOrEmpty(path))
            {
                return null;
            }

            XContainer _lastContainer = container;

            var _splitPath = path.Split('/');

            if ((_splitPath == null) || (_splitPath.Length <= 0))
            {
                return null;
            }

            foreach (string _name in _splitPath)
            {
                if (_name.Contains('|'))
                {
                    var _splitName = _name.Split('|');

                    XContainer _lastContainer2 = null;

                    foreach (string _containerName2 in _splitName)
                    {
                        _lastContainer2 = _lastContainer.Element(_containerName2);
                        if (_lastContainer2 != null)
                        {
                            break;
                        }
                    }

                    _lastContainer = _lastContainer2;
                }
                else
                {
                    _lastContainer = _lastContainer.Element(_name);
                }

                if (_lastContainer == null)
                {
                    return null;
                }
            }

            return (XElement)_lastContainer;
        }

        /// <summary>Retrieves the elements nodes.</summary>
        /// <param name="elements">The elements.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetNodeContent(this IEnumerable<XElement> elements)
        {
            return string.Concat(elements.Nodes());
        }

        /// <summary>Retrieves the nodes content.</summary>
        /// <param name="elements">The elements.</param>
        /// <returns>The <see cref="XNode" />.</returns>
        public static IEnumerable<XElement> GetNodes(this IEnumerable<XElement> elements)
        {
            return elements.Descendants();
        }

        /// <summary>Retrieves the node from the path.</summary>
        /// <param name="container">The container.</param>
        /// <param name="path">The full node path.</param>
        /// <returns>The <see cref="XElement" />.</returns>
        public static XElement[] GetNodes(this XContainer container, string path)
        {
            path = path.Trim().Trim('/');

            if ((container == null) || string.IsNullOrEmpty(path))
            {
                return null;
            }

            int _index = path.LastIndexOf('/');

            if (_index <= -1)
            {
                return null;
            }

            string _leftPath = path.Left(_index);
            string _lastPath = path.RemoveLeft(_index + 1);
            XElement lastNode = container.GetNode(_leftPath);

            return lastNode?.Elements(_lastPath).Where(x => x != null).ToArray();
        }

        /// <summary>Retrieves the value.</summary>
        /// <param name="element">The element.</param>
        /// <param name="path">The full path.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetValue(this XContainer element, string path, string defaultValue = null)
        {
            return element.GetNode(path)?.Value ?? defaultValue;
        }

        public static XmlNode PrependElement(this XmlNode parent, string tagName)
        {
            return parent.PrependElement(tagName, null, false);
        }

        public static XmlNode PrependElement(this XmlNode parent, string tagName, string textContent, bool checkTextContent = true)
        {
            if (!checkTextContent || !string.IsNullOrEmpty(textContent))
            {
                XmlDocument xd;

                if (parent is XmlDocument)
                {
                    xd = (XmlDocument)parent;
                }
                else
                {
                    xd = parent.OwnerDocument;
                }

                XmlNode node = xd.CreateElement(tagName);
                parent.PrependChild(node);

                if (textContent != null)
                {
                    XmlNode content = xd.CreateTextNode(textContent);
                    node.PrependChild(content);
                }

                return node;
            }

            return null;
        }

        public static void WriteElementIfNotEmpty(this XmlTextWriter writer, string name, string value)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
            {
                writer.WriteElementString(name, value);
            }
        }

        #endregion Public Methods and Operators
    }
}