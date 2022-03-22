using ByteSizeLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSwag
{
    public class DocumentManager
    {
        public static DocumentManager instance { get; private set; } = new DocumentManager();
        public Document ActiveDocument { get; set; }
        public List<Document> Documents { get; set; } = new List<Document>();

        private DocumentManager() {
            if (instance == null)
            {
                instance = this;
            }
            else {
                throw new Exception("Tried to initialize DocumentManager instance twice!");
            }
        }

        public void CreateNewDocument() {
            Documents.Add(new Document());
        }
        public void CreateNewDocument(string path)
        {
            Documents.Add(new Document(path));
        }

        public void DeleteActiveDocument() {
            Documents.Remove(ActiveDocument);
            MainForm.instance.tabControlDocuments.TabPages.Remove(ActiveDocument.TabPage);
        }

        public DocumentStatus CheckIfActiveDocumentIsSaved() {
            if (!File.Exists(ActiveDocument.Directory))
            {
                return DocumentStatus.UnsavedWithNoPath;
            }
            else if (File.ReadAllText(ActiveDocument.Directory) == ActiveDocument.TextBox.Text)
            {
                return DocumentStatus.Saved;
            }
            else 
            {
                return DocumentStatus.Unsaved;
            }
        }

        public void ApplySettingsOnActiveDocument() {
            ActiveDocument.TextBox.Font = Properties.Settings.Default.Font;
            ActiveDocument.TextBox.WordWrap = Properties.Settings.Default.IsWordWrapEnabled;
            MainForm.instance.panelInfoBar.Visible = Properties.Settings.Default.IsInfoBarVisible;
            MainForm.instance.panelTabControlDocuments.Location = Properties.Settings.Default.IsInfoBarVisible ? new Point(130, 57) : new Point(5, 57);
            MainForm.instance.MainForm_SizeChanged(this, EventArgs.Empty);
        }

        public void SetActiveDocumentFileInfo() {
            if (ActiveDocument.Directory != null)
            {
                long bytes;
                try {
                    bytes = new System.IO.FileInfo(ActiveDocument.Directory).Length;
                }
                catch {
                    MainForm.instance.labelFileSize.Text = "Not found";
                    return;
                }

                MainForm.instance.labelFileSize.Text = ByteSize.FromBytes(bytes).LargestWholeNumberDecimalValue.ToString() + ByteSize.FromBytes(bytes).LargestWholeNumberDecimalSymbol.ToString();
                MainForm.instance.labelFileType.Text = Path.GetExtension(ActiveDocument.Directory).Substring(1).ToUpper();
                MainForm.instance.labelEncoding.Text = GetEncoding().WebName;
            }
            else {
                MainForm.instance.labelFileSize.Text = "-";
                MainForm.instance.labelFileType.Text = "-";
                MainForm.instance.labelEncoding.Text = "-";
            }
            
        }

        private Encoding GetEncoding()
        {
            using (var reader = new StreamReader(ActiveDocument.Directory, Encoding.Default, true))
            {
                if (reader.Peek() >= 0) 
                    reader.Read();
                return reader.CurrentEncoding;
            }
        }

    }
}
