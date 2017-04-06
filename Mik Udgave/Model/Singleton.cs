using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Mik_Udgave.Model
{
    class Singleton
    {

        //oprette singleton
        private static readonly Singleton _instance = new Singleton();

        public static Singleton Instance
        {
            get { return _instance; }
        }

        public ObservableCollection<Guest> GuestCollection { get; set; }


        //lave kald til vores persistencyservice
        public void AddGuest(Guest GuestToAdd)
        {
            Persistency.PersistencyService.PostGuest(GuestToAdd);
            GuestCollection.Add(GuestToAdd);
        }

        public void PutGuest(Guest GuestToPut)
        {
            Persistency.PersistencyService.PutGuest(GuestToPut);
            Hent();
        }

        public void DeleteGuest(Guest GuestToDelete)
        {
            Persistency.PersistencyService.DeleteGuest(GuestToDelete);
            if (GuestToDelete != null)
            {
                GuestCollection.Remove(GuestToDelete);
            }
        }

        public void Hent()
        {
            try
            {
                GuestCollection = Persistency.PersistencyService.GetGuest();
            }
            catch (Exception e)
            {
                MessageDialog Error = new MessageDialog("Error : " + e);
                Error.Commands.Add(new UICommand { Label = "Ok" });
                Error.ShowAsync().AsTask();
            }
        }


        //add,dele post, get
        //husk ctor

        public Singleton()
        {
            GuestCollection = new ObservableCollection<Guest>();
            Hent();
        }
    }
}
