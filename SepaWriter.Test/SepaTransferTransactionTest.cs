using Xunit;

namespace Perrich.SepaWriter.Test
{
    public class SepaTransferTransactionTest
    {
        [Fact]
        public void ShouldRejectAmountGreaterOrEqualsThan1000000000()
        {
            var exception = Assert.Throws<SepaRuleException>(() => { new SepaCreditTransferTransaction { Amount = 1000000000 }; });
            Assert.Contains("Invalid amount value", exception.Message);
        }

        [Fact]
        public void ShouldRejectAmountLessThan1Cents()
        {
            var exception = Assert.Throws<SepaRuleException>(() => { new SepaCreditTransferTransaction { Amount = 0 }; });
            Assert.Contains("Invalid amount value", exception.Message);
        }

        [Fact]
        public void ShouldRejectAmountWithMoreThan2Decimals()
        {
            var exception = Assert.Throws<SepaRuleException>(() => { new SepaCreditTransferTransaction { Amount = 12.012m }; });
            Assert.Contains("Amount should have at most 2 decimals", exception.Message);
        }

        [Fact]
        public void ShouldRejectEndToEndIdGreaterThan35()
        {
            var exception = Assert.Throws<SepaRuleException>(() => { new SepaCreditTransferTransaction { EndToEndId = "012345678901234567890123456789012345" }; });
            Assert.Contains("cannot be greater than 35", exception.Message);
        }
    }
}