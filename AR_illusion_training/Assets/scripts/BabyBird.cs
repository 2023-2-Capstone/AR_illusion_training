using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBird : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.4f;
    public float rotation_damping =4f;
    public Transform MamaBird;
    void Start()
    {
        //하나의 마마버드만 따라감
        // MamaBird=GameObject.FindGameObjectWithTag("MamaBird").GetComponent<Transform>();

        GameObject[] MamaBirds=GameObject.FindGameObjectsWithTag("MamaBird");
        int chosenMamaBird=Random.Range(0,MamaBirds.Length);
        MamaBird=MamaBirds[chosenMamaBird].GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation of mamabird toward camera
        var rotation= Quaternion.LookRotation(MamaBird.transform.position-transform.position);
        this.transform.rotation=Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime*rotation_damping);

        //babybird will follow mama
        float step=speed*Time.deltaTime;
        this.transform.position=Vector3.MoveTowards(transform.position,MamaBird.position,step);
    }
}
