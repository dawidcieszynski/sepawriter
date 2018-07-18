using Xunit;
using Perrich.SepaWriter.Utils;
using System;

namespace Perrich.SepaWriter.Test.Utils
{
    public class SepaSequenceTypeUtilsTest
    {
        [Fact]
        public void ShouldRetrieveSequenceTypeFromString()
        {
            Assert.Equal(SepaSequenceType.OOFF, SepaSequenceTypeUtils.SepaSequenceTypeFromString("OOFF"));
            Assert.Equal(SepaSequenceType.FIRST, SepaSequenceTypeUtils.SepaSequenceTypeFromString("FRST"));
            Assert.Equal(SepaSequenceType.RCUR, SepaSequenceTypeUtils.SepaSequenceTypeFromString("RCUR"));
            Assert.Equal(SepaSequenceType.FINAL, SepaSequenceTypeUtils.SepaSequenceTypeFromString("FNAL"));
        }

        [Fact]
        public void ShouldRejectUnknownSequenceType()
        {
            var exception = Assert.Throws<ArgumentException>(() => { SepaSequenceTypeUtils.SepaSequenceTypeFromString("unknown value"); });
            Assert.Contains("Unknown Sequence Type", exception.Message);
        }

        [Fact]
        public void ShouldRetrieveStringFromSequenceType()
        {
            Assert.Equal("OOFF", SepaSequenceTypeUtils.SepaSequenceTypeToString(SepaSequenceType.OOFF));
            Assert.Equal("FRST", SepaSequenceTypeUtils.SepaSequenceTypeToString(SepaSequenceType.FIRST));
            Assert.Equal("RCUR", SepaSequenceTypeUtils.SepaSequenceTypeToString(SepaSequenceType.RCUR));
            Assert.Equal("FNAL", SepaSequenceTypeUtils.SepaSequenceTypeToString(SepaSequenceType.FINAL));
        }
    }
}