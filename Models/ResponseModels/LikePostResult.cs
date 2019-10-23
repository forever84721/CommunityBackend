using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ResponseModels
{
    public class LikePostResult : BaseResponse<int>
    {
        public LikePostResult() : base()
        {

        }
        public LikePostResult(bool success, string msg, int n) : base(success, msg, n)
        {

        }
    }
}
