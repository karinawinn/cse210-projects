class AdiabaticDensity : Trajectory {
    private double temperature;
    public AdiabaticDensity(double[] startPosition,double startVelocity,double launchAngle,double mass,double rho,double A,double C,double temperature) : base(startPosition,startVelocity,launchAngle,rho,mass,A,C) {
        this.xposition = [startPosition[0]];
        this.yposition = [startPosition[1]];
        this.launchAngle = launchAngle * Math.PI/180;
        this.xvelocity = [startVelocity*Math.Cos(launchAngle)];
        this.yvelocity = [startVelocity*Math.Sin(launchAngle)];
        this.mass = mass;
        this.rho = rho;
        this.A = A;
        this.C = C;
        this.temperature = temperature;
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
        double a = 6.5*Math.Pow(10,-3);
        double alpha = 2.5;
        double currentrho = rho*Math.Pow(1 - a*r[1]/temperature,alpha);
        double xderiv = v[0];
        double yderiv = v[1]; 
        double vxderiv = 0 - 0.5*rho*A*C*v[0]*velocity/mass;
        double vyderiv = -9.8 - 0.5*(currentrho/rho)*A*C*v[1]*velocity/mass;
        return [xderiv, yderiv, vxderiv, vyderiv];
    }
}