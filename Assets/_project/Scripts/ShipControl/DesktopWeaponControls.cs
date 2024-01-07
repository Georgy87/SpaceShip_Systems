using UnityEngine;
public class DesktopWeaponControls : WeaponControlsBase
{
    public override bool PrimaryFired => Input.GetMouseButton(0);

    public override bool SecondaryFired => Input.GetMouseButton(1);
}