using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ResponseModels
{
    public class LikePostResponseModel
    {
        public LikePostResponseModel(int newType, int numOfLike)
        {
            NewType = newType;
            NumOfLike = numOfLike;
        }

        public int NewType { get; set; }
        public int NumOfLike { get; set; }
    }
}
