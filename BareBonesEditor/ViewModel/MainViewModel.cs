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
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BareBonesEditor", "exam.cs"); ; }
        }

        private readonly TextEditor _editor;

        public MainViewModel(TextEditor editor)
        {
            _editor = editor;
            SaveCommand = new RelayCommand(SaveFile);

            editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");

            var directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(FilePath))
            {
                editor.Load(FilePath);
            }
            else
            {
                File.WriteAllText(FilePath, ""); 
            }
        }

        public ICommand SaveCommand { get; set; }

        private void SaveFile()
        {
            File.WriteAllText(FilePath, _editor.Text);
        }
    }
}
