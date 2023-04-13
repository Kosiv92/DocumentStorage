namespace DocumentStorageMVC.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(string title)
        {
            SelectedTitle = title;
        }

        public object SelectedTitle { get; private set; }
    }
}
