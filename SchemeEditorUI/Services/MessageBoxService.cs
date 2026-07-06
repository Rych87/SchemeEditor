using System.Windows;

namespace SchemeEditorUI.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowInfo(string message)
        {
            MessageBox.Show(message);
        }

        public bool ShowYesNo(string message)
        {
            return MessageBox.Show(message, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes ? true : false;
        }

        public DialogResult ShowYesNoCancel(string message)
        {
            switch (MessageBox.Show(message, "", MessageBoxButton.YesNoCancel))
            {
                case MessageBoxResult.Yes: return DialogResult.Yes;
                case MessageBoxResult.No: return DialogResult.No;
                default: return DialogResult.Cancel;
            }
        }
    }
}
