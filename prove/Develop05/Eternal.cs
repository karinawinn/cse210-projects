class Eternal : Goal {
    public Eternal(string name, string description, int points) : base(name, description, points) {
        SetStatus("no");
    }
    public override void RecordEvent() {
        SetStatus("no");
        CompileTotal(_points);
    }
    public override string GetStringRepresentation() {
        string representation = $"EternalGoal:{_name},{_description},{_points}";
        return representation;
    }
}