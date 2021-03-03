using NUnit.Framework;

namespace LibreTranslate.Net.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            var translate = new Translate();
            string english = "Hello World!";
            string spanish = translate.TranslateText(Language.En, Language.Es, english);
            Assert.Equals(spanish, "¡Hola Mundo!");
        }
        public void Test2()
        {
            var translate = new Translate();
            Assert.True(translate.SupportedLanguages().Contains(Language.En));
            Assert.True(translate.SupportedLanguages().Contains(Language.Ar));
            Assert.True(translate.SupportedLanguages().Contains(Language.Zh));
            Assert.True(translate.SupportedLanguages().Contains(Language.Fr));
            Assert.True(translate.SupportedLanguages().Contains(Language.De));
            Assert.True(translate.SupportedLanguages().Contains(Language.It));
            Assert.True(translate.SupportedLanguages().Contains(Language.Pt));
            Assert.True(translate.SupportedLanguages().Contains(Language.Ru));
            Assert.True(translate.SupportedLanguages().Contains(Language.Es));
            //assumes server has all languages available!
        }
    }
}