using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class rurha_harjoitus : PhysicsGame

{
    PhysicsObject omena = new PhysicsObject(90, 90);
    public override void Begin()
    {
        uusiomena();
        Level.CreateBorders(false);
        Camera.ZoomToLevel();
        Timer ajastin = new Timer();
        ajastin.Interval = 4.0;
        ajastin.Timeout += uusiomena;
        ajastin.Start(100);
        
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        IsMouseVisible = true;
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void uusiomena()
    {
        //luoomena();
        PhysicsObject omena = luoomena();
        Mouse.ListenOn(omena, MouseButton.Left, ButtonState.Pressed, osui, "", omena);
        if 
    }
    PhysicsObject luoomena()                                                     
        {
             PhysicsObject omena = new PhysicsObject(90,90);
             omena.Shape = Shape.Circle;
             omena.Color = Color.Red;
             Gravity = new Vector(0, -300.0);
             Vector leveys = RandomGen.NextVector(-400,300);
             leveys.Y = +100;
             //Vector korkeus = new Vector(RandomGen.NextVector(-100, 0));
             omena.Position = leveys;
             Add(omena);
             
             return omena;
        }
     void osui(IPhysicsObject omena)
     {
         
          omena.Destroy();
     }
     void maa()
     {
         omena.Color = Color.Black;
     }
}
