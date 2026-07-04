using SchemeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SchemeEditorUI.ViewModels
{
    class ShapeViewModel<TShape> : INotifyPropertyChanged, INotifyDataErrorInfo
        where TShape : Shape
    {
        Scheme _scheme;
        TShape _shape;
        
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
                _scheme.RenameShape(_shape, value);
                OnPropertyChanged();
            } 
        }

        public int PositionX
        {
            get => _shape.Position.X;
            set
            {
                _shape.Position = new System.Drawing.Point(value, _shape.Position.Y);
                OnPropertyChanged();
            }
        }

        public int PositionY
        {
            get => _shape.Position.Y;
            set
            {
                _shape.Position = new System.Drawing.Point(_shape.Position.X, value);
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
