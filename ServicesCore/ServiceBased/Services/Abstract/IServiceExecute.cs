public interface IServiceExecute<Input,Output>
    where Input : class
    where Output : class
{
    Output Execute(Input input);
}