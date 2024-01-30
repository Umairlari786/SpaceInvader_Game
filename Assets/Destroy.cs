using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
