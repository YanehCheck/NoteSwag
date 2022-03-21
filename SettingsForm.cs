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
        public SettingsForm()
        {
            InitializeComponent();
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
            Settings.Theme = (Themes.Theme) Enum.Parse(typeof(Themes.Theme), comboBoxTheme.Text);
            Themes.SetThemeColors(Settings.Theme);
            MainForm.instance.ApplyThemeToForm();
            MainForm.AboutBox.ApplyThemeToForm();
            MainForm.SettingsForm.ApplyThemeToForm();
            SettingsSerializer.Serialize();
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
            comboBoxTheme.Text = Enum.GetName(typeof(Themes.Theme), Settings.Theme);
        }

        private void checkBoxBracketMatching_CheckedChanged(object sender, EventArgs e)
        {
            Settings.IsBracketMatchingEnabled = checkBoxBracketMatching.Checked;
            SettingsSerializer.Serialize();
        }
    }
}
