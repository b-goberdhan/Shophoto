using Shophoto.Command;
using Shophoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Projects.AuxView
{
    public class CreateProjectVM : BaseVM
    {
        public CreateProjectVM()
        {

        }

        public event EventHandler OnGoBackClicked;
        private ICommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand = new CommandHandler(() =>
                {
                    OnGoBackClicked?.Invoke(this, null);
                }));
            }
        }
    }
}
