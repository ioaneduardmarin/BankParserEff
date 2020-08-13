using System;
using System.Collections.Generic;

namespace BankParserEff
{
    public class ExtrasParser
    {
        // It's default, but you should still mention that the field is "private"
        Extras _extras;
        
        // This one should either BE a property or be wrapped by one. Preferrably the first, in this case.
        public int _lastProcessedLineIndex;

        // First off, *stop it* with the arbitrary empty lines
        // You shouldn't need more than one line of empty space at once, and you should
        // always have an empty line between methods for example

        // All these  methods, why are they public? If you don't expect to use them from outside the class, make all of them private
        // Only keep public what you expect to be used by consumers of the class, this OOP basics.
        
        // Is this method even used anymore?        
        public Extras Tag20(string sir)
        {// Why an empty line below?

            sir = sir.Replace("\n", "").Replace("\r", "");
            sir = $"\n\n\n" + sir;// Why the $ (verbatim) prefix? You're still doing concatenation unsing '+' ; also, why are you adding three newlines?
            _extras._numarReferinta = sir;
            return _extras;
        }
        public Extras Tag25(string sir)
        {

            string iban = sir;
            // Look up newline variations; MT940 seems to always have its lines ended in CR+LF (so \r\n), never just \r or just \n so you can just
            // replace \r\n in one go. 
            // 
            // Also, do the lines you're processing even still have line feeds at the end, after using ReadAllLines?
            // Because the "remarks" section of the documentation says they don't and it's lazy to not check the docs or with a debugger:
            // https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalllines?view=netcore-3.1
            _extras._iban = iban.Replace("\n", "").Replace("\r", "");
            return _extras;
        }
        public Extras Tag28(string sir)
        {

            sir = sir.Replace("\n", "").Replace("\r", "");
            _extras._nrExtras = sir.Substring(0, 5);
            _extras._nrSecventa = sir.Substring(6);
            _extras._nrSecventa = _extras._nrSecventa.Replace("\n", "").Replace("\r", "");
            return _extras;
        }


        public Extras Tag60(string sir)
        {

            if (sir.Contains(":86:"))
            { // Does this branch of the "if" EVER get used? Isn't it from when you were still not parsing line-by-line? Same goes for others like it

                _extras._codSoldInitial = sir.Substring(0, 1);
                _extras._dataSoldInitial = (DateTime.ParseExact("20" + sir.Substring(1, 6), "yyyyMMdd", null));
                _extras._valutaSoldInitial = sir.Substring(7, 3);
                _extras._sumaSoldInitial = Convert.ToDecimal((sir[10..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)).Replace("\n", "").Replace("\r", "")) / 100;
                _extras._informatiiPentruClientSoldInitial = sir.Substring(sir.IndexOf(":86:") + 4);
            }
            else
            {
                _extras._codSoldInitial = sir.Substring(0, 1);
                _extras._dataSoldInitial = (DateTime.ParseExact("20" + sir.Substring(1, 6), "yyyyMMdd", null));
                _extras._valutaSoldInitial = sir.Substring(7, 3);
                _extras._sumaSoldInitial = Convert.ToDecimal((sir[10..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)).Replace("\n", "").Replace("\r", "")) / 100;
                _extras._informatiiPentruClientSoldInitial = "";
            }
            return _extras;
        }

        public Tranzactie Tag61(string sir)
        {
            Tranzactie tranzactie = new Tranzactie();

            // I don't like the code in the "if/else"
            //
            // Any code that is COMMON should sit outside the "if/else" and be written ONLY ONCE, you're just cloning code here
            // and making it extremely hard for anyone to follow what's going on ; most of the if and the else parts share the
            // same exact code
            //
            // This applies for ALL these if/else blocks.
            //
            // By the way, you might know now, but I sure as hell don't, how the :61: tag looks, so why not write an explanation
            // in a code comment about its structure? You end up _needing_ two displays otherwise.
            if (sir.IndexOf(":86:") - sir.IndexOf("/") <= 52)
            {
                // I'm not sure this code, which is probably rolled over by from when you had just one long string, behaves as it should
                // Especially since you probably haven't checked how it handles :61: tags that are on two lines
                //
                tranzactie._dataTranzactie = (DateTime.ParseExact("20" + sir.Substring(0, 6), "yyyyMMdd", null)).Date;
                tranzactie._codTranzactie = sir.Substring(10, 1);
                tranzactie._sumaTranzactie = Convert.ToDecimal(sir[11..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)) / 100;
                tranzactie._tipTranzactie = sir.Substring(sir.IndexOf(',') + 3, 4);
                tranzactie._referintaClient = sir.Substring(sir.IndexOf(',') + 7, 16);
                tranzactie._detaliiTranzactie = "";
                tranzactie._informatiiPentruClient = sir.Substring(sir.IndexOf(":86:") + 4);
            }
            // Should this not be just an "else"?
            if (sir.IndexOf(":86:") - sir.IndexOf("/") > 52)
            {
                tranzactie._dataTranzactie = (DateTime.ParseExact("20" + sir.Substring(0, 6), "yyyyMMdd", null)).Date;
                tranzactie._codTranzactie = sir.Substring(10, 1);
                tranzactie._sumaTranzactie = Convert.ToDecimal(sir[11..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)) / 100;
                tranzactie._tipTranzactie = sir.Substring(sir.IndexOf(',') + 3, 4);
                tranzactie._referintaClient = sir.Substring(sir.IndexOf(',') + 7, 16);
                tranzactie._detaliiTranzactie = sir.Substring(sir.IndexOf(',') + 74, sir.IndexOf(":86:") - sir.IndexOf(',') + 74);
                tranzactie._informatiiPentruClient = sir.Substring(sir.IndexOf(":86:") + 4);
            }


            return tranzactie;

        }

        public Extras Tag62(string sir)
        {

            if (sir.Contains(":86:"))
            {

                _extras._codSoldFinalRezervat = sir.Substring(0, 1);
                _extras._dataSoldRezervat = (DateTime.ParseExact("20" + sir.Substring(1, 6), "yyyyMMdd", null)).Date;
                _extras._valutaSoldRezervat = sir.Substring(7, 3);
                _extras._sumaSoldRezervat = Convert.ToDecimal((sir[10..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)).Replace("\n", "").Replace("\r", "")) / 100;
                _extras._informatiiPentruClientSoldRezervat = sir.Substring(sir.IndexOf(":86:") + 4);
            }
            else
            {
                _extras._codSoldFinalRezervat = sir.Substring(0, 1);
                _extras._dataSoldRezervat = (DateTime.ParseExact("20" + sir.Substring(1, 6), "yyyyMMdd", null)).Date;
                _extras._valutaSoldRezervat = sir.Substring(7, 3);
                _extras._sumaSoldRezervat = Convert.ToDecimal((sir[10..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)).Replace("\n", "").Replace("\r", "")) / 100;
                _extras._informatiiPentruClientSoldRezervat = "";
            }


            return _extras;
        }

        public Extras Tag64(string sir)
        {

            if (sir.Contains(":86:"))
            {
                _extras._codSoldFinalDisponibil = sir.Substring(0, 1);
                _extras._dataSoldFinalDisponibil = (DateTime.ParseExact("20" + sir.Substring(1, 6), "yyyyMMdd", null)).Date;
                _extras._valutaSoldFinalDisponibil = sir.Substring(7, 3);
                _extras._sumaSoldFinalDisponibil = Convert.ToDecimal((sir[10..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)).Replace("\n", "").Replace("\r", "")) / 100;
                _extras._informatiiPentruClientSoldFinalDisponibil = sir.Substring(sir.IndexOf(":86:") + 4);
            }
            else
            {
                _extras._codSoldFinalDisponibil = sir.Substring(0, 1);
                _extras._dataSoldFinalDisponibil = (DateTime.ParseExact("20" + sir.Substring(1, 6), "yyyyMMdd", null)).Date;
                _extras._valutaSoldFinalDisponibil = sir.Substring(7, 3);
                _extras._sumaSoldFinalDisponibil = Convert.ToDecimal((sir[10..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)).Replace("\n", "").Replace("\r", "")) / 100;
                _extras._informatiiPentruClientSoldFinalDisponibil = "";
            }


            return _extras;
        }


        public Extras Tag65(string sir)
        {
            if (sir.Contains(":86:"))
            {
                _extras._codSoldDisponibil = sir.Substring(0, 1);
                _extras._dataSoldDisponibil = (DateTime.ParseExact("20" + sir.Substring(1, 6), "yyyyMMdd", null)).Date;
                _extras._valutaSoldDisponibil = sir.Substring(7, 3);
                _extras._sumaSoldDisponibil = Convert.ToDecimal((sir[10..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)).Replace("\n", "").Replace("\r", "")) / 100;
                _extras._informatiiPentruClientSoldDisponibil = sir.Substring(sir.IndexOf(":86:") + 4);
            }
            else
            {
                _extras._codSoldDisponibil = sir.Substring(0, 1);
                _extras._dataSoldDisponibil = (DateTime.ParseExact("20" + sir.Substring(1, 6), "yyyyMMdd", null)).Date;
                _extras._valutaSoldDisponibil = sir.Substring(7, 3);
                _extras._sumaSoldDisponibil = Convert.ToDecimal((sir[10..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)).Replace("\n", "").Replace("\r", "")) / 100;
                _extras._informatiiPentruClientSoldDisponibil = "";
            }
            return _extras;
        }


//...
// Why the random whitespace though?



        public Extras InternalParse(string[] liniiIntrare, int indexLiniePrimita)
        {

            _extras = new Extras();
            string randNetichetat = ""; // *Ne_e_tichetat
            bool isExtrasValid = false;

            // You don't really need linieActuala; you could write "for (;indexLiniePrimita < ....; indexLiniePrimita += 1)"
            // as you're only interesting in moving "indexLiniePrimita" forward
            //
            // Though it is usually a _bad idea_ to modify a received parameter, you should probably copy it to a local variable
            for (int linieActuala = indexLiniePrimita; linieActuala < liniiIntrare.Length; linieActuala += 1)
            {
                if (String.IsNullOrEmpty(liniiIntrare[linieActuala]))
                {
                    indexLiniePrimita = indexLiniePrimita + 1;
                    _lastProcessedLineIndex = indexLiniePrimita;

                }
                else { break; } // Why not invert the "if" check? Then you don't even need the "else" as the code after "break" won't be executed

            }
            for (int linieCurenta = indexLiniePrimita; linieCurenta < liniiIntrare.Length; linieCurenta += 1)
            {




                if (liniiIntrare[linieCurenta].StartsWith(":20:"))
                {
                    isExtrasValid = true;
                    Tag20(liniiIntrare[linieCurenta].Substring(4));
                }
                


                if (liniiIntrare[linieCurenta].StartsWith(":25:"))
                {
                    Tag25(liniiIntrare[linieCurenta].Substring(4));
                }

                if (liniiIntrare[linieCurenta].StartsWith(":28C:"))
                {
                    Tag28(liniiIntrare[linieCurenta].Substring(5));
                }

                if (liniiIntrare[linieCurenta].StartsWith(":60F:"))
                {
                    randNetichetat = ConcatenareRanduriDetalii(liniiIntrare, linieCurenta);
                    // This is a very bad idea as *you're modifying the source array* when you set
                    // this changed value on it. And you probably didn't even register this issue.
                    //
                    // WHY NOT JUST USE A LOCAL VARIABLE to store your intermediate data?!
                    // For some unknwon reason you avoid them and it makes no sense.
                    //
                    // Also applies to the rest of the places like this one
                    liniiIntrare[linieCurenta] = liniiIntrare[linieCurenta] + randNetichetat;
                    Tag60(liniiIntrare[linieCurenta].Substring(5));
                }

                if (liniiIntrare[linieCurenta].StartsWith(":61:"))
                {
                    // Be aware that the :61: tag is the only tag other than :86: that can
                    // have two lines, with the second line being "supplementary details"
                    // I'm not sure you're processing this correctly here
                    // And I think you've not checked that since you haven't created for yourself
                    // input data that contains that sort of :61: tag
                    randNetichetat = ConcatenareRanduriDetalii(liniiIntrare, linieCurenta);
                    liniiIntrare[linieCurenta] = liniiIntrare[linieCurenta] + randNetichetat;
                    _extras._tranzactii.Add(Tag61(liniiIntrare[linieCurenta].Substring(4)));
                }

                if (liniiIntrare[linieCurenta].StartsWith(":62F:"))
                {
                    //
                    // .
                    // .
                    // .
                    // Okay, after scratching my head, I figured out what you're doing here, you're concatenating
                    // several lines into one long line, and then using the code you previously had to parse the 
                    // entire line in one go. This is fine, it's one solution you could handle implementing.
                    //
                    // But it _is_ wasteful as you still end up going over any :86: fields you encounter, you should
                    // have tried to skip ahead as well.
                    //
                    // And if you don't know how to return multiple values from a method, well then
                    //
                    // a) https://lmgtfy.com/?q=how+to+return+multiple+values+c%23 (not that hard was it?)
                    //
                    // or
                    //
                    // b) ASK!
                    //
                    randNetichetat = ConcatenareRanduriDetalii(liniiIntrare, linieCurenta);
                    liniiIntrare[linieCurenta] = liniiIntrare[linieCurenta] + randNetichetat;
                    Tag62(liniiIntrare[linieCurenta].Substring(5));
                }

                if (liniiIntrare[linieCurenta].Contains(":64:"))
                {
                    randNetichetat = ConcatenareRanduriDetalii(liniiIntrare, linieCurenta);
                    liniiIntrare[linieCurenta] = liniiIntrare[linieCurenta] + randNetichetat;
                    Tag64(liniiIntrare[linieCurenta].Substring(4));
                }

                if (liniiIntrare[linieCurenta].Contains(":65:"))
                {
                    randNetichetat = ConcatenareRanduriDetalii(liniiIntrare, linieCurenta);
                    liniiIntrare[linieCurenta] = liniiIntrare[linieCurenta] + randNetichetat;
                    Tag65(liniiIntrare[linieCurenta].Substring(4));
                }


                if (liniiIntrare[linieCurenta].StartsWith("-}") || liniiIntrare[linieCurenta].Equals(""))
                    // "-}" should be saved as a constant field on the class level, as you should always avoid "magic strings"
                    // Also, we don't compare a string to "", we use string.IsNullOrWhitespace(myStr) instead
                {
                    _lastProcessedLineIndex = linieCurenta + 1;
                    break;
                }

                



            }


            if (isExtrasValid == true)
            {
                return _extras;
            }
            else
            {
                return null;
            }

            
        }



        public List<Extras> Parse(string[] liniiIntrare)
        {
            List<Extras> listaExtrase = new List<Extras>();
            _lastProcessedLineIndex = 0;

            do
            {
                Extras extras = InternalParse(liniiIntrare, _lastProcessedLineIndex);
                if (extras != null)
                {
                    listaExtrase.Add(extras);
                }
            } while (_lastProcessedLineIndex < liniiIntrare.Length);

            return listaExtrase;
        }


        public string ConcatenareRanduriDetalii(string[] liniiIntrare, int linieCurenta)
        {
            string randNetichetat = ""; // Ne_e_tichetat
            for (int linieFaraTagSauCuTag86 = linieCurenta + 1; linieFaraTagSauCuTag86 < liniiIntrare.Length; linieFaraTagSauCuTag86 += 1)
            {
                if (!liniiIntrare[linieFaraTagSauCuTag86].StartsWith(":6") && !liniiIntrare[linieFaraTagSauCuTag86].StartsWith("-}") && !String.IsNullOrWhiteSpace(liniiIntrare[linieFaraTagSauCuTag86]))
                {
                    randNetichetat += liniiIntrare[linieFaraTagSauCuTag86];

                }
                if (String.IsNullOrWhiteSpace(liniiIntrare[linieFaraTagSauCuTag86]) || liniiIntrare[linieFaraTagSauCuTag86].Contains("-}"))
                {
                    break;
                }

            }
            return randNetichetat;
        }




    }
}
