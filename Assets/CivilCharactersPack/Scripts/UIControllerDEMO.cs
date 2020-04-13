using UnityEngine;
using UnityEngine.UI;

public class UIControllerDEMO : MonoBehaviour
{
    public CharacterCustomization CharacterCustomization;

    //UI element number
    public Text head_text;
    public Text skincolor_text;

    public Text hat_text;
    public Text accessory_text;
    public Text shirt_text;
    public Text pant_text;
    public Text shoes_text;

    public Text playbutton_text;

    public Animator[] animators;

    #region ButtonEvents
    public void HeadChange_Event(int next)
    {
        if (next == -1)
            CharacterCustomization.PrevHead();
        else if (next == 1)
            CharacterCustomization.NextHead();

        head_text.text = CharacterCustomization.headActiveIndex.ToString();
    }

    public void HatChange_Event(int next)
    {
        if (next == -1)
            CharacterCustomization.PrevElement(CharacterCustomization.ClothesPartType.Hat);
        else if (next == 1)
            CharacterCustomization.NextElement(CharacterCustomization.ClothesPartType.Hat);

        if (CharacterCustomization.clothesActiveIndexes[CharacterCustomization.ClothesPartType.Hat] == -1)
            hat_text.text = "-";
        else
            hat_text.text = (CharacterCustomization.clothesActiveIndexes[CharacterCustomization.ClothesPartType.Hat] + 1).ToString();
    }

    public void AccessoryChange_Event(int next)
    {
        if (next == -1)
            CharacterCustomization.PrevElement(CharacterCustomization.ClothesPartType.Accessory);
        else if (next == 1)
            CharacterCustomization.NextElement(CharacterCustomization.ClothesPartType.Accessory);

        if (CharacterCustomization.clothesActiveIndexes[CharacterCustomization.ClothesPartType.Accessory] == -1)
            accessory_text.text = "-";
        else
            accessory_text.text = (CharacterCustomization.clothesActiveIndexes[CharacterCustomization.ClothesPartType.Accessory] + 1).ToString();
    }

    public void ShirtChange_Event(int next)
    {
        if (next == -1)
            CharacterCustomization.PrevElement(CharacterCustomization.ClothesPartType.TShirt);
        else if (next == 1)
            CharacterCustomization.NextElement(CharacterCustomization.ClothesPartType.TShirt);

        if (CharacterCustomization.clothesActiveIndexes[CharacterCustomization.ClothesPartType.TShirt] == -1)
            shirt_text.text = "-";
        else
            shirt_text.text = (CharacterCustomization.clothesActiveIndexes[CharacterCustomization.ClothesPartType.TShirt] + 1).ToString();
    }
    public void PantChange_Event(int next)
    {
        if (next == -1)
            CharacterCustomization.PrevElement(CharacterCustomization.ClothesPartType.Pants);
        else if (next == 1)
            CharacterCustomization.NextElement(CharacterCustomization.ClothesPartType.Pants);

        if (CharacterCustomization.clothesActiveIndexes[CharacterCustomization.ClothesPartType.Pants] == -1)
            pant_text.text = "-";
        else
            pant_text.text = (CharacterCustomization.clothesActiveIndexes[CharacterCustomization.ClothesPartType.Pants] + 1).ToString();
    }

    public void ShoesChange_Event(int next)
    {
        if (next == -1)
            CharacterCustomization.PrevElement(CharacterCustomization.ClothesPartType.Shoes);
        else if (next == 1)
            CharacterCustomization.NextElement(CharacterCustomization.ClothesPartType.Shoes);

        if (CharacterCustomization.clothesActiveIndexes[CharacterCustomization.ClothesPartType.Shoes] == -1)
            shoes_text.text = "-";
        else
            shoes_text.text = (CharacterCustomization.clothesActiveIndexes[CharacterCustomization.ClothesPartType.Shoes] + 1).ToString();
    }

    public void SkinChange_Event(int next)
    {
        if (next == -1)
            CharacterCustomization.PrevCharacterMaterial();
        else if (next == 1)
            CharacterCustomization.NextCharacterMaterial();

        skincolor_text.text = CharacterCustomization.materialActiveIndex.ToString();
    }

    bool walk_active = false;
    public void PlayAnim()
    {
        walk_active = !walk_active;

        foreach(var animator in animators)
            animator.SetBool("walk", walk_active);

        playbutton_text.text = (walk_active) ? "STOP" : "PLAY";
    }


    #endregion
}
