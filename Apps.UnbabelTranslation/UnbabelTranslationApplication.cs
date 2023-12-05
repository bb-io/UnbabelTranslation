using Blackbird.Applications.Sdk.Common;

namespace Apps.UnbabelTranslation;

public class UnbabelTranslationApplication : IApplication
{
    public string Name
    {
        get => "Unbabel Translation";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}