using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ResponseModels
{
    public class LikePostResult : BaseResponse<LikePostResponseModel>
    {
        public LikePostResult() : base()
        {

        }
        public LikePostResult(bool success, string msg, LikePostResponseModel n) : base(success, msg, n)
        {

        }
    }
}
