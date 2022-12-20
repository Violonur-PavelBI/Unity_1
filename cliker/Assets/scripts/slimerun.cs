using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimerun : MonoBehaviour
{
    public float moveSpeed=1;
    public float RotateSpeed=1;
    Vector3 previous =new Vector3(0.0f,0.0f,0.0f); 
    float smooth = 5.0f;
    public GameObject[] foods;
    GameObject closest;
    GameObject nearest;
    public float EatSpeed=10;
    public int level=1;
    public int coins=1;
    public Color[] customColor;
    // Start is called before the first frame update
    void Start()
    {
    }
    // вызов функция ближайшего обьекта
    GameObject FindClosestEnemy() {
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in foods) {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if(curDistance< distance) {
                    closest = go;
                    distance = curDistance;
                }
            }
            return closest;
        }
    void OnCollisionEnter(Collision collision) {
        // если слайм коснулся еды то он вырастет
        if(collision.gameObject.tag =="food")
        {
            // втягивание еды внутрь
            float step = EatSpeed* Time.deltaTime;
            collision.gameObject.transform.position=Vector3.MoveTowards(collision.gameObject.transform.position,transform.position,step);
            // уничтожение обьекта еды
            Destroy(collision.gameObject,0.3f);
            // расчёт очков и изменения размера
            coins +=1;
            // повышение уровня и изменения цвета 
            if (coins >= (23.0f*level))
            {
                level+=1;
                Renderer m_Renderer = GetComponent<Renderer>();
                //изменения цвета
                m_Renderer.material.SetColor("_Color", customColor[level-1]);;
            }
            // изменения размера
            float scalefactor= (float)System.Math.Pow(1.1f, (coins % 23));
            this.transform.localScale = new Vector3(1f*scalefactor, 0.8f*scalefactor, 1f*scalefactor);
            // обнуление уровня
            if (coins>=230) 
            {
                coins=1;
                level=1;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        float velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        // вывод скорости
        // если слайм упал и не катиться
        if (velocity<=0.1)//заменить что касаеться пола или стены
        {
            // если он перевернулся выравниваеться
            if (System.Math.Abs(transform.rotation.eulerAngles.x)>=45.0 || System.Math.Abs(transform.rotation.eulerAngles.z)>=45.0)
            {
                // Rotate the cube by converting the angles into a quaternion.
                Quaternion target = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                // Dampen towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
            }
            else{}
             // вызов функции ближайшего обьекта
            foods = GameObject.FindGameObjectsWithTag("food");
            if(foods.Length !=0)
            {
                nearest =FindClosestEnemy();
                // функция перемещения/прыжка
                Vector3 diff = nearest.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                float step = moveSpeed* Time.deltaTime;
                transform.position=Vector3.MoveTowards(transform.position,nearest.transform.position,step);
            }
        }
    }
}



