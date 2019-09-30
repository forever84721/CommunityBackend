using System;
using System.Collections.Generic;
using System.Text;

namespace Models.RequestModels
{
    public class LikePostRequestModel
    {
        public long PostId { get; set; }
        public int LikeType { get; set; }
    }
}
