using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDisabler : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

}
