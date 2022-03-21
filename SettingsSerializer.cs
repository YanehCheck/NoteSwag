using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSwag
{
    static class SettingsSerializer
    {

        public static void Serialize() {
            Properties.Settings.Default.Font = Settings.Font;
            Properties.Settings.Default.IsInfoBarVisible = Settings.IsInfoBarVisible;
            Properties.Settings.Default.IsWordWrapEnabled = Settings.IsWordWrapEnabled;
            Properties.Settings.Default.Theme = (int) Settings.Theme;
            Properties.Settings.Default.IsBracketMatchingEnabled = Settings.IsBracketMatchingEnabled;

            Properties.Settings.Default.Save();
        }
        public static void Deserialize()
        {
            Settings.Font = Properties.Settings.Default.Font;
            Settings.IsInfoBarVisible = Properties.Settings.Default.IsInfoBarVisible;
            Settings.IsWordWrapEnabled = Properties.Settings.Default.IsWordWrapEnabled;
            Settings.Theme = (Themes.Theme) Properties.Settings.Default.Theme;
            Settings.IsBracketMatchingEnabled = Properties.Settings.Default.IsBracketMatchingEnabled;
            Themes.SetThemeColors(Settings.Theme);

            MainForm.instance.infoBarToolStripMenuItem.Image = Settings.IsInfoBarVisible ? Properties.Resources.checkmark : null;
            MainForm.instance.wordWrapToolStripMenuItem.Image = Settings.IsWordWrapEnabled ? Properties.Resources.checkmark : null;
            MainForm.SettingsForm.checkBoxBracketMatching.Checked = Settings.IsBracketMatchingEnabled;
        }
    }
}
