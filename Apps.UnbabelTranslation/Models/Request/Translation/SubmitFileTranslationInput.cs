using Apps.UnbabelTranslation.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.UnbabelTranslation.Models.Request.Translation;

public class SubmitFileTranslationInput
{
    [Display("File", Description = "Only txt, html and xliff supported")]
    public File File { get; set; }

    [Display("Pipeline")]
    [DataSource(typeof(PipelineDataHandler))]
    public string PipelineId { get; set; }
}