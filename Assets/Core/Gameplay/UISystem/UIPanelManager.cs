using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Gameplay.UISystem
{
    public class UIPanelController
    {
        public IReadOnlyList<UIPanel> Panels => _panels;
        private List<UIPanel> _panels = new List<UIPanel>();

        public void RegisterPanel(UIPanel panel)
        {
            if(_panels.Contains(panel)) throw new ArgumentException("Panel already registered");
            _panels.Add(panel);
        }
        public void UnregisterPanel(UIPanel panel)
        {
            if(!_panels.Remove(panel)) throw new ArgumentException("Panel not registered");
        }

        public void OpenPanel(string panelName) => OpenPanel(GetPanel(panelName)); 
        public void OpenPanel(UIPanel panel)
        {
            if(!_panels.Contains(panel)) throw new ArgumentException("Panel not registered");
            panel.Show(GetState());
        }
        
        public void OpenPanelAlone(string panelName) => OpenPanelAlone(GetPanel(panelName));
        public void OpenPanelAlone(UIPanel panel)
        {
            if(!_panels.Contains(panel)) throw new ArgumentException("Panel not registered");
            var s = GetState();
            CloseAllPanel();
            panel.Show(s);
        }
        
        public void ClosePanel(string panelName) => ClosePanel(GetPanel(panelName)); 
        public void ClosePanel(UIPanel panel)
        {
            if(!_panels.Contains(panel)) throw new ArgumentException("Panel not registered");
            panel.Hide(GetState());
        }
        
        private void CloseAllPanel()
        {
            var s = GetState();
            _panels.ForEach(x =>
            {
                if (x.IsHided)
                    return;
                x.Hide(s, false);
            });
        }
        
        public void SwitchPanel(UIPanel from, string to) => SwitchPanel(from, GetPanel(to));
        public void SwitchPanel(string from, UIPanel to) => SwitchPanel(GetPanel(from), to);
        public void SwitchPanel(string from, string to) => SwitchPanel(GetPanel(from), GetPanel(to));
        public void SwitchPanel(UIPanel from, UIPanel to)
        {
            if(!_panels.Contains(from)) throw new ArgumentException($"Panel ({from.PanelName}) not registered");
            if(!_panels.Contains(to)) throw new ArgumentException($"Panel ({to.PanelName}) not registered");
            var s = GetState();
            from.Hide(s);
            to.Show(s);
        }

        public void LoadState(PanelState state, params UIPanel[] ignorePanels)
        {
            var s = GetState();
            foreach (var configuration in state.Configurations)
            {
                var panel = GetPanel(configuration.PanelName);
                if (ignorePanels!= null && ignorePanels.Contains(panel))
                    continue;
                if (configuration.IsHidden)
                    panel.Hide(s, false);
                else
                    panel.Show(s, false);
            }
        }
        
        public UIPanel GetPanel(string name)
        {
            var panel = _panels.FirstOrDefault(x => x.PanelName == name);
            if(panel == null) throw new ArgumentException("Panel not registered");
            return panel;
        }

        public PanelState GetState() => new PanelState(_panels);
    }
}