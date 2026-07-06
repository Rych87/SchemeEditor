using SchemeEditorUI.Services;
using SchemeModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SchemeEditorUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ShapeViewModel<Shape>> _dataSource;
        private IFileDialogService _fileDialogService;
        private IMessageBoxService _messageBoxService;
        private SchemeRepositoryJson _schemeRepositoryJson;
        private bool _isPanelVisible;
        private Scheme _scheme;
        private bool _isModified;

        private string? CurrentFilePath { get; set; } = null;
        private bool IsModified 
        { 
            get => (Scheme?.Shapes.Any() ?? false) && _isModified; 
            set => _isModified = value; 
        }

        private Scheme Scheme
        {
            get
            {
                if(_scheme == null) 
                    Scheme = new Scheme();
                return _scheme;
            }
            set
            {
                _scheme = value;
                var items = _scheme.Shapes.Select(s => new ShapeViewModel<Shape>(s, _scheme, _messageBoxService)).ToArray();
                foreach(var item in items) item.PropertyChanged += ItemViewModel_PropertyChanged;
                DataSource = new ObservableCollection<ShapeViewModel<Shape>>(items);
                OnPropertyChanged();
            }
        }

        public ICommand AddShapeCommand { get; }
        public ICommand ShowAddPanelCommand { get; }
        public ICommand NewFileCommand { get; }
        public ICommand OpenFileCommand {  get; }
        public ICommand SaveFileCommand { get; }
        public ICommand RemoveShapeCommand { get; }

        public ObservableCollection<ShapeViewModel<Shape>> DataSource
        {
            get => _dataSource;
            set
            {
                _dataSource = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IMessageBoxService messageBoxService, IFileDialogService fileDialogService, SchemeRepositoryJson repository)
        {
            _messageBoxService = messageBoxService;
            _fileDialogService = fileDialogService;
            _schemeRepositoryJson = repository;

            AddShapeCommand = new RelayCommand((x) =>
            {
                try
                {
                    var shape = Activator.CreateInstance(x as Type, new Object[] { GetNewName() }) as Shape;
                    Scheme.AddShape(shape);
                    var itemViewModel = new ShapeViewModel<Shape>(shape, Scheme, _messageBoxService);
                    itemViewModel.PropertyChanged += ItemViewModel_PropertyChanged;
                    DataSource.Add(itemViewModel);
                    IsModified = true;
                }
                catch (Exception ex) 
                { 
                    _messageBoxService.ShowInfo(ex.Message); 
                }
            });

            RemoveShapeCommand = new RelayCommand((x) =>
            {
                var item = x as ShapeViewModel<Shape>;
                Scheme.RemoveShape(item.Shape);
                DataSource.Remove(item);
                IsModified = true;
            });

            NewFileCommand = new RelayCommand((x) =>
            {
                if (CheckModifiedAndContinue())
                    Scheme = new Scheme();
            });

            ShowAddPanelCommand = new RelayCommand((x) => IsPanelVisible = true);

            OpenFileCommand = new RelayCommand((x) =>
            {
                if (CheckModifiedAndContinue())
                {
                    string path = _fileDialogService.LoadFile();
                    if (path != null)
                    {
                        try
                        {
                            CurrentFilePath = path;
                            Scheme = _schemeRepositoryJson.LoadScheme(CurrentFilePath);
                            IsModified = false;
                        }
                        catch (Exception ex)
                        {
                            _messageBoxService.ShowInfo("Не могу открыть файл. Неверный формат или файл повреждён");
                        }
                    }
                }
            });

            SaveFileCommand = new RelayCommand((x) =>
            {
                if (!IsModified) return;

                if (CurrentFilePath == null)
                {
                    string path = _fileDialogService.SaveFile();
                    if (path == null) return;
                    CurrentFilePath = path;
                }
                _schemeRepositoryJson.SaveScheme(Scheme, CurrentFilePath);
                IsModified = false;
            });
        }

        private string GetNewName()
        {
            string baseName = "Новая фигура";
            var names = Scheme.Shapes.Select(s => s.Name);
            if (!names.Contains(baseName))
                return baseName;
            var hashNames = names.Where(name => name.StartsWith(baseName)).ToHashSet();
            int i = 1;
            while (hashNames.Contains(baseName + " " + i))
            {
                ++i;
            }
            return baseName + " " + i;

        }

        internal bool CheckModifiedAndContinue()
        {
            if (IsModified)
            {
                var result = _messageBoxService.ShowYesNoCancel("Сохранить изменения?");
                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFileCommand?.Execute(null);
                        return true;

                    case DialogResult.No: return true;
                    case DialogResult.Cancel: return false;
                }
            }
            return true;
        }

        private void ItemViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            IsModified = true;
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
