using System.Dynamic;

public abstract class Goal {
    protected string _name;
    protected string _description;
    protected int _points;
    private int _total;
    private string _status;
    public Goal(string name, string description, int points) {
        _name = name;
        _description = description;
        _points = points;
        _total = 0;
    }
    public string GetStatus() {
        return _status;
    }
    public void SetStatus(string complete) {
        _status = complete;
    }
    public string GetName() {
        return _name;
    }
    public int GetPoints() {
        return _points;
    }
    public void CompileTotal(int points) {
        _total += points;
    }
    public override string ToString() {
        string toPrint;
        string status = GetStatus();
        if (status == "yes") {
            toPrint = $"[X] {_name} ({_description})";
        }
        else {
            toPrint = $"[ ] {_name} ({_description})";
        }
        return toPrint;
    }
    public virtual void RecordEvent() {}
    public virtual string GetStringRepresentation() {
        string representation = $"Goal:{_name},{_description},{_points}";
        return representation;
    }



}