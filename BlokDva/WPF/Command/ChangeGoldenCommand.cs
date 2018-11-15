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
    public class ChangeGoldenCommand : ICommand     
    {
        public event EventHandler CanExecuteChanged;
        
        private AdminViewModel viewModel;
        private NetTcpBinding binding;
        private EndpointAddress address;

        public ChangeGoldenCommand(AdminViewModel viewModel, NetTcpBinding binding, EndpointAddress address)
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
            if (viewModel.SelectedRequest == null)
                return;

            WCFClient client = new WCFClient(binding, address, ConnectionUser.Instance().CurrentUser.Username); 

            bool result = client.DoRequest(viewModel.SelectedRequest); 
            if (result)
            {
                MessageBox.Show("You have succesfully change users golden membership");
                viewModel.ListRequest = client.ReturnRequest(); 
            }
            else
                MessageBox.Show("Error");
        }
    }
}
