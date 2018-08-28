using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Controller : MonoBehaviour {

    public TMPro.TextMeshProUGUI testTex;

	// Use this for initialization
	void Start ()
    {
        testTex.text = "You didnt say the magic word!";
        testTex.ForceMeshUpdate();
	}

    // Update is called once per frame
    void Update()
    {
        testTex.ForceMeshUpdate();
        TMPro.TMP_MeshInfo mesh = testTex.textInfo.meshInfo[0];
        Vector3 basePosition = testTex.transform.position;
        Vector3[] verts = mesh.vertices;
        int charCount = testTex.textInfo.characterCount;
        
        for (int i = 0; i < charCount; i++)
        {
            TMPro.TMP_CharacterInfo charInfo = testTex.textInfo.characterInfo[i];
            if (!charInfo.isVisible)
            {
                continue;
            }
            int vertIndex = charInfo.vertexIndex;
            verts[vertIndex + 0] += new Vector3(0, Mathf.Sin(Time.time * 3f + (float)i * 3) * 5f, 0);
            verts[vertIndex + 1] += new Vector3(0, Mathf.Sin(Time.time * 3f + (float)i * 3) * 5f, 0);
            verts[vertIndex + 2] += new Vector3(0, Mathf.Sin(Time.time * 3f + (float)i * 3) * 5f, 0);
            verts[vertIndex + 3] += new Vector3(0, Mathf.Sin(Time.time * 3f + (float)i * 3) * 5f, 0);
            
           

        }

        testTex.UpdateVertexData();
        


        
        
	}
}
