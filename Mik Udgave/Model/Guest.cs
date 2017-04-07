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
        public string Name { get; set; }

        public string Address { get; set; }

        public int Guest_No { get; set; }


        //ctor
        public Guest(string Navn, string Adresse)
        {
            this.Name = Navn;
            this.Address = Adresse;
        }


        //overskriver vores Tostring 
        public override string ToString()
        {
            return "ID:" + Guest_No + " " + Name + " " + Address;
        }


    }
}
