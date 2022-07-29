using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectionManager
{
    private static SelectionManager instance = null;

    private GraphNode selectedTile;
    private Movable selectedMovable;

    public Movable SelectedMovable
    {
        get => selectedMovable; 
    }

    public GraphNode SelectedTile
	{
        get => selectedTile;
	}

    public void Select(GraphNode node)
    {
        selectedTile = node; 
        if(selectedMovable != null)
        {
            selectedMovable.moveTo(selectedTile);
            selectedMovable = null;
            selectedTile = null; 
	    }
    }

    public void Select(Selectable selectable)
    {
        Movable attachedMovableComponent = selectable.GetComponent<Movable>();
        if (attachedMovableComponent != null)
        {
            selectedMovable = attachedMovableComponent;
	    } 
    }

    private SelectionManager()
    { 
    }

    public static SelectionManager getInstance()
    {
	    if(instance == null)
        {
            instance = new SelectionManager(); 
	    }
        return instance;
    }
}
