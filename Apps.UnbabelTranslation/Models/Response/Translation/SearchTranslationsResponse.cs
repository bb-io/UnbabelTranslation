using Apps.UnbabelTranslation.Models.Entities;

namespace Apps.UnbabelTranslation.Models.Response.Translation;

public record SearchTranslationsResponse(List<TranslationEntity> Translations);