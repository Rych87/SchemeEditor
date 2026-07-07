using SchemeModel;

namespace TestModel
{
    [TestClass]
    public sealed class ShapeTest
    {
        [TestMethod]
        public void TestShapeCreate()
        {
            var square = new Square("some name");
            Assert.IsNotNull(square);
            Assert.AreEqual("some name", square.Name);
        }

        [TestMethod]
        public void TestShapeCreateEmptyName()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Square(""));
        }

        [TestMethod]
        public void TestShapeChangeCoord()
        {
            var circle = new Circle("circ");
            circle.Position = new System.Drawing.Point(120, 50);
            Assert.AreEqual(120, circle.Position.X);
            Assert.AreEqual(50, circle.Position.Y);
        }

        [TestMethod]
        public void TestShapeNegativeCoord()
        {
            var circle = new Circle("some name");
            Assert.ThrowsException<ArgumentException>(() => circle.Position = new System.Drawing.Point(-1,0));
            Assert.ThrowsException<ArgumentException>(() => circle.Position = new System.Drawing.Point(0, -1));
        }
    }
}
