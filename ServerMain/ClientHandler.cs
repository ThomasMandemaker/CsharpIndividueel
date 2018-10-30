using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerMain
{
    public class ClientHandler
    {
        public delegate void UserNameSent(ClientHandler player, string playerName);
        public delegate void GameModeSelected(GameType type);
        public delegate void TurnSent(int xpos);
        public delegate void RoomSelected(string playerName, string roomName);
        public delegate void MessageReceived(string message, string sender, ClientHandler client);
        public delegate void Disconected(string playerName);
        
        public event UserNameSent onUserNameSent;
        public event GameModeSelected onGameModeSelected;
        public event TurnSent onTurnSent;
        public event RoomSelected onRoomSelected;
        public event MessageReceived onMessageReceived;
        public event UserNameSent onRoomWant;
        public event Disconected onDisconected;

        private Dictionary<string, Action<JObject>> commands;
        private string username;
        private TcpClient client;
        private Thread reader;
        private Socket client2;
        
        public ClientHandler(TcpClient client, UserNameSent u)
        {
            onUserNameSent += u;
            this.client = client;

            commands = new Dictionary<string, Action<JObject>>();

            CreateDictionary();

            

            reader = new Thread(Reader);
            reader.Start();
        }

        private void CreateDictionary()
        {
            commands.Add("rooms/get", RoomsGet);
            commands.Add("player/connect", ConnectPlayer);
            commands.Add("player/connect/room", ConnectPlayerRoom);
            commands.Add("player/turnsent", LocalTurnSent);
            commands.Add("message/sendtoserver", MessageHandler);
            commands.Add("player/disconect", Disconect);
        }

        private void Reader()
        {
            while (true)
            {
                try
                {
                    string message = "";
                    message = ReadMessage();
                    if (message != "")
                    {
                        JObject jObject = JObject.Parse(message);
                        commands[(string)jObject["command"]].Invoke(jObject);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        public void WriteMessage(JObject message)
        {
            byte[] bytesJson = Encoding.UTF8.GetBytes(message.ToString());

            byte[] byteLength = BitConverter.GetBytes(bytesJson.Length);
            byte[] sendArray = new byte[byteLength.Length + bytesJson.Length];
            Array.Copy(byteLength, sendArray, byteLength.Length);
            Array.Copy(bytesJson, 0, sendArray, 4, bytesJson.Length);
            client.GetStream().Write(sendArray, 0, sendArray.Length);
        }

        private string ReadMessage()
        {
            NetworkStream stream = client.GetStream();
            byte[] lengthBytes = new Byte[4];
            StringBuilder message = new StringBuilder();
            try
            {
                stream.Read(lengthBytes, 0, lengthBytes.Length);
                int length = BitConverter.ToInt32(lengthBytes, 0);
                int totalSizeRead = 0;
                byte[] receiveBuffer = new byte[length];

                do
                {
                    var numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length - totalSizeRead);
                    totalSizeRead += numberOfBytesRead;
                    message.AppendFormat("{0}", Encoding.UTF8.GetString(receiveBuffer, 0, numberOfBytesRead));
                } while (totalSizeRead < length);
            }
            catch (Exception e)
            {
                throw e;
            }

            return message.ToString();
        }

        private void ConnectPlayer(JObject message)
        {
            username = (string)message["data"];
            onUserNameSent(this, username);
        }

        private void RoomsGet(JObject message)
        {
            onRoomWant(this, username);
        }

        private void ConnectPlayerRoom(JObject message)
        {
            string room = (string)message["data"];
            onRoomSelected(username, room);
        }

        private void MessageHandler(JObject message) => onMessageReceived?.Invoke(Data.ReceivedMessageFromUser(message), username, this);
        
        private void LocalTurnSent(JObject message) => onTurnSent(Data.GetXPos(message));

        private void Disconect(JObject message)
        {
            onDisconected(username);
            reader.Abort();
            client.Close();
        }
    }
}
