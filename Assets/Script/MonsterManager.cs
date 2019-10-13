using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject[] monsters;

    private IDictionary<string, GameObject> monsterMap = new Dictionary<string, GameObject>();

    public int monstersAlive = 0;
    public int monstersEscaped = 0;
    void Awake()
    {
        // build monster dictionary
        foreach (GameObject monster in monsters)
        {
            string name = monster.GetComponent<UnitStateController>().name;
            monsterMap.Add(name, monster);
        }
    }

    public void OnMonsterSpawn(GameObject monster)
    {
        monstersAlive++;
    }

    public void OnMonsterDie(GameObject monster)
    {
        monstersAlive--;
    }

    public void OnMonsterEscape(GameObject monster)
    {
        monstersAlive--;
        monstersEscaped++;
    }

    public GameObject GetMonsterByName(string name)
    {
        if (monsterMap.ContainsKey(name))
        {
            return monsterMap[name];
        }

        return null;
    }
}
