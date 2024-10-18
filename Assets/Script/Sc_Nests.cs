using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sc_Nests : MonoBehaviour
{
    [SerializeField] private GameObject _webs;

    public bool defaultNests;
    public List<GameObject> nestList = new List<GameObject>();
    public List<GameObject> webList = new List<GameObject>();

    void Start()
    {
        if (defaultNests)
        {
            foreach (GameObject nest in nestList.ToList())
            {
                SpawnWeb(gameObject, nest);
            }
        }
    }

    public void SpawnWeb(GameObject start_Pos, GameObject end_Pos)
    {
        GameObject new_Webs = Instantiate(_webs);
        new_Webs.GetComponent<Sc_Webs>().startPos = start_Pos.transform;
        new_Webs.GetComponent<Sc_Webs>().endPos = end_Pos.transform;
        webList.Add(new_Webs);
    }
}
