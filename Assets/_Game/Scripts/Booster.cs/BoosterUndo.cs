using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BoosterUndo : BaseBooster
{
    public UndoManager undoManager;
    public override BOOSTER ID => BOOSTER.UNDO;

    public BoosterUndo()
    {
        undoManager = new();
    }

    public override void Activate()
    {
        undoManager.Undo();
    }
}
public class UndoManager
{
    MoveRecord record;

    public void SetRecord(Slot from, Slot to, Cube cube)
    {
        record = new MoveRecord
        {
            from = from,
            to = to,
            cube = cube
        };
    }

    public void Undo()
    {
        if (record != null)
        {
            Slot fromSlot = record.from;
            Slot toSlot = record.to;
            Cube cube = record.cube;

            toSlot.Free();
            fromSlot.Assign(cube);
            cube.AnimMoveToPosition();

            record = null;
        }
    }
}

public class MoveRecord
{
    public Slot from;
    public Slot to;
    public Cube cube;
}
