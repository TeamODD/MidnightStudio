using UnityEngine;

public class SynopsisColorChange : MonoBehaviour
{
    public Obj_Production Ink_production;
    public Obj_Production Client_production;

    public SpriteRenderer Client;
    public SpriteRenderer InkHead;

    private void SetColor(Obj_Production target, string color) {
        switch (color) { 
            case "white":
                target.Coloring("Instant", 0.2f, new Color(1f, 1f, 1f, 1f), new Color(0.415f, 0.415f, 0.415f, 1f));
                break;
            case "black":
                target.Coloring("Instant", 0.2f, new Color(0.415f, 0.415f, 0.415f, 1f), new Color(1f, 1f, 1f, 1f));
                break;
            default:
                break;
        }

    }

    public void SetCharacterColorDark() {
        //obj_production.Coloring("Instant", 0.2f, new Color(1f, 1f, 1f, 1f), new Color(0.415f, 0.415f, 0.415f, 1f));
    }

    public void SetCharacterColorWhite()
    {
        //obj_production.Coloring("Instant", 0.2f, new Color(0.415f, 0.415f, 0.415f, 1f), new Color(1f, 1f, 1f, 1f));
    }


    public void SetCharacterColor(string target, string color) {
        switch (target) {
            case "ink":
                //코드
                break;
            case "client":
                //코드
                break;
            default: 
                //코드
                break;
        }
    }
}


