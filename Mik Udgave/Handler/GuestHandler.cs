using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mik_Udgave.Handler
{
    class GuestHandler
    {

        //vi laver en prop af GuestViewModel så vi kan få fat i den til vores metoder
        public Viewmodel.GuestViewModel gvm {get; set;}

        //ctor
        //test->
        public GuestHandler()
        {
           
        }


        //metode. vi laver en tempguest i guest, parameter er altså navenet og adress. husk gvm er er viewmodel
        public void AddGuest()
        {
            Model.Guest TempGuest = new Model.Guest(gvm.GuestName, gvm.GuestAdress);
            Model.Singleton.Instance.AddGuest(TempGuest);
            Model.Singleton.Instance.Hent();
        }

        //vi sætter vores delete til at være = den selectedguest vi har fra igen vores viewmodel - men bliver håndteret her.
        public void DeleteGuest()
        {
            Model.Singleton.Instance.DeleteGuest(gvm.SelectedGuest);
        }

        //vi vælger at sætte vores put med selectedguest, da vi vælger den fra en liste og trykker på "putGuest"
        public void PutGuest()
        {
            Model.Singleton.Instance.PutGuest(gvm.SelectedGuest);
        }

    }
}
