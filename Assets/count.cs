using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count : MonoBehaviour
{
    public int numOfSeats = 0;
    GameObject[] seats;

    void Start()
    {
        seats = GameObject.FindGameObjectsWithTag("Seat");
        numOfSeats = seats.Length;
        Debug.Log(numOfSeats);
    }
}
