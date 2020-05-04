using Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILangResolver : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<LangResolver>().ResolveTexts();
    }
}
