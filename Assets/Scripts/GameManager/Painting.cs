using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Painting : MonoBehaviour
{
    public int index;
    public float precision, threshold;
    public Texture2D brush;
    public RectTransform[] rtArr;
    public RectTransform rt;
    public Texture2D wherePainted;
    private int width, height, sW, sH;
    private Color[,] stamp;
    public SpriteMask[] smArr;
    public SpriteMask sm;
    private Vector2 currentScrn, lastScrn, current, last;
    private bool calcCoolDown = false;
    private OverworldManager om;
    private EnemiesAliveHUD eaHUD;

    // Start is called before the first frame update
    void Start()
    {
        om = FindObjectOfType<OverworldManager>();
        eaHUD = FindObjectOfType<EnemiesAliveHUD>();
        index = PaintInfo.index;
        if(index > -1)
        {
            rt = rtArr[index];
            sm = smArr[index];
            width = (int)rt.rect.width;
            height = (int)rt.rect.height;
            sW = brush.width;
            sH = brush.height;
            stamp = BuildStamp();
            ClearPaper();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && rt != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rt, Input.mousePosition, Camera.main, out Vector2 temp);

            temp.x = width - (width / 2 - temp.x);
            temp.y = Mathf.Abs(height / 2 - temp.y - height);

            last = (int)last.x < 0 ? temp : current;

            current = temp;

            for (int i = 0; i < sH; i++)
            {
                for (int j = 0; j < sW; j++)
                {
                    int xPos, yPos;

                    xPos = (int)(temp.x - sW / 2) + j;
                    yPos = (int)(temp.y - sH / 2) + i;

                    if (wherePainted.GetPixel(xPos, yPos).a != 1)
                        wherePainted.SetPixel(xPos, yPos, stamp[i, j]);
                }
            }

            SlowDraw();

            wherePainted.Apply();
            Sprite s = Sprite.Create(wherePainted, new Rect(0, 0, 100, 100), new Vector2(.5f, .5f));
            sm.sprite = s;
            if(!calcCoolDown) StartCoroutine("PercentagePainted");
        }
        else last = Vector2.left;
    }


    private void SlowDraw()
    {
        if (last.x > -1 && Vector2.Distance(last, current) > sW / 2)
        {
            int xPos, yPos, b;
            float m;

            m = (current.y - last.y) / (current.x - last.x);

            Vector2 goal;
            for (int i = 0; i < sH; i++)
            {
                for (int j = 0; j < sW; j++)
                {
                    xPos = (int)(last.x - sW / 2) + j;
                    yPos = (int)(last.y - sH / 2) + i;

                    goal.x = (int)(current.x - sW / 2) + j;
                    goal.y = (int)(current.y - sH / 2) + i;
                    b = !float.IsInfinity(m) && !float.IsNaN(m) ? (int)(goal.y - m * goal.x) : 0;

                    int sign;
                    if (float.IsInfinity(m) || float.IsNaN(m)) sign = goal.y < yPos ? -1 : 1;
                    else sign = goal.x < xPos ? -1 : 1;

                    for (int k = 0; k < 100; k++)
                    {
                        xPos = !float.IsInfinity(m) && !float.IsNaN(m) ? xPos + sign : xPos;

                        if (float.IsInfinity(m) || float.IsNaN(m)) yPos = yPos + sign;
                        else if (m != 0) yPos = (int)(m * xPos + b);

                        if (wherePainted.GetPixel(xPos, yPos).a != 1)
                            wherePainted.SetPixel(xPos, yPos, stamp[j, i]);

                        if (IsPassed(new Vector2(xPos, yPos), goal, sign, m)) break;
                    }
                }
            }
        }
    }

    private IEnumerator PercentagePainted()
    {
        calcCoolDown = true;
        Color[] pixels = wherePainted.GetPixels();

        //int percent = (int)(pixels.Length * precision);
        int randIndex = Random.Range(0, pixels.Length);

        int painted = 0;

        for (int i = 0; i < pixels.Length; i++)
            if (pixels[i] == Color.black) painted++;

        if ((painted / (float)pixels.Length * 100) >= threshold)
            PaintBeGone();

        print((painted / (float)pixels.Length * 100) + "%");
        yield return new WaitForSeconds(.75f);
        calcCoolDown = false;
    }

    private bool IsPassed(Vector2 current, Vector2 goal, int sign, float m)
    {
        if(sign > 0)
        {
            if(float.IsInfinity(m) || float.IsNaN(m))
                return current.y >= goal.y;
            else
                return current.x >= goal.x;
        } 
        else
        {
            if (float.IsInfinity(m) || float.IsNaN(m))
                return current.y <= goal.y;
            else
                return current.x <= goal.x;
        }
    }

    private Color[,] BuildStamp()
    {
        Color[] t2c = brush.GetPixels();
        Color[,] t2Map = new Color[sH, sW];

        for(int i = 0; i < sH; i++)
        {
            for(int j = 0; j < sW; j++)
            {
                Color c = t2c[i * sW + j];

                t2Map[i, j] = new Color(c.r, c.g, c.b, c.a);
            }
        }

        return t2Map;
    }

    private void ClearPaper()
    {
        Color[] c = wherePainted.GetPixels();

        for(int i = 0; i < c.Length; i++)
            wherePainted.SetPixel(i % wherePainted.width, i / wherePainted.width, new Color(0, 0, 0, 0));

        wherePainted.Apply();
        Sprite s = Sprite.Create(wherePainted, new Rect(0, 0, 100, 100), new Vector2(.5f, .5f));
        sm.sprite = s;
    }

    private void PaintBeGone()
    {
        om.inkTiles[index].SetActive(false);
        ActiveOverworldEntity.entityCount[1]--;
        eaHUD.UpdateDisplay();
        index = -1;
    }
}
