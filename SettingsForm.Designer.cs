
namespace NoteSwag
{
    partial class SettingsForm
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
            this.panelControls = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelThemeText = new System.Windows.Forms.Label();
            this.comboBoxTheme = new System.Windows.Forms.ComboBox();
            this.checkBoxBracketMatching = new System.Windows.Forms.CheckBox();
            this.labelBracketMatchingText = new System.Windows.Forms.Label();
            this.comboBoxFont = new System.Windows.Forms.ComboBox();
            this.labelFontText = new System.Windows.Forms.Label();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.Black;
            this.panelControls.Controls.Add(this.pictureBox1);
            this.panelControls.Controls.Add(this.labelTitle);
            this.panelControls.Controls.Add(this.buttonClose);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(317, 30);
            this.panelControls.TabIndex = 1;
            this.panelControls.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MakeControlDraggableEvent);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gray;
            this.pictureBox1.Image = global::NoteSwag.Properties.Resources.icon24;
            this.pictureBox1.Location = new System.Drawing.Point(9, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MakeControlDraggableEvent);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(32, 9);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(51, 15);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "Settings";
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MakeControlDraggableEvent);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(287, 6);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(19, 20);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "X";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelThemeText
            // 
            this.labelThemeText.AutoSize = true;
            this.labelThemeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThemeText.ForeColor = System.Drawing.Color.White;
            this.labelThemeText.Location = new System.Drawing.Point(6, 41);
            this.labelThemeText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelThemeText.Name = "labelThemeText";
            this.labelThemeText.Size = new System.Drawing.Size(64, 18);
            this.labelThemeText.TabIndex = 6;
            this.labelThemeText.Text = "Theme:";
            // 
            // comboBoxTheme
            // 
            this.comboBoxTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTheme.FormattingEnabled = true;
            this.comboBoxTheme.Items.AddRange(new object[] {
            "Dark",
            "DarkPlus",
            "Classic",
            "Midnight",
            "Classy"});
            this.comboBoxTheme.Location = new System.Drawing.Point(59, 41);
            this.comboBoxTheme.Name = "comboBoxTheme";
            this.comboBoxTheme.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTheme.TabIndex = 7;
            this.comboBoxTheme.SelectedIndexChanged += new System.EventHandler(this.comboBoxTheme_SelectedIndexChanged);
            // 
            // checkBoxBracketMatching
            // 
            this.checkBoxBracketMatching.AutoSize = true;
            this.checkBoxBracketMatching.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.checkBoxBracketMatching.ForeColor = System.Drawing.Color.White;
            this.checkBoxBracketMatching.Location = new System.Drawing.Point(132, 66);
            this.checkBoxBracketMatching.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxBracketMatching.Name = "checkBoxBracketMatching";
            this.checkBoxBracketMatching.Size = new System.Drawing.Size(18, 17);
            this.checkBoxBracketMatching.TabIndex = 8;
            this.checkBoxBracketMatching.UseVisualStyleBackColor = true;
            this.checkBoxBracketMatching.CheckedChanged += new System.EventHandler(this.checkBoxBracketMatching_CheckedChanged);
            // 
            // labelBracketMatchingText
            // 
            this.labelBracketMatchingText.AutoSize = true;
            this.labelBracketMatchingText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBracketMatchingText.ForeColor = System.Drawing.Color.White;
            this.labelBracketMatchingText.Location = new System.Drawing.Point(6, 63);
            this.labelBracketMatchingText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBracketMatchingText.Name = "labelBracketMatchingText";
            this.labelBracketMatchingText.Size = new System.Drawing.Size(144, 18);
            this.labelBracketMatchingText.TabIndex = 9;
            this.labelBracketMatchingText.Text = "Bracket matching:";
            // 
            // comboBoxFont
            // 
            this.comboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFont.FormattingEnabled = true;
            this.comboBoxFont.Location = new System.Drawing.Point(59, 83);
            this.comboBoxFont.Name = "comboBoxFont";
            this.comboBoxFont.Size = new System.Drawing.Size(200, 21);
            this.comboBoxFont.TabIndex = 11;
            this.comboBoxFont.SelectedIndexChanged += new System.EventHandler(this.comboBoxFont_SelectedIndexChanged);
            // 
            // labelFontText
            // 
            this.labelFontText.AutoSize = true;
            this.labelFontText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFontText.ForeColor = System.Drawing.Color.White;
            this.labelFontText.Location = new System.Drawing.Point(6, 83);
            this.labelFontText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFontText.Name = "labelFontText";
            this.labelFontText.Size = new System.Drawing.Size(47, 18);
            this.labelFontText.TabIndex = 10;
            this.labelFontText.Text = "Font:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(317, 116);
            this.Controls.Add(this.comboBoxFont);
            this.Controls.Add(this.labelFontText);
            this.Controls.Add(this.labelBracketMatchingText);
            this.Controls.Add(this.checkBoxBracketMatching);
            this.Controls.Add(this.comboBoxTheme);
            this.Controls.Add(this.labelThemeText);
            this.Controls.Add(this.panelControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Activated += new System.EventHandler(this.SettingsForm_Activated);
            this.Shown += new System.EventHandler(this.SettingsForm_Shown);
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelThemeText;
        private System.Windows.Forms.ComboBox comboBoxTheme;
        private System.Windows.Forms.Label labelBracketMatchingText;
        public System.Windows.Forms.CheckBox checkBoxBracketMatching;
        private System.Windows.Forms.ComboBox comboBoxFont;
        private System.Windows.Forms.Label labelFontText;
    }
}