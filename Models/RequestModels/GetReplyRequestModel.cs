using System;
using System.Collections.Generic;
using System.Text;

namespace Models.RequestModels
{
    public class GetReplyRequestModel
    {
        public long PostId { get; set; }
        public int Page { get; set; }
    }
}
