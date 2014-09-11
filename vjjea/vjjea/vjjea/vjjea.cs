using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class vjjea : Game
{
    PhysicsObject SeInA;
    List<Label> valikonKohdat;
    public override void Begin()
    {
        valikko();
        IsMouseVisible = true;
        
        //jäämetsä();
        //Camera.ZoomToLevel();
        
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

    }
    void jäämetsä()
    {
        ColorTileMap jaametsa = ColorTileMap.FromLevelAsset("ice_forest");
        jaametsa.SetTileMethod(Color.Black, seina);
        jaametsa.Execute(40, 40);
    }
    void seina(Vector paikka,double leveys,double korkeus)
    {
        SeInA = new PhysicsObject(40, 40);
        SeInA.Color = Color.Blue;
        SeInA.Position=paikka;
        Add(SeInA);
    }
    void valikko()
    {
        valikonKohdat = new List<Label>();
        Label kohta1 = new Label("play mode");  
        kohta1.Position = new Vector(0, 40);  
        valikonKohdat.Add(kohta1);
        Label kohta2 = new Label("Arcade mode");
        kohta2.Position = new Vector(0, 0);
        valikonKohdat.Add(kohta2);
        Label kohta3 = new Label("options");
        kohta3.Position = new Vector(0, -40);
        valikonKohdat.Add(kohta3);
        Label kohta4 = new Label("Quit");
        kohta4.Position = new Vector(0, -80);
        valikonKohdat.Add(kohta4);
        foreach (Label valikonKohta in valikonKohdat)
        {
            Add(valikonKohta);
        }
        Mouse.ListenOn(kohta1, MouseButton.Left, ButtonState.Pressed, playmode, null);
        Mouse.ListenOn(kohta2, MouseButton.Left, ButtonState.Pressed, arcademode, null);
        Mouse.ListenOn(kohta3, MouseButton.Left, ButtonState.Pressed, options, null);
        Mouse.ListenOn(kohta4, MouseButton.Left, ButtonState.Pressed, Exit, null);
        Mouse.ListenMovement(1.0, ValikossaLiikkuminen, null);
    }
    void playmode()
    {
    }
    void arcademode()
    {

    }
    void options()
    {
    }
    void ValikossaLiikkuminen(AnalogState hiirenTila)
    {
        foreach (Label kohta in valikonKohdat)
        {
            if (Mouse.IsCursorOn(kohta))
            {
                kohta.TextColor = Color.Red;
            }
            else
            {
                kohta.TextColor = Color.Black;
            }

        }
    }
}
