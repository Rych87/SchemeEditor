using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchemeModel
{
    public class SchemeRepositoryJson
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
                ret.RestoreScheme(shapes);
                return ret;
            }
        }
    }
}
