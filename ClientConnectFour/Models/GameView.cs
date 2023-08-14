using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientConnectFour.Models
{
    public class GameView
    {
        [Key]
        public int GameID { get; set; }
        public int UserID { get; set; }
        public string Moves { get; set; }
    }
}
