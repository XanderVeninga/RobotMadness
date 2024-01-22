using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeToggle : MonoBehaviour
{
    public bool toggle;
       public GameObject recipe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            recipe.SetActive(!recipe.activeSelf);
        }

    }
}
