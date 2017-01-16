using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ChessSharp.Models;

namespace ChessSharp.CoreStuff.Classes
{
    public class Request
    {
        public Guid RequestId { get; set; }
        public DateTime TimeSent { get; set; }
        public ColorRequest ColorRequest { get; set; }

        public string RecieverId { get; set; }
        public ChessUser Reciever { get; set; }

        public string SenderId { get; set; }
        public ChessUser Sender { get; set; }

        public Request(string senderId, string recieverId)
        {
            RequestId = Guid.NewGuid();
            SenderId = senderId;
            RecieverId = recieverId;
            TimeSent = DateTime.Now;
            ColorRequest = ColorRequest.WHITE;
        }

        public Request()
        {
            
        }
    }
}
