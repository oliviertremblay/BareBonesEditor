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
    class MainViewModel : INotifyPropertyChanged
    {
        public static string FilePath
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BareBonesEditor", "exam.cs"); ; }
        }

        private string _saveMessage = "";
        public string SaveMessage
        {
            get => _saveMessage;
            set
            {
                _saveMessage = value;
                OnPropertyChanged(nameof(SaveMessage));
            }
        }

        private readonly TextEditor _editor;

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand SaveCommand { get; set; }

        public MainViewModel(TextEditor editor)
        {
            _editor = editor;
            SaveCommand = new RelayCommand(SaveFile);
            editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
            LoadExamFile();
        }

        private void LoadExamFile()
        {
            var directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(FilePath))
            {
                _editor.Load(FilePath);
            }
            else
            {
                File.WriteAllText(FilePath, "");
            }
        }

        private void SaveFile()
        {
            File.WriteAllText(FilePath, _editor.Text);
            SaveMessage = "Fichier sauvegardé!";
            ShowTemporarySaveMessage();

        }

        private void ShowTemporarySaveMessage()
        {
            var timer = new System.Timers.Timer(3000);
            timer.Elapsed += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();
                SaveMessage = "";
            };
            timer.AutoReset = false;
            timer.Start();
        }
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
