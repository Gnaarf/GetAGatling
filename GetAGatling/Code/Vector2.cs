
using System;
public struct Vector2
{
    public float X;
    public float Y;

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return "(" + X + ", " + Y + ")";
    }

    //------------------------------------------//
    //                  Casts                   //
    //------------------------------------------//
    // Box2DX-Vectors
    public static implicit operator Vector2(Box2DX.Common.Vec2 v)
    {
        return new Vector2(v.X, v.Y);
    }

    public static implicit operator Box2DX.Common.Vec2(Vector2 v)
    {
        return new Box2DX.Common.Vec2(v.X, v.Y);
    }

    // SFML-Vectors
    public static implicit operator Vector2(SFML.Window.Vector2f v)
    {
        return new Vector2(v.X, v.Y);
    }

    public static implicit operator SFML.Window.Vector2f(Vector2 v)
    {
        return new SFML.Window.Vector2f(v.X, v.Y);
    }


    //------------------------------------------//
    //                 Constants                //
    //------------------------------------------//
    public static Vector2 Zero { get { return new Vector2(0F, 0F); } }
    public static Vector2 One { get { return new Vector2(1F, 1F); } }
    public static Vector2 Up { get { return new Vector2(0F, 1F); } }
    public static Vector2 Right { get { return new Vector2(1F, 0F); } }


    //------------------------------------------//
    //           Arithmetic Functions           //
    //------------------------------------------//
    public float length { get { return (float)Math.Sqrt(X * X + Y * Y); } }
    public float lengthSqr { get { return X * X + Y * Y; } }

    public Vector2 normalized { get { return this / length; } }
    public Vector2 normalize() { return this /= length; }

    public Vector2 right { get { return new Vector2(Y, -X); } }
    public Vector2 rightNormalized { get { return new Vector2(Y, -X) / length; } }

    //------------------------------------------//
    //           Arithmetic Operators           //
    //------------------------------------------//
    // Addition
    public static Vector2 operator +(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
    }

    // Subtraction
    public static Vector2 operator -(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
    }

    // Multiplication
    public static Vector2 operator *(Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
    }

    public static Vector2 operator *(float f, Vector2 v)
    {
        return new Vector2(f * v.X, f * v.Y);
    }

    public static Vector2 operator *(Vector2 v, float f)
    {
        return new Vector2(f * v.X, f * v.Y);
    }

    // Division
    public static Vector2 operator /(Vector2 v, float f)
    {
        return new Vector2(v.X / f, v.Y / f);
    }
}

