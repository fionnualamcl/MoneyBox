using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain
{
    public class Bank
    {
        // International Bank Account Number(IBAN): 
        // Iso country code + Bank Identifier Code(BIC) + SortCode + Acoount Number

        public string Name { get; set; }
        public string IsoCountryCode {get; set;}
        public string BankIdentifierCode { get; set; }
        public string SortCode { get; set; }
        public Account Account { get; set; }
        public Address Address { get; set; }
    }
}
