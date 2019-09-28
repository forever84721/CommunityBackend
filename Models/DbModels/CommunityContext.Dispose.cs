using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DbModels
{
    public partial class CommunityContext : DbContext
    {
        public bool IsDead { get; set; }
        public override void Dispose()
        {
            IsDead = true;
            base.Dispose();
        }
    }
}
