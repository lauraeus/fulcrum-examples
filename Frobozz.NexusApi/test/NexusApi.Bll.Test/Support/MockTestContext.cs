using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frobozz.NexusApi.Bll.Test.Support
{
    public static class MockTestContext
    {
        public static string GetClientName() => "client-name";
        public static string GetServerName() => "server-name";
    }
}
