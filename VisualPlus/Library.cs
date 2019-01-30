#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Library.cs
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

using VisualPlus.Utilities;

#endregion

namespace VisualPlus
{
    /// <summary>A collection of retrievable <see cref="VisualPlus " /> framework information.</summary>
    public static partial class Library
    {
        #region Constants

        /// <summary>Returns the <c>Assembly File</c> extension of the <see cref="VisualPlus " /> framework.</summary>
        public const string DefaultAssemblyExtension = ".dll";

        /// <summary>Returns the <c>Assembly Name</c> of the <see cref="VisualPlus " /> framework.</summary>
        public const string DefaultAssemblyName = "VisualPlus";

        /// <summary>Returns the <c>Assembly Long Description</c> of the <see cref="VisualPlus " /> framework.</summary>
        public const string DescriptionLong = "The VisualPlus Framework (VPF) for WinForms allows you to rapidly deploy professional .NET applications with customizable components and controls.";

        #endregion

        #region Public Properties

        /// <summary>Returns the <c>Description</c> of the <see cref="VisualPlus " /> framework.</summary>
        public static string Description
        {
            get
            {
                try
                {
                    // Retrieve default assembly description attributes  
                    AssemblyDescriptionAttribute descriptionAttribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).OfType<AssemblyDescriptionAttribute>().FirstOrDefault();

                    // Check if the description attribute is null then display default message
                    return descriptionAttribute != null ? descriptionAttribute.Description : DescriptionLong;
                }
                catch
                {
                    return DescriptionLong;
                }
            }
        }

        /// <summary>Returns the full <see cref="Directory " /> path of the <see cref="VisualPlus " /> framework.</summary>
        public static string Directory
        {
            get
            {
                return DirectoryInfo.FullName;
            }
        }

        /// <summary>Returns the <see cref="DirectoryInfo " /> for the <see cref="VisualPlus " /> framework.</summary>
        public static DirectoryInfo DirectoryInfo
        {
            get
            {
                return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        /// <summary>Returns the <c>File Name</c> of the <see cref="VisualPlus " /> framework.</summary>
        public static string FileName
        {
            get
            {
                return Name + DefaultAssemblyExtension;
            }
        }

        /// <summary>Returns the full <see cref="File " /> path of the <see cref="VisualPlus " /> framework.</summary>
        public static string Location
        {
            get
            {
                return Path.Combine(Directory, FileName);
            }
        }

        /// <summary>Returns the <c>Name</c> of the <see cref="VisualPlus " /> framework.</summary>
        public static string Name
        {
            get
            {
                try
                {
                    return Assembly.GetExecutingAssembly().GetName().Name;
                }
                catch
                {
                    return DefaultAssemblyName;
                }
            }
        }

        /// <summary>Returns the <see cref="Uri" /> for the project releases of the <see cref="VisualPlus " /> framework.</summary>
        public static Uri ProjectReleases
        {
            get
            {
                return new Uri("https://github.com/DarkByte7/VisualPlus/releases");
            }
        }

        /// <summary>Returns the <c>Website</c> of the <see cref="VisualPlus " /> framework.</summary>
        public static string Website
        {
            get
            {
                return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalTrademarks;
            }
        }

        /// <summary>Returns the <c>Version</c> of the <see cref="VisualPlus " /> framework.</summary>
        public static Version Version
        {
            get
            {
                try
                {
                    return Assembly.GetExecutingAssembly().GetName().Version;
                }
                catch
                {
                    return new Version(0, 0, 0, 0);
                }
            }
        }

        /// <summary>Returns the <c>Assembly</c> of the <see cref="VisualPlus " /> framework.</summary>
        public static Assembly VisualPlus
        {
            get
            {
                // Get directory
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Retrieve the path of the framework
                string filePath = Path.Combine(baseDirectory, FileName);

                // Returns the assembly
                return AssemblyManager.LoadAssembly(filePath);
            }
        }

        #endregion
    }
}