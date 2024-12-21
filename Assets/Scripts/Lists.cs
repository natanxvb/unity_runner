using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lists : MonoBehaviour
{
    [SerializeField] List<string> friends = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        print(friends[0]);
        print(friends[1]);
        print(friends[2]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
