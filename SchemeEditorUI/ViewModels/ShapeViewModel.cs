using SchemeEditorUI.Services;
using SchemeModel;
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SchemeEditorUI.ViewModels
{
    public class ShapeViewModel<TShape> : INotifyPropertyChanged
        where TShape : Shape
    {
        IMessageBoxService _messageBoxService;
        Scheme _scheme;
        TShape _shape;

        public Shape Shape { get { return _shape; } }

        public ShapeViewModel(TShape shape, Scheme scheme, IMessageBoxService messageBoxService)
        {
            _shape = shape;
            _scheme = scheme;
            _messageBoxService = messageBoxService;
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
                    _messageBoxService.ShowInfo(ex.Message);
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
                    _messageBoxService.ShowInfo(ex.Message);
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
                    _messageBoxService.ShowInfo(ex.Message);
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
