using System.Collections.Generic;

namespace Core.Gameplay.UISystem
{
    public class PanelState
    {
        public IReadOnlyList<PanelConfiguration> Configurations => _configurations;
        private List<PanelConfiguration> _configurations;

        public PanelState(List<UIPanel> panels)
        {
            _configurations = new List<PanelConfiguration>(panels.Count);
            panels.ForEach(x => _configurations.Add(new PanelConfiguration(x)));
        }
    }

    public class PanelConfiguration
    {
        public bool IsHidden { get; private set; }
        public string PanelName { get; private set; }
        
        public PanelConfiguration(UIPanel uiPanel)
        {
            IsHidden = uiPanel.IsHided;
            PanelName = uiPanel.PanelName;
        }
    }
}