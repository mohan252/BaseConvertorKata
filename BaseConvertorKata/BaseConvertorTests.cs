using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace BaseConvertorKata
{
    [TestFixture]
    public class BaseConvertorTests
    {
        private BaseConvertor _converter;
        private IStringHelper _stringHelper;
        [SetUp]
        public void Init()
        {
            _stringHelper = Substitute.For<IStringHelper>();
            _stringHelper.Reverse(Arg.Any<List<int>>()).Returns(new List<int> {0});
            _converter = new BaseConvertor(_stringHelper);
        }

        [Test]
        public void Convert_SplitsTheInput()
        {
            var input = "1123";

            _converter.Convert(input, 2, 10);

            _stringHelper.Received().SplitInputIntoList(input);
        }

        [Test]
        public void Convert_Reverses_TheInputStringArray()
        {
            var input = "1123";
            var inputAsListOfIntegers = new List<int> { 1, 1, 2, 3 };
            _stringHelper.SplitInputIntoList(input).Returns(inputAsListOfIntegers);

            _converter.Convert(input, 2, 10);

            _stringHelper.Received().Reverse(inputAsListOfIntegers);
        }

        [Test]
        public void Covert_Zero_ToPower_OfAnything_WouldBeZero()
        {
            var input = "0";
            var inputAsListOfIntegers = new List<int> { 0 };
            _stringHelper.SplitInputIntoList(input).Returns(inputAsListOfIntegers);

            var result = _converter.Convert(input, 2, 10);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Covert_10_FromBase2To10_WouldBeTwo()
        {
            var input = "10";
            var inputAsListOfIntegers = new List<int> { 0 , 1 };
            _stringHelper.Reverse(Arg.Any<List<int>>()).Returns(inputAsListOfIntegers);

            var result = _converter.Convert(input, 2, 10);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Convert_90_ToBase3_WouldBe_4010()
        {
            //90 / 3

            // 90 = 1 * 3^4 +  0 * 3^3 + 1 * 3^2 + 0 * 3^1 + 0 * 3^0 = 10100

            // 90/1 = reminder = 90 ==> 0 at 0 position
            //90/3 = reminder of 0 ==> 0 at 1 position
            //90/9 = 
            //90/27
            //90/81

            // 90 cube root => 4.48 ==> starting position is 4
            // 90/81 ==> 9 is reminder
            // 9/27 ==> 0   ==> 3rd position
            // 9/9 ==> 1    ==> 2nd position
            // fill remaining with 0s
        }
    }

    public class BaseConvertor
    {
        private readonly IStringHelper _stringHelper;

        public BaseConvertor(IStringHelper stringHelper)
        {
            _stringHelper = stringHelper;
        }

        public int Convert(string input, int fromBase, int toBase)
        {
            var inputInDigits = _stringHelper.SplitInputIntoList(input);
            var inputInReverseOrder = _stringHelper.Reverse(inputInDigits);
            var inputConvertedToBase10 = ConvertToBase10(fromBase, inputInReverseOrder);
            return inputConvertedToBase10;
        }

        private static int ConvertToBase10(int fromBase, List<int> inputInReverseOrder)
        {
            int output = 0;
            for (int integerPosition = 0; integerPosition < inputInReverseOrder.Count; integerPosition++)
            {
                var currentDigit = inputInReverseOrder[integerPosition];
                var currentMultiplier = System.Convert.ToInt32(Math.Pow(fromBase, integerPosition));
                output += currentMultiplier*currentDigit;
            }
            return output;
        }
    }
}
