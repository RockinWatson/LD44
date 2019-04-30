using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScore : MonoBehaviour {

    private TextMesh _textMesh;

	// Use this for initialization
	void Start () {
        _textMesh = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        _textMesh.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
