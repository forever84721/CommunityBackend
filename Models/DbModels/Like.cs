using System;
using System.Collections.Generic;

namespace Models.DbModels
{
    public partial class Like
    {
        public int UserId { get; set; }
        public long PostId { get; set; }
        public int LikeType { get; set; }
        public DateTime LikeTime { get; set; }
    }
}
