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
            panel.Show();
        }
        
        public void OpenPanelFrom(string panelName, UIPanel from) => OpenPanelFrom(GetPanel(panelName), from); 
        public void OpenPanelFrom(UIPanel panel, UIPanel from)
        {
            if(!_panels.Contains(panel)) throw new ArgumentException("Panel not registered");
            if(!_panels.Contains(from)) throw new ArgumentException($"Panel {from} not registered");
            panel.PreviousPanel = from;
            panel.Show();
        }
        
        public void OpenPanelAlone(string panelName) => OpenPanelAlone(GetPanel(panelName));
        public void OpenPanelAlone(UIPanel panel)
        {
            if(!_panels.Contains(panel)) throw new ArgumentException("Panel not registered");
            CloseAllPanel();
            panel.Show();
        }
        
        public void ClosePanel(string panelName) => ClosePanel(GetPanel(panelName)); 
        public void ClosePanel(UIPanel panel)
        {
            if(!_panels.Contains(panel)) throw new ArgumentException("Panel not registered");
            panel.Hide();
        }

        public void CloseAllPanel()
        {
            _panels.ForEach(x =>
            {
                if (x.IsHided)
                    return;
                x.Hide();
            });
        }
        
        public void SwitchPanel(UIPanel from, string to) => SwitchPanel(from, GetPanel(to));
        public void SwitchPanel(string from, UIPanel to) => SwitchPanel(GetPanel(from), to);
        public void SwitchPanel(string from, string to) => SwitchPanel(GetPanel(from), GetPanel(to));
        public void SwitchPanel(UIPanel from, UIPanel to)
        {
            if(!_panels.Contains(from)) throw new ArgumentException($"Panel ({from.PanelName}) not registered");
            if(!_panels.Contains(to)) throw new ArgumentException($"Panel ({to.PanelName}) not registered");
            from.Hide();
            to.PreviousPanel = from;
            to.Show();
        }

        public UIPanel GetPanel(string name)
        {
            var panel = _panels.FirstOrDefault(x => x.PanelName == name);
            if(panel == null) throw new ArgumentException("Panel not registered");
            return panel;
        }
    }
}