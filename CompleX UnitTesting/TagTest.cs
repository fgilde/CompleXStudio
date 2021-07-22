using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CompleX_Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompleX_UnitTesting
{
    [TestClass]
    public class TagTest
    {
        /// <summary>
        /// Prüft ob ein neuer Tag erstellt werden kann, und ob das ToString ergebnis stimmt
        /// (Test tag img) 
        /// </summary>
        [TestMethod]
        public void TestImageTagToString()
        {
            Environment.CurrentDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            var tag = TagFactory.CreateTag(TagLanguage.HTML, "img", true); 
            Assert.IsTrue(tag.Attributes.Count > 0, " Tag Atrribute konnten nicht geladen werden");
            TagAttribute sourceAttribute = tag.Attributes.Where(attribute => attribute.AtrributeName == "src").ToList()[0];
            sourceAttribute.AtrributeValue = "testimage.png";
            const string s = "<img src=\"testimage.png\" />";
            Assert.AreEqual(s, tag.ToString());
        }
    }
}
