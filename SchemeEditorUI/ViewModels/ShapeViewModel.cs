using SchemeModel;
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SchemeEditorUI.ViewModels
{
    class ShapeViewModel<TShape> : INotifyPropertyChanged
        where TShape : Shape
    {
        Scheme _scheme;
        TShape _shape;
        private readonly Dictionary<string, List<string>> _errors = new();

        public Shape Shape { get { return _shape; } }

        public ShapeViewModel(TShape shape, Scheme scheme)
        {
            _shape = shape;
            _scheme = scheme;
        }
        public string Name 
        { 
            get => _shape.Name;
            set 
            {
                try
                {
                    _scheme.RenameShape(_shape, value);
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
                OnPropertyChanged();
            } 
        }

        public int PositionX
        {
            get => _shape.Position.X;
            set
            {
                try
                {
                    _shape.Position = new System.Drawing.Point(value, _shape.Position.Y);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }      
                OnPropertyChanged();
            }
        }

        public int PositionY
        {
            get => _shape.Position.Y;
            set
            {
                try
                {
                    _shape.Position = new System.Drawing.Point(_shape.Position.X, value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
