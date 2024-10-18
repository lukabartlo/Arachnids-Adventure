using System.Linq;
using UnityEngine;

public class Sc_Spiders : MonoBehaviour
{
    [SerializeField] private GameObject _nestOrigin;
    [SerializeField] private LayerMask _nestLayer;

    private float _speed = 2.5f;
    private float _radiusMax = 3.0f;
    private float _radiusMin = 1.0f;
    private Sc_Nests _nestComp;

    public bool hasBeenClicked = false;
    public bool isPlaced = false;
    private bool isLinked = false;

    [SerializeField] private LayerMask _spiderExcludeLayerMask;
    [SerializeField] private LayerMask _nestExcludeLayerMask;

    private void Start()
    {
        _nestComp = GetComponent<Sc_Nests>();
        GetComponent<CircleCollider2D>().excludeLayers = _spiderExcludeLayerMask;
    }

    void Update()
    {
        if (!hasBeenClicked)
        {
            Spider_Mouvement();
        }

        if (hasBeenClicked && !isPlaced) 
        {
            Collider2D[] HitCollidersReviewMax = Physics2D.OverlapCircleAll(transform.position, _radiusMax, _nestLayer);
            Collider2D[] HitCollidersReviewMin = Physics2D.OverlapCircleAll(transform.position, _radiusMin, LayerMask.GetMask("Nests","Walls"));

            if (HitCollidersReviewMax.Length >= 2)
            {
                foreach (Collider2D hitcolliders in HitCollidersReviewMax)
                {
                    if (!CheckIfWebExist(hitcolliders.gameObject))
                    {
                        _nestComp.SpawnWeb(gameObject, hitcolliders.gameObject);
                    }
                }
            }

            foreach (GameObject oldWebs in _nestComp.webList.ToList())
            {
                isLinked = false;

                foreach (Collider2D hitcolliders in HitCollidersReviewMax)
                {
                    if (oldWebs.GetComponent<Sc_Webs>().endPos == hitcolliders.transform)
                    {
                        isLinked = true;
                        break;
                    }
                }

                if (!isLinked || HitCollidersReviewMin.Length > 0)
                {
                    Destroy(oldWebs);
                    _nestComp.webList.Remove(oldWebs);
                }
            }

            if (_nestComp.webList.Count == 1)
            {
                Destroy(_nestComp.webList[0]);
                _nestComp.webList.Remove(_nestComp.webList[0]);
            }
        }

        if (this.gameObject.transform.position != _nestOrigin.transform.position)
        {
            return;
        }

        if (this.gameObject.transform.position == _nestOrigin.transform.position)
        {
            Check_Available_Nests();
        }

        foreach (GameObject nest in _nestComp.nestList.ToList())
        {
            _nestComp.SpawnWeb(gameObject, nest);
        }
    }

    private void Check_Available_Nests()
    {
        _nestOrigin = _nestOrigin.GetComponent<Sc_Nests>().nestList[Random.Range(0, _nestOrigin.GetComponent<Sc_Nests>().nestList.Count)];
        transform.parent = _nestOrigin.transform;
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

            foreach (GameObject oldwebs in _nestComp.webList.ToList())
            {
                Destroy(oldwebs);
                _nestComp.webList.Remove(oldwebs);
            }
        }

        else
        {
            isPlaced = true;
            gameObject.layer = LayerMask.NameToLayer("Nests");

            foreach (Collider2D hitcollider in HitCollidersMax)
            {
                _nestComp.nestList.Add(hitcollider.gameObject);
                hitcollider.GetComponent<Sc_Nests>().nestList.Add(gameObject);
                SpringJoint2D joints = gameObject.AddComponent<SpringJoint2D>();
                joints.connectedBody = hitcollider.GetComponent<Rigidbody2D>();
                joints.autoConfigureDistance = false;
                joints.frequency = 5.0f;
                GetComponent<CircleCollider2D>().excludeLayers = _nestExcludeLayerMask;

                if (!CheckIfWebExist(hitcollider.gameObject))
                {
                    _nestComp.SpawnWeb(gameObject, hitcollider.gameObject);
                }
            }
            GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }

    public bool CheckIfWebExist(GameObject nest)
    {
        foreach(GameObject web in _nestComp.webList)
        {

            if(web.GetComponent<Sc_Webs>().endPos == nest.transform)
            {
                return true;
            }

        }
        return false;
    }
}