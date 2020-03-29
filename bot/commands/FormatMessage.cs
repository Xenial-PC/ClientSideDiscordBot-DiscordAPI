using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSideSelfBot.bot.commands
{
    public class FormatMessage
    {
        private static string _msg;
        public static string _message;
        public static string _output;

        public static int _times;
        public static int _min;
        public static int _max;

        public static void FormatString(int commandLength, string input)
        {
            var msg = input.Substring(input.IndexOf((Program.prefix))); // Starts a new string at the index of ; << and makes that the new starting point /0
            msg = msg.Remove(0, commandLength); // removes the ;command + the space

            _msg = msg;
            _output = msg;
        }

        public static void FormatStringAdvanced(int commandLength, string input)
        {
            FormatString(commandLength, input);

            var msg = _msg;
            var message = msg.Remove(msg.IndexOf(","), input.Substring(input.IndexOf(",")).Length);

            msg = input.Substring(input.IndexOf(",")); // Starts a new string at the index of , << /0
            var times = int.Parse(msg.Remove(0, 2)); // Removes the space and only uses the numbers in the string

            _message = message; // sets the global message to this output
            _times = times; // sets the global times to this output
        }

        public static void FormatStringInt(int commandLength, string input)
        {
            FormatString(commandLength, input);

            var msg = _msg;
            var min = int.Parse(msg.Remove(msg.IndexOf(","), input.Substring(input.IndexOf(",")).Length));

            msg = input.Substring(input.IndexOf(",")); // Starts a new string at the index of , << /0
            var max = int.Parse(msg.Remove(0, 2)); // Removes the space and only uses the numbers in the string

            _min = min; // sets the global min value to this output
            _max = max; // sets the gloabl max value to this output
        }
    }
}
