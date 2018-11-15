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
    public class EditVehicleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private AdminViewModel viewModel;
        private NetTcpBinding binding;
        private EndpointAddress address;
        private AdminView view;

        public EditVehicleCommand(AdminViewModel viewModel, NetTcpBinding binding, EndpointAddress address, AdminView view)
        {
            this.viewModel = viewModel;
            this.binding = binding;
            this.address = address;
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

            bool result = client.EditVehicle(view.nameTextBox.Text, view.modelTextBox.Text, Convert.ToInt32(view.yearTextBox.Text), Convert.ToInt32(view.priceTextBox.Text), Convert.ToInt32(view.cenaTextBox.Text), viewModel.Selected);
            if (result)
            {
                MessageBox.Show("You have succesfully edited vehicle");
                viewModel.List = client.ReturnVehicle(0);
            }
            else
                MessageBox.Show("Vehicle is not avalible");
        }
    }
}
