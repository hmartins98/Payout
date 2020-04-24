namespace ServiceBased.RequestMessages
{
    public class RequestMessageBase<IRequest, IContext, IRepository>
        where IRequest : class
        where IContext : class
        where IRepository : class
    {
        public IRequest Request { get; set; }
        public IContext Context { get; set; }
        public IRepository Repository { get; set; }
    }

    public class RequestMessageBaseWithCache<IRequest, IContext, IRepository, ICacheRepository> : RequestMessageBase<IRequest, IContext, IRepository>
        where IRequest : class
        where IContext : class
        where IRepository : class
        where ICacheRepository : class
    {
        public ICacheRepository CacheRepository { get; set; }
    }
}
