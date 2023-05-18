using DG.Tweening.Plugins.Core.PathCore;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChangePanelScript : MonoBehaviour 
{
    public delegate void NextPanelDelegate(string path);
    public delegate void ButtonActivateFunction();

    public static void CreateButtonsInPanel(IEnumerable<string> pathes, PanelButton panelButton, GameObject panel, NextPanelDelegate nextPanel, params ButtonActivateFunction[] functions)
    {
        ClearPanel(panel);
        try
        {
            foreach (var path in pathes)
            {
                CreateButton(path, panelButton, panel, nextPanel, functions);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void ClearPanel(GameObject panel)
    {
        foreach (Transform children in panel.GetComponent<Transform>())
        {
            Destroy(children.gameObject);
        }
    }

    public static PanelButton CreateButton(string path, PanelButton button, GameObject panel, NextPanelDelegate nextPanel, params ButtonActivateFunction[] functions)
    {
        PanelButton buttonPanel = Instantiate(button, panel.transform);
        buttonPanel.Button.onClick.AddListener(() => buttonPanel.Button.interactable = false);
        if (nextPanel != null)
            buttonPanel.Button.onClick.AddListener(() => nextPanel?.Invoke(path));
        foreach (var func in functions)
            buttonPanel.Button.onClick.AddListener(() => func?.Invoke());
        buttonPanel.ButtonText.text = GetLastName(path);
        return buttonPanel;
    }

    public static string GetLastName(string path)
    {
        return new DirectoryInfo(path).Name;
    }
}
