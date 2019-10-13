using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<WaveInstruction> instructionQueue;
    public Waypoint waypoint;

    public bool finishedSpawningCurrentWave = false;
    private MonsterManager monsterManager;
    private LevelManager levelManager;


    void Start()
    {
        GameObject levelManagerObject = GameObject.Find("/LevelManager");
        levelManager = levelManagerObject.GetComponent<LevelManager>();
        monsterManager = levelManagerObject.GetComponent<MonsterManager>();
    }

    public void StartWave()
    {
        ProcessInstructions();
    }

    public void SetInstructions(List<WaveInstruction> waveInstructions)
    {
        instructionQueue = waveInstructions;
        finishedSpawningCurrentWave = false;
    }

    void ProcessInstructions()
    {
        if (instructionQueue.Count == 0)
        {
            finishedSpawningCurrentWave = true;
            return;
        }

        WaveInstruction instruction = instructionQueue[0];

        if (instruction.type == WaveInstructionType.Wait)
        {
            instructionQueue.Remove(instruction);
        } 
        else if (instruction.type == WaveInstructionType.Spawn)
        {
            SpawnMonsters(instruction.monsters, instruction.terminatePointIndex);
            instructionQueue.Remove(instruction);
        }
        else if (instruction.type == WaveInstructionType.RepeatSpawn)
        {
            SpawnMonsters(instruction.monsters, instruction.terminatePointIndex);
            
            instruction.reps--;
            if (instruction.reps <= 0)
            {
                instructionQueue.Remove(instruction);
            }
        }

        Invoke("ProcessInstructions", instruction.timer);
    }

    void SpawnMonsters(string[] monsters, int targetTerminatePointIndex)
    {
        foreach (string monsterName in monsters)
        {
            GameObject monsterObject = monsterManager.GetMonsterByName(monsterName);
            if (monsterObject)
            {
                GameObject monsterInstance = Instantiate(monsterObject, transform.position, Quaternion.identity);
                monsterInstance.GetComponent<MonsterMovementController>().currentWaypoint = waypoint;
                monsterManager.OnMonsterSpawn(monsterInstance);
            }
        }
    }
}
