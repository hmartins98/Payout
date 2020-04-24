public interface IServiceValidate<Input>
    where Input : class
{
    void Validate(Input input);
}