using UnityEngine;
using System.Collections;

public class ObsMech : MonoBehaviour
{

    GameObject bal;
    public float rotatespeedL = 5f, rotatespeedR;
    public float forwardMovementSpeed = 10f;
    public float ScreenSizeX;

    public bool forward, reverse;

    public bool seeda, saada, b, br, bg, bgr;


    // Use this for initialization
    void Start()
    {
        ScreenSizeX = Demo.instance.screenX;
        bal = Demo.instance.gameObject;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (saada)
        {
            //rotatespeedL = Demo.instance.rotateDirL;
            //forwardMovementSpeed = Demo.instance.rotateRevSpeed;            
            Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
            newVelocity.x = forwardMovementSpeed;
            GetComponent<Rigidbody2D>().velocity = newVelocity;
            transform.Rotate(0, 0, rotatespeedR);
        }
        else if (seeda)
        {
            //rotatespeedL = Demo.instance.rotateDirL;
            //=forwardMovementSpeed = Demo.instance.rotateRevSpeed;
            transform.Rotate(0, 0, rotatespeedL);
        }
        else if (reverse)
        {
            //rotatespeedL = Demo.instance.rotateDirL;
            //forwardMovementSpeed = Demo.instance.rotateRevSpeed;
            Vector2 newVelocity = GetComponent<Rigidbody2D>().velocity;
            newVelocity.x = forwardMovementSpeed;
            GetComponent<Rigidbody2D>().velocity = newVelocity;
            transform.Rotate(0, 0, rotatespeedR);
        }
        else if (forward)
        {
            //rotatespeedL = Demo.instance.rotateDirL;
            //=forwardMovementSpeed = Demo.instance.rotateRevSpeed;
            transform.Rotate(0, 0, rotatespeedL);
        }
    }

    void OnEnable()
    {
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        seeda = Demo.instance.Straight;
        saada = Demo.instance.Reverse;
        b = Demo.instance.ballStraight;
        br = Demo.instance.ballReverse;
        bg = Demo.instance.ballStrGravity;
        bgr = Demo.instance.ballRevGravity;
        ScreenSizeX = Demo.instance.screenX;
        rotatespeedL = Demo.instance.rotateDirL;
        rotatespeedR = Demo.instance.rotateDirR;
        forwardMovementSpeed = Demo.instance.rotateRevSpeed;
        InvokeRepeating("ObjectDestroyMethod", 1f, 0.5f);
    }

    void ObjectDestroyMethod()
    {
        if (b || bg)
        {
            if (seeda || forward)
            {
                if (transform.position.x < (bal.transform.position.x - ScreenSizeX - 1))
                {
                    Demo.instance.DestroyPooledObject(gameObject);
                }
            }
            else if (saada || reverse)
            {
                if (transform.position.x > (bal.transform.position.x + ScreenSizeX + 1))
                {
                    Demo.instance.DestroyPooledObject(gameObject);
                }
            }
            if (forward && reverse)
            {
                Demo.instance.DestroyPooledObject(gameObject);
            }
        }
        if (br || bgr)
        {
            if (seeda || forward)
            {
                if (transform.position.x > (bal.transform.position.x + ScreenSizeX + 1))
                {
                    Demo.instance.DestroyPooledObject(gameObject);
                }
            }
            else if (saada || reverse)
            {
                if (transform.position.x < (bal.transform.position.x - ScreenSizeX - 1))
                {
                    Demo.instance.DestroyPooledObject(gameObject);
                }
            }
            if (forward && reverse)
            {
                Demo.instance.DestroyPooledObject(gameObject);
            }
        }
    }
    void OnDisable()
    {
        reverse = forward = saada = seeda = b = br = bg = bgr = false;
        rotatespeedL = 0;
        rotatespeedR = 0;
        forwardMovementSpeed = 0;
        CancelInvoke();
    }
}
