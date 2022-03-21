using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NoteSwag
{
    public partial class MainForm : Form
    {
        public DocumentManager DocumentManager { get; set; }
        public static MainForm instance { get; set; }

        public static SearchAndReplaceForm SearchAndReplaceForm { get; } = new SearchAndReplaceForm();
        public static AboutBox AboutBox { get; } = new AboutBox();
        public static SettingsForm SettingsForm { get; } = new SettingsForm();
        public MainForm()
        {
            instance = this;
            InitializeComponent();
            DocumentManager = DocumentManager.instance;
            SettingsSerializer.Deserialize();
            ApplyThemeToForm();
            CheckForCommandLineArgumentFiles();
            DocumentManager.ApplySettingsOnActiveDocument();
            //SetAssociation_User("txt", Application.ExecutablePath, Path.GetFileName(Application.ExecutablePath));
        }

        #region WinAPI calls for custom border
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
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
            else
            {
                panelTabControlDocuments.Size = new Size(Width - 10, Height - 65);
            }
        }

        #endregion

        #region File Handling
        public static void SetAssociation_User(string Extension, string OpenWith, string ExecutableName)
        {
            try
            {
                using (RegistryKey User_Classes = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\", true))
                using (RegistryKey User_Ext = User_Classes.CreateSubKey("." + Extension))
                using (RegistryKey User_AutoFile = User_Classes.CreateSubKey(Extension + "_auto_file"))
                using (RegistryKey User_AutoFile_Command = User_AutoFile.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command"))
                using (RegistryKey ApplicationAssociationToasts = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\ApplicationAssociationToasts\\", true))
                using (RegistryKey User_Classes_Applications = User_Classes.CreateSubKey("Applications"))
                using (RegistryKey User_Classes_Applications_Exe = User_Classes_Applications.CreateSubKey(ExecutableName))
                using (RegistryKey User_Application_Command = User_Classes_Applications_Exe.CreateSubKey("shell").CreateSubKey("open").CreateSubKey("command"))
                using (RegistryKey User_Explorer = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\." + Extension))
                using (RegistryKey User_Choice = User_Explorer.OpenSubKey("UserChoice"))
                {
                    User_Ext.SetValue("", Extension + "_auto_file", RegistryValueKind.String);
                    User_Classes.SetValue("", Extension + "_auto_file", RegistryValueKind.String);
                    User_Classes.CreateSubKey(Extension + "_auto_file");
                    User_AutoFile_Command.SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
                    ApplicationAssociationToasts.SetValue(Extension + "_auto_file_." + Extension, 0);
                    ApplicationAssociationToasts.SetValue(@"Applications\" + ExecutableName + "_." + Extension, 0);
                    User_Application_Command.SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
                    User_Explorer.CreateSubKey("OpenWithList").SetValue("a", ExecutableName);
                    User_Explorer.CreateSubKey("OpenWithProgids").SetValue(Extension + "_auto_file", "0");
                    if (User_Choice != null) User_Explorer.DeleteSubKey("UserChoice");
                    User_Explorer.CreateSubKey("UserChoice").SetValue("ProgId", @"Applications\" + ExecutableName);
                }
                SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
            }
            catch (Exception except)
            {
            }
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        private void CheckForCommandLineArgumentFiles()
        {
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                for (int i = 1; i < Environment.GetCommandLineArgs().Length; i++)
                {
                    var path = Environment.GetCommandLineArgs()[i];
                    if (File.Exists(path))
                    {
                        DocumentManager.CreateNewDocument(path);
                    }
                }
            }
            else
            {
                DocumentManager.CreateNewDocument();
            }
            DocumentManager.ActiveDocument = DocumentManager.Documents.ElementAt(0);
        }
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
            else
            {
                SaveAs();
            }
        }

        private void SaveAs()
        {
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
            SettingsSerializer.Serialize();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Font = new Font(Settings.Font.FontFamily, Settings.Font.Size - 1, Settings.Font.Style, Settings.Font.Unit, Settings.Font.GdiCharSet, Settings.Font.GdiVerticalFont);
            DocumentManager.ApplySettingsOnActiveDocument();
            SettingsSerializer.Serialize();
        }
        #endregion

        #region Sidebar
        private void infoBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.IsInfoBarVisible = !Settings.IsInfoBarVisible;
            infoBarToolStripMenuItem.Image = Settings.IsInfoBarVisible ? Properties.Resources.checkmark : null;
            DocumentManager.ApplySettingsOnActiveDocument();
            SettingsSerializer.Serialize();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.IsWordWrapEnabled = !Settings.IsWordWrapEnabled;
            wordWrapToolStripMenuItem.Image = Settings.IsWordWrapEnabled ? Properties.Resources.checkmark : null;
            DocumentManager.ApplySettingsOnActiveDocument();
            SettingsSerializer.Serialize();
        }
        public void ChangeLineAndCharacterNumber(int lineNumber, int charNumber)
        {
            labelLine.Text = lineNumber.ToString();
            labelChar.Text = charNumber.ToString();
        }
        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox.TopMost = true;
            AboutBox.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm.TopMost = true;
            SettingsForm.Show();
        }

        public void ApplyThemeToForm()
        {
            labelTitle.ForeColor = Themes.TopPanelTextColor;
            panelControls.BackColor = Themes.TopPanelColor;
            BackColor = Themes.BackColor;
            toolBarStrip.BackColor = Themes.ToolStripMenuItemsColor;
            foreach (Label item in panelInfoBar.Controls)
            {
                if (item.GetType() == typeof(Label))
                {
                    var label = (Label)item;
                    label.ForeColor = Themes.InfoBarTextColor;
                }
            }
            foreach (Document document in DocumentManager.Documents)
            {
                document.TextBox.ForeColor = Themes.DocumentTextColor;
                document.TextBox.BackColor = Themes.DocumentColor;
            }

            List<ToolStripItem> allItems = new List<ToolStripItem>();
            foreach (ToolStripItem toolItem in toolBarStrip.Items)
            {
                allItems.Add(toolItem);
                allItems.AddRange(GetItems(toolItem));
            }

            foreach (ToolStripItem toolItem in allItems)
            {
                toolItem.ForeColor = Themes.ToolStripMenuItemsTextColor;
                toolItem.BackColor = Themes.ToolStripMenuItemsColor;
            }
            IEnumerable<ToolStripItem> GetItems(ToolStripItem item)
            {
                foreach (ToolStripItem tsi in (item as ToolStripMenuItem).DropDownItems)
                {
                    if (tsi is ToolStripMenuItem)
                    {
                        if ((tsi as ToolStripMenuItem).HasDropDownItems)
                        {
                            foreach (ToolStripItem subItem in GetItems((tsi as ToolStripMenuItem)))
                                yield return subItem;
                        }
                        yield return (tsi as ToolStripMenuItem);
                    }
                    else if (tsi is ToolStripSeparator)
                    {
                        yield return (tsi as ToolStripSeparator);
                    }
                }

            }

        }

        private void searchWithGoogleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DocumentManager.ActiveDocument.TextBox.SelectedText)) {
                Process.Start($"http://google.com/search?q={DocumentManager.ActiveDocument.TextBox.SelectedText}");
            }
            else {
                SystemSounds.Exclamation.Play();
            }
        }
    }
}
