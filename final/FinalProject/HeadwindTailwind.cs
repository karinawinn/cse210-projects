class HeadwindTailwind : Trajectory {
    private double headwind;
    private double tailwind;
    public HeadwindTailwind(double[] startPosition, double startVelocity, double launchAngle, double mass, double rho, double A, double C, double headwind,double tailwind) : base (startPosition,startVelocity,launchAngle,mass,rho,A,C) {
        this.xposition = [startPosition[0]];
        this.yposition = [startPosition[1]];
        this.launchAngle = launchAngle * Math.PI/180;
        this.xvelocity = [startVelocity*Math.Cos(launchAngle)];
        this.yvelocity = [startVelocity*Math.Sin(launchAngle)];
        this.mass = mass;
        this.rho = rho;
        this.A = A;
        this.C = C;
        this.headwind = headwind;
        this.tailwind = tailwind;    
    }
    public override double[] RK4Step() {
        double dt = 0.01;
        double[] vars1 = [xposition[^1],yposition[^1],xvelocity[^1] - headwind + tailwind,yvelocity[^1]];
        double[] deriv1 = Derivatives(vars1);
        double[] vars2 = [];
        double[] vars3 = [];
        double[] vars4 = [];
        double[] k1 = [];
        double[] k2 = [];
        double[] k3 = [];
        double[] k4 = [];
        for (int i = 0; i < 4; i++) {
            k1 = k1.Append(dt * deriv1[i]).ToArray();
            vars2 = vars2.Append(vars1[i] + (0.5 * k1[i])).ToArray();
        }
        double[] deriv2 = Derivatives(vars2);
        for (int i = 0; i < 4; i++ ) {
            k2 = k2.Append(dt * deriv2[i]).ToArray();
            vars3 = vars3.Append(vars1[i] + (0.5 * k2[i])).ToArray();
        }
        double[] deriv3 = Derivatives(vars3);
        for (int i = 0; i < 4; i++ ) {
            k3 = k3.Append(dt * deriv3[i]).ToArray();
            vars4 = vars4.Append(vars1[i] + k3[i]).ToArray();
        }
        double[] deriv4 = Derivatives(vars4);
        for (int i = 0; i < 4; i++ ) {
            k4 = k4.Append(dt * deriv4[i]).ToArray();
        }
        double[] vars5 = [];
        for (int i = 0; i < 4; i++ ) {
            double start = vars1[i];
            double add = (double)0.16666666667 * (k1[i] + (2 * k2[i]) + (2 * k3[i]) + k4[i]);
            double value = start + add;
            vars5 = vars5.Append(value).ToArray();
        }
        return vars5;
    }
}