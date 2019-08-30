using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Models.ResponseModels
{
    public class PostViewModel
    {
        public string Name { get; set; }
        public DateTime PostTime { get; set; }
        public string Content { get; set; }
        public int NumOfLike { get; set; }
        public int NumOfComment { get; set; }
        public int NumOfShare { get; set; }
    }
}
