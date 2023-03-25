using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnvironmentParameters
{
    [SerializeField] private List<GameObject> rooms;
    [SerializeField] private List<PeopleParameters> people;
    [SerializeField] private int addCountRoomThroughFloor;
    [SerializeField] private static int startCountRooms;

    public List<GameObject> Rooms => rooms;
    public List <PeopleParameters> People => people;
    public int AddCountRoomThroughFloor => addCountRoomThroughFloor;
    public int StartCountRooms => startCountRooms;
}
