using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF.View;
using WPF.ViewModel;

namespace WPF.Command
{
    public class RentVehicleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private RegularViewModel viewModel;
        private NetTcpBinding binding;
        private EndpointAddress address;
        private string endDate;
        private RegularView view;

        public RentVehicleCommand(RegularViewModel viewModel, NetTcpBinding binding, EndpointAddress address, string endDate, RegularView view)
        {
            this.viewModel = viewModel;
            this.binding = binding;
            this.address = address;
            this.endDate = endDate;
            this.view = view;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (viewModel.Selected == null)
                return;

            WCFClient client = new WCFClient(binding, address, ConnectionUser.Instance().CurrentUser.Username); 

            int result = client.MakeRent(ConnectionUser.Instance().CurrentUser, viewModel.Selected, endDate);
            if (result == 0)
                MessageBox.Show("Error!");
            else if (result == 1)
                MessageBox.Show("You cant rent any more vehicles, you need to return some vehicles to rent new one");
            else if (result == 2)
            {
                MessageBox.Show("Success");
                viewModel.List = client.ReturnVehicle(1);

                view.billTextBox.Text = client.ReturnBill(ConnectionUser.Instance().CurrentUser).ToString();
            }
            else if (result == 3)
                MessageBox.Show("End time has to be higher then start time!");
            else if (result == 4)
                MessageBox.Show("You have to declare end time!");
            else
                MessageBox.Show("You dont have enough funds to make a rent");
        }
    }
}
