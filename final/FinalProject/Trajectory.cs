using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;
class Trajectory {
    protected double[] xposition;
    protected double[] yposition;
    protected double[] xvelocity;
    protected double[] yvelocity;
    protected double launchAngle;
    protected double mass;
    protected double rho;
    protected double A;
    protected double C;

    public Trajectory(double[] startPosition, double startVelocity, double launchAngle, double mass, double rho, double A, double C) {
        this.xposition = [startPosition[0]];
        this.yposition = [startPosition[1]];
        this.launchAngle = launchAngle * Math.PI/180;
        this.xvelocity = [startVelocity*Math.Cos(launchAngle)];
        this.yvelocity = [startVelocity*Math.Sin(launchAngle)];
        this.mass = mass;
        this.rho = rho;
        this.A = A;
        this.C = C;
    }
    public virtual double[] Derivatives(double[] vars) {
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
        double vxderiv = 0 - 0.5*rho*A*C*v[0]*velocity/mass;
        double vyderiv = -9.8 - 0.5*rho*A*C*v[1]*velocity/mass;

        return [xderiv, yderiv, vxderiv, vyderiv];
    }
    public virtual double[] RK4Step() {
        double dt = 0.01;
        double[] vars1 = [xposition[^1],yposition[^1],xvelocity[^1],yvelocity[^1]];
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
    public virtual (double[],double[]) CalcTrajectory() {
        while (yposition[^1] >= 0) {
            double[] variables = RK4Step();
            xposition = xposition.Append(variables[0]).ToArray();
            yposition = yposition.Append(variables[1]).ToArray();
            xvelocity = xvelocity.Append(variables[2]).ToArray();
            yvelocity = yvelocity.Append(variables[3]).ToArray();
        }
        return (xposition,yposition);
    }
}