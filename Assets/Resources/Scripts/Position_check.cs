using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_check : MonoBehaviour {

    public float Y_coordinate;
    Transform player_pos;
    private SpriteRenderer object_layer;

	// Use this for initialization
	void Start () {
        player_pos = GameObject.Find("Player").GetComponent<Transform>();
        object_layer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player_pos.position.y > Y_coordinate)
        {
            object_layer.sortingLayerName = "Objects front";
        }
        if(player_pos.position.y < Y_coordinate)
        {
            object_layer.sortingLayerName = "Objects back";
        }
	}
}

