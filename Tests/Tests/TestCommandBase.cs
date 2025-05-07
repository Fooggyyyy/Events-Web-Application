using Events_Web_Application.src.Infastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests
{
    public class TestCommandBase : IDisposable
    {
        protected AppDbContext context;

        public TestCommandBase()
        {
             context = AppDBContextFactory.Create();
        }
        public void Dispose()
        {
            AppDBContextFactory.Destroy(context);
        }
    }
}
