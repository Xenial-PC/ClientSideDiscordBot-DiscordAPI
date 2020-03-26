using System.Collections.Generic;

namespace ClientSideSelfBot
{
    public class ConfigJson
    {
        public string Token { get; set; }
        public string Bot { get; set; }
        public string Owner { get; set; }
        public List<string> Admin { get; set; }
        public List<string> Mod { get; set; }
        public List<string> Trusted { get; set; }
        public List<string> Temp { get; set; }
        
    }
}
