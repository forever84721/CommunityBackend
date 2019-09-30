using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class PostViewModel
    {
        public long PostId { get; set; }
        public int IssuerId { get; set; }
        public string Name { get; set; }
        public DateTime PostTime { get; set; }
        public string Content { get; set; }
        public int NumOfLike { get; set; }
        public int NumOfComment { get; set; }
        public int NumOfShare { get; set; }
        public int LikeType { get; set; }
    }
}
