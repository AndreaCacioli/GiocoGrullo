public class SelectionManager
{
    public delegate void selectionEventHandler();
    public static event selectionEventHandler OnSelectionChanged;

    private static SelectionManager instance = null;

    private GraphNode selectedTile;

    private Selectable _selectable;

    public Selectable Selectable
    {
        get { return _selectable; }
        private set
        {
            if (value != _selectable)
            {
                _selectable = value;
                OnSelectionChanged?.Invoke();
            }
        }
    }


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
