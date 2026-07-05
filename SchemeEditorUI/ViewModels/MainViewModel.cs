using SchemeModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace SchemeEditorUI.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ShapeViewModel<Shape>> _dataSource;
        private bool _isPanelVisible;
        public ICommand AddShapeCommand { get; }
        public ICommand ShowAddPanelCommand { get; }

        public ObservableCollection<ShapeViewModel<Shape>> DataSource => _dataSource;

        public MainViewModel(Scheme scheme)
        {
            AddShapeCommand = new RelayCommand((x) => 
            {
                try
                {
                    var item = Activator.CreateInstance(x as Type, new Object[] { "Новая фигура" }) as Shape;
                    scheme.AddShape(item);
                    _dataSource.Add(new ShapeViewModel<Shape>(item, scheme));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            ShowAddPanelCommand = new RelayCommand((x) => IsPanelVisible = true);

            _dataSource = new ObservableCollection<ShapeViewModel<Shape>>(scheme.Shapes.Select(s => new ShapeViewModel<Shape>((Square)s,scheme)));
        }

        public bool IsPanelVisible
        {
            get => _isPanelVisible;
            set
            {
                _isPanelVisible = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
