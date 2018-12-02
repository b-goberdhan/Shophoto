using Shophoto.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Buttons
{
    public class CollectionsFABButtonVM : FABPlusButtonVM
    {
        public event EventHandler OnUploadClicked;
        public event EventHandler OnDeleteClicked;

        public override bool HasDeleteButton => true;
        public override bool HasDownloadButton => true;
        public override bool HasUploadButton => true;
        public override bool HasTagButton => true;

        private ICommand _uploadButtonCommand;
        public override ICommand UploadButtonCommand
        {
            get
            {
                return _uploadButtonCommand ?? (_uploadButtonCommand = _uploadButtonCommand = new CommandHandler(() =>
                {
                    OnUploadClicked?.Invoke(this, null);
                }));
            }
        }

        private ICommand _deleteButtonCommand;
        public override ICommand DeleteButtonCommand
        {
            get
            {
                return _deleteButtonCommand ?? (_deleteButtonCommand = new CommandHandler(() =>
                {
                    OnDeleteClicked?.Invoke(this, null);
                }));
            }
        }

        private ICommand _tagButtonCommand;
        public override ICommand TagButtonCommand
        {
            get
            {
                return _tagButtonCommand ?? (_tagButtonCommand = new CommandHandler(() =>
                {

                }));
            }
        }

        private ICommand _downloadButtonCommand;
        public override ICommand DownloadButtonCommand
        {
            get
            {
                return _downloadButtonCommand ?? (_downloadButtonCommand = new CommandHandler(() =>
                {

                }));
            }
        }
    }
}
