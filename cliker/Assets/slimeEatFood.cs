using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeEatFood : MonoBehaviour
{
    public Color[] customColor;
    public float EatSpeed=10;
    public int level=1;
    // Start is called before the first frame update
    public int coins=1;
    void Start()
    {
    }
    public Transform explosionPrefab;
    
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag =="food")
        {
            float step = EatSpeed* Time.deltaTime;
            collision.gameObject.transform.position=Vector3.MoveTowards(collision.gameObject.transform.position,transform.position,step);
            Destroy(collision.gameObject,0.3f);
            coins +=1;
            if (coins >= (23.0f*level))
            {
                level+=1;
                Renderer m_Renderer = GetComponent<Renderer>();
                
                m_Renderer.material.SetColor("_Color", customColor[level-1]);;
            }
                float scalefactor= (float)System.Math.Pow(1.1f, (coins % 23));
                this.transform.localScale = new Vector3(1f*scalefactor, 0.8f*scalefactor, 1f*scalefactor);
            if (coins>=230) {coins=1;
            level=1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
