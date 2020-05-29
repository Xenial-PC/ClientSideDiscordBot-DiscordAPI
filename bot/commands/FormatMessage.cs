using System;
// ReSharper disable ExceptionNotDocumented
namespace ClientSideSelfBot.Bot.Commands
{
    /* this is my format library I use it on about any text based format system I make */
    public class FormatMessage
    {
        private static string _msg;
        public static string AdvancedMessage;
        public static string Output;
        public static string AdvancedOutputOne;
        public static string AdvancedOutputTwo;

        public static int AdvancedTimes;
        public static int IntMin;
        public static int IntMax;

        public static void FormatString(int commandLength, string input)
        {
            var msg = input.Substring(input.IndexOf(Program.Prefix, StringComparison.Ordinal)); // Starts a new string at the index of ; << and makes that the new starting point /0
            msg = msg.Remove(0, commandLength); // removes the ;command + the space

            _msg = msg;
            Output = msg;
        }

        public static void FormatStringAdvanced(int commandLength, string input)
        {
            FormatString(commandLength, input);

            var msg = _msg;
            var message = msg.Remove(msg.IndexOf(",", StringComparison.Ordinal), input.Substring(input.IndexOf(",", StringComparison.Ordinal)).Length);

            msg = input.Substring(input.IndexOf(",", StringComparison.Ordinal)); // Starts a new string at the index of , << /0
            var times = int.Parse(msg.Remove(0, 2)); // Removes the space and only uses the numbers in the string

            AdvancedMessage = message; // sets the global message to this output
            AdvancedTimes = times; // sets the global times to this output
        }

        public static void FormatStrings(int commandLength, string input)
        {
            FormatString(commandLength, input);

            var msg = _msg;
            var outputOne = msg.Remove(msg.IndexOf(",", StringComparison.Ordinal), input.Substring(input.IndexOf(",", StringComparison.Ordinal)).Length);

            msg = input.Substring(input.IndexOf(",", StringComparison.Ordinal)); // Starts a new string at the index of , << /0
            var outputTwo = msg.Remove(0, 2); // Removes the space and uses the other half of the string

            AdvancedOutputOne = outputOne; // sets the global message to this output
            AdvancedOutputTwo = outputTwo; // sets the global times to this output
        }

        public static void FormatStringAdvanced(string input)
        {
            // inputOne:inputTwo
            var outputOneInput = input; // Saves the initial input
            var outputOne = outputOneInput.Remove(outputOneInput.IndexOf(":", StringComparison.Ordinal),
                outputOneInput.Substring(outputOneInput.IndexOf(":", StringComparison.Ordinal)).Length);

            var outputTwoInput = input; // Saves the initial input
            var outputTwo = outputTwoInput.Substring(outputTwoInput.IndexOf(":", StringComparison.Ordinal)); // Starts a new string at the index of : << /0
            outputTwo = outputTwo.Remove(0, 1); // Removes the : and only uses the last half of the output in the string

            AdvancedOutputOne = outputOne; // sets the global output 
            AdvancedOutputTwo = outputTwo; // sets the global output
        }

        public static void FormatStringInt(int commandLength, string input)
        {
            FormatString(commandLength, input);

            var msg = _msg;
            var min = int.Parse(msg.Remove(msg.IndexOf(",", StringComparison.Ordinal), input.Substring(input.IndexOf(",", StringComparison.Ordinal)).Length));

            msg = input.Substring(input.IndexOf(",", StringComparison.Ordinal)); // Starts a new string at the index of , << /0
            var max = int.Parse(msg.Remove(0, 2)); // Removes the space and only uses the numbers in the string

            IntMin = min; // sets the global min value to this output
            IntMax = max; // sets the global max value to this output
        }
    }
}
