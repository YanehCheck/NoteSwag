using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteSwag
{
    class SyntaxHighlighter
    {
        private static string[] Blocks = new string[]{"foreach", "goto", "else", "finally", "default","try", "catch", "for","while","do", "if", "switch", "case", "break", "continue", "return"};
        private static string[] Keywords = new string[] {"abstract", "as", "base", "bool", "byte", "char", "checked", "class", "const", "decimal", "delegate", "double", "enum", "event", "explicit", "extern", "false", "fixed", "float", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "this", "throw", "true", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile" };
        private static string[] Quotes = new string[] { "\"", "'" };
        public static void HighlightSyntaxOfRichTextbox(RichTextBox richTextBox)
        {
            DefaultColor(richTextBox);
            Highlight(richTextBox, Blocks, Themes.SyntaxHighlightBlocks);
            Highlight(richTextBox, Keywords, Themes.SyntaxHighlightKeywords);
            HighlightEverythingBetweenTwoQuotes(richTextBox, Quotes, Themes.SyntaxHighlightQuotes);
        }

        private static void DefaultColor(RichTextBox richTextBox)
        {
            int oldPosition = richTextBox.SelectionStart;

            richTextBox.SelectionStart = 0;
            richTextBox.SelectionLength = richTextBox.Text.Length;
            richTextBox.SelectionColor = Themes.DocumentTextColor;

            richTextBox.SelectionStart = oldPosition;
        }

        private static void Highlight(RichTextBox richTextBox, string[] words, Color color) {
            int oldPosition = richTextBox.SelectionStart;
            foreach (var word in words) {
                List<int> occurenceIndexes = FindAllOccurencesOfWord(richTextBox, word);
                foreach (var index in occurenceIndexes)
                {
                    richTextBox.SelectionStart = index;
                    richTextBox.SelectionLength = word.Length;
                    richTextBox.SelectionColor = color;
                }
            }
            richTextBox.SelectionStart = oldPosition;
            richTextBox.SelectionLength = 0;
            richTextBox.SelectionColor = Themes.DocumentTextColor;
        }

        private static void HighlightEverythingBetweenTwoQuotes(RichTextBox richTextBox, string[] words, Color color) {
            int oldPosition = richTextBox.SelectionStart;
            foreach (var word in words)
            {
                List<int> occurenceIndexes = FindAllOccurencesOfWord(richTextBox, word);
                for (int i = 0; i < occurenceIndexes.Count; i += 2)
                {
                    if (i + 1 >= occurenceIndexes.Count) {
                        richTextBox.SelectionStart = occurenceIndexes[i];
                        richTextBox.SelectionLength = 1;
                        richTextBox.SelectionColor = color;
                    }
                    else {
                        richTextBox.SelectionStart = occurenceIndexes[i];
                        richTextBox.SelectionLength = occurenceIndexes[i + 1] - occurenceIndexes[i] + 1;
                        richTextBox.SelectionColor = color;
                    }
                }
            }
            richTextBox.SelectionStart = oldPosition;
            richTextBox.SelectionLength = 0;
            richTextBox.SelectionColor = Themes.DocumentTextColor;
        }

        private static List<int> FindAllOccurencesOfWord(RichTextBox richTextBox, string word) {
            int lastIndex = 0;
            List<int> occurences = new List<int>();
            while (true)
            {
                int index;
                if (occurences.Count != 0) {
                    index = richTextBox.Text.IndexOf(word, lastIndex + 1);
                }
                else {
                    index = richTextBox.Text.IndexOf(word);
                }

                if (index == -1) {
                    return occurences;
                }
                else {
                    if (index != 0 && word == "\"" || word == "'") {
                        if (richTextBox.Text[index - 1] == '\\')
                        {
                            lastIndex = index;
                            continue;
                        }
                        else {
                            occurences.Add(index);
                            lastIndex = index;
                            continue;
                        }
                    }
                    occurences.Add(index);
                    lastIndex = index;
                }
            }
        }
    }
}
