using Xunit;

namespace Perrich.SepaWriter.Test
{
    public class SepaIbanDataTest
    {
        private const string Bic = "SOGEFRPPXXX";
        private const string Iban = "FR7030002005500000157845Z02";
        private const string IbanWithSpace = "FR70 30002  005500000157845Z    02";
        private const string Name = "A_NAME";

        [Fact]
        public void ShouldBeValidIfAllDataIsNotNull()
        {
            var data = new SepaIbanData
            {
                Bic = Bic,
                Iban = Iban,
                Name = Name
            };

            Assert.True(data.IsValid);
        }

        [Fact]
        public void ShouldBeValidIfAllDataIsNotNullAndBicIsUnknown()
        {
            var data = new SepaIbanData
            {
                UnknownBic = true,
                Iban = Iban,
                Name = Name
            };

            Assert.True(data.IsValid);
        }

        [Fact]
        public void ShouldRemoveSpaceInIban()
        {
            var data = new SepaIbanData
            {
                Bic = Bic,
                Iban = IbanWithSpace,
                Name = Name
            };

            Assert.True(data.IsValid);
            Assert.Equal(Iban, data.Iban);
        }

        [Fact]
        public void ShouldKeepNameIfLessThan70Chars()
        {
            var data = new SepaIbanData
            {
                Bic = Bic,
                Iban = Iban,
                Name = Name
            };

            Assert.Equal(Bic, data.Bic);
            Assert.Equal(Name, data.Name);
            Assert.Equal(Iban, data.Iban);
        }

        [Fact]
        public void ShouldNotBeValidIfBicIsNull()
        {
            var data = new SepaIbanData
            {
                Iban = Iban,
                Name = Name
            };

            Assert.False(data.IsValid);
        }

        [Fact]
        public void ShouldNotBeValidIfIbanIsNull()
        {
            var data = new SepaIbanData
            {
                Bic = Bic,
                Name = Name
            };

            Assert.False(data.IsValid);
        }

        [Fact]
        public void ShouldNotBeValidIfNameIsNull()
        {
            var data = new SepaIbanData
            {
                Bic = Bic,
                Iban = Iban
            };

            Assert.False(data.IsValid);
        }

        [Fact]
        public void ShouldReduceNameIfGreaterThan70Chars()
        {
            const string longName = "12345678901234567890123456789012345678901234567890123456789012345678901234567890";
            const string expectedName = "1234567890123456789012345678901234567890123456789012345678901234567890";
            var data = new SepaIbanData
            {
                Bic = Bic,
                Iban = Iban,
                Name = longName
            };

            Assert.Equal(expectedName, data.Name);
        }

        [Fact]
        public void ShouldRejectBadBic()
        {
            var exception = Assert.Throws<SepaRuleException>(() => { new SepaIbanData { Bic = "BIC" }; });
            Assert.Contains("Null or Invalid length of BIC", exception.Message);
        }

        [Fact]
        public void ShouldRejectTooLongIban()
        {
            var exception = Assert.Throws<SepaRuleException>(() => { new SepaIbanData { Iban = "FR012345678901234567890123456789012" }; });
            Assert.Contains("Null or Invalid length of IBAN code", exception.Message);
        }

        [Fact]
        public void ShouldRejectTooShortIban()
        {
            var exception = Assert.Throws<SepaRuleException>(() => { new SepaIbanData { Iban = "FR01234567890" }; });
            Assert.Contains("Null or Invalid length of IBAN code", exception.Message);
        }
    }
}