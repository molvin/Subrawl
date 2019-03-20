using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class CountDown : MonoBehaviour
{
    public float timeBetweenSprites = 1f;
    public float GoTime = 2.0f;
    public Sprite[] timerSprites;
    public Image go;
    public Image vs;
    public Image Player1;
    public Image Player2;
    public Image WASD;
    public Image Arrows;
    public Image Timer;
    private bool HasTimerWent;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        Assert.IsNotNull(image);
    }

    private void Start()
    {
        StartCoroutine(CountDownTime());
    }

    private IEnumerator CountDownTime()
    {

        int index = -1;
        for (int i = 0; i < timerSprites.Length; i++)
        {
             index++;
             image.sprite = timerSprites[index];
             yield return new WaitForSeconds(timeBetweenSprites);
        }
        vs.gameObject.SetActive(false);
        go.gameObject.SetActive(true);
        Player1.gameObject.SetActive(false);
        Player2.gameObject.SetActive(false);
        WASD.gameObject.SetActive(false);
        Arrows.gameObject.SetActive(false);
        Timer.enabled = (false);

        yield return new WaitForSeconds(GoTime);
        go.gameObject.SetActive(false);
        Debug.Log("yeet");
    }

}
