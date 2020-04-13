using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for customization character
/// </summary>
public class CharacterCustomization : MonoBehaviour
{
    /// <summary>
    /// All character mesh parts
    /// </summary>
    public List<CharacterPart> characterParts = new List<CharacterPart>();
    [Space(15)]
    /// <summary>
    /// Anchors for clothes
    /// </summary>
    public List<ClothesAnchor> clothesAnchors = new List<ClothesAnchor>();

    /// <summary>
    /// LodGroup component
    /// </summary>
    LODGroup lodGroup;
    
    public enum ClothesPartType: int
    {
        Hat,
        TShirt,
        Pants,
        Shoes,
        Accessory
    }

    //Presets for character customization
    [Space(15)]    
    public List<HeadPreset> headsPresets = new List<HeadPreset>();
    [Space(5)]
    public List<ClothPreset> hatsPresets = new List<ClothPreset>();
    public List<ClothPreset> accessoryPresets = new List<ClothPreset>();
    public List<ClothPreset> shirtsPresets = new List<ClothPreset>();
    public List<ClothPreset> pantsPresets = new List<ClothPreset>();
    public List<ClothPreset> shoesPresets = new List<ClothPreset>();

    public List<Material> skinMaterialPresets = new List<Material>();
    public int headActiveIndex { get; private set; } = 0;
    public int materialActiveIndex { get; private set; } = 0;

    /// <summary>
    /// All indexes for each cloth
    /// </summary>
    public Dictionary<ClothesPartType, int> clothesActiveIndexes = new Dictionary<ClothesPartType, int>()
    {
        { ClothesPartType.Hat, -1 },
        { ClothesPartType.Accessory, -1 },
        { ClothesPartType.TShirt, -1 },
        { ClothesPartType.Pants, -1 },
        { ClothesPartType.Shoes, -1 },
    };

    #region Character elements iterator
    public void NextHead()
    {
        int next = headActiveIndex + 1;

        if (next > headsPresets.Count -1)
            next = 0;

        SetHeadByIndex(next);
    }
    public void PrevHead()
    {
        int next = headActiveIndex - 1;

        if (next < 0)
            next = headsPresets.Count - 1;

        SetHeadByIndex(next);
    }

    public void NextElement(ClothesPartType type)
    {
        int next = clothesActiveIndexes[type] + 1;



        if (next > getPresetArray(type).Count - 1)
            next = -1;

        SetElementByIndex(type, next);
    }
    public void PrevElement(ClothesPartType type)
    {
        int next = clothesActiveIndexes[type] - 1;

        if (next < -1)
            next = getPresetArray(type).Count - 1;

        SetElementByIndex(type, next);
    }

    public void NextCharacterMaterial()
    {
        int next = materialActiveIndex + 1;

        if (next > skinMaterialPresets.Count - 1)
            next = 0;

        SetCharacterMaterialByIndex(next);
    }
    public void PrevCharacterMaterial()
    {
        int next = materialActiveIndex - 1;

        if (next < 0)
            next = skinMaterialPresets.Count - 1;

        SetCharacterMaterialByIndex(next);
    }
    #endregion

    #region Basic functions

    /// <summary>
    /// Set character clothes
    /// </summary>
    /// <param name="type">Type of clothes</param>
    /// <param name="index">Index of element</param>
    public void SetElementByIndex(ClothesPartType type, int index)
    {

        ClothesAnchor ca = GetClothesAnchor(type);

        ClothPreset clothPreset = getPreset(type, clothesActiveIndexes[type]);


        if (clothPreset != null)
            UnHideParts(clothPreset.hideParts, type);

        if (index != -1)
        {
            var newPreset = getPreset(type, index);

            for(var i = 0; i < ca.skinnedMesh.Length; i++)
            {
                ca.skinnedMesh[i].sharedMesh = newPreset.mesh[i];
            }

            HideParts(newPreset.hideParts);
        }
        else
        {
            foreach(var sm in ca.skinnedMesh)
                sm.sharedMesh = null;
        }

        clothesActiveIndexes[type] = index;
    }

    /// <summary>
    /// Set character head mesh
    /// </summary>
    /// <param name="index"Index of element></param>
    public void SetHeadByIndex(int index)
    {
        CharacterPart head = GetCharacterPart("Head");

        for(var i = 0;i < headsPresets[index].mesh.Length; i++)
        {
            head.skinnedMesh[i].sharedMesh = headsPresets[index].mesh[i];
        }

        headActiveIndex = index;
    }

    /// <summary>
    /// Get preset array by type
    /// </summary>
    List<ClothPreset> getPresetArray(ClothesPartType type)
    {
        switch (type)
        {
            case ClothesPartType.Hat:
                return hatsPresets;

            case ClothesPartType.TShirt:
                return shirtsPresets;

            case ClothesPartType.Pants:
                return pantsPresets;

            case ClothesPartType.Shoes:
                return shoesPresets;

            case ClothesPartType.Accessory:
                return accessoryPresets;
            default:
                return null;
        }
    }

    /// <summary>
    /// Get preset element
    /// </summary>
    /// <param name="type">Type of clothes</param>
    /// <param name="index">Index</param>
    /// <returns></returns>
    ClothPreset getPreset(ClothesPartType type, int index)
    {
        if (index == -1)
            return null;
        switch (type)
        {
            case ClothesPartType.Hat:
                return hatsPresets[index];

            case ClothesPartType.TShirt:
                return shirtsPresets[index];

            case ClothesPartType.Pants:
                return pantsPresets[index];

            case ClothesPartType.Shoes:
                return shoesPresets[index];

            case ClothesPartType.Accessory:
                return accessoryPresets[index];
            default:
                return null;
        }
    }
    /// <summary>
    /// Get clothes anchor by type
    /// </summary>
    /// <param name="type">Type of clothes</param>
    /// <returns></returns>
    public ClothesAnchor GetClothesAnchor(ClothesPartType type)
    {
        foreach (ClothesAnchor p in clothesAnchors)
        {
            if (p.partType == type)
                return p;
        }
        return null;
    }

    /// <summary>
    /// Get character part by name
    /// </summary>
    /// <param name="name">Part name</param>
    /// <returns></returns>
    public CharacterPart GetCharacterPart(string name)
    {
        foreach(CharacterPart p in characterParts)
        {
            if (p.name == name)
                return p;
        }
        return null;
    }

    /// <summary>
    /// Hide character parts
    /// </summary>
    /// <param name="parts">Array of parts to hide</param>
    public void HideParts(string[] parts)
    {
        foreach(string p in parts)
        {
            foreach(CharacterPart cp in characterParts)
            {
                if (cp.name == p && cp.skinnedMesh[0].enabled)
                {
                    foreach(var mesh in cp.skinnedMesh)
                        mesh.enabled = false;

                }
            }
        }
    }
    /// <summary>
    /// UnHide character parts
    /// </summary>
    /// <param name="parts">Array of parts to unhide</param>
    public void UnHideParts(string[] parts, ClothesPartType hidePartsForElement)
    {
        foreach (string p in parts)
        {
            bool ph_in_shirt = false, ph_in_pants = false, ph_in_shoes = false;

            #region Code to exclude the UnHide parts of the character that are hidden in active presets
            int shirt_i = clothesActiveIndexes[ClothesPartType.TShirt];
            int pants_i = clothesActiveIndexes[ClothesPartType.Pants];
            int shoes_i = clothesActiveIndexes[ClothesPartType.Shoes];

            if (shirt_i != -1 && hidePartsForElement != ClothesPartType.TShirt) {
                foreach(var shirtPart in getPreset(ClothesPartType.TShirt, shirt_i).hideParts)
                {
                    if (shirtPart == p)
                    {
                        ph_in_shirt = true;
                        break;
                    }
                }               
            }
            if (pants_i != -1 && hidePartsForElement != ClothesPartType.Pants)
            {
                foreach (var pantPart in getPreset(ClothesPartType.Pants, pants_i).hideParts)
                {
                        if (pantPart == p)
                        {
                            ph_in_pants = true;
                            break;
                        }
                }
            }
            if (shoes_i != -1 && hidePartsForElement != ClothesPartType.Shoes)
            {
                foreach (var shoesPart in getPreset(ClothesPartType.Shoes, shoes_i).hideParts)
                {
                        if (shoesPart == p)
                        {
                            ph_in_shoes = true;
                            break;
                        }
                }
            }

            if (ph_in_shirt || ph_in_pants || ph_in_shoes)
                continue;
            #endregion 

            foreach (CharacterPart cp in characterParts)
            {
                if (cp.name == p)
                    foreach (var mesh in cp.skinnedMesh)
                        mesh.enabled = true;
            }
        }
    }

    /// <summary>
    /// Set character material by index
    /// </summary>
    /// <param name="i">Index</param>
    public void SetCharacterMaterialByIndex(int i)
    {
        if (i > skinMaterialPresets.Count - 1 || i < 0)
            return;

        foreach(var cp in characterParts)
        {
            foreach (var mesh in cp.skinnedMesh)
                mesh.material = skinMaterialPresets[i];
        }
        materialActiveIndex = i;
    }
    #endregion

    #region Basic classes
    [System.Serializable]
    public class CharacterPart
    {
        public string name;
        public SkinnedMeshRenderer[] skinnedMesh;
    }
    [System.Serializable]
    public class ClothesAnchor
    {
        public ClothesPartType partType;
        public SkinnedMeshRenderer[] skinnedMesh;
    }
    [System.Serializable]
    public class HeadPreset
    {
        public string name;
        public Mesh[] mesh;
    }

    [System.Serializable]
    public class ClothPreset
    {
        public string name;
        public Mesh[] mesh;
        public string[] hideParts;
    }
    #endregion
}
