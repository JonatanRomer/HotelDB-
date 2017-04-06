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

        //oprettet singleton
        private static readonly Singleton _instance = new Singleton();

        public static Singleton Instance
        {
            get { return _instance; }
        }

        //obs af guest pga den skal opdatere ved ændringer?
        public ObservableCollection<Guest> GuestCollection { get; set; }


        //lave kald til vores persistencyservice
        //vi vil gerne lave vores kald i singleton så de er det samme på alle views.
        public void AddGuest(Guest GuestToAdd)
        {
            Persistency.PersistencyService.PostGuest(GuestToAdd);
            GuestCollection.Add(GuestToAdd);
        }

        //vi vælger at køre hent metode efter en opdatering for at få den nye liste.
        public void PutGuest(Guest GuestToPut)
        {
            Persistency.PersistencyService.PutGuest(GuestToPut);
            Hent();
        }

        //vi bruger et if statment så hvis der ikke er nogen at delete kan den ikke delete der for != når den ikke er null altså.
        public void DeleteGuest(Guest GuestToDelete)
        {
            Persistency.PersistencyService.DeleteGuest(GuestToDelete);
            if (GuestToDelete != null)
            {
                GuestCollection.Remove(GuestToDelete);
            }
        }

        // sætter vores GuestCollection . obs til at være vores get liste.
        public void Hent()
        {
                GuestCollection = Persistency.PersistencyService.GetGuest();
        }


        //ctor, vi vil gerne køre Hent hver gang så vi får en frisk liste
        public Singleton()
        {
            GuestCollection = new ObservableCollection<Guest>();
            Hent();
        }
    }
}
