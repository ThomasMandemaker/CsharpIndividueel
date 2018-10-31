using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace ServerMain
{
    public static class Data
    {
        private static string dir = $"{Environment.CurrentDirectory}//data//";

        public static string GetData(string user)
        {
            if (!File.Exists(Path.Combine(dir, user)))
                return String.Empty;
            StreamReader sr = new StreamReader(Path.Combine(dir, user));
            return sr.ReadToEnd();
        }

        public static void SaveData(JObject message)
        {
            using (StreamWriter sw = new StreamWriter(File.Create(Path.Combine(dir, (string)message["data"]["user"]))))
                sw.WriteLine(message);
        }

        public static JObject SendPlayerTurn(int x, int y)
        {
            JObject message = new JObject
            {
                { "command", "player/turn"},
                { "data", new JObject{ { "x", x}, { "y", y} } }
            };
            return message;
        }

        public static JObject SendLoginNotOk()
        {
            JObject message = new JObject
            {
                { "command", "login/nok" }
            };
            return message;
        }

        public static JObject PlayerDisconected(string username)
        {
            JObject message = new JObject
            {
                { "command", "player/disconect" },
                { "data", username }
            };
            return message;
        }

        public static JObject SendLoginOk()
        {
            JObject message = new JObject
            {
                { "command", "login/ok" }
            };
            return message;
        }

        public static JObject SendRooms(Dictionary<string, ConnectFourRoom> d)
        {
            Dictionary<string, bool> l = new Dictionary<string, bool>();
            foreach (var v in d)
                l.Add(v.Key, v.Value.full);

            JObject message = new JObject
            {
                { "command", "rooms/return" },
                { "data", JsonConvert.SerializeObject(l) }
            };
            return message;
        }

        public static JObject SendPlayerTurnInvalid()
        {
            JObject message = new JObject
            {
                { "command", "player/invalidturn"}
            };
            return message;
        }

        public static JObject TurnDone(int x, int y)
        {
            JObject message = new JObject
            {
                { "command", "player/validturn"},
                { "data", new JObject{ { "x", x }, { "y", y } } }
            };
            return message;
        }

        public static JObject SendPlayerWon()
        {
            JObject message = new JObject
            {
                { "command", "player/won"}
            };
            return message;
        }

        public static JObject SendMessageFromUser(string s)
        {
            JObject message = new JObject
            {
                { "command", "message/sendtoserver" },
                { "data", s }
            };
            return message;
        }

        public static JObject SendMessageFromServer(string messageData, string user)
        {
            JObject message = new JObject
            {
                { "command", "message/sendfromserver" },
                { "data", new JObject{ { "message", messageData }, { "sender", user } } }
            };
            return message;
        }

        public static string ReceivedMessageFromUser(JObject message)
        {
            return (string)message["data"];
        }

        public static (string, string) ReceivedMessageFromServer(JObject message)
        {
            string s1 = (string)message["data"]["message"];
            string s2 = (string)message["data"]["sender"];
            return (s1, s2);
        }

        public static JObject SendPlayerLost()
        {
            JObject message = new JObject
            {
                { "command", "player/lost"}
            };
            return message;
        }

        public static int GetXPos(JObject message)
        {
            return int.Parse((string)message["data"]["x"]);
        }

        public static (int, int) GetXYPos(JObject message)
        {
            int xpos = int.Parse((string)message["data"]["x"]);
            int ypos = int.Parse((string)message["data"]["y"]);
            return (xpos, ypos);
        }

        public static JObject SendPlayerType(int i)
        {
            JObject message = new JObject
            {
                { "command", "player/playertype" },
                { "data", i }
            };

            return message;
        }
    }
}
