using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ThemeData
{
    public string id;   // "original", "green", "blue", "dark"

    public float messageR, messageG, messageB, messageA;
    public float backgroundR, backgroundG, backgroundB, backgroundA;
    public float textR, textG, textB, textA;
    public float buttonBackgroundR, buttonBackgroundG, buttonBackgroundB, buttonBackgroundA;
    public float buttonPressedR, buttonPressedG, buttonPressedB, buttonPressedA;
    public float placeholderR, placeholderG, placeholderB, placeholderA;
    public float navButtonR, navButtonG, navButtonB, navButtonA;

    public Color ButtonBackgroundColor =>
        new Color(buttonBackgroundR, buttonBackgroundG, buttonBackgroundB, buttonBackgroundA);

    public Color MessageColor =>
        new Color(messageR, messageG, messageB, messageA);

    public Color BackgroundColor =>
        new Color(backgroundR, backgroundG, backgroundB, backgroundA);

    public Color ButtonTextColor =>
        new Color(textR, textG, textB, textA);

    public Color ButtonPressedColor =>
        new Color(buttonPressedR, buttonPressedG, buttonPressedB, buttonPressedA);

    public Color PlaceholderColor =>
        new Color(placeholderR, placeholderG, placeholderB, placeholderA);

    public Color NavButtons =>
        new Color(navButtonR, navButtonG, navButtonB, navButtonA);
}

[System.Serializable]
public class ThemeCollection
{
    public string activeThemeId;
    public List<ThemeData> themes;
}

