using Shophoto.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Common
{
    public class DeleteConfirmationBarVM : ConfirmationBarVM
    {
        public event EventHandler OnDeleteConfirmed;
        public DeleteConfirmationBarVM() : base()
        {
           
        }

        public override string ConfrimationText
        {
            get { return "Are you sure you want to delete?"; }
        }

        private ICommand _okClickedCommand;
        public override ICommand OkClickedCommand
        {
            get
            {
                return _okClickedCommand ?? (_okClickedCommand = new CommandHandler(() =>
                {
                    base.IsOnConfirmation = true;
                }));
            }
            
        }

        private ICommand _cancelClickedCommand;
        public override ICommand CancelClickedCommand
        {
            get
            {
                return _cancelClickedCommand ?? (_cancelClickedCommand = new CommandHandler(() =>
                {
                    Reset();
                }));
            }
        }

        private ICommand _confirmationYesCommand;
        public override ICommand ConfirmationYesClickedCommand
        {
            get
            {
                return _confirmationYesCommand ?? (_confirmationYesCommand = new CommandHandler(() =>
                {
                    
                    OnDeleteConfirmed?.Invoke(this, null);
                    Reset();
                }));
            }
        }

        private ICommand _confirmationNoCommand;
        public override ICommand ConfirmationNoClickedCommand
        {
            get
            {
                return _confirmationNoCommand ?? (_confirmationNoCommand = new CommandHandler(() =>
                {
                    Reset();
                }));
            }
        }
        //NOTE THAT RESET SHOULD ALWAYS BE CALLED AFTER YOU HAVE DONE ALL YOUR
        //COMPUTING
        private void Reset()
        {
            base.IsVisible = false;
            base.IsOnConfirmation = false;
            base.NumberSelected = 0;
        }
       
    }
}
