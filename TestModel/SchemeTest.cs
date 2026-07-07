using SchemeModel;

namespace TestModel;

[TestClass]
public class SchemeTest
{
    [TestMethod]
    public void TestAddElements()
    {
        var scheme = new Scheme();
        scheme.AddShape(new Circle("A"));
        scheme.AddShape(new Square("B"));
        scheme.AddShape(new Diamond("C"));
        Assert.AreEqual(3, scheme.Shapes.Count());
        Assert.AreEqual(scheme.Shapes.Count(), scheme.Shapes.Select(s => s.Id).Distinct().Count());
    }

    [TestMethod]
    public void TestAddSameNameElement()
    {
        var scheme = new Scheme();
        scheme.AddShape(new Circle("A"));
        Assert.ThrowsException<ArgumentException>(() => scheme.AddShape(new Square("A")));     
    }

    [TestMethod]
    public void TestRenameElement()
    {
        var scheme = new Scheme();
        var circle = new Circle("A");
        scheme.AddShape(circle);
        scheme.RenameShape(circle, "B");
        Assert.AreEqual("B", circle.Name);

        //одингаковое имя
        var diamond = new Diamond("A");
        scheme.AddShape(diamond);
        Assert.ThrowsException<ArgumentException>(() => scheme.RenameShape(diamond, "B"));
    }
}
