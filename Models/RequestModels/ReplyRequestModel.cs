using System;
using System.Collections.Generic;
using System.Text;

namespace Models.RequestModels
{
    public class ReplyRequestModel
    {
        public int UserId { get; set; }
        public int ReplyType { get; set; }
        public int TargetId { get; set; }
        public string Content { get; set; }
    }
}
