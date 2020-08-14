using System;

namespace BankParserEff
{
    public class Tranzactie
    {
       // Why the wrong indenting?
        
        // These should be properties
        // https://stackoverflow.com/a/295109
        
        // Also ONLY PRIVATE fields should be prefixed with _ (as mentioned)
        // And also fields should never need to be private, we have properties for that
        public DateTime _dataTranzactie;
        public string _codTranzactie;
        public decimal _sumaTranzactie;
        public string _tipTranzactie;
        public string _referintaClient;

        public string _detaliiTranzactie;

        public string _informatiiPentruClient;
             
// The indenting here is wrong
// Visual Studio has a code cleanup feature, USE IT! LEARN THE SHORTCUTS!
// https://docs.microsoft.com/en-us/visualstudio/ide/code-styles-and-code-cleanup?view=vs-2019#apply-code-styles
}
}
