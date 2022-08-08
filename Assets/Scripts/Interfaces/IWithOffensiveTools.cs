using System.Collections.Generic;
public interface IWithOffensiveTools
{
    public List<IOffenseTool> GetOffensiveTools();

    public void SelectTool(IOffenseTool tool);

    public IOffenseTool getSelectedTool();

    public uint getNumberOfAttacks();
}