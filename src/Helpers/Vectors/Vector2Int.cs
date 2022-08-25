namespace ProjectCheddarServer.Vectors;

public class Vector2Int : IEquatable<Vector2Int>
{
    private int x;
    public int X { get { return x; } set { x = value; } }
    private int y;
    public int Y { get { return y; } set { y = value; } }

    public Vector2Int()
    {
        x = 0;
        y = 0;
    }

    public Vector2Int(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public override string ToString()
    {
        return "Vector2Int: (" + x + ", " + y + ")";
    }

    ///<summary>Returns the absolute difference between two vectors.</summary>
    public static Vector2Int Difference(Vector2Int a, Vector2Int b)
    {
        return new() { X = Math.Abs(a.X - b.X), Y = Math.Abs(a.Y - b.Y)};
    }
    ///<summary>Returns true if both values of both V2 is the same, otherwise returns false.</summary>
    public bool Equals(Vector2Int other)
    {
        if (other is null) return false;
        if (this.X == other.X && this.Y == other.Y) return true;
        return false;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 89;
            hash = hash * 97 + x.GetHashCode();
            hash = hash * 97 + y.GetHashCode();
            return hash;
        }
    }
}