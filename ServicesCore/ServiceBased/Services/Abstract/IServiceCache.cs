public interface IServiceCache<Input,Output>
    where Input : class
    where Output : class
{
    Output GetFromCache(Input input);
    void AddToCache(Input input, Output output);
}

public interface IServiceInvalidatesCache<Input>
    where Input : class
{
    void InvalidateCache(Input input);
}