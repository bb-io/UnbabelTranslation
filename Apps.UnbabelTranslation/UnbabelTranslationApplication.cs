using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.UnbabelTranslation;

public class UnbabelTranslationApplication : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.MachineTranslationAndMtqe];
        set { }
    }
    
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