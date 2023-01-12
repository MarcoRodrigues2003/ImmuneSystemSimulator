using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multitouch : MonoBehaviour
{
    public GameObject touch1;
    public GameObject touch2;



    private void Update()
    {
        //if(Input.touchCount > 0)
        //{
        //    Vector2 touchPos = Input.GetTouch(0).position;


        //    touch1.transform.position = touchPos;

        //    if(Input.touchCount > 1)
        //    {

        //        Vector2 touchPos2 = Input.GetTouch(1).position;

        //        touch2.transform.position = touchPos2;

        //    }

        //}

       if(Input.touchCount > 0)
       {
            Vector3 hitPoint = new Vector3();

            hitPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10f));

            Debug.Log(hitPoint.y);

            if(hitPoint.x > 2f)
            {
                hitPoint.x = 2f;
            }
            else if (hitPoint.x < -2f)
            {
                hitPoint.x = -2f;
            }
            if(hitPoint.y > 3.5f)
            {
                hitPoint.y = 3.5f;
            }
            else if(hitPoint.y < -2.5f)
            {
                hitPoint.y = -2.5f;
            }
            touch1.transform.position = hitPoint;

            if(Input.touchCount > 1)
            {
                Vector3 hitPoint2 = new Vector3();

                hitPoint2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y, 10f));

                if (hitPoint2.x > 2.5f)
                {
                    hitPoint2.x = 2.5f;
                }
                else if (hitPoint2.x < -2.5f)
                {
                    hitPoint2.x = -2.5f;
                }
                if (hitPoint2.y > 3.5f)
                {
                    hitPoint2.y = 3.5f;
                }
                else if (hitPoint2.y < -2.5f)
                {
                    hitPoint2.y = -2.5f;
                }
                touch2.transform.position = hitPoint2;
            }
       }else
        {
            touch1.transform.position = new Vector3(0, 0, 0);
            touch2.transform.position = new Vector3(0, 0, 0);
        }
                
            

       


    }

        
}




