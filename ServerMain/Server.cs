using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerMain
{
    public class Server
    {
        private TcpListener listener;
        private int port = 5678;
        private Dictionary<string, ClientHandler> activePlayers;
        private List<ClientHandler> nonAssignedPlayers;
        private Dictionary<string, ConnectFourRoom> allRooms;

        static void Main(string[] args)
        {
            new Server();
        }

        public Server()
        {
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();

            nonAssignedPlayers = new List<ClientHandler>();
            activePlayers = new Dictionary<string, ClientHandler>();
            allRooms = new Dictionary<string, ConnectFourRoom>();
            allRooms.Add("Room1", new ConnectFourRoom());
            allRooms.Add("Room2", new ConnectFourRoom());
            new Thread(ClientConnector).Start();
        }

        private void ClientConnector()
        {
            while (true)
            {
                TcpClient player = listener.AcceptTcpClient();
                ClientHandler c = new ClientHandler(player, Identify);
                Console.WriteLine("Player connected");
            }
        }

        void Identify(ClientHandler player, string playerName)
        {
            if(activePlayers.ContainsKey(playerName))
            {
                player.WriteMessage(Data.SendLoginNotOk());
                return;
            }
            activePlayers.Add(playerName, player);
            
            player.onRoomSelected += AssignToRoom;
            player.onRoomWant += ReturnRooms;
            player.onDisconected += Disconnect;
            player.WriteMessage(Data.SendLoginOk());
        }

        void Disconnect(string username)
        {
            activePlayers.Remove(username);
        }

        void ReturnRooms(ClientHandler client, string username)
        {
            client.WriteMessage(Data.SendRooms(allRooms));
        }

        private void AssignToRoom(string player, string room)
        {
           // try
            //{
                allRooms[room].AddPlayer(activePlayers[player], player);
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("Room is full or doesn't exist");
            //}
        }
    }

    public enum PlayerType
    {
        MainPlayer = 0,
        SecondPlayer = 1
    }

    public class TestObject
    {
        public Socket work = null;

        public const int BufferSize = 1024;

        public byte[] buffer = new byte[BufferSize];

        public StringBuilder sb = new StringBuilder();

    }

    public class AsyncSocketListenerTest
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public AsyncSocketListenerTest()
        {

        }

        


        public static void StartListening()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Console.WriteLine(ipAddress);

            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Bind(localEndPoint);
                socket.Listen(100);

                while (true)
                {
                    allDone.Reset();

                    Console.WriteLine("Waiting for a connection...");
                    socket.BeginAccept(new AsyncCallback(AcceptCallback), socket);

                    allDone.WaitOne();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();

            Socket socket = (Socket)ar.AsyncState;
            Socket handle = socket.EndAccept(ar);
            TestObject state = new TestObject();
            state.work = handle;

            handle.BeginReceive(state.buffer, 0, TestObject.BufferSize, 0, new AsyncCallback(ReadCallBack), state);
        }

        public static void ReadCallBack(IAsyncResult ar)
        {
            String content = String.Empty;

            TestObject state = (TestObject)ar.AsyncState;
            Socket handler = state.work;

            int bytesRead = handler.EndReceive(ar);

            
            if(bytesRead > 0)
            {
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                content = state.sb.ToString();

                if(content.IndexOf("<EOF>") > -1)
                {
                    Console.WriteLine("Read{0} bytes from socket. \n Data : {1}", content.Length, content);
                    Send(handler, content);
                }
                else
                {
                    handler.BeginReceive(state.buffer, 0, TestObject.BufferSize, 0, new AsyncCallback(ReadCallBack), state);
                }
            }
        }

        public static void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallBack), handler);
            Console.WriteLine("here");
        }

        private static void SendCallBack(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;

                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
