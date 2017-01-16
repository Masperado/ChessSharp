using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessSharp.CoreStuff.Classes;

namespace ChessSharp.Models.ProfileViewModels
{
    public class SendRequestModel
    {
        public string ReceiverId { get; set; }

        public ColorRequest ColorRequest { get; set; }

        public string SenderId { get; set; }
    }
}
