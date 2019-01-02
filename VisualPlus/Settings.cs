#region Namespace

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using VisualPlus.Enumerators;

#endregion

namespace VisualPlus
{
    public class Settings
    {
        #region Static Fields

        public static readonly string DebugLogFile = "VisualPlus-Debug.log";

        #endregion

        public struct DefaultValue
        {
            #region Constants

            public const bool Animation = true;
            public const int BorderThickness = 1;
            public const ShapeTypes BorderType = ShapeTypes.Rounded;
            public const bool BorderVisible = true;
            public const int ColumnWidth = 60;
            public const Themes DefaultStyle = Themes.Visual;
            public const HatchStyle HatchStyle = System.Drawing.Drawing2D.HatchStyle.DarkDownwardDiagonal;
            public const bool HatchVisible = true;
            public const float ProgressSize = 5F;
            public const bool TextVisible = true;
            public const bool WatermarkVisible = false;

            #endregion

            #region Static Fields

            public static readonly Size HatchSize = new Size(2, 2);
            public static readonly string WatermarkText = "Watermark text";
            public static TextRenderingHint TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            #endregion

            public struct Rounding
            {
                #region Constants

                public const int BoxRounding = 3;
                public const int Default = 6;
                public const int RoundedRectangle = 12;
                public const int ToggleBorder = 20;
                public const int ToggleButton = 18;

                #endregion
            }
        }
    }
}