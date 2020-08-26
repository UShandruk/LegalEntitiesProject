using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellChecker;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void SpellCheckTest()
        {
            string dictionary = "rain spain plain plaint pain main mainly the in on fall falls his was";
            string originalText = "hte      rame in pain fells   mainy oon  teh lain was hints pliant";
            string separator = Environment.NewLine + "===" + Environment.NewLine;
            string originalTextAndDictionary = dictionary + separator + originalText;

            string expectedText = "the      rain in pain fall   rain in  the rain was his plaint";
            string resultText = TextChecker.GetCorrectedText(originalTextAndDictionary);

            Assert.AreEqual(resultText, expectedText);
        }
    }
}