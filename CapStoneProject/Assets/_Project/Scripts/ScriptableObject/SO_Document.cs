using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Files", fileName = "Document")]
public class SO_Document : ScriptableObject
{
    [SerializeField] private Sprite backgroundFile;
    [SerializeField] private string titleFile;
    [SerializeField] private string textFile;

    public Sprite BackgroundFile => backgroundFile;
    public string TitleFile => titleFile;
    public string TextFile => textFile;
}
