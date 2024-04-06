class Combo : Trajectory {
    private double temperature;
    private double headwind;
    private double tailwind;
    private double angVelocity;
    private double[] zposition;
    private double[] zvelocity;
    private double B1;
    private double B2;
    private string[] combo;
    private double[] variables;
    public Combo(double[] startPosition, double startVelocity, double launchAngle, double mass, double rho, double A, double C,string[] combo, double[] variables) : base(startPosition,startVelocity,launchAngle,mass,rho,A,C) {
        this.xposition = [startPosition[0]];
        this.yposition = [startPosition[1]];
        this.launchAngle = launchAngle * Math.PI/180;
        this.xvelocity = [startVelocity*Math.Cos(launchAngle)];
        this.yvelocity = [startVelocity*Math.Sin(launchAngle)];
        this.mass = mass;
        this.rho = rho;
        this.A = A;
        this.C = C;
        this.combo = combo;
        this.variables = variables;
        foreach (string i in combo) {
            if (i == "Magnus Force") {
                this.zposition = [startPosition[2]];
                this.zvelocity = [0];
            }
        }
    }
    private void InitializeVariables() {
        int index = 0;
        foreach (string i in combo) {
            if (i == "Adiabatic Density") {
                this.temperature = variables[index];
                index += 1;
            }
            else if (i == "Headwind/Tailwind") {
                this.headwind = variables[index];
                this.tailwind = variables[index + 1];
                index += 2;
            }
            else if (i == "Magnus Force") {
                this.angVelocity = variables[index];
                index += 1;
            }
            else if (i == "Viscous/Collisional Drag") {
                this.B1 = variables[index];
                this.B2 = variables[index + 1];
                index += 2;
            }
        }
    }
    private double CalcMagnusForce(double S0, double[] v) {
        double MagnusForce = S0*angVelocity*v[0];
        return MagnusForce;
    }
    public override double[] Derivatives(double[] vars) {
        double[] r = [];
        double[] v = [];
        int option = 0;
        bool AD = false;
        bool MF = false;
        bool VC = false;
        foreach (string i in combo) {
            if (i == "Adiabatic Density") {
                AD = true;
            }
            if (i == "Magnus Force") {
                MF = true;
            }
            if (i == "Viscous/Collisional Drag") {
                VC = true;
            }
        }
        if (AD == true && MF == true && VC == true) {
            option = 1;
        }
        else if (AD == true && MF == true) {
            option = 2;
        }
        else if (AD == true && VC == true) {
            option = 3;
        }
        else if (MF == true && VC == true) {
            option = 4;
        }
        else if (AD == true && MF == false && VC == false) {
            option = 5;
        }
        else if (MF == true && AD == false && VC == false) {
            option = 6;
        }
        else if (VC == true && AD == false && MF == false) {
            option = 7;
        }

        if (option == 3 || option == 5 || option == 7) {
            for (int i = 0; i < vars.Length; i++) {
                if (i < 2) {
                    r = r.Append(vars[i]).ToArray();
                }
                if (i >= 2) {
                    v = v.Append(vars[i]).ToArray();
                }
            }
        }
        else if (option == 1 || option == 2 || option == 4 || option == 6) {
            for (int i = 0; i < vars.Length; i++) {
                if (i < 3) {
                    r = r.Append(vars[i]).ToArray();
                }
                if (i >= 3) {
                    v = v.Append(vars[i]).ToArray();
                }
            }
        }
        double velocity;
        double xderiv = v[0];
        double yderiv = v[1];
        double zderiv;
        double vxderiv;
        double vyderiv;
        double vzderiv;
        if (option == 1) {
            double S0 = 4.1*Math.Pow(10,-4)*mass;
            double a = 6.5*Math.Pow(10,-3);
            double alpha = 2.5;
            double currentrho = rho*Math.Pow(1 - a*r[1]/temperature,alpha);
            velocity = Math.Sqrt(v[0]*v[0] + v[1]*v[1] + v[2]*v[2]);
            zderiv = v[2];
            vxderiv = 0 - 0.5*rho*A*C*v[0]*velocity/mass + (-B1*v[0] - B2*velocity*v[0])/mass; 
            vyderiv = -9.8 - 0.5*(currentrho/rho)*A*C*v[1]*velocity/mass + (-B1*v[1] - B2*velocity*v[1])/mass;
            vzderiv = - CalcMagnusForce(S0,v)/mass;
            return [xderiv, yderiv, zderiv, vxderiv, vyderiv, vzderiv];
        }
        else if (option == 2) {
            double S0 = 4.1*Math.Pow(10,-4)*mass;
            double a = 6.5*Math.Pow(10,-3);
            double alpha = 2.5;
            double currentrho = rho*Math.Pow(1 - a*r[1]/temperature,alpha);
            velocity = Math.Sqrt(v[0]*v[0] + v[1]*v[1] + v[2]*v[2]);
            zderiv = v[2];
            vxderiv = 0 - 0.5*rho*A*C*v[0]*velocity/mass;
            vyderiv = -9.8 - 0.5*(currentrho/rho)*A*C*v[1]*velocity/mass;
            vzderiv = - CalcMagnusForce(S0,v)/mass;
            return [xderiv, yderiv, zderiv, vxderiv, vyderiv, vzderiv];
        }
        else if (option == 3) {
            double a = 6.5*Math.Pow(10,-3);
            double alpha = 2.5;
            double currentrho = rho*Math.Pow(1 - a*r[1]/temperature,alpha);
            velocity = Math.Sqrt(v[0]*v[0] + v[1]*v[1]);
            vxderiv = 0 - 0.5*rho*A*C*v[0]*velocity/mass + (-B1*v[0] - B2*velocity*v[0])/mass;
            vyderiv = -9.8 - 0.5*(currentrho/rho)*A*C*v[1]*velocity/mass + (-B1*v[1] - B2*velocity*v[1])/mass;
            return [xderiv, yderiv, vxderiv, vyderiv];
        }
        else if (option == 4) {
            double S0 = 4.1*Math.Pow(10,-4)*mass;
            velocity = Math.Sqrt(v[0]*v[0] + v[1]*v[1] + v[2]*v[2]);
            zderiv = v[2];
            vxderiv = 0 - 0.5*rho*A*C*v[0]*velocity/mass + (-B1*v[0] - B2*velocity*v[0])/mass;
            vyderiv = -9.8 - 0.5*rho*A*C*v[1]*velocity/mass + (-B1*v[1] - B2*velocity*v[1])/mass;
            vzderiv = - CalcMagnusForce(S0,v)/mass;
            return [xderiv, yderiv, zderiv, vxderiv, vyderiv, vzderiv];
        }
        else if (option == 5) {
            double a = 6.5*Math.Pow(10,-3);
            double alpha = 2.5;
            double currentrho = rho*Math.Pow(1 - a*r[1]/temperature,alpha);
            velocity = Math.Sqrt(v[0]*v[0] + v[1]*v[1]);
            vxderiv = 0 - 0.5*rho*A*C*v[0]*velocity/mass;
            vyderiv = -9.8 - 0.5*(currentrho/rho)*A*C*v[1]*velocity/mass;
            return [xderiv, yderiv, vxderiv, vyderiv];
        }
        else if (option == 6) {
            double S0 = 4.1*Math.Pow(10,-4)*mass;
            velocity = Math.Sqrt(v[0]*v[0] + v[1]*v[1] + v[2]*v[2]);
            zderiv = v[2];
            vxderiv = 0 - 0.5*rho*A*C*v[0]*velocity/mass;
            vyderiv = -9.8 - 0.5*rho*A*C*v[1]*velocity/mass;
            vzderiv = - CalcMagnusForce(S0,v)/mass;
            return [xderiv, yderiv, zderiv, vxderiv, vyderiv, vzderiv];
        } 
        else if (option == 7) {
            velocity = Math.Sqrt(v[0]*v[0] + v[1]*v[1]);
            vxderiv = 0 - 0.5*rho*A*C*v[0]*velocity/mass + (-B1*v[0] - B2*velocity*v[0])/mass;
            vyderiv = -9.8 - 0.5*rho*A*C*v[1]*velocity/mass + (-B1*v[1] - B2*velocity*v[1])/mass;
            return [xderiv, yderiv, vxderiv, vyderiv];
        }
        return [];
    }
    public override double[] RK4Step() {
        double dt = 0.01;
        bool HT = false;
        bool MF = false;
        foreach (string i in combo) {
            if (i == "Headwind/Tailwind") {
                HT = true;
            }
            if (i == "Magnus Force") {
                MF = true;
            }
        }
        double[] vars1 = [];
        if (HT == false && MF == false) {
            vars1 = [xposition[^1],yposition[^1],xvelocity[^1],yvelocity[^1]];
        }
        else if (HT == true && MF == false) {
            vars1 = [xposition[^1],yposition[^1],xvelocity[^1] - headwind + tailwind,yvelocity[^1]];
        }
        else if (MF == true && HT == false) {
            vars1 = [xposition[^1],yposition[^1],zposition[^1],xvelocity[^1],yvelocity[^1],zvelocity[^1]];
        }
        else if (HT == true && MF == true) {
            vars1 = [xposition[^1],yposition[^1],zposition[^1],xvelocity[^1] - headwind + tailwind,yvelocity[^1],zvelocity[^1]];
        }
        double[] deriv1 = Derivatives(vars1);
        double[] vars2 = [];
        double[] vars3 = [];
        double[] vars4 = [];
        double[] k1 = [];
        double[] k2 = [];
        double[] k3 = [];
        double[] k4 = [];
        for (int i = 0; i < vars1.Length; i++) {
            k1 = k1.Append(dt * deriv1[i]).ToArray();
            vars2 = vars2.Append(vars1[i] + (0.5 * k1[i])).ToArray();
        }
        double[] deriv2 = Derivatives(vars2);
        for (int i = 0; i < vars1.Length; i++ ) {
            k2 = k2.Append(dt * deriv2[i]).ToArray();
            vars3 = vars3.Append(vars1[i] + (0.5 * k2[i])).ToArray();
        }
        double[] deriv3 = Derivatives(vars3);
        for (int i = 0; i < vars1.Length; i++ ) {
            k3 = k3.Append(dt * deriv3[i]).ToArray();
            vars4 = vars4.Append(vars1[i] + k3[i]).ToArray();
        }
        double[] deriv4 = Derivatives(vars4);
        for (int i = 0; i < vars1.Length; i++ ) {
            k4 = k4.Append(dt * deriv4[i]).ToArray();
        }
        double[] vars5 = [];
        for (int i = 0; i < vars1.Length; i++ ) {
            double start = vars1[i];
            double add = (double)0.16666666667 * (k1[i] + (2 * k2[i]) + (2 * k3[i]) + k4[i]);
            double value = start + add;
            vars5 = vars5.Append(value).ToArray();
        }
        return vars5;
    }
    public override (double[],double[]) CalcTrajectory() {
        InitializeVariables();
        while (yposition[^1] >= 0) {
            double[] variables = RK4Step();
            int option = 1;
            foreach (string i in combo) {
                if (i == "Magnus Force") {
                    option = 2;
                }
            }
            if (option == 1) {
                xposition = xposition.Append(variables[0]).ToArray();
                yposition = yposition.Append(variables[1]).ToArray();
                xvelocity = xvelocity.Append(variables[2]).ToArray();
                yvelocity = yvelocity.Append(variables[3]).ToArray();
            }
            else if (option == 2) {
                xposition = xposition.Append(variables[0]).ToArray();
                yposition = yposition.Append(variables[1]).ToArray();
                zposition = zposition.Append(variables[2]).ToArray();
                xvelocity = xvelocity.Append(variables[3]).ToArray();
                yvelocity = yvelocity.Append(variables[4]).ToArray();
                zvelocity = zvelocity.Append(variables[5]).ToArray();
            }
        }
        return (xposition,yposition);
    }
}