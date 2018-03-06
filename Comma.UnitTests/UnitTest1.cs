using Comma.DomainEnitites;
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

        [TestMethod]
        public void TestConditionalPerfectPersoanaADouaPlural()
        {
            var conjugare = Conjugare.MakeIndicativViitor(PersoanaEnum.ATreia, NumarEnum.Plural
                , new Verb { Radacina = "vedea", Terminatie = "" });

            Assert.AreEqual(conjugare.ToString(), "Ei/Ele vor vedea");
        }
    }
}