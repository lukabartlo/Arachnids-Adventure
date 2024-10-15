using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sc_Spiders : MonoBehaviour
{
    [SerializeField] private GameObject _nestOrigin;
    [SerializeField] private GameObject _webs;
    [SerializeField] private LayerMask _nestLayer;

    private float _speed = 5.0f;
    private float _radiusMax = 3.0f;
    private float _radiusMin = 1.0f;

    public bool hasBeenClicked = false;
    public bool isPlaced = false;

    void Update()
    {
        if (!hasBeenClicked)
        {
            Spider_Mouvement();
        }

        if (this.gameObject.transform.position != _nestOrigin.transform.position)
        {
            return;
        }

        if (this.gameObject.transform.position == _nestOrigin.transform.position)
        {
            Check_Available_Nests();
        }

        foreach (GameObject nest in GetComponent<Sc_Nests>().nestList.ToList())
        {
            SpawnWeb(gameObject, nest);
        }
    }

    private void Check_Available_Nests()
    {
        _nestOrigin = _nestOrigin.GetComponent<Sc_Nests>().nestList[Random.Range(0, _nestOrigin.GetComponent<Sc_Nests>().nestList.Count)];
    }

    private void Spider_Mouvement()
    {
        float step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _nestOrigin.transform.position, step);
    }

    public void Detect_Nearby_Nests()
    {
        Collider2D[] HitCollidersMax = Physics2D.OverlapCircleAll(transform.position, _radiusMax, _nestLayer);
        Collider2D[] HitCollidersMin = Physics2D.OverlapCircleAll(transform.position, _radiusMin, _nestLayer);

        if (HitCollidersMax.Length <= 1 || HitCollidersMin.Length > 0)
        {
            transform.position = _nestOrigin.transform.position;
            hasBeenClicked = false;
        }

        else
        {
            isPlaced = true;
            gameObject.layer = LayerMask.NameToLayer("Nests");

            foreach (Collider2D hitcollider in HitCollidersMax)
            {
                GetComponent<Sc_Nests>().nestList.Add(hitcollider.gameObject);
                hitcollider.GetComponent<Sc_Nests>().nestList.Add(gameObject);
                SpringJoint2D joints = gameObject.AddComponent<SpringJoint2D>();
                joints.connectedBody = hitcollider.GetComponent<Rigidbody2D>();
                joints.autoConfigureDistance = false;
                joints.frequency = 5.0f;

                SpawnWeb(gameObject, hitcollider.gameObject);
            }
        }
    }

    public void SpawnWeb(GameObject start_Pos, GameObject end_Pos)
    {
        GameObject new_Webs = Instantiate(_webs);
        new_Webs.GetComponent<Sc_Webs>().startPos = start_Pos.transform;
        new_Webs.GetComponent<Sc_Webs>().endPos = end_Pos.transform;
    }
}