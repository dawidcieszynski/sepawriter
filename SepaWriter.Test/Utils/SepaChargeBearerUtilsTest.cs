using Xunit;
using Perrich.SepaWriter.Utils;
using System;

namespace Perrich.SepaWriter.Test.Utils
{
    public class SepaChargeBearerUtilsTest
    {
        [Fact]
        public void ShouldRetrieveChargeBearerFromString()
        {
            Assert.Equal(SepaChargeBearer.CRED, SepaChargeBearerUtils.SepaChargeBearerFromString("CRED"));
            Assert.Equal(SepaChargeBearer.DEBT, SepaChargeBearerUtils.SepaChargeBearerFromString("DEBT"));
            Assert.Equal(SepaChargeBearer.SHAR, SepaChargeBearerUtils.SepaChargeBearerFromString("SHAR"));
        }

        [Fact]
        public void ShouldRejectUnknownChargeBearer()
        {
            var exception = Assert.Throws<ArgumentException>(() => { SepaChargeBearerUtils.SepaChargeBearerFromString("unknown value"); });
            Assert.Contains("Unknown Charge Bearer", exception.Message);
        }

        [Fact]
        public void ShouldRetrieveStringFromChargeBearer()
        {
            Assert.Equal("CRED", SepaChargeBearerUtils.SepaChargeBearerToString(SepaChargeBearer.CRED));
            Assert.Equal("DEBT", SepaChargeBearerUtils.SepaChargeBearerToString(SepaChargeBearer.DEBT));
            Assert.Equal("SHAR", SepaChargeBearerUtils.SepaChargeBearerToString(SepaChargeBearer.SHAR));
        }
    }
}