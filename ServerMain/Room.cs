using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMain
{
    public class Room
    {
        private ClientHandler player1 = null;
        private ClientHandler player2 = null;

        public Room(ClientHandler p1)
        {
            player1 = p1;
        }
    }
}
