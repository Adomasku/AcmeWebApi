using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcmeWebApi.Tests
{
    [TestClass]
    public class TextTests
    {
        [TestMethod]
        public void TestGetWords()
        {
            var words = Word.GetWords("Ryga yra   Latvijos  sostinė.Rīga yra latvijos  sostine. Sostinę aplanko daug turistų.");
            Assert.AreEqual(words.Count, 8);
        }

        [TestMethod]
        public void TestWordsWithAccentComparer()
        {
            WordsWithAccentComparer comparer = new WordsWithAccentComparer();            
            Assert.AreEqual(comparer.Equals("Ryga", "Rīga"), false);//Ryga is a translation from Rīga and not accent, so has to be false
            Assert.AreEqual(comparer.Equals("Riga", "Rīga"), true);
            Assert.AreEqual(comparer.Equals("sostinė", "Sostinę"), true);
            Assert.AreEqual(comparer.Equals("sostinė", "Sostinę"), true);
            Assert.AreEqual(comparer.Equals("test", "testas"), false);
        }
    }
}
