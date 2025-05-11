using UnityEngine;

public class SynopsisCharacterColoring : MonoBehaviour
{
    public Obj_Production Ink_production;
    public Obj_Production Client_production;

    public SpriteRenderer Ink_SR;
    public SpriteRenderer Client_SR;

    public void SetCharacterColor(string ActivationTarget) {
        switch (ActivationTarget) {
            case "ink":
                Ink_production.Coloring("Instant", 0.2f, Ink_SR.color, new Color(1f, 1f, 1f, 1f));
                Client_production.Coloring("Instant", 0.2f, Client_SR.color, new Color(0.415f, 0.415f, 0.415f, 1f));
                break;
            case "client":
                Ink_production.Coloring("Instant", 0.2f, Ink_SR.color, new Color(0.415f, 0.415f, 0.415f, 1f));
                Client_production.Coloring("Instant", 0.2f, Client_SR.color, new Color(1f, 1f, 1f, 1f));
                break;
            case "managers":
                Ink_production.Coloring("Instant", 0.2f, Ink_SR.color, new Color(0.415f, 0.415f, 0.415f, 1f));
                Client_production.Coloring("Instant", 0.2f, Client_SR.color, new Color(0.415f, 0.415f, 0.415f, 1f));
                break;
            default: 
                Ink_production.Coloring("Instant", 0.2f, Ink_SR.color, new Color(1f, 1f, 1f, 1f));
                Client_production.Coloring("Instant", 0.2f, Client_SR.color, new Color(1f, 1f, 1f, 1f));
                break;
        }
    }

}


