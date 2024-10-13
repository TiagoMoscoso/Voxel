using UnityEngine;
using System.Collections.Generic;


public class VoxelManager : MonoBehaviour
{
    public GameObject voxelPrefab;
    public LayerMask groundMask;
    private List<GameObject> voxels = new();
    private bool Running { get; set; }  = false;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceVoxel();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ReplaceVoxels();
        }
    }

    public void PlaceVoxel()
    {
        if (Running) return;
        Running = true;
        ReplaceVoxels();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);


       var hill = 0;
       var numberOfVoxels = 100;
       for (var x = -numberOfVoxels; x < numberOfVoxels; x++)
       {
            for (var z = -numberOfVoxels; z < numberOfVoxels; z++)
            {
                var randomPosition = new Vector3(
                    x,
                    Mathf.Round(Random.Range(-100, 10)),
                    z
                );

                if (randomPosition.y < 0)
                    randomPosition.y = 0;

                if (randomPosition.y==0 && hill > 0 && hill <3)
                    randomPosition.y = hill;
 
                var maxY = randomPosition.y;

                hill = (int)maxY - 1;

                for (var y = 0; y <= maxY; y++)
                {
                    randomPosition.y = y;
                    var newVoxel = Instantiate(voxelPrefab, randomPosition, Quaternion.identity);
                    if (maxY > 3)
                    {
                        if (maxY == y)
                            newVoxel.GetComponent<Voxel>().SetColor(GetColor(10));
                        else
                            newVoxel.GetComponent<Voxel>().SetColor(GetColor(y));
                    }
                    else
                    {
                        newVoxel.GetComponent<Voxel>().SetColor(GetColor(0));
                    }
                    voxels.Add(newVoxel);
                }
            }

       }
        Running = false;
    }

    public void ReplaceVoxels()
    {
        foreach (GameObject voxel in voxels)
        {
            Destroy(voxel);
        }
        voxels.Clear();
    }

    private Color GetColor(float height)
    {
        if (height > 0 && height < 8)
            return new Color(0.65f, 0.16f, 0.16f);//marrom
        else if (height >= 8)
            return new Color(0f, 0.9f, 0f);//verde claro 

        return new Color(0.01f, 0.5f, 0.01f);//verde escuro 
    }
}
