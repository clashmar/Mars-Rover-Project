namespace MarsRover.Input_Layer
{
    public interface IParsable
    {
        public bool IsValid { get; set; }

        public void Parse(string inputString);
    }
}
