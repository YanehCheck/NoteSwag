using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteSwag
{
    public class Document
    {
        public string Directory { get; set; }
        public TabPage TabPage { get; set; }
        public RichTextBox TextBox { get; set; }
        public Document() {
            string title = "New document";
            TabPage = new TabPage(title);
            MainForm.instance.tabControlDocuments.TabPages.Add(TabPage);

            TextBox = new RichTextBox();
            TextBox.Dock = DockStyle.Fill;
            TextBox.Multiline = true;
            TextBox.WordWrap = false;
            TextBox.ScrollBars = RichTextBoxScrollBars.Both;
            TextBox.SelectionChanged += this.UpdateSideBarInfo;
            TextBox.AcceptsTab = true;
            TextBox.ForeColor = Themes.DocumentTextColor;
            TextBox.BackColor = Themes.DocumentColor;
            TabPage.Controls.Add(TextBox);
        }
        public Document(string path)
        {
            Directory = path;

            string title = Path.GetFileName(Directory);
            TabPage = new TabPage(title);
            MainForm.instance.tabControlDocuments.TabPages.Add(TabPage);

            TextBox = new RichTextBox();
            TextBox.Dock = DockStyle.Fill;
            TextBox.Multiline = true;
            TextBox.WordWrap = false;
            TextBox.ScrollBars = RichTextBoxScrollBars.Both;
            TextBox.SelectionChanged += this.UpdateSideBarInfo;
            TextBox.AcceptsTab = true;
            TextBox.ForeColor = Themes.DocumentTextColor;
            TextBox.BackColor = Themes.DocumentColor;
            TextBox.Text = File.ReadAllText(Directory);
            TabPage.Controls.Add(TextBox);
        }

        public void UpdateSideBarInfo(object sender, EventArgs e) {
            int lineNumber = TextBox.GetLineFromCharIndex(TextBox.SelectionStart) + 1;
            int charNumber = TextBox.SelectionStart - (TextBox.GetFirstCharIndexFromLine(1 + TextBox.GetLineFromCharIndex(TextBox.SelectionStart) - 1));
            MainForm.instance.DocumentManager.SetActiveDocumentFileInfo();
            MainForm.instance.ChangeLineAndCharacterNumber(lineNumber, charNumber);
        }
    }
}
