using System;
using Xunit;
using Perrich.SepaWriter.Utils;

namespace Perrich.SepaWriter.Test.Utils
{
    public class StringUtilsTest
    {
        private const string FirstPart = "012345678";

        [Fact]
        public void ShouldTruncateATooLongString()
        {
            const string str = FirstPart + "9" + "another part";
            Assert.Equal(FirstPart + "9", StringUtils.GetLimitedString(str, 10));
        }

        [Fact]
        public void ShouldNotTruncateSmallString()
        {
            Assert.Equal(FirstPart, StringUtils.GetLimitedString(FirstPart, 10));
            Assert.Null(StringUtils.GetLimitedString(null, 10));
        }

        [Fact]
        public void ShouldNotTruncateNullString()
        {
            Assert.Null(StringUtils.GetLimitedString(null, 10));
        }

        [Fact]
        public void ShouldFormatADate()
        {
            var date = new DateTime(2013, 11, 27);
            Assert.Equal("2013-11-27T00:00:00", StringUtils.FormatDateTime(date));
        }

        [Fact]
        public void ShouldCleanUpString()
        {
            Assert.Equal(FirstPart, StringUtils.RemoveInvalidChar(FirstPart));

            var allowedChars = "@/-?:(). ,'\"+";
            Assert.Equal(allowedChars, StringUtils.RemoveInvalidChar(allowedChars));

            Assert.Equal("EAEU", StringUtils.RemoveInvalidChar("éàèù"));
        }
    }
}