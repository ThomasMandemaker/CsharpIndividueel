namespace ServerMain
{
    public class ConnectFourRoom
    {
        private ClientHandler player1 = null;
        private ClientHandler player2 = null;

        private string player1Name = string.Empty;
        private string player2name = string.Empty;

        private bool player1Turn = true;

        public bool full = false;

        private byte[,] connectFourBoard = new byte[7,6];
        
        public ConnectFourRoom()
        {

        }

        public void AddPlayer(ClientHandler client, string playername)
        {
            if (player1 == null)
            {
                player1 = client;
                player1.onDisconected += Disconected;
                player1.WriteMessage(Data.SendPlayerType(1));
                player1Name = playername;

                if (player2 != null)
                    RoomStart();
            }
            else if(player2 == null)
            {
                player2 = client;
                player2.onDisconected += Disconected;
                player2.WriteMessage(Data.SendPlayerType(2));
                player2name = playername;
                
                RoomStart();
            }
        }

        private void RoomStart()
        {
            full = true;
            player1.onGameModeSelected += GameModeSelected;
            GameModeSelected(GameType.ConnectFour);
        }

        private void GameModeSelected(GameType type)
        {
            switch (type)
            {
                case GameType.ConnectFour:
                    for(int i = 0; i <= 6; i++)
                    {
                        for(int j = 0; j <= 5; j++)
                        {
                            connectFourBoard[i, j] = 0;
                        }
                    }
                    player1.onTurnSent += ConnectFourTurnHandler;
                    player2.onTurnSent += ConnectFourTurnHandler;
                    player1.onMessageReceived += onMessageReceived;
                    player2.onMessageReceived += onMessageReceived;
                    break;
                default:
                    break;
            }
        }

        private void ConnectFourTurnHandler(int xpos)
        {
            for(int i = 0; i <= 5; i++)
            {
                if(connectFourBoard[xpos, i] == 0)
                {
                    if (player1Turn)
                    {
                        connectFourBoard[xpos, i] = 1;
                        player2.WriteMessage(Data.SendPlayerTurn(xpos, i));
                        player1.WriteMessage(Data.TurnDone(xpos, i));
                        if (CheckBoard(1))
                        {
                            player1.WriteMessage(Data.SendPlayerWon());
                            player2.WriteMessage(Data.SendPlayerLost());
                            break;
                        }
                    }
                    else
                    {
                        connectFourBoard[xpos, i] = 2;
                        player1.WriteMessage(Data.SendPlayerTurn(xpos, i));
                        player2.WriteMessage(Data.TurnDone(xpos, i));
                        if (CheckBoard(2))
                        {
                            player2.WriteMessage(Data.SendPlayerWon());
                            player1.WriteMessage(Data.SendPlayerLost());
                            break;
                        }
                    }
                    player1Turn = !player1Turn;
                    break;
                }
            }
        }

        private bool CheckBoard(int playercheck)
        {
            //Vertical win
            for(byte x = 0; x < connectFourBoard.GetLength(0) - 3; x++)
                for (byte y = 0; y < connectFourBoard.GetLength(1); y++)
                    if (AllNumsEqual(playercheck, connectFourBoard[x, y], connectFourBoard[x + 1, y], connectFourBoard[x + 2, y], connectFourBoard[x + 3, y]))
                        return true;
            
            //Horizontal win
            for (byte x = 0; x < connectFourBoard.GetLength(0); x++)
                for (byte y = 0; y < connectFourBoard.GetLength(1) - 3; y++)
                    if (AllNumsEqual(playercheck, connectFourBoard[x, y], connectFourBoard[x, y + 1], connectFourBoard[x, y + 2], connectFourBoard[x, y + 3]))
                        return true;

            //Diagonal win \
            for (byte x = 0; x < connectFourBoard.GetLength(0) - 3; x++)
                for (byte y = 0; y < connectFourBoard.GetLength(1) - 3; y++)
                    if (AllNumsEqual(playercheck, connectFourBoard[x, y], connectFourBoard[x + 1, y + 1], connectFourBoard[x + 2, y + 2], connectFourBoard[x + 3, y + 3]))
                        return true;

            //Diagonal win /
            for (byte x = 0; x < connectFourBoard.GetLength(0) - 3; x++)
                for (byte y = 3; y < connectFourBoard.GetLength(1); y++)
                    if (AllNumsEqual(playercheck, connectFourBoard[x, y], connectFourBoard[x + 1, y - 1], connectFourBoard[x + 2, y - 2], connectFourBoard[x + 3, y - 3]))
                        return true;

            return false;
        }

        private bool AllNumsEqual(int ptc, params int[] numbers)
        {
            foreach (int i in numbers)
                if (i != ptc)
                    return false;

            return true;
        }

        void onMessageReceived(string message, string username, ClientHandler handler)
        {
            if (handler == player1)
                player2.WriteMessage(Data.SendMessageFromServer(message, username));
            else
                player1.WriteMessage(Data.SendMessageFromServer(message, username));
        }
        
        void Disconected(string username)
        {
            if (username == player1Name)
            {
                player2.WriteMessage(Data.SendPlayerWon());
                player1 = null;
                player1Name = string.Empty;
            }
            else
            {
                player1.WriteMessage(Data.SendPlayerWon());
                player2 = null;
                player2name = string.Empty;
            }

            full = false;
        }
    }

    public enum GameType
    {
        ConnectFour = 0
    }
}
