using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class holds each enemy type and makes them easily accessible
public class EnemyLibrary : MonoBehaviour
{
    public static int size;
    public enum EnemyList { Mike, Sean, Dan }
    public static Object[] enemyList;

    private void Awake()
    {
        //Accesses folder Enemies inside of folder Resources and creates an array of general objects
        enemyList = Resources.LoadAll("Enemies", typeof(Object));
        size = enemyList.Length;
    }

    //Returns the enemy based on the index given to it
    public static Enemy ChooseEnemy(int num)
    {
        switch(num)
        {
            case 0:
            {
                return (Enemy)enemyList[0];
            }
            case 1:
            {
                    return (Enemy)enemyList[1];
            }
            case 2:
            {
                    return (Enemy)enemyList[2];
            }
            default:
            {
                return null;
            }
        }
    }
}
