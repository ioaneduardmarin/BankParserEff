using System;
using System.Collections.Generic;
using System.Text;

namespace BankParserEff
{
    public class Extras
    {
        public string _numarReferinta;
        public string _iban;
        public string _nrExtras;
        public string _nrSecventa;
       
        public string _codSoldInitial;
        public DateTime _dataSoldInitial;
        public string _valutaSoldInitial;
        public decimal _sumaSoldInitial;
        public string _informatiiPentruClientSoldInitial;

        public List<Tranzactie> _tranzactii=new List<Tranzactie>();
       
        public string _codSoldFinalRezervat;
        public DateTime _dataSoldRezervat;
        public string _valutaSoldRezervat;
        public decimal _sumaSoldRezervat;
        public string _informatiiPentruClientSoldRezervat;

        public string _codSoldFinalDisponibil;
        public DateTime _dataSoldFinalDisponibil;
        public string _valutaSoldFinalDisponibil;
        public decimal _sumaSoldFinalDisponibil;
        public string _informatiiPentruClientSoldFinalDisponibil;

        public string _codSoldDisponibil;
        public DateTime _dataSoldDisponibil;
        public string _valutaSoldDisponibil;
        public decimal _sumaSoldDisponibil;
        public string _informatiiPentruClientSoldDisponibil;


        






    }
}
