using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchemeModel
{
    public abstract class Shape
    {
        private string _name;
        private Point _position;

        public Shape(string name)
        {
            Name = name;
        }

        public string Name 
        { 
            get => _name;
            internal set
            {
                if (string.IsNullOrEmpty(value)) 
                    throw new ArgumentNullException("name");
                _name = value;
            } 
        }

        public Point Position 
        { 
            get => _position;
            set
            {
                if (value.X < 0 || value.Y < 0)
                    throw new ArgumentException("отрицательная координата");
                _position = value;
            }
        }

        public bool IsActive { get; set; }
    }

    public class Square : Shape
    {
        public Square(string name) : base(name) { }
    }

    public class Circle : Shape
    {
        public Circle(string name) : base(name) { }
    }

    public class Diamond : Shape
    {
        public Diamond(string name) : base(name) { }
    }
}
