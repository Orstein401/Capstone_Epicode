using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";
    [Header("Content")]
    [SerializeField] private GameObject hasData;
    [SerializeField] private GameObject noHasData;
    [SerializeField] private TextMeshProUGUI nameSlot;
    [Header("Button")]
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void SetSlot(GameSave data)
    {
        if (data != null)
        {
            hasData.SetActive(true);
            noHasData.SetActive(false);

            nameSlot.SetText(profileId);
        }
        else
        {
            hasData.SetActive(false);
            noHasData.SetActive(true);
        }
    }
    public string GetProfileID()
    {
        return profileId;
    }
    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
    }

}
