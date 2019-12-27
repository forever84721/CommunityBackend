using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Common;
using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Implement
{
    public class BaseService : IDisposable
    {
        internal CommunityContext DbContext;
        internal readonly ApplicationSettings AppSettings;
        public BaseService(CommunityContext dbContext, IOptions<ApplicationSettings> appSettings)
        {
            DbContext = dbContext;
            AppSettings = appSettings?.Value;
        }
        protected void CheckDbContext()
        {
            if (DbContext.IsDead)
            {
                var optionsBuilder = new DbContextOptionsBuilder<CommunityContext>();
                optionsBuilder.UseSqlServer(AppSettings.IdentityConnection);
                DbContext = new CommunityContext(optionsBuilder.Options);
            }
        }

        ~BaseService()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposedValue)
        {
            if (!disposedValue)
            {
                //disposedValue = true;
                if (DbContext != null)
                {
                    DbContext.Dispose();
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                    // 例如，可以將綁定的事件解除
                }
                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。
            }
            //Win32.DestroyHandle(this.CursorFileBitmapIconServiceHandle);
        }
    }
}
