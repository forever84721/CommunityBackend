using System;
using System.Collections.Generic;

namespace Models.DbModels
{
    public partial class Post
    {
        public long PostId { get; set; }
        public DateTime PostTime { get; set; }
        public int IssuerId { get; set; }
        public string InnerText { get; set; }
    }
}
