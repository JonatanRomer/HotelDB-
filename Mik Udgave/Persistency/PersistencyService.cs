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
        const string serverUrl = "http://hoteldatabasefrontendws.azurewebsites.net/";

        public static void PostGuest(Guest PostGuest)
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = Client.PostAsJsonAsync<Guest>("api/guests", PostGuest).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageDialog GuestAdded = new MessageDialog("Din gæst blev tilføjet");
                        GuestAdded.Commands.Add(new UICommand { Label = "Ok" });
                        GuestAdded.ShowAsync().AsTask();
                    }
                    else
                    {
                        MessageDialog Error = new MessageDialog("Error");
                        Error.Commands.Add(new UICommand { Label = "Ok" });
                        Error.ShowAsync().AsTask();
                    }
                }
                catch (Exception e)
                {
                    MessageDialog GuestAdded = new MessageDialog("Din gæst blev IKKE tilføjet");
                    GuestAdded.Commands.Add(new UICommand { Label = "Ok" });
                    GuestAdded.ShowAsync().AsTask();
                }
            }
        }

        public static ObservableCollection<Guest> GetGuest()
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                var response = Client.GetAsync("api/guests").Result;
                if (response.IsSuccessStatusCode)
                {
                    var GuestList = response.Content.ReadAsAsync<ObservableCollection<Guest>>().Result;
                    return GuestList;
                }
                return null;
            }
        }


        public static void DeleteGuest(Guest DeleteGuest)
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                string urlString = "api/guests" + DeleteGuest.GuestNavn.ToString(); 
                try
                {
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


        public static void PutGuest(Guest PutGuest)
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string urlString = "api/guests" + PutGuest.GuestNavn.ToString();
                
                try
                {
                    var response = Client.PutAsJsonAsync<Guest>(urlString, PutGuest).Result;
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
