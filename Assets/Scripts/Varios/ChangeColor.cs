using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    private GameObject objectToChange;
    [SerializeField] private RectTransform _texture;
    [SerializeField] private Texture2D _refSprite;
    [SerializeField] private GameObject menuChange;

    public void OnClickPickerColor()
    {
        SetColor();
    }

    public void OpenColorPicker()
    {
        menuChange.SetActive(true);
        objectToChange = ObjectManager.instance.obj.transform.Find("Parte de color").gameObject;
    }

    private void SetColor()
    {
        Vector3 imagePos = _texture.position;
        float globalPosX = (Input.mousePosition.x - imagePos.x);
        float globalPosY = (Input.mousePosition.y - imagePos.y);

        int localPosX = (int)(globalPosX*(_refSprite.width/_texture.rect.width));
        int localPosY = (int)(globalPosY * (_refSprite.height / _texture.rect.height));

        Color c = _refSprite.GetPixel(localPosX, localPosY);
        SetActualColor(c);
    }

    private void SetActualColor (Color c)
    {
        objectToChange.GetComponent<SpriteRenderer>().material.color = c;
    }
}
