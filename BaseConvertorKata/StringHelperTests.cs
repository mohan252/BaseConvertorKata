using System.Collections.Generic;
using NUnit.Framework;

namespace BaseConvertorKata
{
    [TestFixture]
    public class StringHelperTests
    {
        private StringHelper _stringHelper;
        
        [SetUp]
        public void Init()
        {
            _stringHelper = new StringHelper();
        }

        [Test]
        public void SplitInputIntoList_Splits_String_Into_List()
        {
            var inputString = "110111";
            var expectedList = new List<int> { 1, 1, 0, 1, 1, 1 };

            var result = _stringHelper.SplitInputIntoList(inputString);
            CollectionAssert.AreEqual(result, expectedList);
        }

        [Test]
        public void ReverseList_reverses_TheListOfIntegers()
        {
            var inputList = new List<int> { 2, 3, 4 };
            var expectedList = new List<int> { 4, 3, 2 };

            var result = _stringHelper.Reverse(inputList);

            CollectionAssert.AreEqual(result, expectedList);
        }
    }
}