using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ServerMain;
using Newtonsoft.Json;

namespace PlayerUI
{
    public partial class Form1 : Form
    {
        private Rectangle[] columns;
        private TcpClient client;
        private Thread reader;
        private Dictionary<string, Action<JObject>> commands = new Dictionary<string, Action<JObject>>();
        private bool myturn = true;
        private string username;
        private Brush myBrush;
        private Brush theirBrush;
        private (int, int) latestPick;

        public Form1()
        {
            InitializeComponent();
            FormClosing += Form_Closing;
            Debug.WriteLine(NameEnterPanel.Parent.Text);
            NameEnterPanel.Parent = this;
            NameEnterPanel.Show();
            GamePickPanel.Visible = false;
            GamePickPanel2.Visible = false;
            GameSelectPanel.Visible = false;
            ConnectFourPanel.Visible = false;
            RoomSelectPanel2.Visible = false;
            RoomSelect.Visible = false;


            client = new TcpClient("127.0.0.1", 5678);
            GenerateDictionary();

            reader = new Thread(Reader);
            reader.Start();

            SendUserNameButton.MouseClick += SendUserNameButton_MouseClick;
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            WriteMessage(Data.PlayerDisconected(username));
        }

        private void SendUserNameButton_MouseClick(object sender, MouseEventArgs e)
        {
            username = UserNameTextBox.Text;
            JObject message = new JObject
            {
                { "command", "player/connect" },
                { "data", username }
            };

            WriteMessage(message);
        }

        private void SetupGamePick()
        {
            Invoke((MethodInvoker)delegate () { GameSelectPanel.Show(); });
            ConnectFourSelectButton.MouseClick += ConnectFourButton_MouseClick;
        }

        private void SetupRoomSelect()
        {
            RoomSelectPanel2.Show();

            JObject message = new JObject
            {
                { "command", "rooms/get" }
            };

            WriteMessage(message);
            RoomListBox2.MouseClick += RoomListBox2_SelectedIndexChanged;
        }

        private void RoomListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedIndex == -1)
                return;

            JObject message = new JObject
            {
                { "command", "player/connect/room" },
                { "data", ((ListBox)sender).Text }
            };
            WriteMessage(message);
          
            SetupConnectFour();
        }

        private void ConnectFourButton_MouseClick(object sender, MouseEventArgs e)
        {
            SetupRoomSelect();
        }

        private void SetupConnectFour()
        {
            ConnectFourPanel.Parent = RoomSelectPanel2;
            ConnectFourPanel.BringToFront();
            ConnectFourPanel.Show();
            
            SendBox.KeyPress += SendBox_KeyPress;
            
            columns = new Rectangle[7];
            ConnectFourPanel.Paint += ConnectFourPanel_Paint;
            ConnectFourPanel.MouseClick += ConnectFourPanel_MouseClick;
        }

        private void SendBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if(e.KeyChar == (char)13)
            {
                string data = SendBox.Text;
                JObject message = Data.SendMessageFromUser(data);
                WriteMessage(message);
                ChatMessageList.Text += "You: " + data + "\n";
                SendBox.Text = "";
            }
        }

        private void ConnectFourPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (!myturn)
                return;
            int x = e.X;
            int y = e.Y;

            if(x < 340 && y < 300)
            {
                int columnNum = (x+32) / 48 - 1;
                int rowNum = 5 - (y + 32) / 48;
                latestPick = (columnNum, rowNum);
                JObject message = new JObject
                {
                    { "command", "player/turnsent" },
                    { "data", new JObject{ { "x", columnNum } } }
                };

                WriteMessage(message);
            }
        }

        private void GenerateDictionary()
        {
            commands.Add("login/ok", OkLogin);
            commands.Add("login/nok", NotOkLogin);
            commands.Add("rooms/return", RoomsReturned);
            commands.Add("player/turn", TurnHandler);
            commands.Add("player/validturn", ValidTurn);
            commands.Add("player/playertype", DeterPlayerType);
            commands.Add("player/won", WeWon);
            commands.Add("player/lost", WeLost);
            commands.Add("message/sendfromserver", HandleChat);
        }

        private void RoomsReturned(JObject message)
        {
            List<string> roomlist = JsonConvert.DeserializeObject<List<string>>((string)message["data"]);
            Invoke((MethodInvoker)delegate () {  RoomListBox2.DataSource = roomlist; RoomListBox2.ClearSelected(); });
        }

        private void OkLogin(JObject message)
        {
            //Invoke((MethodInvoker)delegate () { NameEnterPanel.Visible = false; });
            SetupGamePick();
        }

        private void NotOkLogin(JObject message)
        {
            MessageBox.Show("Name already in use");
        }

        private void HandleChat(JObject message)
        {
            string sender = (string)message["data"]["sender"];
            string messageData = (string)message["data"]["message"];
            Invoke((MethodInvoker)delegate () { ChatMessageList.Text += sender + ": " + messageData + "\n"; });
        }

        private void WeWon(JObject message)
        {
            MessageBox.Show("YOU WON :D");
        }

        private void WeLost(JObject message)
        {
            MessageBox.Show("YOU LOST :(");
        }



        private void ValidTurn(JObject message)
        {
            myturn = false;
            Graphics g = ConnectFourPanel.CreateGraphics();
            int x = int.Parse((string)message["data"]["x"]);
            int y = int.Parse((string)message["data"]["y"]);
            g.FillEllipse(myBrush, 32 + 48 * x, 32 + 48 * (5-y), 32, 32);
        }
        
        private void DeterPlayerType(JObject message)
        {
            if(int.Parse((string)message["data"]) == 1)
            {
                myBrush = Brushes.Red;
                theirBrush = Brushes.Yellow;
            }
            else
            {
                myBrush = Brushes.Yellow;
                theirBrush = Brushes.Red;
                myturn = false;
            }
        }

        private void TurnHandler(JObject message)
        {
            var v = Data.GetXYPos(message);
            int x = v.Item1;
            int y = v.Item2;
            Graphics g = ConnectFourPanel.CreateGraphics();
            g.FillEllipse(theirBrush, 32 + 48 * x, 32 + 48 * (5-y), 32, 32);
            myturn = true;
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

        private void ConnectFourPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, 24, 24, 340, 300);
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    if (i == 0)
                        columns[j] = new Rectangle(32 + 48 * j, 24, 32, 300);
                    e.Graphics.FillEllipse(Brushes.White, 32 + 48 * j, 32 + 48 * i, 32, 32);
                }
            }
        }
    }
}
