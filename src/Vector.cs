using System.Dynamic;

public class Vector {


    public static readonly Vector Up = new Vector(0, 1);
    public static readonly Vector Left = new Vector(1, 0);
    public static readonly Vector Down = new Vector(0, 1);
    public static readonly Vector Right = new Vector(-1, 0);

    public int x;
    public int y;

    public Vector() {
        x = y = 0;
    }

    public int Hash() {
        //return 3+(x+y+y);
        return (x+y+y);
        /*if(x != 0)
            return x>0?0:1;
        return y>0?2:3;*/
    }

    public Vector(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public void Set(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public void Set(Vector v) {
        this.x = v.x;
        this.y = v.y;
    }

    public static Vector operator + (Vector v1, Vector v2) {
        return new Vector(v1.x + v2.x, v1.y + v2.y);
    }

    public static Vector operator - (Vector v1, Vector v2) {
        return new Vector(v1.x - v2.x, v1.y - v2.y);
    }

    public override string ToString() {
        return $"{{{x},{y}}}";
    }
}