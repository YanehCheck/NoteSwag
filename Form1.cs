using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad__
{
    public partial class MainForm : Form
    {
        public DocumentManager DocumentManager;
        public static MainForm instance;

        public static SearchAndReplaceForm SearchAndReplaceForm = new SearchAndReplaceForm();
        public static AboutBox AboutBox = new AboutBox();
        public MainForm()
        {
            instance = this;
            InitializeComponent();
            DocumentManager = DocumentManager.instance;
            DocumentManager.CreateNewDocument();
            DocumentManager.ActiveDocument = DocumentManager.Documents.ElementAt(0);
        }

        #region WinAPI calls for custom border
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // Call do WinAPI, aby jsme mohli přesouvat form pomocí panelu
        private void MakeControlDraggableEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        // A ať ji můžeme zvětšovat (tohle už jsem ukradl)
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.X >= this.ClientSize.Width - 16 && pos.Y >= this.ClientSize.Height - 16)
                {
                    m.Result = (IntPtr)17; 
                    return;
                }
            }
            base.WndProc(ref m);
        }

        // Dock fillu u panelu s otevřenými dokumenty se to co dělám nelíbí, takže musím takhle
        public void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (Settings.IsInfoBarVisible)
            {
                panelTabControlDocuments.Size = new Size(Width - 135, Height - 65);
            }
            else {
                panelTabControlDocuments.Size = new Size(Width - 10, Height - 65);
            }
        }

        #endregion

        #region File Handling
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentManager.CreateNewDocument();
            DocumentManager.ActiveDocument = DocumentManager.Documents.ElementAt(tabControlDocuments.SelectedIndex);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DocumentManager.CreateNewDocument(openFileDialog.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckIfFileHasDirectoryAndSave();
        }

        private void CheckIfFileHasDirectoryAndSave()
        {
            if (tabControlDocuments.SelectedIndex == -1) { return; }
            if (DocumentManager.ActiveDocument.Directory != null)
            {
                File.WriteAllText(DocumentManager.ActiveDocument.Directory, DocumentManager.ActiveDocument.TextBox.Text);
            }
            else{
                SaveAs();
            }
        }

        private void SaveAs() {
            if (tabControlDocuments.SelectedIndex == -1) { return; }
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, DocumentManager.ActiveDocument.TextBox.Text);
                DocumentManager.ActiveDocument.TabPage.Text = Path.GetFileName(saveFileDialog.FileName);
                DocumentManager.ActiveDocument.Directory = saveFileDialog.FileName;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void tabControlDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlDocuments.SelectedIndex == -1) { return; }
            DocumentManager.ActiveDocument = DocumentManager.Documents.ElementAt(tabControlDocuments.SelectedIndex);
            DocumentManager.ApplySettingsOnActiveDocument();
            DocumentManager.ActiveDocument.UpdateSideBarInfo(this, EventArgs.Empty);
        }

        private void closeDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var isSaved = DocumentManager.CheckIfActiveDocumentIsSaved();
            if (isSaved == DocumentStatus.Unsaved || isSaved == DocumentStatus.UnsavedWithNoPath)
            {
                var result = MessageBox.Show("You have unsaved changes.\nDo you wish to save your document?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes) { CheckIfFileHasDirectoryAndSave(); }
                if (result == DialogResult.No) { }
                if (result == DialogResult.Cancel) { return; }
            }
            DocumentManager.DeleteActiveDocument();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument p = new PrintDocument();
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(DocumentManager.ActiveDocument.TextBox.Text, new Font("Times New Roman", 12), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

            };
            try
            {
                p.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while printing.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Any unsaved changes will be lost.\nDo you wish to exit the program?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region ProgramControls
        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void buttonMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Text Controls

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentManager.ActiveDocument.TextBox.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentManager.ActiveDocument.TextBox.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentManager.ActiveDocument.TextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentManager.ActiveDocument.TextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentManager.ActiveDocument.TextBox.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentManager.ActiveDocument.TextBox.SelectAll();
        }

        private void timeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentManager.ActiveDocument.TextBox.SelectedText = DateTime.Now.ToString();
        }

        private void searchAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchAndReplaceForm.TopMost = true;
            SearchAndReplaceForm.Show();
        }
        #endregion

        #region View
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Font = new Font(Settings.Font.FontFamily, Settings.Font.Size + 1, Settings.Font.Style, Settings.Font.Unit, Settings.Font.GdiCharSet, Settings.Font.GdiVerticalFont);
            DocumentManager.ApplySettingsOnActiveDocument();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Font = new Font(Settings.Font.FontFamily, Settings.Font.Size - 1, Settings.Font.Style, Settings.Font.Unit, Settings.Font.GdiCharSet, Settings.Font.GdiVerticalFont);
            DocumentManager.ApplySettingsOnActiveDocument();
        }
        #endregion

        #region Sidebar
        private void infoBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.IsInfoBarVisible = !Settings.IsInfoBarVisible;
            infoBarToolStripMenuItem.Image = Settings.IsInfoBarVisible ? Properties.Resources.checkmark : null;
            DocumentManager.ApplySettingsOnActiveDocument();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.IsWordWrapEnabled = !Settings.IsWordWrapEnabled;
            wordWrapToolStripMenuItem.Image = Settings.IsWordWrapEnabled ? Properties.Resources.checkmark : null;
            DocumentManager.ApplySettingsOnActiveDocument();
        }
        public void ChangeLineAndCharacterNumber(int lineNumber, int charNumber) {
            labelLine.Text = lineNumber.ToString();
            labelChar.Text = charNumber.ToString();
        }
        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox.TopMost = true;
            AboutBox.Show();
        }
    }
}
