using System;

public class ServiceBase<IValidate, IExecute, Input, Output>
    where IValidate : IServiceValidate<Input>, new()
    where IExecute : IServiceExecute<Input, Output>, new()
    where Input : class
    where Output : class
{

    public virtual Output Run(Input input)
    {
        try
        {
            IExecute e = new IExecute();
            IValidate v = new IValidate();

            v.Validate(input);
            return e.Execute(input);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

public class ServiceBaseInvalidatesCache<IValidate, ICache, IExecute, Input, Output>
    where IValidate : IServiceValidate<Input>, new()
    where ICache : IServiceInvalidatesCache<Input>, new()
    where IExecute : IServiceExecute<Input, Output>, new()
    where Input : class
    where Output : class
{

    public virtual Output Run(Input input)
    {
        try
        {
            IExecute e = new IExecute();
            ICache c = new ICache();
            IValidate v = new IValidate();

            v.Validate(input);

            Output output = e.Execute(input);
            c.InvalidateCache(input);
            return output;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

public class ServiceBaseWithCache<IValidate, ICache, IExecute, Input, Output> : ServiceBase<IValidate, IExecute, Input, Output>
    where IValidate : IServiceValidate<Input>, new()
    where ICache : IServiceCache<Input, Output>, new()
    where IExecute : IServiceExecute<Input, Output>, new()
    where Input : class
    where Output : class
{

    public override Output Run(Input input)
    {
        try
        {
            IExecute e = new IExecute();
            ICache c = new ICache();
            IValidate v = new IValidate();

            v.Validate(input);

            Output output = c.GetFromCache(input);
            if (output != null && output != default(Output))
                return output;

            output = e.Execute(input);
            c.AddToCache(input, output);
            return output;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}