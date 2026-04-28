using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "File", fileName = "Document")]
public class SO_Document : ScriptableObject
{
    [SerializeField] private Sprite backgroundFile;
    [SerializeField] private string titleFile;
    [TextArea]
    [SerializeField] private string textFile;

    public Sprite BackgroundFile => backgroundFile;
    public string TitleFile => titleFile;
    public string TextFile => textFile;
}
