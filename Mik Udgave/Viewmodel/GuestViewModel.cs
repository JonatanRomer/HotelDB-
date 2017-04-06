using Mik_Udgave.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mik_Udgave.Viewmodel
{
    class GuestViewModel : INotifyPropertyChanged
    {


        //props
        public Handler.GuestHandler GuestHandler { get; set; }

        public ICommand GetGuestCommand { get; set; }
        public ICommand PutGuestCommand { get; set; }
        public ICommand PostGuestCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        //full prop 
        //"nameof" så vi får opdateret vores view hvis GuestName ændre sig
        private string guestName;
        public string GuestName
        {
            get { return guestName; }
            set { guestName = value; OnPropertyChanged(nameof(GuestName)); }
        }

        //hvis value af addressen ændre sig skal view opdatere
        private string guestAddress;
        public string GuestAdress
        {
            get { return guestAddress; }
            set { guestAddress = value; OnPropertyChanged(nameof(GuestAdress)); }
        }

        
        private ObservableCollection<Guest> guestCollection;
        public ObservableCollection<Guest> GuestColletion
        {
            get { return guestCollection; }
            set { guestCollection = value; }
        }

        //ctor
        public GuestViewModel()
        {
            //laver instancer af vores handler samt obs af gæst
            GuestHandler = new Handler.GuestHandler();
            GuestColletion = new ObservableCollection<Guest>();
            GuestColletion = Singleton.Instance.GuestCollection;

            //vi sætter null fordi den gerne vil ha en bool men da der ikke er nogle i vores relaycommand sætter vi "intet"
            PostGuestCommand = new Common.RelayCommand(GuestHandler.AddGuest, null);
            PutGuestCommand = new Common.RelayCommand(GuestHandler.PutGuest, null);
            DeleteCommand = new Common.RelayCommand(GuestHandler.DeleteGuest, null);
        }



        //standard se evt Gist.

        #region vores PropertyChangedEventHandler 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Select event prop & instance field

        private Model.Guest selectedGuest;
        public Model.Guest SelectedGuest
        {
            get { return selectedGuest; }
            set
            {
                selectedGuest = value;
                OnPropertyChanged(nameof(SelectedGuest));
            }
        }

        #endregion

    }
}
