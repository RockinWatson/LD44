using UnityEngine;
using UnityEngine.SceneManagement;

public class BloodController : MonoBehaviour {

    public GameObject BloodLevel;
    private Vector3 _bloodLevelVisualStartPos;
    private Vector3 _bloodLevelVisualZeroPos;
    private float _bloodLevelMaxForVisual = 9000f;
    private float _bloodLevelMaxPosX = 0.26f;
    private float _bloodLevelMinPosX = -12.34f;

    // Use this for initialization
    void Start()
    {
        float scaleToZero = Player.Get().GetHealth() / _bloodLevelMaxForVisual * _bloodLevelMaxPosX;
        _bloodLevelVisualZeroPos = BloodLevel.transform.position - (Vector3.right * scaleToZero);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBloodLevelVisual();

        if (BloodLevel.transform.position.x <= _bloodLevelMinPosX)
        {
            PlayerPrefs.SetInt("HighScore", (int)Player.Get().GetScore());
            SceneManager.LoadScene("GameOver");
        }
    }

    private void UpdateBloodLevelVisual()
    {
        float bloodScale = Mathf.Min(Player.Get().GetHealth(), _bloodLevelMaxForVisual) / _bloodLevelMaxForVisual * _bloodLevelMaxPosX;
        
        BloodLevel.transform.position = Vector3.Lerp(BloodLevel.transform.position, _bloodLevelVisualZeroPos + (Vector3.right * bloodScale), Time.deltaTime * 5);
        //Mathf.Lerp(_bloodLevelMaxPosX,_bloodLevelMinPosX, Vector3.right * bloodScale);
    }
}
