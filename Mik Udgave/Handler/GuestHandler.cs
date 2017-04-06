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

        //herinde vil vi gerne håndtere vores guest
        //put,post,delete

        public Viewmodel.GuestViewModel gvm {get; set;}

        public GuestHandler()
        {
            
        }

        public void AddGuest()
        {
            Model.Guest TempGuest = new Model.Guest(gvm.GuestName, gvm.GuestAdress);
        }

        public void DeleteGuest()
        {
            Model.Singleton.Instance.DeleteGuest(gvm.SelectedGuest);
        }

        public void PutGuest()
        {
            Model.Singleton.Instance.PutGuest(gvm.SelectedGuest);
        }

    }
}
