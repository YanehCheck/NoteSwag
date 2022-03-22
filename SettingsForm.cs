using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteSwag
{
    public partial class SettingsForm : Form
    {
        private FontConverter fontConvertor { get; set; } = new FontConverter();
        public static SettingsForm instance { get; set; }
        public SettingsForm()
        {
            InitializeComponent();
            AddFontsToComboBox();
            instance = this;
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
        #endregion

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void comboBoxTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Theme = (int) Enum.Parse(typeof(Themes.Theme), comboBoxTheme.Text);
            Themes.SetThemeColors((Themes.Theme) Properties.Settings.Default.Theme);
            MainForm.instance.ApplyThemeToForm();
            AboutBox.instance.ApplyThemeToForm();
            SettingsForm.instance.ApplyThemeToForm();
            Properties.Settings.Default.Save();
        }

        public void ApplyThemeToForm()
        {
            labelTitle.ForeColor = Themes.TopPanelTextColor;
            panelControls.BackColor = Themes.TopPanelColor;
            BackColor = Themes.BackColor;
            foreach (var item in Controls)
            {
                if (item.GetType() == typeof(Label))
                {
                    var label = (Label)item;
                    label.ForeColor = Themes.TextColor;
                }
            }
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            comboBoxTheme.Text = Enum.GetName(typeof(Themes.Theme), (Themes.Theme) Properties.Settings.Default.Theme);
        }

        private void checkBoxBracketMatching_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsBracketMatchingEnabled = checkBoxBracketMatching.Checked;
            Properties.Settings.Default.Save();
        }

        private void AddFontsToComboBox() {
            foreach (var font in FontFamily.Families) {
                comboBoxFont.Items.Add(font.Name);
            }
        }

        private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Font = new Font(((Font) fontConvertor.ConvertFromString(comboBoxFont.Text)).FontFamily, Properties.Settings.Default.Font.Size, Properties.Settings.Default.Font.Style, Properties.Settings.Default.Font.Unit, Properties.Settings.Default.Font.GdiCharSet, Properties.Settings.Default.Font.GdiVerticalFont);
            MainForm.instance.DocumentManager.ApplySettingsOnActiveDocument();
            Properties.Settings.Default.Save();
        }

        private void SettingsForm_Activated(object sender, EventArgs e)
        {
            comboBoxFont.Text = Properties.Settings.Default.Font.FontFamily.Name;
        }
    }
}
