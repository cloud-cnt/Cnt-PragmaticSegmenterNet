namespace PragmaticSegmenterNet.Tests.Unit
{
    using Xunit;

    public class SegmenterTests
    {
        [Fact]
        public void HandlesNull()
        {
            var result = Segmenter.Segment(null);

            Assert.Empty(result);
        }

        [Fact]
        public void HandlesEmptyString()
        {
            var result = Segmenter.Segment(string.Empty);

            Assert.Empty(result);
        }

        [Fact]
        public void HandlesNewLine()
        {
            var result = Segmenter.Segment("\n");

            Assert.Empty(result);
        }

        [Fact]
        public void HandlesWhitespace()
        {
            var result = Segmenter.Segment("        ");

            Assert.Empty(result);
        }

        [Fact]
        public void HandlesSimplestCase()
        {
            var result = Segmenter.Segment("Hello world. Hello.");

            Assert.Equal(new []
            {
                "Hello world.",
                "Hello."
            }, result);
        }

        [Fact]
        public void HandlesRegexContainingText()
        {
            var result = Segmenter.Segment("('$0 xyz, $1 abc, $0 def').");

            Assert.Equal("('$0 xyz, $1 abc, $0 def').", Assert.Single(result));
        }

        [Fact]
        public void KeepsSentencesInsideQuotationsTogetherByDefault()
        {
            var result = Segmenter.Segment("He said \"Go home. Stay there.\" Then he left.");

            Assert.Equal(new[]
            {
                "He said \"Go home. Stay there.\"",
                "Then he left."
            }, result);
        }

        [Fact]
        public void SegmentsInsideQuotationsWhenAsked()
        {
            var result = Segmenter.Segment("He said \"Go home. Stay there.\" Then he left.", segmentInsideQuotations: true);

            Assert.Equal(new[]
            {
                "He said \"Go home.",
                "Stay there.",
                "\" Then he left."
            }, result);
        }

        [Fact]
        public void SegmentsPastYearApostropheWhenAsked()
        {
            const string text = "Said on the 12th, '24, he could. Now don't stop. Then it ends.' Next starts here.";

            var ignored = Segmenter.Segment(text);
            var segmented = Segmenter.Segment(text, segmentInsideQuotations: true);

            Assert.Equal(new[]
            {
                "Said on the 12th, '24, he could. Now don't stop. Then it ends.'",
                "Next starts here."
            }, ignored);
            Assert.Equal(new[]
            {
                "Said on the 12th, '24, he could.",
                "Now don't stop.",
                "Then it ends.",
                "' Next starts here."
            }, segmented);
        }

        [Fact]
        public void HandlesNonBreakingSpaceText()
        {
            var result = Segmenter.Segment("Trututu\u00A01. trututu\u00A02. trututu");

            Assert.Equal(3, result.Count);
            Assert.Equal("Trututu", result[0]);
            Assert.Equal("1. trututu", result[1]);
            Assert.Equal("2. trututu", result[2]);
        }
    }
}
