using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchemeModel
{
    public abstract class SchemeRepository
    {
        public Scheme CreateScheme(string schemeName) { return new Scheme(); }

        protected Scheme CreateScheme(IEnumerable<Shape> items)
        {
            var ret = new Scheme();
            foreach (var item in items)
                ret.AddShape(item);
            return ret;
        }

    }

    public class SchemeRepositoryJson// : SchemeRepository 
    {
        public void SaveScheme(Scheme scheme, string filePath) 
        {
            using (FileStream fstream = File.Create(filePath))
            {
                JsonSerializer.Serialize(fstream, scheme.Shapes,
                    new JsonSerializerOptions { WriteIndented = true });
            }
        }
        public Scheme LoadScheme(string fullPath) 
        {
            using (FileStream openStream = File.OpenRead(fullPath))
            {
                Scheme ret = new Scheme();
                var shapes = JsonSerializer.Deserialize<IEnumerable<Shape>>(openStream);
                foreach(var shape in shapes)
                    ret.AddShape(shape);
                return ret;
            }
        }
    }
}
