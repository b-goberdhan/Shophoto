using Shophoto.Command;
using Shophoto.InputBox;
using Shophoto.Menus.Context;
using Shophoto.Services;
using Shophoto.ViewModels;
using Shophoto.Views.Collections;
using Shophoto.Views.Projects.AuxView;
using Shophoto.Views.Projects.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shophoto.Views.Projects.Project
{
    public enum ProjectState
    {
        Normal,
        Edit,
        Delete
    }
    public class ProjectVM : BaseVM
    {
        public event EventHandler OnGoBackClick;
        public ProjectVM(ProjectService projectService, ProjectFolderVM projectFolderVM)
        {

            //InputVMs...
            ProjectService = projectService;
            ProjectFolderVM = projectFolderVM;
        }

        public void Init()
        {
            ContextMenuVM = new ContextMenuVM();
            EditProjectVM = new EditProjectVM(ProjectService);
            DeleteProjectVM = new DeleteProjectVM(ProjectService);
            ContextMenuVM.MenuItems.Add(new ContextMenuItemVM("Edit", () =>
            {
                State = ProjectState.Edit;
                EditProjectVM.IsVisible = true;
                ContextMenuVM.IsOpen = false;
            }));
            ContextMenuVM.MenuItems.Add(new ContextMenuItemVM("Delete", () =>
            {
                State = ProjectState.Delete;
                //TODO: Delete prompt
                DeleteProjectVM.IsVisible = true;
                ContextMenuVM.IsOpen = false;
            }));

            DeleteProjectVM.OnDelete += DeleteProjectVM_OnDelete;

        }

        private void DeleteProjectVM_OnDelete(object sender, EventArgs e)
        {
            ProjectService.DeleteCurrentlyOpenedProject();
            OnGoBackClick?.Invoke(this, null);
        }

        public ProjectService ProjectService { get; }
        public ProjectFolderVM ProjectFolderVM { get; }
        private ProjectState _state;
        public ProjectState State
        {
            get { return _state; }
            set
            {
                _state = value;
                NotifyPropertyChanged();
            }
        }

        public EditProjectVM EditProjectVM { get; private set; }
        public DeleteProjectVM DeleteProjectVM { get; private set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public bool HasSummary { get { return Summary != ""; } }

        private string _summary;
        public string Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("HasSummary");
            }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                NotifyPropertyChanged();
            }
        }

        private string _customerEmail;
        public string CustomerEmail
        {
            get { return _customerEmail; }
            set
            {
                _customerEmail = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _goBackClickCommand;
        public ICommand GoBackClickCommand
        {
            get
            {
                return _goBackClickCommand ?? (_goBackClickCommand = new CommandHandler(() =>
                {
                    CollectionsVM.CollectionsFABButtonVM.IsOpen = false;
                    OnGoBackClick?.Invoke(this, null);
                }));
            }
        }

        private ICommand _toggleContextMenuCommand;
        public ICommand ToggleContextMenuCommand
        {
            get
            {
                return _toggleContextMenuCommand ?? (_toggleContextMenuCommand = new CommandHandler(() =>
                {
                    ContextMenuVM.IsOpen = !ContextMenuVM.IsOpen;
                }));
            }
        }


        private CollectionsVM _collectionsVM;
        public CollectionsVM CollectionsVM
        {
            get { return _collectionsVM; }
            set
            {
                _collectionsVM = value;
                NotifyPropertyChanged();
            }
        }

        public ContextMenuVM ContextMenuVM { get; private set; }
    }


}
