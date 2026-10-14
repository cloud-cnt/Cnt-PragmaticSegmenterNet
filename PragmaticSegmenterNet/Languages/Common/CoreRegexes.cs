namespace PragmaticSegmenterNet.Languages.Common
{
    using System.Text.RegularExpressions;

    internal static class CoreRegexes
    {
        public static Regex MultiPeriodAbbreviationRegex = new Regex(@"\b[a-z](?:\.[a-z])+[.]", RegexOptions.IgnoreCase);

        public static Regex ContinuousPunctuationRegex = new Regex(@"(?<=\S)(!|\?){3,}(?=(\s|\z|$))");

        public static Regex ParenthesesBetweenDoubleQuotesRegex = new Regex(@"[""”]\s\(.*\)\s[""“]");

        public static Regex SentenceBoundaryRegex = new Regex(@"\uff08(?:[^\uff09])*\uff09(?=\s?[A-Z])|\u300c(?:[^\u300d])*\u300d(?=\s[A-Z])|\((?:[^\)]){2,}\)(?=\s[A-Z])|'(?:[^'])*[^,]'(?=\s[A-Z])|""(?:[^""])*[^,]""(?=\s[A-Z])|“(?:[^”])*[^,]”(?=\s[A-Z])|\S.*?[。．.！!?？ȸȹ☉☈☇☄]");

        // Same boundaries, without the alternatives that consume an entire quotation as one sentence.
        // Closing quotes/brackets directly after the sentence-final punctuation stay with that sentence, provided they
        // are followed by whitespace, the end of the text, or the ȸ end marker added when the text lacks final punctuation
        // (so an opening quote glued to the next sentence, as in ."Next, is not taken). Quote state is deliberately not
        // tracked: straight quotes are often unbalanced in transcripts, and one stray quote would invert every later one.
        public static Regex SentenceBoundaryInsideQuotationsRegex = new Regex(@"\uff08(?:[^\uff09])*\uff09(?=\s?[A-Z])|\((?:[^\)]){2,}\)(?=\s[A-Z])|\S.*?[。．.！!?？ȸȹ☉☈☇☄](?:[""'”’»\)\]]+(?=\s|\z|ȸ))?");
        
        public static Regex QuotationAtEndOfSentenceRegex = new Regex(@"[!?\.-][\""\'\u201d\u201c]\s{1}[A-Z]");

        public static Regex SplitSpaceQuotationAtEndOfSentenceRegex = new Regex(@"(?<=[!?\.-][\""\'\u201d\u201c])\s{1}(?=[A-Z])");
    }
}