using Xunit;

namespace Perrich.SepaWriter.Test
{
    public class SepaCreditTransferTransactionTest
    {
        private const string Bic = "SOGEFRPPXXX";
        private const string Iban = "FR7030002005500000157845Z02";
        private const string Name = "A_NAME";

        private readonly SepaIbanData _iBanData = new SepaIbanData
        {
            Bic = Bic,
            Iban = Iban,
            Name = Name
        };

        [Fact]
        public void ShouldHaveADefaultCurrency()
        {
            var data = new SepaCreditTransferTransaction();

            Assert.Equal("EUR", data.Currency);
        }

        [Fact]
        public void ShouldKeepProvidedData()
        {
            const decimal amount = 100m;
            const string currency = "USD";
            const string id = "Batch1";
            const string endToEndId = "Batch1/Row2";
            const string remittanceInformation = "Sample";

            var data = new SepaCreditTransferTransaction
            {
                Creditor = _iBanData,
                Amount = amount,
                Currency = currency,
                Id = id,
                EndToEndId = endToEndId,
                RemittanceInformation = remittanceInformation
            };

            Assert.Equal(currency, data.Currency);
            Assert.Equal(amount, data.Amount);
            Assert.Equal(id, data.Id);
            Assert.Equal(endToEndId, data.EndToEndId);
            Assert.Equal(remittanceInformation, data.RemittanceInformation);
            Assert.Equal(Bic, data.Creditor.Bic);
            Assert.Equal(Iban, data.Creditor.Iban);

            var data2 = data.Clone() as SepaCreditTransferTransaction;

            Assert.NotNull(data2);
            Assert.Equal(currency, data2.Currency);
            Assert.Equal(amount, data2.Amount);
            Assert.Equal(id, data2.Id);
            Assert.Equal(endToEndId, data2.EndToEndId);
            Assert.Equal(remittanceInformation, data2.RemittanceInformation);
            Assert.Equal(Bic, data2.Creditor.Bic);
            Assert.Equal(Iban, data2.Creditor.Iban);
        }

        [Fact]
        public void ShouldRejectInvalidCreditor()
        {
            var exception = Assert.Throws<SepaRuleException>(() => { new SepaCreditTransferTransaction { Creditor = new SepaIbanData() }; });

            Assert.Contains("Creditor IBAN data are invalid.", exception.Message);
        }
    }
}