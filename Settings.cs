using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSwag
{
    class Settings
    {
        public static Font Font { get; set; } = new Font(FontFamily.GenericSansSerif, 10);
        public static bool IsInfoBarVisible { get; set; } = true;
        public static bool IsWordWrapEnabled { get; set; } = false;
        public static Themes.Theme Theme { get; set; } = Themes.Theme.Dark;

        public static bool IsBracketMatchingEnabled = false;
    }
}
