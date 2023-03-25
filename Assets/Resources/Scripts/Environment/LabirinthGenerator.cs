using System;
using UnityEngine;

public class LabirinthGenerator : MonoBehaviour
{
    [SerializeField] private GameParameters parameters;

    private int[,] arrayLabirinth;
    private int widthScreen;
    private int heightScreen;
    private int startCountRooms;

    private void Awake()
    {
        widthScreen = Screen.width;
        heightScreen = Screen.height;
        startCountRooms = parameters.EnvironmentParameters.StartCountRooms;
    }

    private void Start()
    {
        int addRooms = parameters.EnvironmentParameters.AddCountRoomThroughFloor / FloorInformation.occupiedFloor;
        int lengthLabirinth = startCountRooms + addRooms;
        arrayLabirinth = new int[lengthLabirinth, lengthLabirinth];
        int i = 0;
        int lastRoomCreate = Random.Range(0, lengthLabirinth;
        arrayLabirinth[lastRoomCreate, 0] = 1;

        do
        {

        } while ();
    }

    private int[,] FillingArray()
    {
        Random rand = new Random();
        for (int k = 0; k < 10; k++)
        {
            // �������� ��������� ������� ������
            int x, y;
            do
            {
                x = rand.Next(0, 10);
                y = rand.Next(0, 10);
            } while (array[x, y] == 0);
            // ��������� �� �������
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    // ���������, ��� ������� �� ������� �� ������� �������
                    if (i >= 0 && i < 10 && j >= 0 && j < 10)
                    {
                        // ���������, ��� ������ ��������
                        if (array[i, j] == 0)
                        {
                            // �������� ������ � ������������ 50%
                            if (rand.Next(0, 2) == 1)
                            {
                                array[i, j] = 1;
                            }
                        }
                    }
                }
            }
            // ������� ������ �� �����
            PrintArray(array);
        }
    }
}
