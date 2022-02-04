namespace CustomValidator
{
    using System.Collections.Generic;

    public interface IValidatable
    {
        IDictionary<string, List<string>> Validate();
    }
}
