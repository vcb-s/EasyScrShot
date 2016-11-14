using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upload
{

    public enum ResponseType // Localized
    {
        Text,
        RedirectionURL,
        Headers,
        LocationHeader
    }

    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        PATCH,
        DELETE
    }

    public enum ProxyMethod // Localized
    {
        None,
        Manual,
        Automatic
    }

}
