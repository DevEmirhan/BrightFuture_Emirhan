using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public List<MainPanel> MainPanels = new List<MainPanel>();
    private int currentScreen;

    public void OpenPage(int index)
    {
        foreach (MainPanel panel in MainPanels)
        {
            CloseAllPages();
            MainPanels[index].OpenPage();
            RefreshPanel(index);
            currentScreen = index;
        }
    }
    public void CloseAllPages()
    {
        foreach (MainPanel panel in MainPanels)
        {
            panel.ClosePage();
        }
    }
    public void RefreshPanel(int Index)
    {
        MainPanels[Index].Refresh();
    }
    public void RefreshPanelAuto()
    {
        MainPanels[currentScreen].Refresh();
    }


}
