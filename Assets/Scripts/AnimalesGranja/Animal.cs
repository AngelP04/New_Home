using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : PersistenceObject
{
    public Item material;
    public float timeBetweenPicks, timeLastPick;
    public bool picked;

    public override ObjectData ToData()
    {
        float[] position = new float[2];
        position[0] = transform.position.x;
        position[1] = transform.position.y;
        return new ObjectData()
        {
            id = id,
            timeBetweenPicks = timeBetweenPicks,
            timeLastPick = timeLastPick,
            picked = picked,
            position = position,
        };
    }

    public override void FromData(ObjectData data)
    {
        id = data.id;
        timeBetweenPicks = data.timeBetweenPicks;
        timeLastPick = data.timeLastPick;
        picked = data.picked;
        transform.position = new Vector2(data.position[0], data.position[1]);
    }
}
