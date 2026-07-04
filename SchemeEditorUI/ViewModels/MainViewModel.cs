using SchemeModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchemeEditorUI.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ShapeViewModel<Shape>> _dataSource;
        public ICommand AddShapeCommand { get; }

        public ObservableCollection<ShapeViewModel<Shape>> DataSource => _dataSource;

        public MainViewModel(Scheme scheme)
        {
            scheme.AddShape(new Square("A"));
            scheme.AddShape(new Square("B"));

            AddShapeCommand = new RelayCommand((x) => 
            { 
                var item = new Square("Aaa");
                scheme.AddShape(item);
                _dataSource.Add(new ShapeViewModel<Shape>(item, scheme)); 
            }, 
            null);

            _dataSource = new ObservableCollection<ShapeViewModel<Shape>>(scheme.Shapes.Select(s => new ShapeViewModel<Shape>((Square)s,scheme)));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
