using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteSwag
{
    public static class Themes {
        public enum Theme
        {
            Dark,
            DarkPlus,
            Classic,
            Midnight,
            Classy
        }
        public static Color TopPanelColor { get; set; }
        public static Color TopPanelTextColor { get; set; }
        public static Color BackColor { get; set; }
        public static Color ToolStripMenuItemsColor { get; set; }
        public static Color ToolStripMenuItemsTextColor { get; set; }
        public static Color InfoBarTextColor { get; set; }
        public static Color DocumentColor { get; set; }
        public static Color DocumentTextColor { get; set; }
        public static Color TextColor { get; set; }
        public static Color SyntaxHighlightKeywords { get; set; }
        public static Color SyntaxHighlightQuotes { get; set; }
        public static Color SyntaxHighlightBlocks { get; set; }
        public static void SetThemeColors(Theme theme) {
            switch (theme) {
                case Theme.Dark:
                    TopPanelColor = Color.FromName("Black");
                    TopPanelTextColor = Color.FromName("White");
                    BackColor = Color.FromArgb(64, 64, 64);
                    ToolStripMenuItemsColor = Color.FromArgb(64, 64, 64);
                    ToolStripMenuItemsTextColor = Color.FromName("White");
                    InfoBarTextColor = Color.FromName("White");
                    DocumentColor = Color.FromArgb(64, 64, 64);
                    DocumentTextColor = Color.FromName("White");
                    TextColor = Color.FromName("White");
                    SyntaxHighlightKeywords = Color.FromArgb(100, 150, 255);
                    SyntaxHighlightQuotes = Color.FromArgb(245, 200, 60);
                    SyntaxHighlightBlocks = Color.FromName("Pink");
                    break;
                case Theme.DarkPlus:
                    TopPanelColor = Color.FromName("Black");
                    TopPanelTextColor = Color.FromName("White");
                    BackColor = Color.FromArgb(32, 32, 32);
                    ToolStripMenuItemsColor = Color.FromArgb(32, 32, 32);
                    ToolStripMenuItemsTextColor = Color.FromName("White");
                    InfoBarTextColor = Color.FromName("White");
                    DocumentColor = Color.FromArgb(32, 32, 32);
                    DocumentTextColor = Color.FromName("White");
                    TextColor = Color.FromName("White");
                    SyntaxHighlightKeywords = Color.FromArgb(100, 150, 255);
                    SyntaxHighlightQuotes = Color.FromArgb(245, 200, 60);
                    SyntaxHighlightBlocks = Color.FromName("Pink");
                    break;
                case Theme.Classic:
                    TopPanelColor = Color.FromName("Gray");
                    TopPanelTextColor = Color.FromName("Black");
                    BackColor = Color.FromName("White");
                    ToolStripMenuItemsColor = Color.FromName("White");
                    ToolStripMenuItemsTextColor = Color.FromName("Black");
                    InfoBarTextColor = Color.FromName("Black");
                    DocumentColor = Color.FromName("White");
                    DocumentTextColor = Color.FromName("Black");
                    TextColor = Color.FromName("Black");
                    SyntaxHighlightKeywords = Color.FromArgb(100, 150, 255);
                    SyntaxHighlightQuotes = Color.FromArgb(245, 200, 60);
                    SyntaxHighlightBlocks = Color.FromArgb(200, 66, 120);
                    break;
                case Theme.Midnight:
                    TopPanelColor = Color.FromArgb(0, 0, 20);
                    TopPanelTextColor = Color.FromName("White");
                    BackColor = Color.FromArgb(0, 0, 50);
                    ToolStripMenuItemsColor = Color.FromArgb(0, 0, 50);
                    ToolStripMenuItemsTextColor = Color.FromName("White");
                    InfoBarTextColor = Color.FromName("White");
                    DocumentColor = Color.FromArgb(0, 0, 50);
                    DocumentTextColor = Color.FromName("White");
                    TextColor = Color.FromName("White");
                    SyntaxHighlightKeywords = Color.FromArgb(50, 230, 255);
                    SyntaxHighlightQuotes = Color.FromArgb(245, 200, 60);
                    SyntaxHighlightBlocks = Color.FromName("Pink");
                    break;
                case Theme.Classy:
                    TopPanelColor = Color.FromArgb(102, 0, 0);
                    TopPanelTextColor = Color.FromName("Black");
                    BackColor = Color.FromArgb(179, 0, 0);
                    ToolStripMenuItemsColor = Color.FromArgb(179, 0, 0);
                    ToolStripMenuItemsTextColor = Color.FromName("Black");
                    InfoBarTextColor = Color.FromName("Black");
                    DocumentColor = Color.FromArgb(179, 0, 0);
                    DocumentTextColor = Color.FromName("Black");
                    TextColor = Color.FromName("Black");
                    SyntaxHighlightKeywords = Color.FromArgb(50, 230, 255);
                    SyntaxHighlightQuotes = Color.FromArgb(245, 200, 60);
                    SyntaxHighlightBlocks = Color.FromName("Pink");
                    break;
            }
        }
    }
}
