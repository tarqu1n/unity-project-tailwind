using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelWaveInstructions
{
    public List<WaveInstructions> waves;
}

[System.Serializable]
public class WaveInstructions
{
    public int waveIndex;
    public List<WaveInstruction> instructions;
}

[System.Serializable]
public class WaveInstruction
{
    public WaveInstructionType type;

    public float timer = 0f;
    public int reps = 1;
    public string[] monsters;
    public int terminatePointIndex = 0;
}

public enum WaveInstructionType
{
    /**
     * RepeatSpawn uses: timer, reps, monsters, terminatePointIndex
    **/
    RepeatSpawn = 0,
    /**
     * Spawn uses: timer, monsters, terminatePointIndex
    **/
    Spawn = 1,
    /**
     * Spawn uses: timer
    **/
    Wait = 2,
}