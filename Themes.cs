﻿using System;
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
            Classic,
            Midnight
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
                    break;
                case Theme.Midnight:
                    TopPanelColor = Color.FromArgb(60, 20, 200);
                    TopPanelTextColor = Color.FromName("White");
                    BackColor = Color.FromArgb(60, 20, 200);
                    ToolStripMenuItemsColor = Color.FromArgb(60, 20, 190);
                    ToolStripMenuItemsTextColor = Color.FromName("White");
                    InfoBarTextColor = Color.FromName("White");
                    DocumentColor = Color.FromArgb(60, 20, 200);
                    DocumentTextColor = Color.FromName("White");
                    TextColor = Color.FromName("White");
                    break;
            }
        }
    }
}
