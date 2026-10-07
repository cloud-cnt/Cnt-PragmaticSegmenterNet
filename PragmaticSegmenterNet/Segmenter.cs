namespace PragmaticSegmenterNet
{
    using System.Collections.Generic;

    public static class Segmenter
    {
        /// <param name="segmentInsideQuotations">
        /// When <see langword="true"/>, sentence breaks inside quotation marks are kept.
        /// The default suppresses them.
        /// </param>
        public static IReadOnlyList<string> Segment(string text, Language language = Language.English, bool cleanText = true, DocumentType documentType = DocumentType.Any, bool segmentInsideQuotations = false)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new string[0];
            }

            if (text.Length == 1)
            {
                return new [] { text };
            }

            var matchingLanguage = LanguageProvider.Get(language);

            if (cleanText)
            {
                text = Cleaner.Clean(text, matchingLanguage, documentType);
            }

            var result = Processor.Process(text, matchingLanguage, segmentInsideQuotations);

            return result;
        }
    }
}