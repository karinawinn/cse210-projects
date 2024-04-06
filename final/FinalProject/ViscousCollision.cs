class ViscousCollision : Trajectory {
    private double B1;
    private double B2;

    public ViscousCollision(double[] startPosition, double startVelocity, double launchAngle, double mass, double rho, double A, double C, double B1, double B2) : base (startPosition,startVelocity,launchAngle,mass,rho,A,C) {
        this.xposition = [startPosition[0]];
        this.yposition = [startPosition[1]];
        this.launchAngle = launchAngle * Math.PI/180;
        this.xvelocity = [startVelocity*Math.Cos(launchAngle)];
        this.yvelocity = [startVelocity*Math.Sin(launchAngle)];
        this.mass = mass;
        this.rho = rho;
        this.A = A;
        this.C = C; 
        this.B1 = B1;
        this.B2 = B2;
    }
    public override double[] Derivatives(double[] vars) {
        double[] r = [];
        double[] v = [];
        for (int i = 0; i < vars.Length; i++) {
            if (i < 2) {
                r = r.Append(vars[i]).ToArray();
            }
            if (i >= 2) {
                v = v.Append(vars[i]).ToArray();
            }
        }
        double velocity = Math.Sqrt(v[0]*v[0] + v[1]*v[1]);
        double xderiv = v[0];
        double yderiv = v[1]; 
        double vxderiv = 0 - 0.5*rho*A*C*v[0]*velocity/mass + (-B1*v[0] - B2*velocity*v[0])/mass;
        double vyderiv = -9.8 - 0.5*rho*A*C*v[1]*velocity/mass + (-B1*v[1] - B2*velocity*v[1])/mass;
        return [xderiv, yderiv, vxderiv, vyderiv];
    }
}