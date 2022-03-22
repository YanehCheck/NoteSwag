using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace NoteSwag
{
    public class Document
    {
        public string Directory { get; set; }
        public TabPage TabPage { get; set; }
        public RichTextBox TextBox { get; set; }

        public Stack<string> UndoHistory = new Stack<string>();
        public bool RunEvent = true;
        public Document() {
            string title = "New document";
            TabPage = new TabPage(title);
            MainForm.instance.tabControlDocuments.TabPages.Add(TabPage);

            TextBox = new RichTextBox();
            AddCommonTextboxProperties();
            TabPage.Controls.Add(TextBox);
        }
        public Document(string path)
        {
            Directory = path;

            string title = Path.GetFileName(Directory);
            TabPage = new TabPage(title);
            MainForm.instance.tabControlDocuments.TabPages.Add(TabPage);

            TextBox = new RichTextBox();
            AddCommonTextboxProperties();
            TextBox.Text = File.ReadAllText(Directory);
            TabPage.Controls.Add(TextBox);
        }

        private void AddCommonTextboxProperties() {
            TextBox.Dock = DockStyle.Fill;
            TextBox.Multiline = true;
            TextBox.WordWrap = false;
            TextBox.ScrollBars = RichTextBoxScrollBars.Both;
            TextBox.SelectionChanged += this.UpdateSideBarInfo;
            TextBox.MouseDown += this.OpenContextMenu;
            TextBox.KeyPress += this.BracketMatching;
            TextBox.TextChanged += this.SyntaxHighlight;
            TextBox.TextChanged += this.AddToUndoStack;
            TextBox.AcceptsTab = true;
            TextBox.ForeColor = Themes.DocumentTextColor;
            TextBox.BackColor = Themes.DocumentColor;
        }

        public void AddToUndoStack(object sender, EventArgs e) {
            if(RunEvent) UndoHistory.Push(TextBox.Text);
        }

        public void UpdateSideBarInfo(object sender, EventArgs e) {
            int lineNumber = TextBox.GetLineFromCharIndex(TextBox.SelectionStart) + 1;
            int charNumber = TextBox.SelectionStart - (TextBox.GetFirstCharIndexFromLine(1 + TextBox.GetLineFromCharIndex(TextBox.SelectionStart) - 1));
            MainForm.instance.DocumentManager.SetActiveDocumentFileInfo();
            MainForm.instance.ChangeLineAndCharacterNumber(lineNumber, charNumber);
        }

        public void OpenContextMenu(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Right) {
                MainForm.instance.contextMenuStrip.Show(this.TextBox, e.X, e.Y);
            }
        }
        public void BracketMatching(object sender, KeyPressEventArgs e) {
            char bracket;
            if (Properties.Settings.Default.IsBracketMatchingEnabled) {
                switch (e.KeyChar) {
                    case '(':
                        bracket = ')';
                        break;
                    case '{':
                        bracket = '}';
                        break;
                    /* ommited for programming purposes
                    case '<':
                        bracket = '>';
                        break;
                    */
                    case '"':
                        bracket = '"';
                        break;
                    case '\'':
                        bracket = '\'';
                        break;
                    case '‚':
                        bracket = '‘';
                        break;
                    case '„':
                        bracket = '“';
                        break;
                    case '»':
                        bracket = '«';
                        break;
                    default:
                        return;
                }
                TextBox.SelectedText = e.KeyChar.ToString() + bracket;
                TextBox.Select(TextBox.SelectionStart - 1, 0);
                e.Handled = true;
            }
        }

        public void SyntaxHighlight(object sender, EventArgs e) {
            if (Properties.Settings.Default.IsSyntaxHighlightingEnabled) {
                SyntaxHighlighter.HighlightSyntaxOfRichTextbox(TextBox);
            }
            else {
                TextBox.ForeColor = Themes.DocumentTextColor;
            }
        }
    }
}
