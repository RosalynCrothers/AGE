using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generation : MonoBehaviour
{

    public float radius = 1;
    public Vector2 regionSize = Vector2.one;
    public int rejectionSamples = 30;
    public float displayRadius = 1;

    private List<Vector2> points;


    public GameObject prefab;
    public GameObject prefab_hot;

    public GameObject grass1;
    public GameObject grass2;
    public GameObject grass3;

    private List<GameObject> treelist = new List<GameObject>();
    private List<GameObject> grasslist = new List<GameObject>();

    public GameObject plane;
    public GameObject AnchorPoint;

    public Texture2D heatmap;
    private float heatlen;
    private float heatwid;
    private float heat;
    private List<float> heatlist = new List<float>();

    private Vector3 planeSize;


    // Start is called before the first frame update
    void Start()
    {
        planeSize = plane.GetComponent<Terrain>().terrainData.size;

        //prefab.transform.Rotate(90f,0f,0f);

        /*points = PDS.GeneratePoints(radius, regionSize, rejectionSamples);

        foreach(Vector2 p in points)
        {
            treelist.Add(Instantiate(prefab, new Vector3(p.x, 99, p.y), Quaternion.identity));
        }
    */

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                Vector3 Spawnpoint = new Vector3((i*50) + Random.Range(-30f, 30f), 100f, (j*50) + Random.Range(-30f, 30f)) + AnchorPoint.transform.position;

                heatlen = heatmap.height;
                heatwid = heatmap.width;

                float texposx = (Spawnpoint.x / planeSize.x)*100;
                float texposy = (Spawnpoint.y / planeSize.y)*100;

                float heatposx = (texposx / 100) * (int)heatlen;
                float heatposy = (texposy / 100) * (int)heatwid;

                //float heat = heatmap.GetPixel(heatposx, heatposy).grayscale; / 255f * 100;


                //int pix = heatmap.GetPixel(heatposx, heatposy).;

                float heat = heatmap.GetPixel((int)heatposx, (int)heatposy).grayscale;

                Debug.Log(heat);

                heatlist.Add(heat);

                if(heat > 0.85){
                    treelist.Add(Instantiate(prefab_hot, Spawnpoint, Quaternion.Euler(-90f,Random.Range(-180,180),0f)));
                }
                else{
                    treelist.Add(Instantiate(prefab, Spawnpoint, Quaternion.Euler(0f,Random.Range(-180,180),0f)));
                }
                //treelist.Add(Instantiate(prefab, Spawnpoint, Quaternion.Euler(-90f,0f,0f)));
                
                
                
            }
            
            
        }

        foreach(GameObject tree in treelist)
                {
                    Ground(tree);
                    
                }

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Vector3 Spawnpoint = new Vector3((i*20) + Random.Range(-30f, 30f), 100f, (j*20) + Random.Range(-30f, 30f)) + AnchorPoint.transform.position;

                int grasstype = Random.Range(0,2);
                if(grasstype == 0) {
                    grasslist.Add(Instantiate(grass1, Spawnpoint, Quaternion.Euler(0f,Random.Range(-180,180),0f)));
                }
                else if(grasstype == 1) {
                grasslist.Add(Instantiate(grass2, Spawnpoint, Quaternion.Euler(0f,Random.Range(-180,180),0f)));
                }
                else if(grasstype == 2) {
                grasslist.Add(Instantiate(grass3, Spawnpoint, Quaternion.Euler(0f,Random.Range(-180,180),0f)));
                }

               
            }
        }
        foreach(GameObject g in grasslist)
                {
                    Ground(g);
                    
                }
        
        
    }

    private void Ground(GameObject tree)
    {
        RaycastHit hit;
        if (Physics.Raycast(tree.transform.position, -Vector3.up, out hit, Mathf.Infinity))
        {

            tree.transform.position = new Vector3(tree.transform.position.x, tree.transform.position.y - hit.distance + 0.001f, tree.transform.position.z);
        }
        else{
            Destroy(tree);
        }
    }
}

