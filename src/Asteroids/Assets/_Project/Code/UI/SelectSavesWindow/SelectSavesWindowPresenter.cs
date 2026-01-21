namespace Code.UI.SelectSavesWindow
{
    public class SelectSavesWindowPresenter
    {
        public SelectSavesWindowModel Model { get; private set; }
        public SelectSavesWindowView View { get; private set; }

        public SelectSavesWindowPresenter(SelectSavesWindowModel model, SelectSavesWindowView view)
        {
            Model = model;
            View = view;
        }

        public void Destroy()
        {
            Model.Dispose();
            View.Destroy();
        }
    }
}