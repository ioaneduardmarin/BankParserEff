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
            // I'm sorry, but MY computer doesn't have the same paths as yours (as is usually the case)
            // Either use relative paths or figure out a different way to load the data
            string[] randuri = File.ReadAllLines(@"C:\Users\User1\Downloads\bin\STA\3.STA");
            
            extrasParser.Parse(randuri);
         }


    }
}
