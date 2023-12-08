using System.Net.Mime;
using System.Text;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.UnbabelTranslation.Models.Request.Translation;

public class SubmitTranslationRequest
{
    public string TextFormat { get; set; }

    public string SourceText { get; set; }

    public string PipelineId { get; set; }

    public SubmitTranslationRequest(SubmitTextTranslationInput input)
    {
        SourceText = input.SourceText;
        PipelineId = input.PipelineId;
        TextFormat = "txt";
    }

    public SubmitTranslationRequest(SubmitFileTranslationInput input)
    {
        PipelineId = input.PipelineId;
        TextFormat = GetFileTextFormat(input.File);
        SourceText = Encoding.UTF8.GetString(input.File.Bytes);
    }

    private string GetFileTextFormat(File inputFile)
    {
        var textFormat = inputFile.ContentType switch
        {
            MediaTypeNames.Text.Html => "html",
            MediaTypeNames.Text.Plain => "txt",
            MediaTypeNames.Application.Xml => "xliff",
            "application/xliff+xml" => "xliff",
            _ => null
        };

        textFormat ??= Path.GetExtension(inputFile.Name)?.ToLower() switch
        {
            ".html" => "html",
            ".txt" => "txt",
            ".xliff" => "xliff",
            _ => throw new("Wrong file format, Unbabel accepts only HTML, TXT or XLIFF files")
        };

        return textFormat;
    }
}