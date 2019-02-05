#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ThemeSerialization.cs
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
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using VisualPlus.Constants;
using VisualPlus.Enumerators;
using VisualPlus.Extensibility;
using VisualPlus.Localization;
using VisualPlus.Utilities;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Models
{
    public class ThemeSerialization
    {
        #region Public Methods and Operators

        /// <summary>Deserialize the theme contents.</summary>
        /// <param name="themeDocument">The theme document.</param>
        /// <returns>The <see cref="Theme" />.</returns>
        public static Theme Deserialize(XContainer themeDocument)
        {
            const string Header = @"VisualPlus-Theme/";
            const string Information = Header + @"Information/";
            const string StyleTable = Header + @"StyleTable/";
            const string Shared = StyleTable + @"Shared/";
            const string Toolkit = StyleTable + @"Toolkit/";

            ThemeInformation themeInformation = new ThemeInformation();
            ColorPalette colorPalette = new ColorPalette();

            // TODO: Method body is too complex to properly analyze, consider decomposing it.
            try
            {
                themeInformation.Author = XMLManager.ReadElement(themeDocument, Information + "Author");
                themeInformation.Name = XMLManager.ReadElement(themeDocument, Information + "Name");

                colorPalette.BorderNormal = XMLManager.ReadElement(themeDocument, Shared + "Border/Normal").ToColor();
                colorPalette.BorderHover = XMLManager.ReadElement(themeDocument, Shared + "Border/Hover").ToColor();

                colorPalette.TextDisabled = XMLManager.ReadElement(themeDocument, Shared + "Text/Disabled").ToColor();
                colorPalette.TextEnabled = XMLManager.ReadElement(themeDocument, Shared + "Text/Enabled").ToColor();
                colorPalette.TextHover = XMLManager.ReadElement(themeDocument, Shared + "Text/Hover").ToColor();
                colorPalette.TextPressed = XMLManager.ReadElement(themeDocument, Shared + "Text/Pressed").ToColor();
                colorPalette.Selected = XMLManager.ReadElement(themeDocument, Shared + "Text/Selected").ToColor();
                colorPalette.SubscriptColor = XMLManager.ReadElement(themeDocument, Shared + "Text/Subscript").ToColor();
                colorPalette.SuperscriptColor = XMLManager.ReadElement(themeDocument, Shared + "Text/Superscript").ToColor();
                colorPalette.TextLight = XMLManager.ReadElement(themeDocument, Shared + "Text/TextLight").ToColor();

                // colorPalette.Font = FontManager.ResolveFontFamily(themeDocument.GetValue(Shared + "Font/FontFamily"));
                colorPalette.Item = XMLManager.ReadElement(themeDocument, Shared + "ListItem/Normal").ToColor();
                colorPalette.ItemHover = XMLManager.ReadElement(themeDocument, Shared + "ListItem/Hover").ToColor();
                colorPalette.ItemSelected = XMLManager.ReadElement(themeDocument, Shared + "ListItem/Selected").ToColor();
                colorPalette.ItemAlternate = XMLManager.ReadElement(themeDocument, Shared + "ListItem/Alternate").ToColor();

                colorPalette.ColumnHeader = XMLManager.ReadElement(themeDocument, Shared + "ColumnHeader/Header").ToColor();
                colorPalette.ColumnText = XMLManager.ReadElement(themeDocument, Shared + "ColumnHeader/Text").ToColor();

                colorPalette.ControlEnabled = XMLManager.ReadElement(themeDocument, Shared + "Control/Enabled").ToColor();
                colorPalette.ControlDisabled = XMLManager.ReadElement(themeDocument, Shared + "Control/Disabled").ToColor();

                colorPalette.BackCircle = XMLManager.ReadElement(themeDocument, Toolkit + "VisualRadialProgress/BackCircle").ToColor();
                colorPalette.ForeCircle = XMLManager.ReadElement(themeDocument, Toolkit + "VisualRadialProgress/ForeCircle").ToColor();

                colorPalette.ProgressBackground = XMLManager.ReadElement(themeDocument, Shared + "ProgressBar/Background").ToColor();
                colorPalette.Progress = XMLManager.ReadElement(themeDocument, Shared + "ProgressBar/Working").ToColor();
                colorPalette.ProgressDisabled = XMLManager.ReadElement(themeDocument, Shared + "ProgressBar/Disabled").ToColor();

                colorPalette.HatchBackColor = XMLManager.ReadElement(themeDocument, Shared + "Hatch/BackColor").ToColor();
                colorPalette.HatchForeColor = XMLManager.ReadElement(themeDocument, Shared + "Hatch/ForeColor").ToColor();

                colorPalette.ElementDisabled = XMLManager.ReadElement(themeDocument, Shared + "Element/Disabled").ToColor();
                colorPalette.ElementEnabled = XMLManager.ReadElement(themeDocument, Shared + "Element/Enabled").ToColor();

                colorPalette.WatermarkActive = XMLManager.ReadElement(themeDocument, Shared + "Watermark/Active").ToColor();
                colorPalette.WatermarkInactive = XMLManager.ReadElement(themeDocument, Shared + "Watermark/Inactive").ToColor();

                colorPalette.TabPageEnabled = XMLManager.ReadElement(themeDocument, Shared + "TabPage/Enabled").ToColor();
                colorPalette.TabPageDisabled = XMLManager.ReadElement(themeDocument, Shared + "TabPage/Disabled").ToColor();
                colorPalette.TabPageHover = XMLManager.ReadElement(themeDocument, Shared + "TabPage/Hover").ToColor();
                colorPalette.TabPageSelected = XMLManager.ReadElement(themeDocument, Shared + "TabPage/Selected").ToColor();

                colorPalette.Enabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualButton/Enabled").ToColor();
                colorPalette.Disabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualButton/Disabled").ToColor();
                colorPalette.Hover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualButton/Hover").ToColor();
                colorPalette.Pressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualButton/Pressed").ToColor();

                colorPalette.VisualComboBoxDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualComboBox/Disabled").ToColor();
                colorPalette.VisualComboBoxEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualComboBox/Enabled").ToColor();

                colorPalette.FormBackground = XMLManager.ReadElement(themeDocument, Toolkit + "VisualForm/Background").ToColor();
                colorPalette.FormWindowBar = XMLManager.ReadElement(themeDocument, Toolkit + "VisualForm/WindowBar").ToColor();

                colorPalette.HelpButtonBackDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/HelpButton/BackColorState/Disabled").ToColor();
                colorPalette.HelpButtonBackEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/HelpButton/BackColorState/Enabled").ToColor();
                colorPalette.HelpButtonBackHover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/HelpButton/BackColorState/Hover").ToColor();
                colorPalette.HelpButtonBackPressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/HelpButton/BackColorState/Pressed").ToColor();

                colorPalette.HelpButtonForeDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/HelpButton/ForeColorState/Disabled").ToColor();
                colorPalette.HelpButtonForeEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/HelpButton/ForeColorState/Enabled").ToColor();
                colorPalette.HelpButtonForeHover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/HelpButton/ForeColorState/Hover").ToColor();
                colorPalette.HelpButtonForePressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/HelpButton/ForeColorState/Pressed").ToColor();

                colorPalette.MinimizeButtonBackDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MinimizeButton/BackColorState/Disabled").ToColor();
                colorPalette.MinimizeButtonBackEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MinimizeButton/BackColorState/Enabled").ToColor();
                colorPalette.MinimizeButtonBackHover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MinimizeButton/BackColorState/Hover").ToColor();
                colorPalette.MinimizeButtonBackPressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MinimizeButton/BackColorState/Pressed").ToColor();

                colorPalette.MinimizeButtonForeDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MinimizeButton/ForeColorState/Disabled").ToColor();
                colorPalette.MinimizeButtonForeEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MinimizeButton/ForeColorState/Enabled").ToColor();
                colorPalette.MinimizeButtonForeHover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MinimizeButton/ForeColorState/Hover").ToColor();
                colorPalette.MinimizeButtonForePressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MinimizeButton/ForeColorState/Pressed").ToColor();

                colorPalette.MaximizeButtonBackDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MaximizeButton/BackColorState/Disabled").ToColor();
                colorPalette.MaximizeButtonBackEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MaximizeButton/BackColorState/Enabled").ToColor();
                colorPalette.MaximizeButtonBackHover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MaximizeButton/BackColorState/Hover").ToColor();
                colorPalette.MaximizeButtonBackPressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MaximizeButton/BackColorState/Pressed").ToColor();

                colorPalette.MaximizeButtonForeDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MaximizeButton/ForeColorState/Disabled").ToColor();
                colorPalette.MaximizeButtonForeEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MaximizeButton/ForeColorState/Enabled").ToColor();
                colorPalette.MaximizeButtonForeHover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MaximizeButton/ForeColorState/Hover").ToColor();
                colorPalette.MaximizeButtonForePressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/MaximizeButton/ForeColorState/Pressed").ToColor();

                colorPalette.CloseButtonBackDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/CloseButton/BackColorState/Disabled").ToColor();
                colorPalette.CloseButtonBackEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/CloseButton/BackColorState/Enabled").ToColor();
                colorPalette.CloseButtonBackHover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/CloseButton/BackColorState/Hover").ToColor();
                colorPalette.CloseButtonBackPressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/CloseButton/BackColorState/Pressed").ToColor();

                colorPalette.CloseButtonForeDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/CloseButton/ForeColorState/Disabled").ToColor();
                colorPalette.CloseButtonForeEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/CloseButton/ForeColorState/Enabled").ToColor();
                colorPalette.CloseButtonForeHover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/CloseButton/ForeColorState/Hover").ToColor();
                colorPalette.CloseButtonForePressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualControlBox/CloseButton/ForeColorState/Pressed").ToColor();

                colorPalette.ScrollBarDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualScrollBar/Bar/Disabled").ToColor();
                colorPalette.ScrollBarEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualScrollBar/Bar/Enabled").ToColor();

                colorPalette.ScrollThumbDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualScrollBar/Thumb/Disabled").ToColor();
                colorPalette.ScrollThumbEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualScrollBar/Thumb/Enabled").ToColor();
                colorPalette.ScrollThumbHover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualScrollBar/Thumb/Hover").ToColor();
                colorPalette.ScrollThumbPressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualScrollBar/Thumb/Pressed").ToColor();

                colorPalette.ScrollButtonDisabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualScrollBar/Button/Disabled").ToColor();
                colorPalette.ScrollButtonEnabled = XMLManager.ReadElement(themeDocument, Toolkit + "VisualScrollBar/Button/Enabled").ToColor();
                colorPalette.ScrollButtonHover = XMLManager.ReadElement(themeDocument, Toolkit + "VisualScrollBar/Button/Hover").ToColor();
                colorPalette.ScrollButtonPressed = XMLManager.ReadElement(themeDocument, Toolkit + "VisualScrollBar/Button/Pressed").ToColor();

                colorPalette.Star = XMLManager.ReadElement(themeDocument, Toolkit + "VisualRating/Star").ToColor();
                colorPalette.StarBorder = XMLManager.ReadElement(themeDocument, Toolkit + "VisualRating/Border").ToColor();
                colorPalette.StarDull = XMLManager.ReadElement(themeDocument, Toolkit + "VisualRating/Dull").ToColor();
                colorPalette.StarDullBorder = XMLManager.ReadElement(themeDocument, Toolkit + "VisualRating/DullBorder").ToColor();

                colorPalette.VisualSeparatorLine = XMLManager.ReadElement(themeDocument, Toolkit + "VisualSeparator/Line").ToColor();
                colorPalette.VisualSeparatorShadow = XMLManager.ReadElement(themeDocument, Toolkit + "VisualSeparator/Shadow").ToColor();
            }
            catch (Exception e)
            {
                Logger.WriteDebug(e);
            }

            Theme theme = new Theme(themeInformation, colorPalette);
            return theme;
        }

        /// <summary>Deserialize a internal theme from resources.</summary>
        /// <param name="themes">The internal themes.</param>
        /// <returns>The <see cref="Theme" />.</returns>
        public static Theme Deserialize(Themes themes)
        {
            Theme _theme = null;

            try
            {
                string rawThemeResource = ResourcesManager.ReadResource(Assembly.GetExecutingAssembly().Location, $"{DefaultConstants.ThemeResourceLocation}{themes.ToString()}.xml");
                XDocument resourceDocumentTheme = XDocument.Parse(rawThemeResource);
                _theme = Deserialize(resourceDocumentTheme);
            }
            catch (Exception e)
            {
                Logger.WriteDebug(e);
            }

            return _theme;
        }

        /// <summary>Deserialize the theme from a file path.</summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The <see cref="Theme" />.</returns>
        public static Theme Deserialize(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new NoNullAllowedException(ArgumentMessages.IsNullOrEmpty());
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(ArgumentMessages.FileNotFound(filePath));
            }

            Theme _theme = null;

            try
            {
                XDocument themeDocument = XDocument.Load(filePath);
                _theme = Deserialize(themeDocument);
            }
            catch (Exception e)
            {
                Logger.WriteDebug(e);
            }

            return _theme;
        }

        /// <summary>Serialize the theme contents to a string.</summary>
        /// <param name="themeInformation">The information.</param>
        /// <param name="colorPalette">The color palette.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Serialize(ThemeInformation themeInformation, ColorPalette colorPalette)
        {
            const string Header = @"VisualPlus-Theme";
            const string Information = @"Information";
            const string StyleTable = @"StyleTable";
            const string Shared = @"Shared";
            const string Toolkit = @"Toolkit";

            string serializedXML;

            // TODO: Simplify to decompose body.
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(outputStream, XMLManager.WriterSettings()))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteWhitespace("\n\n");

                    // Write main theme header
                    xmlWriter.WriteStartElement(Header);

                    // Write theme information header.
                    var themeDataDictionary = new Dictionary<string, string> { { "Name", themeInformation.Name }, { "Author", themeInformation.Author } };

                    XMLManager.WriteElementGroup(xmlWriter, Information, themeDataDictionary);

                    // Write theme style table header.
                    xmlWriter.WriteStartElement(StyleTable);

                    // Write theme shared.
                    xmlWriter.WriteStartElement(Shared);

                    var borderDictionary = new Dictionary<string, Color> { { "Normal", colorPalette.BorderNormal }, { "Hover", colorPalette.BorderHover } };

                    XMLManager.WriteElementGroup(xmlWriter, "Border", borderDictionary);

                    var columnHeaderDictionary = new Dictionary<string, Color> { { "Header", colorPalette.ColumnHeader }, { "Text", colorPalette.ColumnText } };

                    XMLManager.WriteElementGroup(xmlWriter, "ColumnHeader", columnHeaderDictionary);

                    var controlDictionary = new Dictionary<string, Color> { { "Enabled", colorPalette.ControlEnabled }, { "Disabled", colorPalette.ControlDisabled } };

                    XMLManager.WriteElementGroup(xmlWriter, "Control", controlDictionary);

                    var flatControlDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.ElementDisabled }, { "Enabled", colorPalette.ElementEnabled } };

                    XMLManager.WriteElementGroup(xmlWriter, "Element", flatControlDictionary);

                    var textDictionary = new Dictionary<string, Color>
                        {
                            { "Disabled", colorPalette.TextDisabled },
                            { "Enabled", colorPalette.TextEnabled },
                            { "Hover", colorPalette.TextHover },
                            { "Pressed", colorPalette.TextPressed },
                            { "Selected", colorPalette.Selected },
                            { "Subscript", colorPalette.SubscriptColor },
                            { "Superscript", colorPalette.SuperscriptColor },
                            { "TextLight", colorPalette.TextLight }
                        };

                    XMLManager.WriteElementGroup(xmlWriter, "Text", textDictionary);

                    var listItemDictionary = new Dictionary<string, Color> { { "Normal", colorPalette.Item }, { "Hover", colorPalette.ItemHover }, { "Selected", colorPalette.ItemSelected }, { "Alternate", colorPalette.ItemAlternate } };

                    XMLManager.WriteElementGroup(xmlWriter, "ListItem", listItemDictionary);

                    var hatchDictionary = new Dictionary<string, Color> { { "BackColor", colorPalette.HatchBackColor }, { "ForeColor", colorPalette.HatchForeColor } };

                    XMLManager.WriteElementGroup(xmlWriter, "Hatch", hatchDictionary);

                    var progressDictionary = new Dictionary<string, Color> { { "Background", colorPalette.ProgressBackground }, { "Working", colorPalette.Progress }, { "Disabled", colorPalette.ProgressDisabled } };

                    XMLManager.WriteElementGroup(xmlWriter, "ProgressBar", progressDictionary);

                    var tabPageDictionary = new Dictionary<string, Color> { { "Enabled", colorPalette.TabPageEnabled }, { "Disabled", colorPalette.TabPageDisabled }, { "Selected", colorPalette.TabPageSelected }, { "Hover", colorPalette.TabPageHover } };

                    XMLManager.WriteElementGroup(xmlWriter, "TabPage", tabPageDictionary);

                    var watermarkDictionary = new Dictionary<string, Color> { { "Active", colorPalette.WatermarkActive }, { "Inactive", colorPalette.WatermarkInactive } };

                    XMLManager.WriteElementGroup(xmlWriter, "Watermark", watermarkDictionary);

                    // End shared.
                    xmlWriter.WriteEndElement();

                    // Write theme toolkit.
                    xmlWriter.WriteStartElement(Toolkit);

                    var visualButton = new Dictionary<string, Color> { { "Enabled", colorPalette.Enabled }, { "Disabled", colorPalette.Disabled }, { "Hover", colorPalette.Hover }, { "Pressed", colorPalette.Pressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "VisualButton", visualButton);

                    var visualComboBox = new Dictionary<string, Color> { { "Disabled", colorPalette.VisualComboBoxDisabled }, { "Enabled", colorPalette.VisualComboBoxEnabled } };

                    XMLManager.WriteElementGroup(xmlWriter, "VisualComboBox", visualComboBox);

                    xmlWriter.WriteStartElement("VisualControlBox");

                    xmlWriter.WriteStartElement("HelpButton");

                    var helpBackDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.HelpButtonBackDisabled }, { "Enabled", colorPalette.HelpButtonBackEnabled }, { "Hover", colorPalette.HelpButtonBackHover }, { "Pressed", colorPalette.HelpButtonBackPressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "BackColorState", helpBackDictionary);

                    var helpForeDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.HelpButtonForeDisabled }, { "Enabled", colorPalette.HelpButtonForeEnabled }, { "Hover", colorPalette.HelpButtonForeHover }, { "Pressed", colorPalette.HelpButtonForePressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "ForeColorState", helpForeDictionary);

                    // End Help Button
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("MinimizeButton");

                    var minBackDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.MinimizeButtonBackDisabled }, { "Enabled", colorPalette.MinimizeButtonBackEnabled }, { "Hover", colorPalette.MinimizeButtonBackHover }, { "Pressed", colorPalette.MinimizeButtonBackPressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "BackColorState", minBackDictionary);

                    var minForeDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.MinimizeButtonForeDisabled }, { "Enabled", colorPalette.MinimizeButtonForeEnabled }, { "Hover", colorPalette.MinimizeButtonForeHover }, { "Pressed", colorPalette.MinimizeButtonForePressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "ForeColorState", minForeDictionary);

                    // End Minimize Button
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("MaximizeButton");

                    var maxBackDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.MaximizeButtonBackDisabled }, { "Enabled", colorPalette.MaximizeButtonBackEnabled }, { "Hover", colorPalette.MaximizeButtonBackHover }, { "Pressed", colorPalette.MaximizeButtonBackPressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "BackColorState", maxBackDictionary);

                    var maxForeDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.MaximizeButtonForeDisabled }, { "Enabled", colorPalette.MaximizeButtonForeEnabled }, { "Hover", colorPalette.MaximizeButtonForeHover }, { "Pressed", colorPalette.MaximizeButtonForePressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "ForeColorState", maxForeDictionary);

                    // End Maximize Button
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("CloseButton");

                    var closeBackDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.CloseButtonBackDisabled }, { "Enabled", colorPalette.CloseButtonBackEnabled }, { "Hover", colorPalette.CloseButtonBackHover }, { "Pressed", colorPalette.CloseButtonBackPressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "BackColorState", closeBackDictionary);

                    var closeForeDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.CloseButtonForeDisabled }, { "Enabled", colorPalette.CloseButtonForeEnabled }, { "Hover", colorPalette.CloseButtonForeHover }, { "Pressed", colorPalette.CloseButtonForePressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "ForeColorState", closeForeDictionary);

                    // End Close Button
                    xmlWriter.WriteEndElement();

                    // End VisualControlBox
                    xmlWriter.WriteEndElement();

                    var formDictionary = new Dictionary<string, Color> { { "Background", colorPalette.FormBackground }, { "WindowBar", colorPalette.FormWindowBar } };

                    XMLManager.WriteElementGroup(xmlWriter, "VisualForm", formDictionary);

                    var radialDictionary = new Dictionary<string, Color> { { "BackCircle", colorPalette.BackCircle }, { "ForeCircle", colorPalette.ForeCircle } };

                    XMLManager.WriteElementGroup(xmlWriter, "VisualRadialProgress", radialDictionary);

                    xmlWriter.WriteStartElement("VisualScrollBar");

                    var barDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.ScrollBarDisabled }, { "Enabled", colorPalette.ScrollBarEnabled } };

                    XMLManager.WriteElementGroup(xmlWriter, "Bar", barDictionary);

                    var scrollButtonDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.ScrollButtonDisabled }, { "Enabled", colorPalette.ScrollButtonEnabled }, { "Hover", colorPalette.ScrollButtonHover }, { "Pressed", colorPalette.ScrollButtonPressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "Button", scrollButtonDictionary);

                    var scrollThumbDictionary = new Dictionary<string, Color> { { "Disabled", colorPalette.ScrollThumbDisabled }, { "Enabled", colorPalette.ScrollThumbEnabled }, { "Hover", colorPalette.ScrollThumbHover }, { "Pressed", colorPalette.ScrollThumbPressed } };

                    XMLManager.WriteElementGroup(xmlWriter, "Thumb", scrollThumbDictionary);

                    // End visual scrollbar.
                    xmlWriter.WriteEndElement();

                    var visualRating = new Dictionary<string, Color> { { "Star", colorPalette.Star }, { "Border", colorPalette.StarBorder }, { "Dull", colorPalette.StarDull }, { "DullBorder", colorPalette.StarDullBorder } };

                    XMLManager.WriteElementGroup(xmlWriter, "VisualRating", visualRating);

                    var visualSeparator = new Dictionary<string, Color> { { "Line", colorPalette.VisualSeparatorLine }, { "Shadow", colorPalette.VisualSeparatorShadow } };

                    XMLManager.WriteElementGroup(xmlWriter, "VisualSeparator", visualSeparator);

                    // End toolkit.
                    xmlWriter.WriteEndElement();

                    // End Style table.
                    xmlWriter.WriteEndElement();

                    // End main header.
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndDocument();
                }

                serializedXML = Encoding.Default.GetString(outputStream.ToArray());
            }

            return serializedXML;
        }

        /// <summary>Serialize the theme to a raw string.</summary>
        /// <param name="theme">The theme.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Serialize(Theme theme)
        {
            return Serialize(theme.Information, theme.ColorPalette);
        }

        #endregion
    }
}