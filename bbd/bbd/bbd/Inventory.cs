using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jypeli;
using Jypeli.Widgets;


/// <summary>
/// Esinevalikko.
/// </summary>
class Inventory : Widget
{
    /// <summary>
    /// Tapahtuma, kun esine on valittu.
    /// </summary>
    public event Action<PhysicsObject> ItemSelected;


    /// <summary>
    /// Luo uuden esinevalikon.
    /// </summary>
    public Inventory()
        : base(new HorizontalLayout())

    {
        
    }
        
    /// <summary>
    /// Lisää esineen.
    /// </summary>
    /// <param name="item">Lisättävä esine.</param>
    /// <param name="kuva">Esineen ikoni, joka näkyy valikossa.</param>
    public void AddItem(PhysicsObject item, Image kuva)
    {
        Image isokuva = new Image(kuva.Width * 3, kuva.Height * 3, Color.White);
        for (int x = 0; x < kuva.Width; x++)
        {
            for (int y = 0; y < kuva.Height; y++)
            {
                isokuva[x * 3, y * 3] = kuva[x, y];
                isokuva[x * 3+1, y * 3] = kuva[x, y];
                isokuva[x * 3+2, y * 3] = kuva[x, y];
                isokuva[x * 3, y * 3+1] = kuva[x, y];
                isokuva[x * 3+1, y * 3+1] = kuva[x, y];
                isokuva[x * 3+2, y * 3+1] = kuva[x, y];
                isokuva[x * 3, y * 3+2] = kuva[x, y];
                isokuva[x * 3+1, y * 3+2] = kuva[x, y];
                isokuva[x * 3+2, y * 3+2] = kuva[x, y];
            }    
        }
        PushButton icon = new PushButton(isokuva);
        Add(icon);
        icon.Clicked += delegate() { SelectItem(item); };
    }

    public void SelectItem(PhysicsObject item)
    {
        if (ItemSelected != null)
        {
            ItemSelected(item);
        }
    }

}
