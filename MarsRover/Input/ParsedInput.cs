namespace MarsRover.Input_Layer
{
    public class ParsedInput
    {
        public virtual string[] FilterBracketsAndCommas(string[] inputArray)
        {
            var charsToRemove = new[] { "(", ")", "," };

            foreach (string c in charsToRemove)
            {
                for (int i = 0; i < inputArray.Length; i++)
                {
                    inputArray[i] = inputArray[i].Replace(c, string.Empty);
                }
            }
            return inputArray;
        }
    }
}
