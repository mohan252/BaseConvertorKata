using System;
using System.Collections.Generic;

namespace BaseConvertorKata
{
    public interface IStringHelper
    {
        List<int> SplitInputIntoList(string input);
        List<int> Reverse(List<int> inputList);
    }

    public class StringHelper : IStringHelper
    {
        public List<int> SplitInputIntoList(string input)
        {
            List<int> inputInArray = new List<int>();
            ParseBitStringIntoList(input, inputInArray);
            return inputInArray;
        }

        public List<int> Reverse(List<int> inputList)
        {
            inputList.Reverse();
            return inputList;
        }

        private static void ParseBitStringIntoList(string input, List<int> inputInArray)
        {
            for (var charPosition = 0; charPosition < input.Length; charPosition++)
            {
                var charAsString = Convert.ToString(input[charPosition]);
                var charIntegerRepresentation = Int32.Parse(charAsString);
                inputInArray.Add(charIntegerRepresentation);
            }
        }
    }
}