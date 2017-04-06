using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Mik_Udgave.Model;

namespace Mik_Udgave.Persistency
{
    class PersistencyService
    {
        //vi laver vores serverurl, via en const string 
        const string serverUrl = "http://hoteldatabasefrontendws.azurewebsites.net/";


        //starter vores postguest metode og giver den en guest parameter.
        public static void PostGuest(Guest PostGuest)
        {
            //åbner til vores database(altså hvor vi kan lave kald til den):
            using (var Client = new HttpClient())
            {
                //her sætter vi hvad vores addresse er, man kunne skrive en string ind i parameter men lav kopling er lækkert
                Client.BaseAddress = new Uri(serverUrl);
                //clear så vi selv kan bestemme hvad der står i headers
                Client.DefaultRequestHeaders.Clear();
                //vi sætter nye headders ind og siger det skal være application/json
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //vi laver en try så hele programmet ikke lukker hvis vi ikke kan få forbindelse
                try
                {
                    //vi sætter vores response at den skal post via json og siger det skal ske via api/guests med "PostGuest" som parameter
                    var response = Client.PostAsJsonAsync("api/guests", PostGuest).Result;

                    //hvis der er forbindelse og det virker går vi til vores if
                    if (response.IsSuccessStatusCode)
                    {
                        //standard messageDialog (:
                        MessageDialog GuestAdded = new MessageDialog("Din gæst blev tilføjet");
                        GuestAdded.Commands.Add(new UICommand { Label = "Ok" });
                        GuestAdded.ShowAsync().AsTask();
                    }
                }
                catch (Exception e)
                {
                    MessageDialog GuestAdded = new MessageDialog("Din gæst blev IKKE tilføjet" + e);
                    GuestAdded.Commands.Add(new UICommand { Label = "Ok" });
                    GuestAdded.ShowAsync().AsTask();
                }
            }
        }

        //vi vil gerne ha obs af gæst så den opdatere viewet
        public static ObservableCollection<Guest> GetGuest()
        {
            //åbner op så vi kan lave kald
            using (var Client = new HttpClient())
            {
                //sætter base addresse
                Client.BaseAddress = new Uri(serverUrl);
                //suger vu gerne vil ha svarende tilbage fra api/guests
                var response = Client.GetAsync("api/guests").Result;

                //hvis det virker! går vi til if
                if (response.IsSuccessStatusCode)
                {
                    //vi laver en liste denne gang kaldet GuestList, af vores svar fra DB
                    var GuestList = response.Content.ReadAsAsync<ObservableCollection<Guest>>().Result;
                    //den retunere vi så
                    return GuestList;
                }
                //får vi ikke noget svar smider vi altså null tilbage ingen ting. det er ikke en void metode så den skal ha en return
                return null;
            }
        }

        
        public static void DeleteGuest(Guest DeleteGuest)
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                //vi siger +deleteguest.navn for at slette !! dette er normalt en okay idé men det skal være uni, hvad hvis 2 hedder Mik?
                string urlString = "api/guests" + DeleteGuest.GuestNavn.ToString(); 
                try
                {
                    //her bruger vi så vores url string vi før lavede til at slette den "url" altså brugeren
                    var response = Client.DeleteAsync(urlString).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageDialog GuestDeleted = new MessageDialog("Gæst slettet");
                        GuestDeleted.Commands.Add(new UICommand { Label = "Ok" });
                        GuestDeleted.ShowAsync().AsTask();
                    }
                }
                catch (Exception e)
                {
                    MessageDialog Error = new MessageDialog("Fejl, gæst blev IKKE slettet" + e);
                    Error.Commands.Add(new UICommand { Label = "Ok" });
                    Error.ShowAsync().AsTask();
                }    
            }
        }

        //opdaterings metode "put"
        public static void PutGuest(Guest PutGuest)
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //vi laver en ny og det bliver i api/guests + guestnavn ja guestnavn er stadig dumt og skal være unikt. gerne et tlf nr.
                string urlString = "api/guests" + PutGuest.GuestNavn.ToString();
                
                try
                {
                    //her bruger vi vores put metode sammen emd putguest bemærk putguest er af Guest klassen derfor kan vi opdatere (;
                    var response = Client.PutAsJsonAsync(urlString, PutGuest).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageDialog GuestUpdated = new MessageDialog("Gæst opdateret");
                        GuestUpdated.Commands.Add(new UICommand { Label = "Ok" });
                        GuestUpdated.ShowAsync().AsTask();
                    }
                }
                catch (Exception e)
                {
                    MessageDialog Error = new MessageDialog("Fejl, gæst blev IKKE opdateret" + e);
                    Error.Commands.Add(new UICommand { Label = "Ok" });
                    Error.ShowAsync().AsTask();
                }
            }
        }
    }
}
