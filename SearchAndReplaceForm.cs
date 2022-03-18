using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Notepad__
{
    public partial class SearchAndReplaceForm : Form
    {
        private int SearchIndex { get; set; } = 0;
        private string SearchedWord { get; set; }
        public SearchAndReplaceForm()
        {
            InitializeComponent();
        }

        #region WinAPI calls for custom border
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void MakeControlDraggableEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
        #endregion
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            RichTextBox textBox = MainForm.instance.DocumentManager.ActiveDocument.TextBox;
            string wordToFind = textBoxFind.Text;

            if (wordToFind == "")
            {
                SystemSounds.Hand.Play();
                return;
            }

            if (wordToFind != SearchedWord) {
                SearchIndex = 0;
            }

            int position = textBox.Text.IndexOf(wordToFind, SearchIndex);
            if (position != -1)
            {
                textBox.SelectionStart = position;
                textBox.SelectionLength = textBoxFind.Text.Length;
                SearchIndex = position + textBoxFind.Text.Length;
                MainForm.instance.Activate();
                SearchedWord = wordToFind;
            }
            else {
                SearchIndex = 0;
                SystemSounds.Hand.Play();
            }
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            RichTextBox textBox = MainForm.instance.DocumentManager.ActiveDocument.TextBox;
            string wordToFind = textBoxFind.Text;
            string wordToReplace = textBoxReplaceWith.Text;

            if (wordToFind== "") {
                SystemSounds.Hand.Play();
                return;
            }

            string newText = textBox.Text.Replace(wordToFind, wordToReplace);
            if (newText == textBox.Text) {
                SystemSounds.Hand.Play();
            }
            else {
                textBox.Text = newText;
            }
        }
    }
}
