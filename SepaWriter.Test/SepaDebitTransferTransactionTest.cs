using System;
using Xunit;

namespace Perrich.SepaWriter.Test
{
    public class SepaDebitTransferTransactionTest
    {
        private const string Bic = "SOGEFRPPXXX";
        private const string Iban = "FR7030002005500000157845Z02";
        private const string Name = "A_NAME";

        private readonly SepaIbanData iBanData = new SepaIbanData
        {
            Bic = Bic,
            Iban = Iban,
            Name = Name
        };

        [Fact]
        public void ShouldHaveADefaultCurrency()
        {
            var data = new SepaDebitTransferTransaction();

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
            const string mandateId = "MyMandate";
            var signatureDate = new DateTime(2012, 12, 2);
            var seqType = SepaSequenceType.FIRST;

            var data = new SepaDebitTransferTransaction
            {
                Debtor = iBanData,
                Amount = amount,
                Currency = currency,
                Id = id,
                EndToEndId = endToEndId,
                RemittanceInformation = remittanceInformation,
                DateOfSignature = signatureDate,
                MandateIdentification = mandateId,
                SequenceType = SepaSequenceType.FIRST
            };

            Assert.Equal(currency, data.Currency);
            Assert.Equal(amount, data.Amount);
            Assert.Equal(id, data.Id);
            Assert.Equal(endToEndId, data.EndToEndId);
            Assert.Equal(remittanceInformation, data.RemittanceInformation);
            Assert.Equal(Bic, data.Debtor.Bic);
            Assert.Equal(Iban, data.Debtor.Iban);
            Assert.Equal(Iban, data.Debtor.Iban);
            Assert.Equal(mandateId, data.MandateIdentification);
            Assert.Equal(signatureDate, data.DateOfSignature);
            Assert.Equal(seqType, data.SequenceType);

            var data2 = data.Clone() as SepaDebitTransferTransaction;

            Assert.NotNull(data2);
            Assert.Equal(currency, data2.Currency);
            Assert.Equal(amount, data2.Amount);
            Assert.Equal(id, data2.Id);
            Assert.Equal(endToEndId, data2.EndToEndId);
            Assert.Equal(remittanceInformation, data2.RemittanceInformation);
            Assert.Equal(Bic, data2.Debtor.Bic);
            Assert.Equal(Iban, data2.Debtor.Iban);
            Assert.Equal(mandateId, data2.MandateIdentification);
            Assert.Equal(signatureDate, data2.DateOfSignature);
            Assert.Equal(seqType, data2.SequenceType);
        }

        [Fact]
        public void ShouldRejectInvalidDebtor()
        {
            var exception = Assert.Throws<SepaRuleException>(() => { new SepaDebitTransferTransaction { Debtor = new SepaIbanData() }; });
            Assert.Contains("Debtor IBAN data are invalid.", exception.Message);
        }

        [Fact]
        public void ShouldUseADefaultSequenceType()
        {
            var transfert = new SepaDebitTransferTransaction();
            Assert.Equal(SepaSequenceType.OOFF, transfert.SequenceType);
        }
    }
}