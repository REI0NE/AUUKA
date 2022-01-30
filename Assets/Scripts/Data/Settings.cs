using System.ComponentModel;

public class Settings
{
    private bool _isPause = false;
    public bool IsPause { get => _isPause; set => _isPause = value; }

    private float _music = .5f;
    public float Music { get => _music; set => _music = value; }

    private float _sound = .5f;
    public float Sound { get => _sound; set => _sound = value; }
    
    private bool _postfx = true;
    public bool Postfx { get => _postfx; set => _postfx = value; }

    private Language _language = Language.Ukrainian;
    public Language Language { get => _language; set => _language = value; }
    
}

public enum Language
{
    [Description("UK")]
    Ukrainian,
    [Description("EN")]
    English,
    [Description("RU")]
    Russian
}
