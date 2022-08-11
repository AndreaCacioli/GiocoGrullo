using System.Collections.Generic;
public interface IWithOffensiveTools
{
    public delegate void OffensiveToolChangedHandler(IOffenseTool newTool);
    public event OffensiveToolChangedHandler onOffensiveToolChanged;
    public List<IOffenseTool> GetOffensiveTools();

    public void SelectTool(IOffenseTool tool);

    public IOffenseTool getSelectedTool();

    public uint getNumberOfAttacks();
}