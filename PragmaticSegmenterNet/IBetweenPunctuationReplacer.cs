namespace PragmaticSegmenterNet
{
    internal interface IBetweenPunctuationReplacer
    {
        string Replace(string text, bool segmentInsideQuotations);
    }
}