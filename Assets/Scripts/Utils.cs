using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace everedxCode
{
    public static class Utils
    {
        

        public static void showFloatingText(string text, Vector3 position, Transform parent, float xSpeed, float ySpeed)
        {
            GameObject gameObject = new GameObject("text", typeof(TextMesh));
            Rigidbody2D rBody= gameObject.AddComponent<Rigidbody2D>();
            rBody.gravityScale = 0;
            rBody.velocity = new Vector2(xSpeed,ySpeed);
   
            gameObject.transform.position = position;
            gameObject.transform.parent = parent;
            TextMesh textObj = gameObject.GetComponent<TextMesh>();
            textObj.text = text;

            GameObject.Destroy(gameObject,2);
        }


        


    }

}
