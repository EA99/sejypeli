using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class RainbowFly : PhysicsGame
{
    GameObject sateenkaari;
    PhysicsObject hahmo;
    Vector lanta;
    IntMeter pisteLaskuri;
    
    public override void Begin()
    {
        
        hahmoluo();
        luoraha();
        LuoPistelaskuri();

        Image bgImg = LoadImage("tausta");
        Level.Size = new Vector(bgImg.Width * 50, bgImg.Height);
        Level.Background.Image = LoadImage("tausta");
        Level.Background.TileToLevel();
        Level.Background.MoveTo(new Vector(-Screen.Width, 0), 100);
        
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Down, ButtonState.Down, liial,"",new Vector(0,-140));
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Down, ButtonState.Released,stopperi, "");
        Keyboard.Listen(Key.Up, ButtonState.Down, liial, "", new Vector(0,140));
        Keyboard.Listen(Key.Up, ButtonState.Released, stopperi, "");
    }
    void hahmoluo()
    {
        hahmo = new PhysicsObject(40, 65);
        hahmo.Shape = Shape.Circle;
        hahmo.Color = Color.Red;
        Add(hahmo);
        Camera.Follow(hahmo);
        Gravity = new Vector(200,0);
        hahmo.LinearDamping=0.996;
        hahmo.CanRotate = false;
        lanta = hahmo.Position;
        sateenkaari= new GameObject(1,65);
        Add(sateenkaari,-1);
        sateenkaari.Image = LoadImage("sateenkaari");
        
    }

    protected override void Update(Time time)
    {
        sateenkaari.X = (lanta.X + hahmo.X)/2;
        sateenkaari.Y = hahmo.Y;
        sateenkaari.Width = hahmo.X - lanta.X;
        base.Update(time);
    }

    void luoraha()
    {
        IPhysicsObject raha=new PhysicsObject(20, 20);
        raha.Color = Color.Yellow;
        raha.Shape = Shape.Circle;
        //raha.Position = RandomGen.NextVector(100, 400);
        raha.Position = Level.GetRandomPosition();
        raha.IgnoresGravity = true;
        Add(raha);
        AddCollisionHandler(hahmo, raha, rahsaa);
        Timer.SingleShot(0.2, luoraha);
    }
    
    void liial(Vector suunta)
    {
        hahmo.Push(suunta);
    }
    void stopperi()
    {
        hahmo.Velocity = new Vector(
            hahmo.Velocity.X, 0 );
    }
    void rahsaa(IPhysicsObject hahmo,IPhysicsObject raha)
    {
        raha.Destroy();
        pisteLaskuri.Value += 100;
    }
    void LuoPistelaskuri()
    {
        pisteLaskuri = new IntMeter(0);

        Label pisteNaytto = new Label(100,100);
        pisteNaytto.X = Screen.Left + 100;
        pisteNaytto.Y = Screen.Top - 100;
        pisteNaytto.TextColor = Color.Black;
        pisteNaytto.Color = Color.White;
        pisteNaytto.TextScale = new Vector(2,2);

        pisteNaytto.BindTo(pisteLaskuri);
        Add(pisteNaytto);
    }
}
