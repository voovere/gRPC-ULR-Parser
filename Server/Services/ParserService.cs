using System;
using System.Threading.Tasks;
using System.Web;
using Parse;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Server
{
    public class ParserService : Parser.ParserBase
    {
        private readonly ILogger _logger;

        public ParserService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ParserService>();
        }

        public override Task<ParserReply> ParseUrl(ParserRequest request, ServerCallContext context)
        {
            Uri tmp = new Uri(request.Url);

            _logger.LogInformation($"Parsing url: {request.Url}");
            return Task.FromResult(new ParserReply { Scheme = tmp.Scheme,
                                                    Host = tmp.Host,
                                                    Path = HttpUtility.UrlDecode(tmp.AbsolutePath),
                                                    Query = tmp.Query });
        }
    }
}
