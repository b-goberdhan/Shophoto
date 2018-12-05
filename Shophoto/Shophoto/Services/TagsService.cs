using Shophoto.Views.Collections.Aux;
using System.Collections.ObjectModel;
using System.Linq;


namespace Shophoto.Services
{
    public class TagsService
    {

        public TagsService()
        {
            Tags = new ObservableCollection<TagItemVM>();
        }

        public ObservableCollection<TagItemVM> Tags
        {
            get;
            private set;
        }

        public bool TagExist(TagItemVM tagItem)
        {
            return Tags.Any((tag) =>
            {
                return tag.Name == tagItem.Name;
            });
        }
    }
}
