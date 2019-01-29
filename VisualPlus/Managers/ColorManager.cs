#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ColorManager.cs
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
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

using VisualPlus.Enumerators;
using VisualPlus.Native;

#endregion

namespace VisualPlus.Managers
{
    [Description("The color manager.")]
    public sealed class ColorManager
    {
        #region Public Methods and Operators

        /// <summary>Blends the colors.</summary>
        /// <param name="backColor">The back color.</param>
        /// <param name="foreColor">The fore color.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color BlendColor(Color backColor, Color foreColor, double alpha)
        {
            double _ratio = alpha / 255d;
            double _inverseRatio = 1d - _ratio;
            var _r = (int)((backColor.R * _inverseRatio) + (foreColor.R * _ratio));
            var _g = (int)((backColor.G * _inverseRatio) + (foreColor.G * _ratio));
            var _b = (int)((backColor.B * _inverseRatio) + (foreColor.B * _ratio));
            return Color.FromArgb(_r, _g, _b);
        }

        /// <summary>Blends the colors.</summary>
        /// <param name="backColor">The back color.</param>
        /// <param name="foreColor">The fore color.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color BlendColor(Color backColor, Color foreColor)
        {
            return BlendColor(backColor, foreColor, foreColor.A);
        }

        /// <summary>Get the color from the Hex value.</summary>
        /// <param name="alpha">The alpha value.</param>
        /// <param name="value">The Hex value.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color ColorFromHex(int alpha, string value)
        {
            return Color.FromArgb(alpha, ColorTranslator.FromHtml(value));
        }

        /// <summary>Get the color from the Hex value.</summary>
        /// <param name="value">The Hex value.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color ColorFromHex(string value)
        {
            return ColorTranslator.FromHtml(value);
        }

        /// <summary>Get the color from position.</summary>
        /// <param name="position">The position.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color ColorFromPosition(Point position)
        {
            Bitmap _pixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);

            using (Graphics _graphicsDestination = Graphics.FromImage(_pixel))
            {
                using (Graphics _graphicsSource = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr handleContextSource = _graphicsSource.GetHdc();
                    IntPtr handleContextDestination = _graphicsDestination.GetHdc();
                    int _retrievedResult = gdi32.BitBlt(handleContextDestination, 0, 0, 1, 1, handleContextSource, position.X, position.Y, (int)CopyPixelOperation.SourceCopy);
                    _graphicsDestination.ReleaseHdc();
                    _graphicsSource.ReleaseHdc();
                }
            }

            return _pixel.GetPixel(0, 0);
        }

        /// <summary>Create a color from RGB values.</summary>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color ColorFromRGB(int red, int green, int blue)
        {
            // Correct Red element
            int r = red;
            if (r > 255)
            {
                r = 255;
            }

            if (r < 0)
            {
                r = 0;
            }

            // Correct Green element
            int g = green;
            if (g > 255)
            {
                g = 255;
            }

            if (g < 0)
            {
                g = 0;
            }

            // Correct Blue Element
            int b = blue;
            if (b > 255)
            {
                b = 255;
            }

            if (b < 0)
            {
                b = 0;
            }

            return Color.FromArgb(r, g, b);
        }

        /// <summary>Get the color underneath the cursor.</summary>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color CursorPointerColor()
        {
            Point cursor = new Point();
            user32.GetCursorPos(ref cursor);
            return ColorFromPosition(cursor);
        }

        /// <summary>Insert the color on to another color.</summary>
        /// <param name="baseColor">The base color.</param>
        /// <param name="insertColor">The insertColor.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color InsertColor(Color baseColor, Color insertColor)
        {
            return Color.FromArgb((baseColor.R + insertColor.R) / 2, (baseColor.G + insertColor.G) / 2, (baseColor.B + insertColor.B) / 2);
        }

        /// <summary>Creates an opacity mix color.</summary>
        /// <param name="baseColor">The base color.</param>
        /// <param name="blendColor">The blend color.</param>
        /// <param name="opacity">The opacity value.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color OpacityMix(Color baseColor, Color blendColor, int opacity)
        {
            int _r0 = blendColor.R;
            int _g0 = blendColor.G;
            int _b0 = blendColor.B;
            int _r1 = baseColor.R;
            int _g1 = baseColor.G;
            int _b1 = baseColor.B;
            var _r2 = (int)((_r0 * ((float)opacity / 100)) + (_r1 * (1 - ((float)opacity / 100))));
            var _g2 = (int)((_g0 * ((float)opacity / 100)) + (_g1 * (1 - ((float)opacity / 100))));
            var _b2 = (int)((_b0 * ((float)opacity / 100)) + (_b1 * (1 - ((float)opacity / 100))));

            return ColorFromRGB(_r2, _g2, _b2);
        }

        /// <summary>Creates an overlay mix color.</summary>
        /// <param name="baseColor">The base color.</param>
        /// <param name="blendColor">The blend color.</param>
        /// <param name="opacity">The opacity value.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color OverlayMix(Color baseColor, Color blendColor, int opacity)
        {
            int _r0 = baseColor.R;
            int _g0 = baseColor.G;
            int _b0 = baseColor.B;
            int _r1 = blendColor.R;
            int _g1 = blendColor.G;
            int _b1 = blendColor.B;
            int _r2 = OverlayMath(baseColor.R, blendColor.R);
            int _g2 = OverlayMath(baseColor.G, blendColor.G);
            int _b3 = OverlayMath(baseColor.B, blendColor.B);
            return OpacityMix(baseColor, ColorFromRGB(_r2, _g2, _b3), opacity);
        }

        /// <summary>Generates a random color.</summary>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color RandomColor()
        {
            Random _random = new Random();
            return Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
        }

        /// <summary>Creates a soft light mix color.</summary>
        /// <param name="baseColor">The base color.</param>
        /// <param name="blendColor">The blend color.</param>
        /// <param name="opacity">The opacity value.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color SoftLightMix(Color baseColor, Color blendColor, int opacity)
        {
            int _r0 = baseColor.R;
            int _g0 = baseColor.G;
            int _b0 = baseColor.B;
            int _r1 = blendColor.R;
            int _g1 = blendColor.G;
            int _b1 = blendColor.B;
            int _r2 = SoftLightMath(_r0, _r1);
            int _g2 = SoftLightMath(_g0, _g1);
            int _b2 = SoftLightMath(_b0, _b1);
            return OpacityMix(ColorFromRGB(_r2, _g2, _b2), baseColor, opacity);
        }

        /// <summary>Create a color step.</summary>
        /// <param name="color">The color.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color StepColor(Color color, int alpha)
        {
            if (alpha == 100)
            {
                return color;
            }

            byte _a = color.A;
            byte _r = color.R;
            byte _g = color.G;
            byte _b = color.B;
            float background;

            int _newAlpha = Math.Min(alpha, 200);
            _newAlpha = Math.Max(_newAlpha, 0);
            double doubleAlpha = (_newAlpha - 100.0) / 100.0;

            if (doubleAlpha > 100)
            {
                // Blend with white
                background = 255.0F;

                // 0 = transparent fg; 1 = opaque fg
                doubleAlpha = 1.0F - doubleAlpha;
            }
            else
            {
                // Blend with black
                background = 0.0F;

                // 0 = transparent fg; 1 = opaque fg
                doubleAlpha = 1.0F + doubleAlpha;
            }

            _r = (byte)BlendColor(background, _r, doubleAlpha);
            _g = (byte)BlendColor(background, _g, doubleAlpha);
            _b = (byte)BlendColor(background, _b, doubleAlpha);

            return Color.FromArgb(_a, _r, _g, _b);
        }

        /// <summary>Creates a tinted color.</summary>
        /// <param name="brightness">The brightness.</param>
        /// <param name="color">The color.</param>
        /// <param name="data">The byte.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color TintColor(Brightness brightness, Color color, byte data)
        {
            Color _tintedColor;
            byte _r;
            byte _g;
            byte _b;

            switch (brightness)
            {
                case Brightness.Darker:
                    {
                        _r = 0;
                        _g = 0;
                        _b = 0;

                        if (color.R > data)
                        {
                            _r = (byte)(color.R - data);
                        }

                        if (color.G > data)
                        {
                            _g = (byte)(color.G - data);
                        }

                        if (color.B > data)
                        {
                            _b = (byte)(color.B - data);
                        }

                        _tintedColor = Color.FromArgb(_r, _g, _b);
                        break;
                    }

                case Brightness.Lighter:
                    {
                        _r = 255;
                        _g = 255;
                        _b = 255;

                        if (color.R + data < 255)
                        {
                            _r = (byte)(color.R + data);
                        }

                        if (color.G + data < 255)
                        {
                            _g = (byte)(color.G + data);
                        }

                        if (color.B + data < 255)
                        {
                            _b = (byte)(color.B + data);
                        }

                        _tintedColor = Color.FromArgb(_r, _g, _b);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(brightness), brightness, null);
                    }
            }

            return _tintedColor;
        }

        /// <summary>Retrieves the transition color between two other colors.</summary>
        /// <param name="value">The progress value in the transition.</param>
        /// <param name="beginColor">The beginning color.</param>
        /// <param name="endColor">The ending color.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color TransitionColor(int value, Color beginColor, Color endColor)
        {
            try
            {
                try
                {
                    int _red = int.Parse(Math.Round(beginColor.R + ((endColor.R - beginColor.R) * value * 0.01), 0).ToString(CultureInfo.CurrentCulture));
                    int _green = int.Parse(Math.Round(beginColor.G + ((endColor.G - beginColor.G) * value * 0.01), 0).ToString(CultureInfo.CurrentCulture));
                    int _blue = int.Parse(Math.Round(beginColor.B + ((endColor.B - beginColor.B) * value * 0.01), 0).ToString(CultureInfo.CurrentCulture));
                    return Color.FromArgb(255, _red, _green, _blue);
                }
                catch (Exception)
                {
                    return beginColor;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion

        #region Methods

        /// <summary>Blends the colors.</summary>
        /// <param name="backColor">The back color.</param>
        /// <param name="foreColor">The fore color.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>The <see cref="double" />.</returns>
        private static double BlendColor(double backColor, double foreColor, double alpha)
        {
            double result = backColor + (alpha * (foreColor - backColor));
            if (result < 0.0)
            {
                result = 0.0;
            }

            if (result > 255)
            {
                result = 255;
            }

            return result;
        }

        /// <summary>Calculate the overlay.</summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>The <see cref="int" />.</returns>
        private static int OverlayMath(int baseValue, int alpha)
        {
            double _baseOverlay = (double)baseValue / 255;
            double _alphaOverlay = (double)alpha / 255;
            if (_baseOverlay < 0.5)
            {
                return (int)(2 * _baseOverlay * _alphaOverlay * 255);
            }
            else
            {
                return (int)((1 - (2 * (1 - _baseOverlay) * (1 - _alphaOverlay))) * 255);
            }
        }

        /// <summary>Calculate the soft light.</summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>The <see cref="int" />.</returns>
        private static int SoftLightMath(int baseValue, int alpha)
        {
            float _softLightBase = (float)baseValue / 255;
            float _softLightAlpha = (float)alpha / 255;
            if (_softLightAlpha < 0.5)
            {
                return (int)(((2 * _softLightBase * _softLightAlpha) + (Math.Pow(_softLightBase, 2) * (1 - (2 * _softLightAlpha)))) * 255);
            }
            else
            {
                return (int)(((Math.Sqrt(_softLightBase) * ((2 * _softLightAlpha) - 1)) + (2 * _softLightBase * (1 - _softLightAlpha))) * 255);
            }
        }

        #endregion
    }
}