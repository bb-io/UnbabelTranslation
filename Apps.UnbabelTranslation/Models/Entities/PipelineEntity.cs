namespace Apps.UnbabelTranslation.Models.Entities;

public class PipelineEntity
{
    public string Id { get; set; }
    
    public string? Name { get; set; }
    
    public PipelineGroupEntity Group { get; set; }

    public string DisplayName => Name ?? $"{Id}-{Group.Name}";
}