using System.Numerics;
class Checklist : Goal {
    private int _toComplete;
    private int _eventCount;
    private int _bonus;
    public Checklist(string name, string description, int points, int toComplete, int bonus) : base(name,description,points) {
        _toComplete = toComplete;
        _bonus = bonus;
        _eventCount = 0;
    }
    public Checklist(string name, string description, int points, int bonus, int toComplete, int eventCount) : base(name,description,points) {
        _toComplete = toComplete;
        _bonus = bonus;
        _eventCount = eventCount;
    }
    public override string ToString() {
        string toPrint;
        string status = GetStatus();
        if (status == "yes") {
            toPrint = $"[X] {_name} ({_description})";
        }
        else {
            toPrint = $"[ ] {_name} ({_description}) -- Currently Completed: {_eventCount}/{_toComplete}";
        }
        return toPrint;
    }
    public override void RecordEvent() {
        _eventCount += 1;
        if (_eventCount == _toComplete) {
            SetStatus("yes");
            CompileTotal(_points);
            CompileTotal(_bonus);
        }
        if (_eventCount != _toComplete) {
            SetStatus("no");
            CompileTotal(_points);
        }
    }
    public override string GetStringRepresentation() {
        string representation = $"ChecklistGoal:{_name},{_description},{_points},{_bonus},{_toComplete},{_eventCount}";
        return representation;
    }

}