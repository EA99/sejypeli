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
        PushButton icon = new PushButton(kuva);
        Add(icon);
        icon.Clicked += delegate() { SelectItem(item); };
    }

    void SelectItem(PhysicsObject item)
    {
        if (ItemSelected != null)
        {
            ItemSelected(item);
        }
    }

}
