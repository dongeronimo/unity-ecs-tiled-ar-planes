using Unity.Entities;
namespace UserInteractions.Components
{
    public struct Selectable : IComponentData
    {
        public bool IsSelected;
    }
}
