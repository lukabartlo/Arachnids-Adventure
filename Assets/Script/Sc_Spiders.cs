using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sc_Spiders : MonoBehaviour
{
    [SerializeField] private GameObject _nestOrigin;

    private float _speed = 5.0f;
    private 

    void Start()
    {

    }

    void Update()
    {
        Spider_Mouvement();

        if (this.gameObject.transform.position != _nestOrigin.transform.position)
        {
            return;
        }

        if (this.gameObject.transform.position == _nestOrigin.transform.position)
        {
            Check_Available_Nests();
        }
    }

    private void Check_Available_Nests()
    {
        _nestOrigin = _nestOrigin.GetComponent<Sc_Nests>().nests[Random.Range(0, _nestOrigin.GetComponent<Sc_Nests>().nests.Count)];
    }

    private void Spider_Mouvement()
    {
        float step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _nestOrigin.transform.position, step);
    }
}
