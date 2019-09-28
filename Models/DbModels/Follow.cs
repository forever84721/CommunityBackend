using System;
using System.Collections.Generic;

namespace Models.DbModels
{
    public partial class Follow
    {
        public int UserId { get; set; }
        public int FollowUserId { get; set; }
        public DateTime StartFollowTime { get; set; }
    }
}
