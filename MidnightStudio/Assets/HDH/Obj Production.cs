using System.Collections;
using UnityEngine;

public class Obj_Production : MonoBehaviour
{
    private IEnumerator Move_Coroutine;
    private IEnumerator Position_Coroutine;
    private IEnumerator Rotation_Coroutine;
    private IEnumerator Scale_Coroutine;
    private IEnumerator Color_Coroutine;

    public void Move(string Move_Way, bool Local, float Moving_Time, string Axis, float Start_Axis, float End_Axis)
    {
        if (Move_Coroutine != null) { StopCoroutine(Move_Coroutine); }
        Move_Coroutine = Move_Sign(Move_Way, Local, Moving_Time, Axis, Start_Axis, End_Axis);
        StartCoroutine(Move_Coroutine);
    }

    public void Position(string Positioning_Way, bool Local, float Positioning_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Position_Coroutine != null) { StopCoroutine(Position_Coroutine); }
        Position_Coroutine = Position_Sign(Positioning_Way, Local, Positioning_Time, Start_Pos, End_Pos);
        StartCoroutine(Position_Coroutine);
    }

    public void Scale(string Scaling_Way, float Scaling_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Scale_Coroutine != null) { StopCoroutine(Scale_Coroutine); }
        Scale_Coroutine = Scale_Sign(Scaling_Way, Scaling_Time, Start_Pos, End_Pos);
        StartCoroutine(Scale_Coroutine);
    }

    public void Coloring(string Coloring_Way, float Coloring_Time, Color Start_Color, Color End_Color)
    {
        if (Color_Coroutine != null) { StopCoroutine(Color_Coroutine); }
        Color_Coroutine = Coloring_Sign(Coloring_Way, Coloring_Time, Start_Color, End_Color);
        StartCoroutine(Color_Coroutine);
    }

    private IEnumerator Move_Sign(string Move_Way, bool Local, float Moving_Time, string Axis, float Start_Axis, float End_Axis)
    {
        if (Move_Way == "Instant")
        {
            Transform Target_Obj_Transform = gameObject.transform;
            Vector3 Target_Obj_Pos;

            if (Local == false) { Target_Obj_Pos = Target_Obj_Transform.position; }
            else { Target_Obj_Pos = Target_Obj_Transform.localPosition; }

            if (Axis == "x") { Target_Obj_Pos.x = End_Axis; }
            else if (Axis == "y") { Target_Obj_Pos.y = End_Axis; }
            else if (Axis == "z") { Target_Obj_Pos.z = End_Axis; }

            if (Local == false) { Target_Obj_Transform.position = Target_Obj_Pos; }
            else { Target_Obj_Transform.localPosition = Target_Obj_Pos; }
        }
        else
        {
            Transform Target_Obj_Transform = gameObject.transform;

            Vector3 Start_Pos;
            Vector3 End_Pos;

            if (Local == false)
            {
                Start_Pos = Target_Obj_Transform.position;
                End_Pos = Target_Obj_Transform.position;
            }
            else
            {
                Start_Pos = Target_Obj_Transform.localPosition;
                End_Pos = Target_Obj_Transform.localPosition;
            }

            if (Axis == "x") { Start_Pos.x = Start_Axis; }
            else if (Axis == "y") { Start_Pos.y = Start_Axis; }
            else if (Axis == "z") { Start_Pos.z = Start_Axis; }

            if (Axis == "x") { End_Pos.x = End_Axis; }
            else if (Axis == "y") { End_Pos.y = End_Axis; }
            else if (Axis == "z") { End_Pos.z = End_Axis; }

            if (Local == false) { Target_Obj_Transform.position = Start_Pos; }
            else { Target_Obj_Transform.localPosition = Start_Pos; }

            float ElapsedTime = 0f;
            while (ElapsedTime < Moving_Time)
            {
                float Obj_Speed = ElapsedTime / Moving_Time;
                if (Move_Way == "Lerp")
                {
                    if (Local == false) { Target_Obj_Transform.position = new Vector2(Mathf.Lerp(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.Lerp(Start_Pos.y, End_Pos.y, Obj_Speed)); }
                    else { Target_Obj_Transform.localPosition = new Vector2(Mathf.Lerp(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.Lerp(Start_Pos.y, End_Pos.y, Obj_Speed)); }
                }
                else if (Move_Way == "Smooth")
                {
                    if (Local == false) { Target_Obj_Transform.position = new Vector2(Mathf.SmoothStep(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.SmoothStep(Start_Pos.y, End_Pos.y, Obj_Speed)); }
                    else { Target_Obj_Transform.localPosition = new Vector2(Mathf.SmoothStep(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.SmoothStep(Start_Pos.y, End_Pos.y, Obj_Speed)); }
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            if (Local == false) { Target_Obj_Transform.position = End_Pos; }
            else { Target_Obj_Transform.localPosition = End_Pos; }
        }
    }

    private IEnumerator Position_Sign(string Positioning_Way, bool Local, float Positioning_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Positioning_Way == "Instant")
        {
            Transform Target_Obj_Transform = gameObject.transform;
            if (Local == false) { Target_Obj_Transform.position = End_Pos; }
            else { Target_Obj_Transform.localPosition = End_Pos; }
        }
        else
        {
            Transform Target_Obj_Transform = gameObject.transform;
            if (Local == false) { Target_Obj_Transform.position = Start_Pos; }
            else { Target_Obj_Transform.localPosition = Start_Pos; }

            float ElapsedTime = 0f;
            while (ElapsedTime < Positioning_Time)
            {
                float Obj_Speed = ElapsedTime / Positioning_Time;
                if (Positioning_Way == "Lerp")
                {
                    if (Local == false) { Target_Obj_Transform.position = new Vector3(Mathf.Lerp(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.Lerp(Start_Pos.y, End_Pos.y, Obj_Speed), Mathf.Lerp(Start_Pos.z, End_Pos.z, Obj_Speed)); }
                    else { Target_Obj_Transform.localPosition = new Vector3(Mathf.Lerp(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.Lerp(Start_Pos.y, End_Pos.y, Obj_Speed), Mathf.Lerp(Start_Pos.z, End_Pos.z, Obj_Speed)); }
                }
                else if (Positioning_Way == "Smooth")
                {
                    if (Local == false) { Target_Obj_Transform.position = new Vector3(Mathf.SmoothStep(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.SmoothStep(Start_Pos.y, End_Pos.y, Obj_Speed), Mathf.SmoothStep(Start_Pos.z, End_Pos.z, Obj_Speed)); }
                    else { Target_Obj_Transform.localPosition = new Vector3(Mathf.SmoothStep(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.SmoothStep(Start_Pos.y, End_Pos.y, Obj_Speed), Mathf.SmoothStep(Start_Pos.z, End_Pos.z, Obj_Speed)); }
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }

            if (Local == false) { Target_Obj_Transform.position = End_Pos; }
            else { Target_Obj_Transform.localPosition = End_Pos; }
        }
    }

    public IEnumerator Scale_Sign(string Scaling_Way, float Scaling_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Scaling_Way == "Instant")
        {
            Transform Target_Obj_Rect = gameObject.GetComponent<Transform>();
            Target_Obj_Rect.localScale = End_Pos;
        }
        else
        {
            Transform Target_Obj_Rect = gameObject.GetComponent<Transform>();
            Target_Obj_Rect.localScale = Start_Pos;
            float ElapsedTime = 0f;
            while (ElapsedTime < Scaling_Time)
            {
                float Obj_Speed = ElapsedTime / Scaling_Time;
                if (Scaling_Way == "Lerp")
                {
                    Target_Obj_Rect.localScale = new Vector3(Mathf.Lerp(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.Lerp(Start_Pos.y, End_Pos.y, Obj_Speed), Mathf.Lerp(Start_Pos.z, End_Pos.z, Obj_Speed));
                }
                else if (Scaling_Way == "Smooth")
                {
                    Target_Obj_Rect.localScale = new Vector3(Mathf.SmoothStep(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.SmoothStep(Start_Pos.y, End_Pos.y, Obj_Speed), Mathf.SmoothStep(Start_Pos.z, End_Pos.z, Obj_Speed));
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_Rect.localScale = End_Pos;
        }
    }

    public IEnumerator Coloring_Sign(string Coloring_Way, float Coloring_Time, Color Start_Color, Color End_Color)
    {
        if (Coloring_Way == "Instant")
        {
            SpriteRenderer Target_Obj_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Target_Obj_SpriteRenderer.color = End_Color;
        }
        else
        {
            SpriteRenderer Target_Obj_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Target_Obj_SpriteRenderer.color = Start_Color;
            float ElapsedTime = 0f;
            while (ElapsedTime < Coloring_Time)
            {
                float Obj_Speed = ElapsedTime / Coloring_Time;
                if (Coloring_Way == "Lerp")
                {
                    Target_Obj_SpriteRenderer.color = Color.Lerp(Start_Color, End_Color, Obj_Speed);
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_SpriteRenderer.color = End_Color;
        }
    }
}
