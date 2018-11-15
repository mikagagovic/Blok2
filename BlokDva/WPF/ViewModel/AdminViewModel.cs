using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WPF.Command;
using WPF.View;

namespace WPF.ViewModel
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        private List<Vehicle> list;
        public Vehicle selected;

        private List<Request> listRequest;
        public Request selectedRequest;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public ChangeGoldenCommand ChangeGoldenCommand { get; set; }
        public DeleteVehicleCommand DeleteVehicleCommand { get; set; }
        public EditVehicleCommand EditVehicleCommand { get; set; }

        public AdminViewModel(AdminView viewParam, NetTcpBinding binding, EndpointAddress address)
        {
            this.ChangeGoldenCommand = new ChangeGoldenCommand(this, binding, address);
            this.DeleteVehicleCommand = new DeleteVehicleCommand(this, binding, address);
            this.EditVehicleCommand = new EditVehicleCommand(this, binding, address, viewParam);

            WCFClient client = new WCFClient(binding, address, ConnectionUser.Instance().CurrentUser.Username);
            List = client.ReturnVehicle(0);
            ListRequest = client.ReturnRequest();
        }

        public Vehicle Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Selected"));
            }
        }

        public List<Vehicle> List
        {
            get { return list; }
            set
            {
                list = value;
                OnPropertyChanged(new PropertyChangedEventArgs("List"));
            }
        }

        public Request SelectedRequest
        {
            get { return selectedRequest; }
            set
            {
                selectedRequest = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedRequest"));
            }
        }

        public List<Request> ListRequest
        {
            get { return listRequest; }
            set
            {
                listRequest = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ListRequest"));
            }
        }
    }
}
