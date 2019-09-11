using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!GameManager.gameManagerCreated)
        {
            DontDestroyOnLoad(this.transform.gameObject);
        }else
        {
            Destroy(gameObject);
        }
        
    }
}
