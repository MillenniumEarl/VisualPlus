#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ResourcesManager.cs
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
using System.IO;
using System.Linq;
using System.Reflection;

using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Managers
{
    public class ResourcesManager
    {
        #region Public Methods and Operators

        /// <summary>Retrieve the resource names from the file.</summary>
        /// <param name="file">The file path.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static List<string> GetResourceNames(string file)
        {
            Assembly _assembly = AssemblyManager.LoadAssembly(file);
            return _assembly.GetManifestResourceNames().ToList();
        }

        /// <summary>Read the resource from the file.</summary>
        /// <param name="file">The file path.</param>
        /// <param name="resource">The resource name.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ReadResource(string file, string resource)
        {
            Assembly _assembly = AssemblyManager.LoadAssembly(file);

            try
            {
                string result;
                using (Stream stream = _assembly.GetManifestResourceStream(resource))
                using
                    (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }

                return result;
            }
            catch (ArgumentNullException e)
            {
                // Value cannot be null.Parameter name: stream'
                // The embedded resource cannot be found. Set type to 'Embedded Resource'.
                ConsoleEx.WriteDebug(e);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }

            return null;
        }

        #endregion
    }
}