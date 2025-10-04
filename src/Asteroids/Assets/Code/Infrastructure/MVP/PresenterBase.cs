public class PresenterBase
{
    public ViewBase View { get; }
    public ModelBase Model { get; }

    protected PresenterBase(ModelBase model, ViewBase view)
    {
        Model = model;
        View = view;
    }
}