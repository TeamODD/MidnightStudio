using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Production : MonoBehaviour
{
    private IEnumerator Move_Coroutine;
    private IEnumerator Position_Coroutine;
    private IEnumerator WorldPosition_Coroutine;
    private IEnumerator Rotation_Coroutine;
    private IEnumerator Scale_Coroutine;
    private IEnumerator Area_Coroutine;
    private IEnumerator Stretch_Coroutine;
    private IEnumerator Color_Coroutine;
    private IEnumerator Alpha_Coroutine;

    public void Move(string Move_Way, float Moving_Time, string Axis, float Start_Axis, float End_Axis)
    {
        if (Move_Coroutine != null) { StopCoroutine(Move_Coroutine); }
        Move_Coroutine = Move_Sign(Move_Way, Moving_Time, Axis, Start_Axis, End_Axis);
        StartCoroutine(Move_Coroutine);
    }

    public void Position(string Positioning_Way, float Positioning_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Position_Coroutine != null) { StopCoroutine(Position_Coroutine); }
        Position_Coroutine = Position_Sign(Positioning_Way, Positioning_Time, Start_Pos, End_Pos);
        StartCoroutine(Position_Coroutine);
    }

    public void WorldPosition(string Positioning_Way, float Positioning_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (WorldPosition_Coroutine != null) { StopCoroutine(WorldPosition_Coroutine); }
        WorldPosition_Coroutine = WorldPosition_Sign(Positioning_Way, Positioning_Time, Start_Pos, End_Pos);
        StartCoroutine(WorldPosition_Coroutine);
    }

    public void Rotation(string Rotating_Way, float Rotating_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Rotation_Coroutine != null) { StopCoroutine(Rotation_Coroutine); }
        Rotation_Coroutine = Rotation_Sign(Rotating_Way, Rotating_Time, Start_Pos, End_Pos);
        StartCoroutine(Rotation_Coroutine);
    }

    public void Scale(string Scaling_Way, float Scaling_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Scale_Coroutine != null) { StopCoroutine(Scale_Coroutine); }
        Scale_Coroutine = Scale_Sign(Scaling_Way, Scaling_Time, Start_Pos, End_Pos);
        StartCoroutine(Scale_Coroutine);
    }

    public void Area(string Enlarging_Way, float Enlarging_Time, Vector2 Start_Area, Vector2 End_Area)
    {
        if (Area_Coroutine != null) { StopCoroutine(Area_Coroutine); }
        Area_Coroutine = Area_Sign(Enlarging_Way, Enlarging_Time, Start_Area, End_Area);
        StartCoroutine(Area_Coroutine);
    }

    public void Stretch(string Enlarging_Way, float Enlarging_Time, float Start_Area, float End_Area)
    {   
        if (Stretch_Coroutine != null) { StopCoroutine(Stretch_Coroutine); }
        Stretch_Coroutine = Stretch_Sign(Enlarging_Way, Enlarging_Time, Start_Area, End_Area);
        StartCoroutine(Stretch_Coroutine);
    }

    public void Coloring(string Coloring_Way, float Coloring_Time, Color Start_Color, Color End_Color)
    {
        if (Color_Coroutine != null) { StopCoroutine(Color_Coroutine); }
        Color_Coroutine = Coloring_Sign(Coloring_Way, Coloring_Time, Start_Color, End_Color);
        StartCoroutine(Color_Coroutine);
    }

    public void Alpha(string Transparenting_Way, float Transparenting_Time, float Start_Alpha, float End_Alpha)
    {
        if (Alpha_Coroutine != null) { StopCoroutine(Alpha_Coroutine); }
        Alpha_Coroutine = Alpha_Sign(Transparenting_Way, Transparenting_Time, Start_Alpha, End_Alpha);
        StartCoroutine(Alpha_Coroutine);
    }

    public void Anchor(Vector2 Target_Max_Anchor, Vector2 Target_Min_Anchor)
    {
        RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
        Vector3 Target_Ob_WorldPos = Target_Obj_Rect.position;

        Target_Obj_Rect.anchorMax = Target_Max_Anchor;
        Target_Obj_Rect.anchorMin = Target_Min_Anchor;

        Target_Obj_Rect.position = Target_Ob_WorldPos;
    }

    public void Image(Sprite Target_Image)
    {
        Image Target_Obj_Image = gameObject.GetComponent<Image>();
        Target_Obj_Image.sprite = Target_Image;

        RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
        Target_Obj_Rect.sizeDelta = new Vector2(Target_Image.rect.width, Target_Image.rect.height);
    }

    public IEnumerator Move_Sign(string Move_Way, float Moving_Time, string Axis, float Start_Axis, float End_Axis)
    {
        if (Move_Way == "Instant")
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
            Vector3 Target_Obj_Pos = Target_Obj_Rect.anchoredPosition;

            if (Axis == "x") { Target_Obj_Pos.x = End_Axis; }
            else if (Axis == "y") { Target_Obj_Pos.y = End_Axis; }
            else if (Axis == "z") { Target_Obj_Pos.z = End_Axis; }

            Target_Obj_Rect.anchoredPosition = Target_Obj_Pos;
        }
        else
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();

            Vector3 Start_Pos = Target_Obj_Rect.anchoredPosition;
            Vector3 End_Pos = Target_Obj_Rect.anchoredPosition;

            if (Axis == "x") { Start_Pos.x = Start_Axis; }
            else if (Axis == "y") { Start_Pos.y = Start_Axis; }
            else if (Axis == "z") { Start_Pos.z = Start_Axis; }

            if (Axis == "x") { End_Pos.x = End_Axis; }
            else if (Axis == "y") { End_Pos.y = End_Axis; }
            else if (Axis == "z") { End_Pos.z = End_Axis; }

            Target_Obj_Rect.anchoredPosition = Start_Pos;

            float ElapsedTime = 0f;
            while (ElapsedTime < Moving_Time)
            {
                float Obj_Speed = ElapsedTime / Moving_Time;
                if (Move_Way == "Lerp") //��ӵ�
                {
                    Target_Obj_Rect.anchoredPosition = new Vector2(Mathf.Lerp(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.Lerp(Start_Pos.y, End_Pos.y, Obj_Speed));
                }
                else if (Move_Way == "Smooth") //����
                {
                    Target_Obj_Rect.anchoredPosition = new Vector2(Mathf.SmoothStep(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.SmoothStep(Start_Pos.y, End_Pos.y, Obj_Speed));
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_Rect.anchoredPosition = End_Pos;
        }
    }

    public IEnumerator Position_Sign(string Positioning_Way, float Positioning_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Positioning_Way == "Instant")
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
            Target_Obj_Rect.anchoredPosition = End_Pos;
        }
        else
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
            Target_Obj_Rect.anchoredPosition = Start_Pos;

            float ElapsedTime = 0f;
            while (ElapsedTime < Positioning_Time)
            {
                float Obj_Speed = ElapsedTime / Positioning_Time;
                if (Positioning_Way == "Lerp")
                {
                    Target_Obj_Rect.anchoredPosition = new Vector3(Mathf.Lerp(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.Lerp(Start_Pos.y, End_Pos.y, Obj_Speed), 0f);
                }
                else if (Positioning_Way == "Smooth")
                {
                    Target_Obj_Rect.anchoredPosition = new Vector3(Mathf.SmoothStep(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.SmoothStep(Start_Pos.y, End_Pos.y, Obj_Speed), 0f);
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_Rect.anchoredPosition = End_Pos;
        }
    }

    public IEnumerator WorldPosition_Sign(string Positioning_Way, float Positioning_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Positioning_Way == "Instant")
        {
            Transform Target_Obj_Transform = gameObject.GetComponent<Transform>();
            Target_Obj_Transform.position = End_Pos;
        }
        else
        {
            Transform Target_Obj_Transform = gameObject.GetComponent<Transform>();
            Target_Obj_Transform.position = Start_Pos;

            float ElapsedTime = 0f;
            while (ElapsedTime < Positioning_Time)
            {
                float Obj_Speed = ElapsedTime / Positioning_Time;
                if (Positioning_Way == "Lerp")
                {
                    Target_Obj_Transform.position = new Vector3(Mathf.Lerp(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.Lerp(Start_Pos.y, End_Pos.y, Obj_Speed), 0f);
                }
                else if (Positioning_Way == "Smooth")
                {
                    Target_Obj_Transform.position = new Vector3(Mathf.SmoothStep(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.SmoothStep(Start_Pos.y, End_Pos.y, Obj_Speed), 0f);
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_Transform.position = End_Pos;
        }
    }

    public IEnumerator Rotation_Sign(string Rotating_Way, float Rotating_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Rotating_Way == "Instant")
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
            Target_Obj_Rect.rotation = Quaternion.Euler(End_Pos);
        }
        else
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
            Target_Obj_Rect.rotation = Quaternion.Euler(Start_Pos);

            float ElapsedTime = 0f;
            while (ElapsedTime < Rotating_Time)
            {
                float Obj_Speed = ElapsedTime / Rotating_Time;
                if (Rotating_Way == "Lerp")
                {
                    Target_Obj_Rect.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.Lerp(Start_Pos.y, End_Pos.y, Obj_Speed), Mathf.Lerp(Start_Pos.z, End_Pos.z, Obj_Speed)));
                }
                else if (Rotating_Way == "Smooth")
                {
                    Target_Obj_Rect.rotation = Quaternion.Euler(new Vector3(Mathf.SmoothStep(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.SmoothStep(Start_Pos.y, End_Pos.y, Obj_Speed), Mathf.Lerp(Start_Pos.z, End_Pos.z, Obj_Speed)));
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_Rect.rotation = Quaternion.Euler(End_Pos);
        }
    }

    public IEnumerator Scale_Sign(string Scaling_Way, float Scaling_Time, Vector3 Start_Pos, Vector3 End_Pos)
    {
        if (Scaling_Way == "Instant")
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
            Target_Obj_Rect.localScale = End_Pos;
        }
        else
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
            Target_Obj_Rect.localScale = Start_Pos;
            float ElapsedTime = 0f;
            while (ElapsedTime < Scaling_Time)
            {
                float Obj_Speed = ElapsedTime / Scaling_Time;
                if (Scaling_Way == "Lerp")
                {
                    Target_Obj_Rect.localScale = new Vector3(Mathf.Lerp(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.Lerp(Start_Pos.y, End_Pos.y, Obj_Speed), 0f);
                }
                else if (Scaling_Way == "Smooth")
                {
                    Target_Obj_Rect.localScale = new Vector3(Mathf.SmoothStep(Start_Pos.x, End_Pos.x, Obj_Speed), Mathf.SmoothStep(Start_Pos.y, End_Pos.y, Obj_Speed), 0f);
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_Rect.localScale = End_Pos;
        }
    }

    public IEnumerator Area_Sign(string Enlarging_Way, float Enlarging_Time, Vector2 Start_Area, Vector2 End_Area)
    {
        if (Enlarging_Way == "Instant")
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
            Target_Obj_Rect.sizeDelta = End_Area;
        }
        else
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
            Target_Obj_Rect.sizeDelta = Start_Area;
            float ElapsedTime = 0f;
            while (ElapsedTime < Enlarging_Time)
            {
                float Obj_Speed = ElapsedTime / Enlarging_Time;
                if (Enlarging_Way == "Lerp")
                {
                    Target_Obj_Rect.sizeDelta = new Vector2(Mathf.Lerp(Start_Area.x, End_Area.x, Obj_Speed), Mathf.Lerp(Start_Area.y, End_Area.y, Obj_Speed));
                }
                else if (Enlarging_Way == "Smooth")
                {
                    Target_Obj_Rect.sizeDelta = new Vector2(Mathf.SmoothStep(Start_Area.x, End_Area.x, Obj_Speed), Mathf.SmoothStep(Start_Area.y, End_Area.y, Obj_Speed));
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_Rect.sizeDelta = End_Area;
        }
    }

    public IEnumerator Stretch_Sign(string Enlarging_Way, float Enlarging_Time, float Start_Pos, float End_Pos)
    {
        if (Enlarging_Way == "Instant")
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();

            Target_Obj_Rect.offsetMin = new Vector2(End_Pos, Target_Obj_Rect.offsetMin.y);
            Target_Obj_Rect.offsetMax = new Vector2(-End_Pos, Target_Obj_Rect.offsetMax.y);
        }
        else
        {
            RectTransform Target_Obj_Rect = gameObject.GetComponent<RectTransform>();
            Target_Obj_Rect.offsetMin = new Vector2(Start_Pos, Target_Obj_Rect.offsetMin.y);
            Target_Obj_Rect.offsetMax = new Vector2(-Start_Pos, Target_Obj_Rect.offsetMax.y);

            float ElapsedTime = 0f;
            while (ElapsedTime < Enlarging_Time)
            {
                float Obj_Speed = ElapsedTime / Enlarging_Time;
                if (Enlarging_Way == "Lerp")
                {
                    Target_Obj_Rect.offsetMin = new Vector2(Mathf.Lerp(Start_Pos, End_Pos, Obj_Speed), Target_Obj_Rect.offsetMin.y);
                    Target_Obj_Rect.offsetMax = new Vector2(Mathf.Lerp(-Start_Pos, -End_Pos, Obj_Speed), Target_Obj_Rect.offsetMax.y);
                }
                else if (Enlarging_Way == "Smooth")
                {
                    Target_Obj_Rect.offsetMin = new Vector2(Mathf.SmoothStep(Start_Pos, End_Pos, Obj_Speed), Target_Obj_Rect.offsetMin.y);
                    Target_Obj_Rect.offsetMax = new Vector2(Mathf.SmoothStep(-Start_Pos, -End_Pos, Obj_Speed), Target_Obj_Rect.offsetMax.y);
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_Rect.offsetMin = new Vector2(End_Pos, Target_Obj_Rect.offsetMin.y);
            Target_Obj_Rect.offsetMax = new Vector2(-End_Pos, Target_Obj_Rect.offsetMax.y);
        }
    }

    public IEnumerator Coloring_Sign(string Coloring_Way, float Coloring_Time, Color Start_Color, Color End_Color)
    {
        if (Coloring_Way == "Instant")
        {
            Image Target_Obj_Image = gameObject.GetComponent<Image>();
            Target_Obj_Image.color = End_Color;
        }
        else
        {
            Image Target_Obj_Image = gameObject.GetComponent<Image>();
            Target_Obj_Image.color = Start_Color;
            float ElapsedTime = 0f;
            while (ElapsedTime < Coloring_Time)
            {
                float Obj_Speed = ElapsedTime / Coloring_Time;
                if (Coloring_Way == "Lerp")
                {
                    Target_Obj_Image.color = Color.Lerp(Start_Color, End_Color, Obj_Speed);
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_Image.color = End_Color;
        }
    }

    public IEnumerator Alpha_Sign(string Transparenting_Way, float Transparenting_Time, float Start_Alpha, float End_Alpha)
    {
        if (Transparenting_Way == "Instant")
        {
            CanvasGroup Target_Obj_Alpha = gameObject.GetComponent<CanvasGroup>();
            Target_Obj_Alpha.alpha = End_Alpha;
        }
        else
        {
            CanvasGroup Target_Obj_Alpha = gameObject.GetComponent<CanvasGroup>();
            Target_Obj_Alpha.alpha = Start_Alpha;
            float ElapsedTime = 0f;
            while (ElapsedTime < Transparenting_Time)
            {
                float Obj_Speed = ElapsedTime / Transparenting_Time;
                if (Transparenting_Way == "Lerp")
                {
                    Target_Obj_Alpha.alpha = Mathf.Lerp(Start_Alpha, End_Alpha, Obj_Speed);
                }
                else if (Transparenting_Way == "Smooth")
                {
                    Target_Obj_Alpha.alpha = Mathf.SmoothStep(Start_Alpha, End_Alpha, Obj_Speed);
                }
                ElapsedTime += Time.deltaTime;
                yield return null;
            }
            Target_Obj_Alpha.alpha = End_Alpha;
        }
    }
}
