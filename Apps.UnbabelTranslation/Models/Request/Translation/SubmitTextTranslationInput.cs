using Apps.UnbabelTranslation.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.UnbabelTranslation.Models.Request.Translation;

public class SubmitTextTranslationInput
{
    [Display("Source text")] public string SourceText { get; set; }

    [Display("Pipeline ID")]
    [DataSource(typeof(PipelineDataHandler))]
    public string PipelineId { get; set; }
}