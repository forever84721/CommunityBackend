using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class ReplyViewModel
    {
        public long PostReplyId { get; set; }
        public int IssuerId { get; set; }
        public string Name { get; set; }
        public DateTime PostTime { get; set; }
        public string Content { get; set; }
    }
}
