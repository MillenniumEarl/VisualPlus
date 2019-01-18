#region Namespace

using System.Reflection;

#endregion

namespace VisualPlus
{
    /// <summary>A collection of the <see cref="VisualPlus"/> API information.</summary>
    public class Library
    {
        #region Static Fields

        public static string Name = "VisualPlus";
        public static string ProjectUrl = "https://github.com/DarkByte7/VisualPlus";

        #endregion

        #region Public Properties

        /// <summary>Returns the <c>AssemblyVersion</c> of this Library.</summary>
        public static string Version
        {
            get
            {
                try
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    AssemblyName assemblyName = assembly.GetName();

                    return assemblyName.Version.ToString();
                }
                catch
                {
                    return "0.0.0.0";
                }
            }
        }

        #endregion
    }
}