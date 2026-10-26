using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Tools.Security
{
    [Flags]
    public enum HttpVerbs
    {
        GET = 1,
        POST = 2,
        PUT = 4,
        PATCH = 8,
        DELETE = 16,
        ALL = 31
    }
}
