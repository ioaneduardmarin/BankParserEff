using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace BankParserEff
{
    class Program
    {
        static void Main()
        {
            ExtrasParser extrasParser = new ExtrasParser();
            string[] randuri = File.ReadAllLines(@"C:\Users\User1\Downloads\bin\STA\3.STA");
            
            extrasParser.Parse(randuri);
         }


    }
}
