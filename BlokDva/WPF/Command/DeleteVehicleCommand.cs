using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF.ViewModel;

namespace WPF.Command
{
    public class DeleteVehicleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AdminViewModel viewModel;
        private NetTcpBinding binding;
        private EndpointAddress address;

        public DeleteVehicleCommand(AdminViewModel viewModel, NetTcpBinding binding, EndpointAddress address)
        {
            this.viewModel = viewModel;
            this.binding = binding;
            this.address = address;
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

            bool result = client.DeleteVehicle(viewModel.Selected);
            if (result)
            {
                MessageBox.Show("You have succesfully deleted vehicle");
                viewModel.List = client.ReturnVehicle(0);
            }
            else
                MessageBox.Show("Vehicle is not avalible");
        }
    }
}
