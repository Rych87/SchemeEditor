using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemeEditorUI.Services
{
    public class JsonFileDialogService : IFileDialogService
    {
        public string? LoadFile()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                DefaultExt = ".json"
            };

            return dialog.ShowDialog() == true
                ? dialog.FileName
                : null;
        }

        public string? SaveFile()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                DefaultExt = ".json",
                AddExtension = true
            };

            return dialog.ShowDialog() == true
                ? dialog.FileName
                : null;
        }
    }
}
