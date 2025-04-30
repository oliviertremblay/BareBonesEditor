using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using BareBonesEditor.Utils;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;

namespace BareBonesEditor.ViewModel
{
    class MainViewModel
    {
        public static string FilePath
        {
            get { return "exam.cs"; }
        }

        private readonly TextEditor _editor;

        public MainViewModel(TextEditor editor)
        {
            _editor = editor;
            SaveCommand = new RelayCommand(SaveFile);

            editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");

            if (File.Exists(FilePath))
            {
                editor.Load(FilePath);
            }
        }

        public ICommand SaveCommand { get; set; }

        private void SaveFile()
        {
            File.WriteAllText(FilePath, _editor.Text);
        }
    }
}
