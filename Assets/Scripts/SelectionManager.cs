public class SelectionManager
{
    private static SelectionManager instance = null;

    private GraphNode selectedTile;
    public Selectable Selectable { get; private set; }


    public Movable SelectedMovable
    {
        get => Selectable != null ? Selectable.GetComponent<Movable>() : null;
    }

    public GraphNode SelectedTile
    {
        get => selectedTile;
    }

    public void Select(GraphNode node)
    {
        selectedTile = node;
        if (SelectedMovable != null)
        {
            SelectedMovable.moveTo(selectedTile);
            Selectable = null;
            selectedTile = null;
        }
    }

    public void Select(Selectable selectable)
    {
        this.Selectable = selectable;
    }

    private SelectionManager()
    {
    }

    public static SelectionManager getInstance()
    {
        if (instance == null)
        {
            instance = new SelectionManager();
        }
        return instance;
    }
}
