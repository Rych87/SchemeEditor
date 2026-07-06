using SchemeEditorUI.Services;
using SchemeEditorUI.ViewModels;
using SchemeModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchemeEditorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ViewModels.MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
                e.Cancel = !mainViewModel.CheckModifiedAndContinue();
        }
    }
}