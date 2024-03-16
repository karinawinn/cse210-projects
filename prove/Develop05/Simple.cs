using System.ComponentModel;
using System.Numerics;

class Simple : Goal {
    public Simple(string name, string description, int points) : base(name, description, points) {
        SetStatus("no");
    }
    public Simple(string name, string description, int points, string status) : base(name, description, points) {
        SetStatus(status);
    }
    public override void RecordEvent() {   
        SetStatus("yes");
        CompileTotal(_points);
    }
    public override string GetStringRepresentation() {
        string status = GetStatus();
        string representation = $"SimpleGoal:{_name},{_description},{_points},{status}";
        return representation;
    }

}