using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchemeEditorUI.Services
{
    public enum DialogResult
    {
        Yes,
        No,
        Cancel
    }

    public interface IMessageBoxService
    {
        void ShowInfo(string message);

        bool ShowYesNo(string message);

        DialogResult ShowYesNoCancel(string message);
    }
}
