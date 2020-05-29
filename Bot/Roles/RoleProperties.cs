using System.Collections.Generic;
namespace ClientSideSelfBot.Bot.Roles
{
    public class RoleProperties
    {
        public static List<string> AdminList { get; set; } = new List<string>();
        public static List<string> ModList { get; set; } = new List<string>();
        public static List<string> TrustedList { get; set; } = new List<string>();
        public static List<string> TempList { get; set; } = new List<string>();
        public static List<string> BannedList { get; set; } = new List<string>();
    }
}