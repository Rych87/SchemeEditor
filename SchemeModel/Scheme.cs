namespace SchemeModel
{
    public class Scheme
    {
        private List<Shape> _shapes = new List<Shape>();
        public IEnumerable<Shape> Shapes => _shapes;

        public void AddShape(Shape shape)
        {
            ValidateName(shape.Name);
            _shapes.Add(shape);
        }

        public void RenameShape(Shape shape, string name)
        {
            if (!_shapes.Contains(shape))
                throw new InvalidOperationException("блок не принадлежит проекту");
            if (string.Equals(shape.Name, name))
                return;
            ValidateName(name);
            shape.Name = name;
        }

        private void ValidateName(string name, Shape shape = null)
        {
            if (_shapes.Any(x => string.Equals(x.Name, name)))
                throw new ArgumentException("блок с таким именем уже существует");
        }
    }
}
