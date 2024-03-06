using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
class Square : Shape {

    private int _side;

    public Square(string color, int side) : base (color) {
        _side = side;
    }

    public override double GetArea() {
        return _side * _side;
    }
}