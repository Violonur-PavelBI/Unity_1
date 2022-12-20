using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawfood : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    
    // Start is called before the first frame update
    void Start()
    {}
    // Update is called once per frame
    void Update()
    {   
        if (Input.GetMouseButtonDown(0))
        {   
            RaycastHit hit;         
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // если луч от камеры через курсор прошёл через mach то там спавним food
            if (Physics.Raycast(ray, out hit))
            {
            Instantiate(prefab, new Vector3(hit.point.x-5.0f,hit.point.y+5.0f,hit.point.z), transform.rotation);
            }
        }
    // для сенсорного экрана
    }
}