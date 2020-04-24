using Grpc.Core;
using GrpcProxy.Attributes;
using GrpcProxy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace GrpcProxy.Middleware
{
    public class ValidateAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidateAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IDistributedCache distributedCache)
        {
            //Get Call endpoint
            Endpoint endpoint = context?.Features?.Get<IEndpointFeature>()?.Endpoint;

            //try to get AllowAnonymousAttribute from method of endpoint
            AllowAnonymousAttribute allowAnonymousAttribute = endpoint?.Metadata?.GetMetadata<AllowAnonymousAttribute>();

            //in case methodn does not contain AllowAnonymousAttribute, check session credentials from header
            if (allowAnonymousAttribute == null) {
                //try to get header of AuthID
                StringValues AuthID = context.Request.Headers["AuthID"];
                //try to get header of SessionKey
                StringValues SessionKey = context.Request.Headers["SessionKey"];
                
                //in case of any of the headers is missing or is greater than 1 send error of credentials required
                if (AuthID.Count != 1 && SessionKey.Count != 1)
                    throw new RpcException(new Status(StatusCode.Unauthenticated, "Auth Credentials Required!"));

                //Create an Session object with received credentials to verify if session is valid
                Session session = new Session()
                {
                    AuthID = Guid.Parse(AuthID[0]),
                    SessionKey = Guid.Parse(SessionKey[0]),
                };
                
                byte[] byteSessionKey = distributedCache.Get(AuthID[0]);

                //if session is not valid send error that credentials are invalid
                if (byteSessionKey == null || byteSessionKey == default)
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Auth Credentials Invalid or Session Expired!"));

                //Validates if session Key is correct
                Guid sessionKeyStored = new Guid(byteSessionKey);
                Guid sessionKeyRequested = Guid.Parse(SessionKey[0]);

                if (!sessionKeyStored.Equals(sessionKeyRequested))
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Auth Credentials Invalid!"));
            }

            //go to next middleware or endpoint
            await _next.Invoke(context);
        }
    }
}
