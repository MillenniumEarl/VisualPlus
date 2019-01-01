using VisualPlus.Toolkit.VisualBase;

namespace UnitTests.Tests
{
    partial class VisualMessageBoxTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualMessageBoxTest));
            this.btnTest = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.tbTitle = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.lTitle = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.lMessage = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.tbMessage = new VisualPlus.Toolkit.Controls.Editors.VisualTextBox();
            this.cbButtons = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.lButtons = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.lIcon = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.cbIcons = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lImage = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.btnLoad = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.tImage = new VisualPlus.Toolkit.Controls.Interactivity.VisualToggle();
            this.visualLabel1 = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.cbType = new VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox();
            this.lResult = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.btnGenerate = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTest.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnTest.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTest.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnTest.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnTest.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.btnTest.Border.HoverVisible = true;
            this.btnTest.Border.Rounding = 6;
            this.btnTest.Border.Thickness = 1;
            this.btnTest.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnTest.Border.Visible = true;
            this.btnTest.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnTest.Image = null;
            this.btnTest.Location = new System.Drawing.Point(190, 297);
            this.btnTest.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(99, 34);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test";
            this.btnTest.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnTest.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnTest.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnTest.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnTest.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnTest.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnTest.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnTest.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.btnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // tbTitle
            // 
            this.tbTitle.AlphaNumeric = false;
            this.tbTitle.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tbTitle.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.tbTitle.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbTitle.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tbTitle.Border.HoverVisible = true;
            this.tbTitle.Border.Rounding = 6;
            this.tbTitle.Border.Thickness = 1;
            this.tbTitle.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tbTitle.Border.Visible = true;
            this.tbTitle.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbTitle.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tbTitle.ButtonBorder.HoverVisible = true;
            this.tbTitle.ButtonBorder.Rounding = 6;
            this.tbTitle.ButtonBorder.Thickness = 1;
            this.tbTitle.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tbTitle.ButtonBorder.Visible = true;
            this.tbTitle.ButtonColor.Disabled = System.Drawing.Color.Empty;
            this.tbTitle.ButtonColor.Enabled = System.Drawing.Color.Empty;
            this.tbTitle.ButtonColor.Hover = System.Drawing.Color.Empty;
            this.tbTitle.ButtonColor.Pressed = System.Drawing.Color.Empty;
            this.tbTitle.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTitle.ButtonIndent = 3;
            this.tbTitle.ButtonText = "visualButton";
            this.tbTitle.ButtonVisible = false;
            this.tbTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbTitle.Image = null;
            this.tbTitle.ImageSize = new System.Drawing.Size(16, 16);
            this.tbTitle.ImageVisible = false;
            this.tbTitle.ImageWidth = 35;
            this.tbTitle.Location = new System.Drawing.Point(73, 35);
            this.tbTitle.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.PasswordChar = '\0';
            this.tbTitle.ReadOnly = false;
            this.tbTitle.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbTitle.Size = new System.Drawing.Size(216, 23);
            this.tbTitle.TabIndex = 2;
            this.tbTitle.TextBoxWidth = 206;
            this.tbTitle.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.tbTitle.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbTitle.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbTitle.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbTitle.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.tbTitle.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.tbTitle.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.tbTitle.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tbTitle.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTitle.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.tbTitle.Watermark.Text = "Window Title";
            this.tbTitle.Watermark.Visible = true;
            this.tbTitle.WordWrap = true;
            // 
            // lTitle
            // 
            this.lTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lTitle.Location = new System.Drawing.Point(13, 34);
            this.lTitle.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lTitle.Name = "lTitle";
            this.lTitle.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.lTitle.Outline = false;
            this.lTitle.OutlineColor = System.Drawing.Color.Red;
            this.lTitle.OutlineLocation = new System.Drawing.Point(0, 0);
            this.lTitle.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lTitle.ReflectionSpacing = 0;
            this.lTitle.ShadowColor = System.Drawing.Color.Black;
            this.lTitle.ShadowDirection = 315;
            this.lTitle.ShadowLocation = new System.Drawing.Point(0, 0);
            this.lTitle.ShadowOpacity = 100;
            this.lTitle.Size = new System.Drawing.Size(54, 23);
            this.lTitle.TabIndex = 3;
            this.lTitle.Text = "Title:";
            this.lTitle.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lTitle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lTitle.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lTitle.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lTitle.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lTitle.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lTitle.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lTitle.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lTitle.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // lMessage
            // 
            this.lMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMessage.Location = new System.Drawing.Point(13, 63);
            this.lMessage.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lMessage.Name = "lMessage";
            this.lMessage.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.lMessage.Outline = false;
            this.lMessage.OutlineColor = System.Drawing.Color.Red;
            this.lMessage.OutlineLocation = new System.Drawing.Point(0, 0);
            this.lMessage.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMessage.ReflectionSpacing = 0;
            this.lMessage.ShadowColor = System.Drawing.Color.Black;
            this.lMessage.ShadowDirection = 315;
            this.lMessage.ShadowLocation = new System.Drawing.Point(0, 0);
            this.lMessage.ShadowOpacity = 100;
            this.lMessage.Size = new System.Drawing.Size(54, 23);
            this.lMessage.TabIndex = 5;
            this.lMessage.Text = "Message:";
            this.lMessage.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lMessage.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lMessage.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lMessage.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMessage.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMessage.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lMessage.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lMessage.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lMessage.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // tbMessage
            // 
            this.tbMessage.AlphaNumeric = false;
            this.tbMessage.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tbMessage.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.tbMessage.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbMessage.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tbMessage.Border.HoverVisible = true;
            this.tbMessage.Border.Rounding = 6;
            this.tbMessage.Border.Thickness = 1;
            this.tbMessage.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tbMessage.Border.Visible = true;
            this.tbMessage.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tbMessage.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tbMessage.ButtonBorder.HoverVisible = true;
            this.tbMessage.ButtonBorder.Rounding = 6;
            this.tbMessage.ButtonBorder.Thickness = 1;
            this.tbMessage.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tbMessage.ButtonBorder.Visible = true;
            this.tbMessage.ButtonColor.Disabled = System.Drawing.Color.Empty;
            this.tbMessage.ButtonColor.Enabled = System.Drawing.Color.Empty;
            this.tbMessage.ButtonColor.Hover = System.Drawing.Color.Empty;
            this.tbMessage.ButtonColor.Pressed = System.Drawing.Color.Empty;
            this.tbMessage.ButtonFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMessage.ButtonIndent = 3;
            this.tbMessage.ButtonText = "visualButton";
            this.tbMessage.ButtonVisible = false;
            this.tbMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbMessage.Image = null;
            this.tbMessage.ImageSize = new System.Drawing.Size(16, 16);
            this.tbMessage.ImageVisible = false;
            this.tbMessage.ImageWidth = 35;
            this.tbMessage.Location = new System.Drawing.Point(73, 64);
            this.tbMessage.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.tbMessage.MultiLine = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.PasswordChar = '\0';
            this.tbMessage.ReadOnly = false;
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMessage.Size = new System.Drawing.Size(216, 70);
            this.tbMessage.TabIndex = 4;
            this.tbMessage.TextBoxWidth = 206;
            this.tbMessage.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.tbMessage.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbMessage.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbMessage.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbMessage.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.tbMessage.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.tbMessage.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.tbMessage.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tbMessage.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMessage.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.tbMessage.Watermark.Text = "Message";
            this.tbMessage.Watermark.Visible = true;
            this.tbMessage.WordWrap = true;
            // 
            // cbButtons
            // 
            this.cbButtons.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.cbButtons.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.cbButtons.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.cbButtons.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.cbButtons.Border.HoverVisible = true;
            this.cbButtons.Border.Rounding = 6;
            this.cbButtons.Border.Thickness = 1;
            this.cbButtons.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.cbButtons.Border.Visible = true;
            this.cbButtons.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.cbButtons.ButtonImage = null;
            this.cbButtons.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.cbButtons.ButtonWidth = 30;
            this.cbButtons.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbButtons.DropDownHeight = 100;
            this.cbButtons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbButtons.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbButtons.FormattingEnabled = true;
            this.cbButtons.ImageList = null;
            this.cbButtons.ImageVisible = false;
            this.cbButtons.Index = 0;
            this.cbButtons.IntegralHeight = false;
            this.cbButtons.ItemHeight = 24;
            this.cbButtons.ItemImageVisible = true;
            this.cbButtons.Location = new System.Drawing.Point(73, 140);
            this.cbButtons.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.cbButtons.MenuItemNormal = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cbButtons.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbButtons.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.cbButtons.Name = "cbButtons";
            this.cbButtons.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(222)))), ((int)(((byte)(220)))));
            this.cbButtons.Size = new System.Drawing.Size(216, 30);
            this.cbButtons.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.cbButtons.TabIndex = 6;
            this.cbButtons.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cbButtons.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.cbButtons.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbButtons.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cbButtons.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cbButtons.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.cbButtons.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbButtons.TextStyle.Hover = System.Drawing.Color.Empty;
            this.cbButtons.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.cbButtons.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cbButtons.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cbButtons.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cbButtons.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cbButtons.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbButtons.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.cbButtons.Watermark.Text = "Watermark text";
            this.cbButtons.Watermark.Visible = false;
            // 
            // lButtons
            // 
            this.lButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lButtons.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lButtons.Location = new System.Drawing.Point(13, 143);
            this.lButtons.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lButtons.Name = "lButtons";
            this.lButtons.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.lButtons.Outline = false;
            this.lButtons.OutlineColor = System.Drawing.Color.Red;
            this.lButtons.OutlineLocation = new System.Drawing.Point(0, 0);
            this.lButtons.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lButtons.ReflectionSpacing = 0;
            this.lButtons.ShadowColor = System.Drawing.Color.Black;
            this.lButtons.ShadowDirection = 315;
            this.lButtons.ShadowLocation = new System.Drawing.Point(0, 0);
            this.lButtons.ShadowOpacity = 100;
            this.lButtons.Size = new System.Drawing.Size(54, 23);
            this.lButtons.TabIndex = 7;
            this.lButtons.Text = "Buttons:";
            this.lButtons.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lButtons.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lButtons.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lButtons.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lButtons.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lButtons.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lButtons.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lButtons.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lButtons.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // lIcon
            // 
            this.lIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lIcon.Location = new System.Drawing.Point(13, 179);
            this.lIcon.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lIcon.Name = "lIcon";
            this.lIcon.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.lIcon.Outline = false;
            this.lIcon.OutlineColor = System.Drawing.Color.Red;
            this.lIcon.OutlineLocation = new System.Drawing.Point(0, 0);
            this.lIcon.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lIcon.ReflectionSpacing = 0;
            this.lIcon.ShadowColor = System.Drawing.Color.Black;
            this.lIcon.ShadowDirection = 315;
            this.lIcon.ShadowLocation = new System.Drawing.Point(0, 0);
            this.lIcon.ShadowOpacity = 100;
            this.lIcon.Size = new System.Drawing.Size(54, 23);
            this.lIcon.TabIndex = 9;
            this.lIcon.Text = "Icons:";
            this.lIcon.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lIcon.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lIcon.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lIcon.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lIcon.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lIcon.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lIcon.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lIcon.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lIcon.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // cbIcons
            // 
            this.cbIcons.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.cbIcons.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.cbIcons.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.cbIcons.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.cbIcons.Border.HoverVisible = true;
            this.cbIcons.Border.Rounding = 6;
            this.cbIcons.Border.Thickness = 1;
            this.cbIcons.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.cbIcons.Border.Visible = true;
            this.cbIcons.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.cbIcons.ButtonImage = null;
            this.cbIcons.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.cbIcons.ButtonWidth = 30;
            this.cbIcons.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbIcons.DropDownHeight = 100;
            this.cbIcons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIcons.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbIcons.FormattingEnabled = true;
            this.cbIcons.ImageList = null;
            this.cbIcons.ImageVisible = false;
            this.cbIcons.Index = 0;
            this.cbIcons.IntegralHeight = false;
            this.cbIcons.ItemHeight = 24;
            this.cbIcons.ItemImageVisible = true;
            this.cbIcons.Location = new System.Drawing.Point(73, 176);
            this.cbIcons.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.cbIcons.MenuItemNormal = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cbIcons.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbIcons.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.cbIcons.Name = "cbIcons";
            this.cbIcons.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(222)))), ((int)(((byte)(220)))));
            this.cbIcons.Size = new System.Drawing.Size(216, 30);
            this.cbIcons.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.cbIcons.TabIndex = 8;
            this.cbIcons.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cbIcons.TextDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.cbIcons.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbIcons.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cbIcons.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cbIcons.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.cbIcons.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbIcons.TextStyle.Hover = System.Drawing.Color.Empty;
            this.cbIcons.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.cbIcons.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cbIcons.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cbIcons.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cbIcons.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cbIcons.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIcons.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.cbIcons.Watermark.Text = "Watermark text";
            this.cbIcons.Watermark.Visible = false;
            // 
            // pbImage
            // 
            this.pbImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbImage.BackgroundImage")));
            this.pbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbImage.Location = new System.Drawing.Point(257, 212);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(32, 32);
            this.pbImage.TabIndex = 10;
            this.pbImage.TabStop = false;
            // 
            // lImage
            // 
            this.lImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lImage.Location = new System.Drawing.Point(13, 220);
            this.lImage.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lImage.Name = "lImage";
            this.lImage.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.lImage.Outline = false;
            this.lImage.OutlineColor = System.Drawing.Color.Red;
            this.lImage.OutlineLocation = new System.Drawing.Point(0, 0);
            this.lImage.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lImage.ReflectionSpacing = 0;
            this.lImage.ShadowColor = System.Drawing.Color.Black;
            this.lImage.ShadowDirection = 315;
            this.lImage.ShadowLocation = new System.Drawing.Point(0, 0);
            this.lImage.ShadowOpacity = 100;
            this.lImage.Size = new System.Drawing.Size(54, 23);
            this.lImage.TabIndex = 11;
            this.lImage.Text = "Image:";
            this.lImage.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lImage.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lImage.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lImage.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lImage.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lImage.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lImage.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lImage.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lImage.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // btnLoad
            // 
            this.btnLoad.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoad.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnLoad.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoad.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLoad.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnLoad.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.btnLoad.Border.HoverVisible = true;
            this.btnLoad.Border.Rounding = 6;
            this.btnLoad.Border.Thickness = 1;
            this.btnLoad.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnLoad.Border.Visible = true;
            this.btnLoad.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLoad.Enabled = false;
            this.btnLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLoad.Image = null;
            this.btnLoad.Location = new System.Drawing.Point(195, 216);
            this.btnLoad.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(56, 25);
            this.btnLoad.TabIndex = 12;
            this.btnLoad.Text = "Load";
            this.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnLoad.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnLoad.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLoad.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLoad.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLoad.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnLoad.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnLoad.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // tImage
            // 
            this.tImage.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tImage.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tImage.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tImage.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tImage.Border.HoverVisible = true;
            this.tImage.Border.Rounding = 20;
            this.tImage.Border.Thickness = 1;
            this.tImage.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tImage.Border.Visible = true;
            this.tImage.ButtonBorder.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tImage.ButtonBorder.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.tImage.ButtonBorder.HoverVisible = true;
            this.tImage.ButtonBorder.Rounding = 18;
            this.tImage.ButtonBorder.Thickness = 1;
            this.tImage.ButtonBorder.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.tImage.ButtonBorder.Visible = true;
            this.tImage.ButtonColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tImage.ButtonColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tImage.ButtonColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tImage.ButtonColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tImage.ButtonSize = new System.Drawing.Size(20, 20);
            this.tImage.FalseTextToggle = "No";
            this.tImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tImage.Location = new System.Drawing.Point(73, 216);
            this.tImage.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.tImage.Name = "tImage";
            this.tImage.ProgressImage = null;
            this.tImage.Size = new System.Drawing.Size(50, 25);
            this.tImage.TabIndex = 14;
            this.tImage.Text = "visualToggle1";
            this.tImage.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.tImage.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tImage.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tImage.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tImage.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.tImage.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.tImage.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.tImage.TrueTextToggle = "Yes";
            this.tImage.Type = VisualPlus.Toolkit.Controls.Interactivity.VisualToggle.ToggleTypes.YesNo;
            this.tImage.ToggleChanged += new VisualPlus.Delegates.ToggleChangedEventHandler(this.TImage_ToggleChanged);
            // 
            // visualLabel1
            // 
            this.visualLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.visualLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualLabel1.Location = new System.Drawing.Point(13, 253);
            this.visualLabel1.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualLabel1.Name = "visualLabel1";
            this.visualLabel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.visualLabel1.Outline = false;
            this.visualLabel1.OutlineColor = System.Drawing.Color.Red;
            this.visualLabel1.OutlineLocation = new System.Drawing.Point(0, 0);
            this.visualLabel1.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualLabel1.ReflectionSpacing = 0;
            this.visualLabel1.ShadowColor = System.Drawing.Color.Black;
            this.visualLabel1.ShadowDirection = 315;
            this.visualLabel1.ShadowLocation = new System.Drawing.Point(0, 0);
            this.visualLabel1.ShadowOpacity = 100;
            this.visualLabel1.Size = new System.Drawing.Size(54, 23);
            this.visualLabel1.TabIndex = 16;
            this.visualLabel1.Text = "Type:";
            this.visualLabel1.TextAlignment = System.Drawing.StringAlignment.Near;
            this.visualLabel1.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualLabel1.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualLabel1.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualLabel1.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualLabel1.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualLabel1.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualLabel1.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualLabel1.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // cbType
            // 
            this.cbType.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.cbType.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.cbType.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.cbType.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.cbType.Border.HoverVisible = true;
            this.cbType.Border.Rounding = 6;
            this.cbType.Border.Thickness = 1;
            this.cbType.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.cbType.Border.Visible = true;
            this.cbType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.cbType.ButtonImage = null;
            this.cbType.ButtonStyle = VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox.ButtonStyles.Arrow;
            this.cbType.ButtonWidth = 30;
            this.cbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbType.DropDownHeight = 100;
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.DropDownWidth = 69;
            this.cbType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbType.FormattingEnabled = true;
            this.cbType.ImageList = null;
            this.cbType.ImageVisible = false;
            this.cbType.Index = 0;
            this.cbType.IntegralHeight = false;
            this.cbType.ItemHeight = 24;
            this.cbType.ItemImageVisible = true;
            this.cbType.Items.AddRange(new object[] {
            "Default",
            "Windows"});
            this.cbType.Location = new System.Drawing.Point(73, 250);
            this.cbType.MenuItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.cbType.MenuItemNormal = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cbType.MenuTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbType.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.cbType.Name = "cbType";
            this.cbType.SeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(222)))), ((int)(((byte)(220)))));
            this.cbType.Size = new System.Drawing.Size(216, 30);
            this.cbType.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.cbType.TabIndex = 15;
            this.cbType.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cbType.TextDisabledColor = System.Drawing.Color.Empty;
            this.cbType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cbType.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cbType.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cbType.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.cbType.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbType.TextStyle.Hover = System.Drawing.Color.Empty;
            this.cbType.TextStyle.Pressed = System.Drawing.Color.Empty;
            this.cbType.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.cbType.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.cbType.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.cbType.Watermark.Active = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cbType.Watermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbType.Watermark.Inactive = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.cbType.Watermark.Text = "Watermark text";
            this.cbType.Watermark.Visible = false;
            // 
            // lResult
            // 
            this.lResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lResult.Location = new System.Drawing.Point(4, 335);
            this.lResult.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lResult.Name = "lResult";
            this.lResult.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.lResult.Outline = false;
            this.lResult.OutlineColor = System.Drawing.Color.Red;
            this.lResult.OutlineLocation = new System.Drawing.Point(0, 0);
            this.lResult.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lResult.ReflectionSpacing = 0;
            this.lResult.ShadowColor = System.Drawing.Color.Black;
            this.lResult.ShadowDirection = 315;
            this.lResult.ShadowLocation = new System.Drawing.Point(0, 0);
            this.lResult.ShadowOpacity = 100;
            this.lResult.Size = new System.Drawing.Size(294, 23);
            this.lResult.TabIndex = 17;
            this.lResult.Text = "Dialog Result: None";
            this.lResult.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lResult.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lResult.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lResult.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lResult.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lResult.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lResult.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lResult.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lResult.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGenerate.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnGenerate.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGenerate.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnGenerate.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnGenerate.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.btnGenerate.Border.HoverVisible = true;
            this.btnGenerate.Border.Rounding = 6;
            this.btnGenerate.Border.Thickness = 1;
            this.btnGenerate.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.btnGenerate.Border.Visible = true;
            this.btnGenerate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnGenerate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGenerate.Image = null;
            this.btnGenerate.Location = new System.Drawing.Point(8, 112);
            this.btnGenerate.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(59, 22);
            this.btnGenerate.TabIndex = 18;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnGenerate.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.btnGenerate.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGenerate.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGenerate.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGenerate.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnGenerate.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.btnGenerate.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // VisualMessageBoxTest
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
            this.ClientSize = new System.Drawing.Size(301, 365);
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
            this.ControlBox.HelpButton.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.ControlBox.HelpButton.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.HelpButton.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.HelpButton.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.HelpButton.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.HelpButton.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.HelpButton.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ControlBox.HelpButton.Visible = false;
            this.ControlBox.Location = new System.Drawing.Point(273, 4);
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
            this.ControlBox.MaximizeButton.Location = new System.Drawing.Point(0, 0);
            this.ControlBox.MaximizeButton.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.ControlBox.MaximizeButton.Name = "";
            this.ControlBox.MaximizeButton.OffsetLocation = new System.Drawing.Point(1, 1);
            this.ControlBox.MaximizeButton.Size = new System.Drawing.Size(24, 25);
            this.ControlBox.MaximizeButton.TabIndex = 2;
            this.ControlBox.MaximizeButton.Text = "1";
            this.ControlBox.MaximizeButton.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.ControlBox.MaximizeButton.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.MaximizeButton.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.MaximizeButton.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.MaximizeButton.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.MaximizeButton.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.MaximizeButton.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ControlBox.MaximizeButton.Visible = false;
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
            this.ControlBox.MinimizeButton.Location = new System.Drawing.Point(0, 0);
            this.ControlBox.MinimizeButton.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.ControlBox.MinimizeButton.Name = "";
            this.ControlBox.MinimizeButton.OffsetLocation = new System.Drawing.Point(2, 0);
            this.ControlBox.MinimizeButton.Size = new System.Drawing.Size(24, 25);
            this.ControlBox.MinimizeButton.TabIndex = 1;
            this.ControlBox.MinimizeButton.Text = "0";
            this.ControlBox.MinimizeButton.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.ControlBox.MinimizeButton.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.MinimizeButton.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.MinimizeButton.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.MinimizeButton.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.MinimizeButton.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.MinimizeButton.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.ControlBox.MinimizeButton.Visible = false;
            this.ControlBox.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.ControlBox.Name = "";
            this.ControlBox.Size = new System.Drawing.Size(24, 25);
            this.ControlBox.TabIndex = 0;
            this.ControlBox.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.ControlBox.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ControlBox.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lResult);
            this.Controls.Add(this.visualLabel1);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.tImage);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lImage);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.lIcon);
            this.Controls.Add(this.cbIcons);
            this.Controls.Add(this.lButtons);
            this.Controls.Add(this.cbButtons);
            this.Controls.Add(this.lMessage);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.lTitle);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.btnTest);
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
            this.Location = new System.Drawing.Point(50, 0);
            this.Name = "VisualMessageBoxTest";
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VisualMessageBox Test";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnTest;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox tbTitle;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel lTitle;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel lMessage;
        private VisualPlus.Toolkit.Controls.Editors.VisualTextBox tbMessage;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox cbButtons;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel lButtons;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel lIcon;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox cbIcons;
        private System.Windows.Forms.PictureBox pbImage;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel lImage;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnLoad;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualToggle tImage;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel visualLabel1;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualComboBox cbType;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel lResult;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton btnGenerate;
    }
}