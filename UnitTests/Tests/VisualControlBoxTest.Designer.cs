using VisualPlus.Toolkit.VisualBase;

namespace UnitTests.Tests
{
    partial class VisualControlBoxTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualControlBoxTest));
            this.TControlBox = new VisualPlus.Toolkit.Controls.Interactivity.VisualToggle();
            this.LControlBoxToggle = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.lHelp = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.tHelp = new VisualPlus.Toolkit.Controls.Interactivity.VisualToggle();
            this.tMaximize = new VisualPlus.Toolkit.Controls.Interactivity.VisualToggle();
            this.lMaximize = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.tClose = new VisualPlus.Toolkit.Controls.Interactivity.VisualToggle();
            this.lClose = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.tMinimize = new VisualPlus.Toolkit.Controls.Interactivity.VisualToggle();
            this.lMinimize = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.SuspendLayout();
            // 
            // TControlBox
            // 
            this.TControlBox.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TControlBox.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.TControlBox.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.TControlBox.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.TControlBox.Border.HoverVisible = true;
            this.TControlBox.Border.Rounding = 20;
            this.TControlBox.Border.Thickness = 1;
            this.TControlBox.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.TControlBox.Border.Visible = true;
            this.TControlBox.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.TControlBox.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.TControlBox.ButtonBorder.HoverVisible = true;
            this.TControlBox.ButtonBorder.Rounding = 18;
            this.TControlBox.ButtonBorder.Thickness = 1;
            this.TControlBox.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.TControlBox.ButtonBorder.Visible = true;
            this.TControlBox.ButtonColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TControlBox.ButtonColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TControlBox.ButtonColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TControlBox.ButtonColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.TControlBox.ButtonSize = new System.Drawing.Size(20, 20);
            this.TControlBox.FalseTextToggle = "No";
            this.TControlBox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TControlBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TControlBox.Location = new System.Drawing.Point(274, 45);
            this.TControlBox.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.TControlBox.Name = "TControlBox";
            this.TControlBox.ProgressImage = null;
            this.TControlBox.Size = new System.Drawing.Size(50, 25);
            this.TControlBox.TabIndex = 1;
            this.TControlBox.Text = "visualToggle1";
            this.TControlBox.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.TControlBox.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TControlBox.TextStyle.Hover = System.Drawing.Color.Empty;
            this.TControlBox.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.TControlBox.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.TControlBox.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.TControlBox.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.TControlBox.Toggled = true;
            this.TControlBox.TrueTextToggle = "Yes";
            this.TControlBox.Type = VisualPlus.Toolkit.Controls.Interactivity.VisualToggle.ToggleTypes.YesNo;
            this.TControlBox.ToggleChanged += new VisualPlus.Delegates.ToggleChangedEventHandler(this.TControlBox_ToggleChanged);
            // 
            // LControlBoxToggle
            // 
            this.LControlBoxToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.LControlBoxToggle.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.LControlBoxToggle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LControlBoxToggle.Location = new System.Drawing.Point(15, 45);
            this.LControlBoxToggle.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.LControlBoxToggle.Name = "LControlBoxToggle";
            this.LControlBoxToggle.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LControlBoxToggle.Outline = false;
            this.LControlBoxToggle.OutlineColor = System.Drawing.Color.Red;
            this.LControlBoxToggle.OutlineLocation = new System.Drawing.Point(0, 0);
            this.LControlBoxToggle.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LControlBoxToggle.ReflectionSpacing = 0;
            this.LControlBoxToggle.ShadowColor = System.Drawing.Color.Black;
            this.LControlBoxToggle.ShadowDirection = 315;
            this.LControlBoxToggle.ShadowLocation = new System.Drawing.Point(0, 0);
            this.LControlBoxToggle.ShadowOpacity = 100;
            this.LControlBoxToggle.Size = new System.Drawing.Size(112, 23);
            this.LControlBoxToggle.TabIndex = 2;
            this.LControlBoxToggle.Text = "Control Box Toggle:";
            this.LControlBoxToggle.TextAlignment = System.Drawing.StringAlignment.Near;
            this.LControlBoxToggle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.LControlBoxToggle.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.LControlBoxToggle.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LControlBoxToggle.TextStyle.Hover = System.Drawing.Color.Empty;
            this.LControlBoxToggle.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.LControlBoxToggle.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.LControlBoxToggle.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.LControlBoxToggle.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // lHelp
            // 
            this.lHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lHelp.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lHelp.Location = new System.Drawing.Point(15, 81);
            this.lHelp.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lHelp.Name = "lHelp";
            this.lHelp.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.lHelp.Outline = false;
            this.lHelp.OutlineColor = System.Drawing.Color.Red;
            this.lHelp.OutlineLocation = new System.Drawing.Point(0, 0);
            this.lHelp.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lHelp.ReflectionSpacing = 0;
            this.lHelp.ShadowColor = System.Drawing.Color.Black;
            this.lHelp.ShadowDirection = 315;
            this.lHelp.ShadowLocation = new System.Drawing.Point(0, 0);
            this.lHelp.ShadowOpacity = 100;
            this.lHelp.Size = new System.Drawing.Size(75, 23);
            this.lHelp.TabIndex = 3;
            this.lHelp.Text = "Help Box:";
            this.lHelp.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lHelp.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lHelp.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lHelp.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lHelp.TextStyle.Hover = System.Drawing.Color.Empty;
            this.lHelp.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.lHelp.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lHelp.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lHelp.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // tHelp
            // 
            this.tHelp.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tHelp.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.tHelp.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tHelp.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tHelp.Border.HoverVisible = true;
            this.tHelp.Border.Rounding = 20;
            this.tHelp.Border.Thickness = 1;
            this.tHelp.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tHelp.Border.Visible = true;
            this.tHelp.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tHelp.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tHelp.ButtonBorder.HoverVisible = true;
            this.tHelp.ButtonBorder.Rounding = 18;
            this.tHelp.ButtonBorder.Thickness = 1;
            this.tHelp.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tHelp.ButtonBorder.Visible = true;
            this.tHelp.ButtonColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tHelp.ButtonColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tHelp.ButtonColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tHelp.ButtonColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tHelp.ButtonSize = new System.Drawing.Size(20, 20);
            this.tHelp.FalseTextToggle = "No";
            this.tHelp.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tHelp.Location = new System.Drawing.Point(274, 81);
            this.tHelp.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.tHelp.Name = "tHelp";
            this.tHelp.ProgressImage = null;
            this.tHelp.Size = new System.Drawing.Size(50, 25);
            this.tHelp.TabIndex = 4;
            this.tHelp.Text = "visualToggle1";
            this.tHelp.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.tHelp.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tHelp.TextStyle.Hover = System.Drawing.Color.Empty;
            this.tHelp.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.tHelp.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.tHelp.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.tHelp.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.tHelp.Toggled = true;
            this.tHelp.TrueTextToggle = "Yes";
            this.tHelp.Type = VisualPlus.Toolkit.Controls.Interactivity.VisualToggle.ToggleTypes.YesNo;
            this.tHelp.ToggleChanged += new VisualPlus.Delegates.ToggleChangedEventHandler(this.THelp_ToggleChanged);
            // 
            // tMaximize
            // 
            this.tMaximize.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tMaximize.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.tMaximize.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tMaximize.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tMaximize.Border.HoverVisible = true;
            this.tMaximize.Border.Rounding = 20;
            this.tMaximize.Border.Thickness = 1;
            this.tMaximize.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tMaximize.Border.Visible = true;
            this.tMaximize.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tMaximize.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tMaximize.ButtonBorder.HoverVisible = true;
            this.tMaximize.ButtonBorder.Rounding = 18;
            this.tMaximize.ButtonBorder.Thickness = 1;
            this.tMaximize.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tMaximize.ButtonBorder.Visible = true;
            this.tMaximize.ButtonColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tMaximize.ButtonColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tMaximize.ButtonColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tMaximize.ButtonColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tMaximize.ButtonSize = new System.Drawing.Size(20, 20);
            this.tMaximize.FalseTextToggle = "No";
            this.tMaximize.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tMaximize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tMaximize.Location = new System.Drawing.Point(274, 143);
            this.tMaximize.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.tMaximize.Name = "tMaximize";
            this.tMaximize.ProgressImage = null;
            this.tMaximize.Size = new System.Drawing.Size(50, 25);
            this.tMaximize.TabIndex = 6;
            this.tMaximize.Text = "visualToggle1";
            this.tMaximize.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.tMaximize.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tMaximize.TextStyle.Hover = System.Drawing.Color.Empty;
            this.tMaximize.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.tMaximize.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.tMaximize.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.tMaximize.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.tMaximize.Toggled = true;
            this.tMaximize.TrueTextToggle = "Yes";
            this.tMaximize.Type = VisualPlus.Toolkit.Controls.Interactivity.VisualToggle.ToggleTypes.YesNo;
            this.tMaximize.ToggleChanged += new VisualPlus.Delegates.ToggleChangedEventHandler(this.TMaximize_ToggleChanged_1);
            // 
            // lMaximize
            // 
            this.lMaximize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lMaximize.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lMaximize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMaximize.Location = new System.Drawing.Point(15, 143);
            this.lMaximize.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lMaximize.Name = "lMaximize";
            this.lMaximize.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.lMaximize.Outline = false;
            this.lMaximize.OutlineColor = System.Drawing.Color.Red;
            this.lMaximize.OutlineLocation = new System.Drawing.Point(0, 0);
            this.lMaximize.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMaximize.ReflectionSpacing = 0;
            this.lMaximize.ShadowColor = System.Drawing.Color.Black;
            this.lMaximize.ShadowDirection = 315;
            this.lMaximize.ShadowLocation = new System.Drawing.Point(0, 0);
            this.lMaximize.ShadowOpacity = 100;
            this.lMaximize.Size = new System.Drawing.Size(75, 23);
            this.lMaximize.TabIndex = 5;
            this.lMaximize.Text = "Maximize Box:";
            this.lMaximize.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lMaximize.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lMaximize.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lMaximize.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMaximize.TextStyle.Hover = System.Drawing.Color.Empty;
            this.lMaximize.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.lMaximize.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lMaximize.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lMaximize.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // tClose
            // 
            this.tClose.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tClose.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.tClose.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tClose.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tClose.Border.HoverVisible = true;
            this.tClose.Border.Rounding = 20;
            this.tClose.Border.Thickness = 1;
            this.tClose.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tClose.Border.Visible = true;
            this.tClose.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tClose.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tClose.ButtonBorder.HoverVisible = true;
            this.tClose.ButtonBorder.Rounding = 18;
            this.tClose.ButtonBorder.Thickness = 1;
            this.tClose.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tClose.ButtonBorder.Visible = true;
            this.tClose.ButtonColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tClose.ButtonColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tClose.ButtonColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tClose.ButtonColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tClose.ButtonSize = new System.Drawing.Size(20, 20);
            this.tClose.FalseTextToggle = "No";
            this.tClose.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tClose.Location = new System.Drawing.Point(274, 174);
            this.tClose.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.tClose.Name = "tClose";
            this.tClose.ProgressImage = null;
            this.tClose.Size = new System.Drawing.Size(50, 25);
            this.tClose.TabIndex = 8;
            this.tClose.Text = "visualToggle1";
            this.tClose.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.tClose.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tClose.TextStyle.Hover = System.Drawing.Color.Empty;
            this.tClose.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.tClose.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.tClose.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.tClose.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.tClose.Toggled = true;
            this.tClose.TrueTextToggle = "Yes";
            this.tClose.Type = VisualPlus.Toolkit.Controls.Interactivity.VisualToggle.ToggleTypes.YesNo;
            this.tClose.ToggleChanged += new VisualPlus.Delegates.ToggleChangedEventHandler(this.TClose_ToggleChanged);
            // 
            // lClose
            // 
            this.lClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lClose.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lClose.Location = new System.Drawing.Point(15, 174);
            this.lClose.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lClose.Name = "lClose";
            this.lClose.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.lClose.Outline = false;
            this.lClose.OutlineColor = System.Drawing.Color.Red;
            this.lClose.OutlineLocation = new System.Drawing.Point(0, 0);
            this.lClose.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lClose.ReflectionSpacing = 0;
            this.lClose.ShadowColor = System.Drawing.Color.Black;
            this.lClose.ShadowDirection = 315;
            this.lClose.ShadowLocation = new System.Drawing.Point(0, 0);
            this.lClose.ShadowOpacity = 100;
            this.lClose.Size = new System.Drawing.Size(75, 23);
            this.lClose.TabIndex = 7;
            this.lClose.Text = "Close Box:";
            this.lClose.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lClose.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lClose.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lClose.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lClose.TextStyle.Hover = System.Drawing.Color.Empty;
            this.lClose.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.lClose.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lClose.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lClose.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // tMinimize
            // 
            this.tMinimize.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tMinimize.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.tMinimize.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tMinimize.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tMinimize.Border.HoverVisible = true;
            this.tMinimize.Border.Rounding = 20;
            this.tMinimize.Border.Thickness = 1;
            this.tMinimize.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tMinimize.Border.Visible = true;
            this.tMinimize.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tMinimize.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tMinimize.ButtonBorder.HoverVisible = true;
            this.tMinimize.ButtonBorder.Rounding = 18;
            this.tMinimize.ButtonBorder.Thickness = 1;
            this.tMinimize.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tMinimize.ButtonBorder.Visible = true;
            this.tMinimize.ButtonColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tMinimize.ButtonColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tMinimize.ButtonColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tMinimize.ButtonColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tMinimize.ButtonSize = new System.Drawing.Size(20, 20);
            this.tMinimize.FalseTextToggle = "No";
            this.tMinimize.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tMinimize.Location = new System.Drawing.Point(274, 112);
            this.tMinimize.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.tMinimize.Name = "tMinimize";
            this.tMinimize.ProgressImage = null;
            this.tMinimize.Size = new System.Drawing.Size(50, 25);
            this.tMinimize.TabIndex = 10;
            this.tMinimize.Text = "visualToggle1";
            this.tMinimize.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.tMinimize.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tMinimize.TextStyle.Hover = System.Drawing.Color.Empty;
            this.tMinimize.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.tMinimize.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.tMinimize.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.tMinimize.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.tMinimize.Toggled = true;
            this.tMinimize.TrueTextToggle = "Yes";
            this.tMinimize.Type = VisualPlus.Toolkit.Controls.Interactivity.VisualToggle.ToggleTypes.YesNo;
            this.tMinimize.ToggleChanged += new VisualPlus.Delegates.ToggleChangedEventHandler(this.TMinimize_ToggleChanged);
            // 
            // lMinimize
            // 
            this.lMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lMinimize.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMinimize.Location = new System.Drawing.Point(15, 112);
            this.lMinimize.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lMinimize.Name = "lMinimize";
            this.lMinimize.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.lMinimize.Outline = false;
            this.lMinimize.OutlineColor = System.Drawing.Color.Red;
            this.lMinimize.OutlineLocation = new System.Drawing.Point(0, 0);
            this.lMinimize.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMinimize.ReflectionSpacing = 0;
            this.lMinimize.ShadowColor = System.Drawing.Color.Black;
            this.lMinimize.ShadowDirection = 315;
            this.lMinimize.ShadowLocation = new System.Drawing.Point(0, 0);
            this.lMinimize.ShadowOpacity = 100;
            this.lMinimize.Size = new System.Drawing.Size(75, 23);
            this.lMinimize.TabIndex = 9;
            this.lMinimize.Text = "Minimize Box:";
            this.lMinimize.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lMinimize.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lMinimize.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lMinimize.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMinimize.TextStyle.Hover = System.Drawing.Color.Empty;
            this.lMinimize.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.lMinimize.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lMinimize.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lMinimize.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // VisualControlBoxTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.Border.HoverVisible = true;
            this.Border.Rounding = 6;
            this.Border.Thickness = 3;
            this.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rectangle;
            this.Border.Visible = true;
            this.ClientSize = new System.Drawing.Size(339, 214);
            // 
            // 
            // 
            this.ControlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlBox.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.ControlBox.HelpButton.BackColorState.Disabled = System.Drawing.Color.Transparent;
            this.ControlBox.HelpButton.BackColorState.Enabled = System.Drawing.Color.Transparent;
            this.ControlBox.HelpButton.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(186)))), ((int)(((byte)(186)))));
            this.ControlBox.HelpButton.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.ControlBox.HelpButton.BoxType = VisualPlus.Toolkit.VisualBase.ControlBoxButton.ControlBoxType.Default;
            this.ControlBox.HelpButton.ForeColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.ControlBox.HelpButton.ForeColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ControlBox.HelpButton.ForeColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ControlBox.HelpButton.ForeColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ControlBox.HelpButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.ControlBox.HelpButton.Location = new System.Drawing.Point(0, 0);
            this.ControlBox.HelpButton.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.ControlBox.HelpButton.Name = "";
            this.ControlBox.HelpButton.OffsetLocation = new System.Drawing.Point(0, 1);
            this.ControlBox.HelpButton.Size = new System.Drawing.Size(24, 25);
            this.ControlBox.HelpButton.TabIndex = 0;
            this.ControlBox.HelpButton.Text = "s";
            this.ControlBox.HelpButton.TextStyle.Disabled = System.Drawing.Color.Empty;
            this.ControlBox.HelpButton.TextStyle.Enabled = System.Drawing.Color.Empty;
            this.ControlBox.HelpButton.TextStyle.Hover = System.Drawing.Color.Empty;
            this.ControlBox.HelpButton.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.ControlBox.HelpButton.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.HelpButton.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.HelpButton.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ControlBox.Location = new System.Drawing.Point(239, 4);
            // 
            // 
            // 
            this.ControlBox.MaximizeButton.BackColorState.Disabled = System.Drawing.Color.Transparent;
            this.ControlBox.MaximizeButton.BackColorState.Enabled = System.Drawing.Color.Transparent;
            this.ControlBox.MaximizeButton.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(186)))), ((int)(((byte)(186)))));
            this.ControlBox.MaximizeButton.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.ControlBox.MaximizeButton.BoxType = VisualPlus.Toolkit.VisualBase.ControlBoxButton.ControlBoxType.Default;
            this.ControlBox.MaximizeButton.ForeColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.ControlBox.MaximizeButton.ForeColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ControlBox.MaximizeButton.ForeColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ControlBox.MaximizeButton.ForeColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ControlBox.MaximizeButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.ControlBox.MaximizeButton.Location = new System.Drawing.Point(48, 0);
            this.ControlBox.MaximizeButton.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.ControlBox.MaximizeButton.Name = "";
            this.ControlBox.MaximizeButton.OffsetLocation = new System.Drawing.Point(1, 1);
            this.ControlBox.MaximizeButton.Size = new System.Drawing.Size(24, 25);
            this.ControlBox.MaximizeButton.TabIndex = 2;
            this.ControlBox.MaximizeButton.Text = "1";
            this.ControlBox.MaximizeButton.TextStyle.Disabled = System.Drawing.Color.Empty;
            this.ControlBox.MaximizeButton.TextStyle.Enabled = System.Drawing.Color.Empty;
            this.ControlBox.MaximizeButton.TextStyle.Hover = System.Drawing.Color.Empty;
            this.ControlBox.MaximizeButton.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.ControlBox.MaximizeButton.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.MaximizeButton.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.MaximizeButton.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // 
            // 
            this.ControlBox.MinimizeButton.BackColorState.Disabled = System.Drawing.Color.Transparent;
            this.ControlBox.MinimizeButton.BackColorState.Enabled = System.Drawing.Color.Transparent;
            this.ControlBox.MinimizeButton.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(186)))), ((int)(((byte)(186)))));
            this.ControlBox.MinimizeButton.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.ControlBox.MinimizeButton.BoxType = VisualPlus.Toolkit.VisualBase.ControlBoxButton.ControlBoxType.Default;
            this.ControlBox.MinimizeButton.ForeColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.ControlBox.MinimizeButton.ForeColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ControlBox.MinimizeButton.ForeColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ControlBox.MinimizeButton.ForeColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ControlBox.MinimizeButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.ControlBox.MinimizeButton.Location = new System.Drawing.Point(24, 0);
            this.ControlBox.MinimizeButton.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.ControlBox.MinimizeButton.Name = "";
            this.ControlBox.MinimizeButton.OffsetLocation = new System.Drawing.Point(2, 0);
            this.ControlBox.MinimizeButton.Size = new System.Drawing.Size(24, 25);
            this.ControlBox.MinimizeButton.TabIndex = 1;
            this.ControlBox.MinimizeButton.Text = "0";
            this.ControlBox.MinimizeButton.TextStyle.Disabled = System.Drawing.Color.Empty;
            this.ControlBox.MinimizeButton.TextStyle.Enabled = System.Drawing.Color.Empty;
            this.ControlBox.MinimizeButton.TextStyle.Hover = System.Drawing.Color.Empty;
            this.ControlBox.MinimizeButton.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.ControlBox.MinimizeButton.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.MinimizeButton.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.MinimizeButton.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ControlBox.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.ControlBox.Name = "";
            this.ControlBox.Size = new System.Drawing.Size(96, 25);
            this.ControlBox.TabIndex = 0;
            this.ControlBox.TextStyle.Disabled = System.Drawing.Color.Empty;
            this.ControlBox.TextStyle.Enabled = System.Drawing.Color.Empty;
            this.ControlBox.TextStyle.Hover = System.Drawing.Color.Empty;
            this.ControlBox.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.ControlBox.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Controls.Add(this.tMinimize);
            this.Controls.Add(this.lMinimize);
            this.Controls.Add(this.tClose);
            this.Controls.Add(this.lClose);
            this.Controls.Add(this.tMaximize);
            this.Controls.Add(this.lMaximize);
            this.Controls.Add(this.tHelp);
            this.Controls.Add(this.lHelp);
            this.Controls.Add(this.LControlBoxToggle);
            this.Controls.Add(this.TControlBox);
            this.HelpButton = true;
            this.Image.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Image.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.Image.Border.HoverVisible = false;
            this.Image.Border.Rounding = 6;
            this.Image.Border.Thickness = 1;
            this.Image.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.Image.Border.Visible = false;
            this.Image.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image3")));
            this.Image.Point = new System.Drawing.Point(5, 7);
            this.Image.Size = new System.Drawing.Size(16, 16);
            this.Image.Visible = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "VisualControlBoxTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VisualControlBox Test";
            this.Load += new System.EventHandler(this.VisualControlBoxTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualPlus.Toolkit.Controls.Interactivity.VisualToggle TControlBox;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel LControlBoxToggle;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel lHelp;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualToggle tHelp;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualToggle tMaximize;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel lMaximize;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualToggle tClose;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel lClose;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualToggle tMinimize;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel lMinimize;
    }
}