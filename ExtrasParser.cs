using System;
using System.Collections.Generic;

namespace BankParserEff
{
    public class ExtrasParser
    {
        Extras _extras;
        public int _lastProcessedLineIndex;


        public Extras Tag20(string sir)
        {

            sir = sir.Replace("\n", "").Replace("\r", "");
            sir = $"\n\n\n" + sir;
            _extras._numarReferinta = sir;
            return _extras;
        }
        public Extras Tag25(string sir)
        {

            string iban = sir;
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
            {

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

            if (sir.IndexOf(":86:") - sir.IndexOf("/") <= 52)
            {
                tranzactie._dataTranzactie = (DateTime.ParseExact("20" + sir.Substring(0, 6), "yyyyMMdd", null)).Date;
                tranzactie._codTranzactie = sir.Substring(10, 1);
                tranzactie._sumaTranzactie = Convert.ToDecimal(sir[11..sir.IndexOf(',')] + sir.Substring(sir.IndexOf(','), 3)) / 100;
                tranzactie._tipTranzactie = sir.Substring(sir.IndexOf(',') + 3, 4);
                tranzactie._referintaClient = sir.Substring(sir.IndexOf(',') + 7, 16);
                tranzactie._detaliiTranzactie = "";
                tranzactie._informatiiPentruClient = sir.Substring(sir.IndexOf(":86:") + 4);
            }

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






        public Extras InternalParse(string[] liniiIntrare, int indexLiniePrimita)
        {

            _extras = new Extras();
            string randNetichetat = "";
            bool isExtrasValid = false;

            for (int linieActuala = indexLiniePrimita; linieActuala < liniiIntrare.Length; linieActuala += 1)
            {
                if (String.IsNullOrEmpty(liniiIntrare[linieActuala]))
                {
                    indexLiniePrimita = indexLiniePrimita + 1;
                    _lastProcessedLineIndex = indexLiniePrimita;

                }
                else { break; }

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
                    liniiIntrare[linieCurenta] = liniiIntrare[linieCurenta] + randNetichetat;
                    Tag60(liniiIntrare[linieCurenta].Substring(5));
                }

                if (liniiIntrare[linieCurenta].StartsWith(":61:"))
                {
                    randNetichetat = ConcatenareRanduriDetalii(liniiIntrare, linieCurenta);
                    liniiIntrare[linieCurenta] = liniiIntrare[linieCurenta] + randNetichetat;
                    _extras._tranzactii.Add(Tag61(liniiIntrare[linieCurenta].Substring(4)));
                }

                if (liniiIntrare[linieCurenta].StartsWith(":62F:"))
                {
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
            string randNetichetat = "";
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
