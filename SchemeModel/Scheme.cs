namespace SchemeModel
{
    public class Scheme
    {
        private int _nextId = 0;
        private List<Shape> _shapes = new List<Shape>();
        public IEnumerable<Shape> Shapes => _shapes;

        public void AddShape(Shape shape)
        {
            ValidateName(shape.Name);   
            shape.Id = _nextId++;
            _shapes.Add(shape);
        }

        public void RemoveShape(Shape shape)
        {
            CheckContains(shape);
            _shapes.Remove(shape);
        }

        public void RenameShape(Shape shape, string name)
        {
            CheckContains(shape);
            if (string.Equals(shape.Name, name))
                return;
            ValidateName(name);
            shape.Name = name;
        }

        public void RestoreScheme(IEnumerable<Shape> shapes)
        {
            
            var shapesList = shapes.ToList();
            var shapesCount = shapesList.Count;
            if (shapesList.Select(s => s.Id).Distinct().Count() != shapesCount)
                throw new Exception("не уникальные Id");
            if (shapesList.Select(s => s.Name).Distinct().Count() != shapesCount)
                throw new Exception("не уникальные имена");
            _nextId = shapesList.Count > 0
                ? shapesList.Max(s => s.Id) + 1
                : 0;
            _shapes = shapesList;
        }

        private void CheckContains(Shape shape)
        {
            if (!_shapes.Contains(shape))
                throw new InvalidOperationException("блок не принадлежит проекту");
        }

        private void ValidateName(string name, Shape shape = null)
        {
            if (_shapes.Any(x => string.Equals(x.Name, name)))
                throw new ArgumentException("блок с таким именем уже существует");
        }
    }
}
