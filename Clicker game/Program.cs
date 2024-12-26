using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickerManager : MonoBehaviour
{
    public GameObject startpanel;
    public GameObject panel;

    public int hp;
    public GameObject hpText;

    //Progress 
    public int stonesBroken;
    public int stonesBrokenOnLevel;
    public int stonesBrokenToUpgrade;
    public int stonelevel;
    public int level;
    public GameObject progressBar;
    public GameObject progressText;

    public int money;
    public GameObject moneyText;

    public bool nextLevel;
    public int oreNum;
    public GameObject[] oreTexture;
    public GameObject obsidian;
    public GameObject diamondBlock;
    public int[] orePrice;
    public int[] oreHp;


    //Damage per Click
    public int clickL;
    public int click;
    public int clickP;

    public GameObject clickLText;
    public GameObject clickPText;
    public GameObject clickButton;

    //Damage per 1 Second
    public int minerL;
    public int miner;
    public int minerP;

    public GameObject minerUpgrades;
    public GameObject minerLText;
    public GameObject minerPText;
    public GameObject minerButton;

    public float minerTimer;
    public GameObject minerPickaxe;

    //Damage per 5 Seconds
    public int bomberL;
    public int bomber;
    public int bomberP;

    public GameObject bomberUpgrades;
    public GameObject bomberLText;
    public GameObject bomberPText;
    public GameObject bomberButton;

    public float bomberTimer;
    public GameObject bomb;

    void Update()
    {
        ButtonUpdate();
        if (hp <= 0)
        {
            Break();
        }
    }

    private void FixedUpdate()
    {
        minerTimer += Time.deltaTime;
        if (minerTimer >= 1)
        {
            MinerHit();
            minerTimer = 0;
        }

        bomberTimer += Time.deltaTime;
        if (bomberTimer >= 4)
        {
            BomberHit();
            bomberTimer = 0;
        }
    }

    public void ButtonUpdate()
    {
        if (money >= clickP)
        {
            clickButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            clickButton.GetComponent<Button>().interactable = false;
        }

        if (money >= minerP)
        {
            minerButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            minerButton.GetComponent<Button>().interactable = false;
        }

        if (money >= bomberP)
        {
            bomberButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            bomberButton.GetComponent<Button>().interactable = false;
        }

        hpText.GetComponent<TextMeshProUGUI>().text = "" + hp;
    }

    void TextUpdate()
    {
        clickLText.GetComponent<TextMeshProUGUI>().text = clickL + " уровень";
        clickPText.GetComponent<TextMeshProUGUI>().text = clickP + "";

        minerLText.GetComponent<TextMeshProUGUI>().text = minerL + " уровень";
        minerPText.GetComponent<TextMeshProUGUI>().text = minerP + "";

        moneyText.GetComponent<TextMeshProUGUI>().text = money + " монет";

        progressText.GetComponent<TextMeshProUGUI>().text = stonesBrokenOnLevel + "/" + stonesBrokenToUpgrade;
        progressBar.GetComponent<Slider>().value = (stonesBrokenOnLevel * 1f) / (stonesBrokenToUpgrade * 1f);

        PlayerPrefs.SetInt("Click", click);
        PlayerPrefs.SetInt("ClickL", clickL);
        PlayerPrefs.SetInt("ClickP", clickP);

        PlayerPrefs.SetInt("Miner", miner);
        PlayerPrefs.SetInt("MinerL", minerL);
        PlayerPrefs.SetInt("MinerP", minerP);

        PlayerPrefs.SetInt("Bomber", bomber);
        PlayerPrefs.SetInt("BomberL", bomberL);
        PlayerPrefs.SetInt("BomberP", bomberP);

        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("StonesBroken", stonesBroken);
        PlayerPrefs.SetInt("StonesBrokenOnLevel", stonesBrokenOnLevel);
        PlayerPrefs.SetInt("StonesBrokenToUpgrade", stonesBrokenToUpgrade);
        PlayerPrefs.SetInt("Level", level);
    }

    public void Click()
    {

        hp -= click;

        if (hp <= 0)
        {
            Break();
        }


    }
    public void HpPerClickUpgrade()
    {
        money -= clickP;

        click += 1;
        clickL += 1;
        clickP = Mathf.CeilToInt(clickP * 1.5f);
        TextUpdate();
    }

    public void MinerHit()
    {
        minerPickaxe.GetComponent<Animation>().Play("MinerPickaxe");
        hp -= miner;

        if (hp <= 0)
        {
            Break();
        }
    }
    public void MinerUpgrade()
    {
        money -= minerP;

        miner += 1;
        minerL += 1;
        minerP = Mathf.CeilToInt(minerP * 1.5f);
        TextUpdate();
        minerPickaxe.SetActive(true);
    }

    public void BomberHit()
    {
        bomb.GetComponent<Animation>().Play("bomberPickaxe");
        hp -= bomber;

        if (hp <= 0)
        {
            Break();
        }
    }
    public void bomberUpgrade()
    {
        money -= bomberP;

        bomber += 1;
        bomberL += 1;
        bomberP = Mathf.CeilToInt(bomberP * 1.5f);
        TextUpdate();
        bomb.SetActive(true);
    }

    public void Break()
    {
        if (nextLevel)
        {
            obsidian.SetActive(false);
            stonesBrokenOnLevel = 0;
            stonesBrokenToUpgrade += 10;
            level += 1;
            nextLevel = false;
            NewLevel();
        }
        else
        {
            money += orePrice[oreNum];
            stonesBroken += 1;
            stonesBrokenOnLevel += 1;
            oreTexture[oreNum].SetActive(false);
        }
        if (stonesBrokenOnLevel >= stonesBrokenToUpgrade)
        {
            obsidian.SetActive(true);
            hp = stonesBrokenToUpgrade * 2;
            nextLevel = true;
        }
        else
        {
            oreNum = Random.Range(0, level);
            oreTexture[oreNum].SetActive(true);
            hp = oreHp[oreNum];
        }
        TextUpdate();

    }
    public void NewLevel()
    {
        if (level == 2)
        {
            minerUpgrades.SetActive(true);
        }
        if (level == 2)
        {
            bomberUpgrades.SetActive(true);
        }
    }

    public void LoadProgress()
    {
        if (PlayerPrefs.GetInt("PlayedBefore") == 1)
        {
            level = PlayerPrefs.GetInt("level");
            stonesBroken = PlayerPrefs.GetInt("StonesBroken");
            stonesBrokenOnLevel = PlayerPrefs.GetInt("StonesBrokenOnLevel");
            stonesBrokenToUpgrade = PlayerPrefs.GetInt("StonesBrokenToUpgrade");
            money = PlayerPrefs.GetInt("Money");

            click = PlayerPrefs.GetInt("click");
            clickL = PlayerPrefs.GetInt("clickL");
            clickP = PlayerPrefs.GetInt("clickP");

            miner = PlayerPrefs.GetInt("Miner");
            minerL = PlayerPrefs.GetInt("MinerL");
            minerP = PlayerPrefs.GetInt("MinerP");

            bomber = PlayerPrefs.GetInt("Bomber");
            bomberL = PlayerPrefs.GetInt("BomberL");
            bomberP = PlayerPrefs.GetInt("BomberP");
        }
        else
        {
            PlayerPrefs.SetInt("PlayedBefore", 1);
        }

        panel.SetActive(true);
        startpanel.SetActive(false);
        TextUpdate();

    }
    public void Reset()
    {
        TextUpdate();
    }
}