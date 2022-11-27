public class SelectionManager
{
    public delegate void selectionEventHandler();
    public static event selectionEventHandler OnSelectionChanged;

    private static SelectionManager instance = null;

    //THINGS THAT CAN BE SELECTED
    private GraphNode selectedTile;
    private Selectable _selectable;
    //////////////////////////////

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
            clearSelection();
        }
    }

    public void Select(Selectable selectable)
    {
        if (Selectable == null)
        {
            this.Selectable = selectable;
            return;
        }
        IWithLeader withLeader1 = selectable.GetComponent<IWithLeader>();
        IWithLeader withLeader2 = Selectable.GetComponent<IWithLeader>();
        if (withLeader1 != null && withLeader2 != null)
        {
            Warrior.Team leader1 = withLeader1.getLeader();
            Warrior.Team leader2 = withLeader2.getLeader();
            GraphNode tile1 = DataStructureManager.getInstance().getNode(Selectable.transform.position);
            GraphNode tile2 = DataStructureManager.getInstance().getNode(selectable.transform.position);
            if (leader1 != leader2 && DataStructureManager.getInstance().areNeighbours(tile1, tile2))
            {
                Selectable.StartCoroutine(CombatManager.getInstance().StartFight(Selectable.GetComponent<ICanCombat>(), selectable.GetComponent<ICanCombat>()));
                clearSelection();
            }
            else Selectable = selectable;
        }

    }

    private void clearSelection()
    {
        Selectable = null;
        selectedTile = null;
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
