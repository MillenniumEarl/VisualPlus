using VisualPlus.Toolkit.VisualBase;

namespace UnitTests.Forms
{
    partial class UnitTestManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitTestManager));
            this.BtnRunTest = new VisualPlus.Toolkit.Controls.Interactivity.VisualButton();
            this.visualListBoxTests = new VisualPlus.Toolkit.Controls.DataManagement.VisualListBox();
            this.visualLabelTestsStats = new VisualPlus.Toolkit.Controls.Interactivity.VisualLabel();
            this.tabController = new VisualPlus.Toolkit.Controls.Navigation.VisualTabControl();
            this.visualTabPage1 = new VisualPlus.Toolkit.Child.VisualTabPage();
            this.visualTabPage2 = new VisualPlus.Toolkit.Child.VisualTabPage();
            this.lvFlaggedTests = new VisualPlus.Toolkit.Controls.DataManagement.VisualListView();
            this.tabController.SuspendLayout();
            this.visualTabPage1.SuspendLayout();
            this.visualTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnRunTest
            // 
            this.BtnRunTest.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnRunTest.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.BtnRunTest.BackColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnRunTest.BackColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BtnRunTest.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.BtnRunTest.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.BtnRunTest.Border.HoverVisible = true;
            this.BtnRunTest.Border.Rounding = 6;
            this.BtnRunTest.Border.Thickness = 1;
            this.BtnRunTest.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.BtnRunTest.Border.Visible = true;
            this.BtnRunTest.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnRunTest.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.BtnRunTest.ForeColor = System.Drawing.Color.Black;
            this.BtnRunTest.Image = null;
            this.BtnRunTest.Location = new System.Drawing.Point(232, 40);
            this.BtnRunTest.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.BtnRunTest.Name = "BtnRunTest";
            this.BtnRunTest.Size = new System.Drawing.Size(88, 28);
            this.BtnRunTest.TabIndex = 2;
            this.BtnRunTest.Text = "Run Test";
            this.BtnRunTest.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.BtnRunTest.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.BtnRunTest.TextStyle.Enabled = System.Drawing.Color.Black;
            this.BtnRunTest.TextStyle.Hover = System.Drawing.Color.Black;
            this.BtnRunTest.TextStyle.Pressed = System.Drawing.Color.Black;
            this.BtnRunTest.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.BtnRunTest.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.BtnRunTest.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.BtnRunTest.Click += new System.EventHandler(this.BtnRunTest_Click);
            // 
            // visualListBoxTests
            // 
            this.visualListBoxTests.AlternateColors = false;
            this.visualListBoxTests.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.visualListBoxTests.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.visualListBoxTests.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.visualListBoxTests.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.visualListBoxTests.Border.HoverVisible = true;
            this.visualListBoxTests.Border.Rounding = 6;
            this.visualListBoxTests.Border.Thickness = 1;
            this.visualListBoxTests.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rounded;
            this.visualListBoxTests.Border.Visible = true;
            this.visualListBoxTests.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.visualListBoxTests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualListBoxTests.ItemAlternate = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(13)))));
            this.visualListBoxTests.ItemHeight = 18;
            this.visualListBoxTests.ItemLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualListBoxTests.ItemNormal = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.visualListBoxTests.ItemSelected = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.visualListBoxTests.Location = new System.Drawing.Point(3, 3);
            this.visualListBoxTests.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualListBoxTests.Name = "visualListBoxTests";
            this.visualListBoxTests.Size = new System.Drawing.Size(294, 180);
            this.visualListBoxTests.TabIndex = 3;
            this.visualListBoxTests.Text = "visualListBox1";
            this.visualListBoxTests.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualListBoxTests.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualListBoxTests.TextStyle.Hover = System.Drawing.Color.Empty;
            this.visualListBoxTests.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.visualListBoxTests.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualListBoxTests.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualListBoxTests.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.visualListBoxTests.SelectedIndexChanged += new System.EventHandler(this.ListBoxTests_SelectedIndexChanged);
            // 
            // visualLabelTestsStats
            // 
            this.visualLabelTestsStats.BackColor = System.Drawing.Color.White;
            this.visualLabelTestsStats.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.visualLabelTestsStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualLabelTestsStats.Location = new System.Drawing.Point(12, 34);
            this.visualLabelTestsStats.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.visualLabelTestsStats.Name = "visualLabelTestsStats";
            this.visualLabelTestsStats.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.visualLabelTestsStats.Outline = false;
            this.visualLabelTestsStats.OutlineColor = System.Drawing.Color.Red;
            this.visualLabelTestsStats.OutlineLocation = new System.Drawing.Point(0, 0);
            this.visualLabelTestsStats.ReflectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualLabelTestsStats.ReflectionSpacing = 0;
            this.visualLabelTestsStats.ShadowColor = System.Drawing.Color.Black;
            this.visualLabelTestsStats.ShadowDirection = 315;
            this.visualLabelTestsStats.ShadowLocation = new System.Drawing.Point(0, 0);
            this.visualLabelTestsStats.ShadowOpacity = 100;
            this.visualLabelTestsStats.Size = new System.Drawing.Size(214, 38);
            this.visualLabelTestsStats.TabIndex = 4;
            this.visualLabelTestsStats.Text = "Test Index: 0\r\nTotal Tests: 0";
            this.visualLabelTestsStats.TextAlignment = System.Drawing.StringAlignment.Near;
            this.visualLabelTestsStats.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualLabelTestsStats.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.visualLabelTestsStats.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.visualLabelTestsStats.TextStyle.Hover = System.Drawing.Color.Empty;
            this.visualLabelTestsStats.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.visualLabelTestsStats.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualLabelTestsStats.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualLabelTestsStats.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // tabController
            // 
            this.tabController.Controls.Add(this.visualTabPage1);
            this.tabController.Controls.Add(this.visualTabPage2);
            this.tabController.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabController.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabController.ItemSize = new System.Drawing.Size(100, 25);
            this.tabController.Location = new System.Drawing.Point(12, 78);
            this.tabController.MinimumSize = new System.Drawing.Size(144, 85);
            this.tabController.Name = "tabController";
            this.tabController.SelectedIndex = 0;
            this.tabController.SelectorAlignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabController.SelectorSpacing = 10;
            this.tabController.SelectorThickness = 5;
            this.tabController.SelectorType = VisualPlus.Toolkit.Controls.Navigation.VisualTabControl.SelectorTypes.Arrow;
            this.tabController.SelectorVisible = true;
            this.tabController.Separator = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.tabController.SeparatorSpacing = 2;
            this.tabController.SeparatorThickness = 2F;
            this.tabController.Size = new System.Drawing.Size(308, 219);
            this.tabController.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabController.State = VisualPlus.Enumerators.MouseStates.Normal;
            this.tabController.TabIndex = 5;
            this.tabController.TabMenu = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(73)))));
            this.tabController.TabSelector = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.tabController.TextRendering = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // visualTabPage1
            // 
            this.visualTabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.visualTabPage1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.visualTabPage1.Border.Rounding = 6;
            this.visualTabPage1.Border.Thickness = 1;
            this.visualTabPage1.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rectangle;
            this.visualTabPage1.Border.Visible = false;
            this.visualTabPage1.Controls.Add(this.visualListBoxTests);
            this.visualTabPage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(181)))), ((int)(((byte)(187)))));
            this.visualTabPage1.HeaderImage = null;
            this.visualTabPage1.Image = null;
            this.visualTabPage1.ImageSize = new System.Drawing.Size(16, 16);
            this.visualTabPage1.Location = new System.Drawing.Point(4, 29);
            this.visualTabPage1.Name = "visualTabPage1";
            this.visualTabPage1.Rectangle = new System.Drawing.Rectangle(2, 2, 100, 25);
            this.visualTabPage1.Size = new System.Drawing.Size(300, 186);
            this.visualTabPage1.TabHover = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(36)))), ((int)(((byte)(38)))));
            this.visualTabPage1.TabIndex = 0;
            this.visualTabPage1.TabNormal = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(73)))));
            this.visualTabPage1.TabSelected = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(76)))), ((int)(((byte)(88)))));
            this.visualTabPage1.Text = "Units";
            this.visualTabPage1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualTabPage1.TextImageRelation = VisualPlus.Toolkit.Child.VisualTabPage.TextImageRelations.Text;
            this.visualTabPage1.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualTabPage1.TextSelected = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            // 
            // visualTabPage2
            // 
            this.visualTabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.visualTabPage2.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.visualTabPage2.Border.Rounding = 6;
            this.visualTabPage2.Border.Thickness = 1;
            this.visualTabPage2.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rectangle;
            this.visualTabPage2.Border.Visible = false;
            this.visualTabPage2.Controls.Add(this.lvFlaggedTests);
            this.visualTabPage2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(181)))), ((int)(((byte)(187)))));
            this.visualTabPage2.HeaderImage = null;
            this.visualTabPage2.Image = null;
            this.visualTabPage2.ImageSize = new System.Drawing.Size(16, 16);
            this.visualTabPage2.Location = new System.Drawing.Point(4, 29);
            this.visualTabPage2.Name = "visualTabPage2";
            this.visualTabPage2.Rectangle = new System.Drawing.Rectangle(102, 2, 100, 25);
            this.visualTabPage2.Size = new System.Drawing.Size(300, 186);
            this.visualTabPage2.TabHover = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(36)))), ((int)(((byte)(38)))));
            this.visualTabPage2.TabIndex = 1;
            this.visualTabPage2.TabNormal = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(73)))));
            this.visualTabPage2.TabSelected = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(76)))), ((int)(((byte)(88)))));
            this.visualTabPage2.Text = "Flagged Tests";
            this.visualTabPage2.TextAlignment = System.Drawing.StringAlignment.Center;
            this.visualTabPage2.TextImageRelation = VisualPlus.Toolkit.Child.VisualTabPage.TextImageRelations.Text;
            this.visualTabPage2.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.visualTabPage2.TextSelected = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            // 
            // lvFlaggedTests
            // 
            this.lvFlaggedTests.AllowColumnResize = true;
            this.lvFlaggedTests.AlternateBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(13)))));
            this.lvFlaggedTests.AlternatingColors = false;
            this.lvFlaggedTests.AutoHeight = true;
            this.lvFlaggedTests.BackColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lvFlaggedTests.BackColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lvFlaggedTests.BackgroundStretchToFit = true;
            this.lvFlaggedTests.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lvFlaggedTests.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.lvFlaggedTests.Border.HoverVisible = true;
            this.lvFlaggedTests.Border.Rounding = 6;
            this.lvFlaggedTests.Border.Thickness = 1;
            this.lvFlaggedTests.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rectangle;
            this.lvFlaggedTests.Border.Visible = true;
            this.lvFlaggedTests.ColumnColorState.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lvFlaggedTests.ColumnColorState.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lvFlaggedTests.ColumnColorState.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.lvFlaggedTests.ColumnColorState.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.lvFlaggedTests.ControlStyle = VisualPlus.Enumerators.LVControlStyles.SuperFlat;
            this.lvFlaggedTests.CornerBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.lvFlaggedTests.DisplayText = "The list is empty.";
            this.lvFlaggedTests.DisplayTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lvFlaggedTests.DisplayTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvFlaggedTests.DisplayTextOnEmpty = true;
            this.lvFlaggedTests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvFlaggedTests.FullRowSelect = true;
            this.lvFlaggedTests.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lvFlaggedTests.GridLines = VisualPlus.Enumerators.GridLines.Both;
            this.lvFlaggedTests.GridLineStyle = VisualPlus.Enumerators.GridLineStyle.Solid;
            this.lvFlaggedTests.GridTypes = VisualPlus.Enumerators.GridTypes.Normal;
            this.lvFlaggedTests.HeaderHeight = 22;
            this.lvFlaggedTests.HeaderVisible = true;
            this.lvFlaggedTests.HeaderWordWrap = false;
            this.lvFlaggedTests.HoverColumnTracking = true;
            this.lvFlaggedTests.HoverEvents = false;
            this.lvFlaggedTests.HoverItemTracking = true;
            this.lvFlaggedTests.HoverTime = 1;
            this.lvFlaggedTests.HoverTrackingColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.lvFlaggedTests.ImageListColumns = null;
            this.lvFlaggedTests.ImageListItems = null;
            this.lvFlaggedTests.ItemHeight = 18;
            this.lvFlaggedTests.ItemSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.lvFlaggedTests.ItemSelectedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvFlaggedTests.ItemWordWrap = false;
            this.lvFlaggedTests.Location = new System.Drawing.Point(3, 3);
            this.lvFlaggedTests.MouseState = VisualPlus.Enumerators.MouseStates.Normal;
            this.lvFlaggedTests.MultiSelect = false;
            this.lvFlaggedTests.Name = "lvFlaggedTests";
            this.lvFlaggedTests.Selectable = true;
            this.lvFlaggedTests.Size = new System.Drawing.Size(294, 180);
            this.lvFlaggedTests.SortType = VisualPlus.Enumerators.SortTypes.InsertionSort;
            this.lvFlaggedTests.TabIndex = 0;
            this.lvFlaggedTests.Text = "visualListView1";
            this.lvFlaggedTests.TextStyle.Disabled = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(129)))), ((int)(((byte)(129)))));
            this.lvFlaggedTests.TextStyle.Enabled = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvFlaggedTests.TextStyle.Hover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvFlaggedTests.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lvFlaggedTests.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lvFlaggedTests.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.lvFlaggedTests.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // UnitTestManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.Border.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(230)))));
            this.Border.HoverVisible = true;
            this.Border.Rounding = 6;
            this.Border.Thickness = 3;
            this.Border.Type = VisualPlus.Enumerators.ShapeTypes.Rectangle;
            this.Border.Visible = true;
            this.ClientSize = new System.Drawing.Size(333, 308);
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
            this.ControlBox.Location = new System.Drawing.Point(257, 4);
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
            this.ControlBox.Size = new System.Drawing.Size(72, 25);
            this.ControlBox.TabIndex = 0;
            this.ControlBox.TextStyle.Disabled = System.Drawing.Color.Empty;
            this.ControlBox.TextStyle.Enabled = System.Drawing.Color.Empty;
            this.ControlBox.TextStyle.Hover = System.Drawing.Color.Empty;
            this.ControlBox.TextStyle.Pressed = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.ControlBox.TextStyle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.TextStyle.TextLineAlignment = System.Drawing.StringAlignment.Center;
            this.ControlBox.TextStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.Controls.Add(this.tabController);
            this.Controls.Add(this.visualLabelTestsStats);
            this.Controls.Add(this.BtnRunTest);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
            this.MinimizeBox = true;
            this.Name = "UnitTestManager";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisualPlus - Unit Test Manager";
            this.Load += new System.EventHandler(this.TestManager_Load);
            this.tabController.ResumeLayout(false);
            this.visualTabPage1.ResumeLayout(false);
            this.visualTabPage1.PerformLayout();
            this.visualTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private VisualPlus.Toolkit.Controls.Interactivity.VisualButton BtnRunTest;
        private VisualPlus.Toolkit.Controls.DataManagement.VisualListBox visualListBoxTests;
        private VisualPlus.Toolkit.Controls.Interactivity.VisualLabel visualLabelTestsStats;
        private VisualPlus.Toolkit.Controls.Navigation.VisualTabControl tabController;
        private VisualPlus.Toolkit.Child.VisualTabPage visualTabPage1;
        private VisualPlus.Toolkit.Child.VisualTabPage visualTabPage2;
        private VisualPlus.Toolkit.Controls.DataManagement.VisualListView lvFlaggedTests;
    }
}