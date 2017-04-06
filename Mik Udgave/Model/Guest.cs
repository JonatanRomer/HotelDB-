    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mik_Udgave.Model
{
    class Guest
    {

        //props
        public string GuestNavn { get; set; }

        public string GuestAddresse { get; set; }


        //ctor
        public Guest(string Navn, string Adresse)
        {
           
        }


        //overskriver vores Tostring 
        public override string ToString()
        {
            return GuestNavn + " " + GuestAddresse;
        }


    }
}
