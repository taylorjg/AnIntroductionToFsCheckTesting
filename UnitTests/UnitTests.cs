using CaseStudy;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        [TestCase('@', "pbv@dcc.fc.up.pt", "pbv", "dcc.fc.up.pt")]
        [TestCase('/', "/usr/include", "", "usr", "include")]
        [TestCase('a', "a", "", "")]
        [TestCase('a', "xxxa", "xxx", "")]
        public void SplitGivenCharAndNonNullString(char c, string s, params string[] expected)
        {
            var actual = StringSplitting.Split(c, s);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SplitGivenCharAndNullString()
        {
            var actual = StringSplitting.Split('a', null);
            Assert.That(actual, Is.Null);
        }
    }
}
