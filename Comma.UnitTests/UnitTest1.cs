using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Comma.UnitTests
{
    [TestClass]
    public class Grammar
    {
        [TestMethod]
        public void AnaAreMere()
        {
            string subject = "Ana";
            string input = "Ana are mere";

            Assert.AreEqual("Ana", subject);
        }
    }
}