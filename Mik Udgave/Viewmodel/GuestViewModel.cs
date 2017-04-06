using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mik_Udgave.Viewmodel
{
    class GuestViewModel : INotifyPropertyChanged
    {


        #region vores PropertyChangedEventHandler 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Select event prop & instance field

        private CLASSNAVN selectedÆNDRE;
        public CLASSNAVN SelectedÆNDRE
        {
            get { return selectedÆNDRE; }
            set
            {
                selectedEvent = value;
                OnPropertyChanged(nameof(SelectedÆNDRE));
            }
        }

        #endregion
    }
}
