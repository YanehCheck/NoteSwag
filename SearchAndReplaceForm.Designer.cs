
namespace Notepad__
{
    partial class SearchAndReplaceForm
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.labelFindText = new System.Windows.Forms.Label();
            this.textBoxReplaceWith = new System.Windows.Forms.TextBox();
            this.labelReplaceText = new System.Windows.Forms.Label();
            this.buttonFind = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.panelControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.Black;
            this.panelControls.Controls.Add(this.labelTitle);
            this.panelControls.Controls.Add(this.buttonClose);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(285, 37);
            this.panelControls.TabIndex = 1;
            this.panelControls.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MakeControlDraggableEvent);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(12, 11);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(137, 17);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "Search and Replace";
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MakeControlDraggableEvent);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(245, 7);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(25, 25);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "X";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxFind
            // 
            this.textBoxFind.Location = new System.Drawing.Point(12, 70);
            this.textBoxFind.Multiline = true;
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.Size = new System.Drawing.Size(150, 25);
            this.textBoxFind.TabIndex = 2;
            // 
            // labelFindText
            // 
            this.labelFindText.AutoSize = true;
            this.labelFindText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFindText.ForeColor = System.Drawing.Color.White;
            this.labelFindText.Location = new System.Drawing.Point(9, 47);
            this.labelFindText.Name = "labelFindText";
            this.labelFindText.Size = new System.Drawing.Size(45, 18);
            this.labelFindText.TabIndex = 4;
            this.labelFindText.Text = "Find:";
            // 
            // textBoxReplaceWith
            // 
            this.textBoxReplaceWith.Location = new System.Drawing.Point(12, 119);
            this.textBoxReplaceWith.Multiline = true;
            this.textBoxReplaceWith.Name = "textBoxReplaceWith";
            this.textBoxReplaceWith.Size = new System.Drawing.Size(150, 25);
            this.textBoxReplaceWith.TabIndex = 5;
            // 
            // labelReplaceText
            // 
            this.labelReplaceText.AutoSize = true;
            this.labelReplaceText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReplaceText.ForeColor = System.Drawing.Color.White;
            this.labelReplaceText.Location = new System.Drawing.Point(9, 98);
            this.labelReplaceText.Name = "labelReplaceText";
            this.labelReplaceText.Size = new System.Drawing.Size(109, 18);
            this.labelReplaceText.TabIndex = 7;
            this.labelReplaceText.Text = "Replace with:";
            // 
            // buttonFind
            // 
            this.buttonFind.Location = new System.Drawing.Point(173, 70);
            this.buttonFind.Margin = new System.Windows.Forms.Padding(0);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(77, 25);
            this.buttonFind.TabIndex = 4;
            this.buttonFind.Text = "Find";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(173, 119);
            this.buttonReplace.Margin = new System.Windows.Forms.Padding(0);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(77, 25);
            this.buttonReplace.TabIndex = 8;
            this.buttonReplace.Text = "Replace";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // SearchAndReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(285, 163);
            this.ControlBox = false;
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.labelReplaceText);
            this.Controls.Add(this.textBoxReplaceWith);
            this.Controls.Add(this.labelFindText);
            this.Controls.Add(this.textBoxFind);
            this.Controls.Add(this.panelControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchAndReplaceForm";
            this.ShowIcon = false;
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxFind;
        private System.Windows.Forms.Label labelFindText;
        private System.Windows.Forms.TextBox textBoxReplaceWith;
        private System.Windows.Forms.Label labelReplaceText;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.Button buttonReplace;
    }
}