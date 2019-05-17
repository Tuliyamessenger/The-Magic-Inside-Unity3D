using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour{
    public GameObject target;

    // Start is called before the first frame update
    void Start(){
        target = GameObject.Find("Player");
        /*
        GameObject plane = GameObject.Find("FirstLevel");
        //Get default width.
        float size_x = plane.GetComponent<MeshFilter>().mesh.bounds.size.x;
        //Get width scale.
        float scale_x = plane.transform.localScale.x;
        //Get default height.
        float size_y = plane.GetComponent<MeshFilter>().mesh.bounds.size.y;
        //Get height scale.
        float scale_y = plane.transform.localScale.y;

        //Calculate the real size.
        float mapWidth = size_x * scale_x;
        float mapHeight = size_y * scale_y;
        */
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10000);
    }
}
