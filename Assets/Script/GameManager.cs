using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuaweiService;

public class GameManager : MonoBehaviour {

	public GameObject canvasManager;
	public GameObject languageManager;

	public GameObject maze;
	public GameObject player;
	public GameObject target;

	private const int STATE_LANGUAGE = 0;
	private const int STATE_MENU_IN = 1;
	private const int STATE_MENU_WAIT = 2;
	private const int STATE_MENU_OUT = 3;
	private const int STATE_MAZE_IN = 4;
	private const int STATE_LEVEL = 5;
	private const int STATE_WIN = 6;
	private const int STATE_MAZE_OUT = 7;

	private int state;

	private int currentLevel;
	private int finishLevel;

	// Use this for initialization
	void Start () {
		state = STATE_LANGUAGE;
		if (PlayerPrefs.HasKey ("GameManager.currentLevel")) {
			currentLevel = PlayerPrefs.GetInt ("GameManager.currentLevel");
		} else {
			currentLevel = 0;
			PlayerPrefs.SetInt ("GameManager.currentLevel", currentLevel);
		}
		if (PlayerPrefs.HasKey("GameManager.finishLevel")) {
			finishLevel = PlayerPrefs.GetInt ("GameManager.finishLevel");
		} else {
			finishLevel = -1;
			PlayerPrefs.SetInt ("GameManager.finishLevel", finishLevel);
		}
		canvasManager.GetComponent<CanvasManager> ().buttonLevelManager.GetComponent<ButtonLevelManager> ().SetLevel (currentLevel, finishLevel);
		canvasManager.GetComponent<CanvasManager> ().buttonLevelManager.GetComponent<ButtonLevelManager> ().SetButtonLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		//print (currentLevel);
		switch (state) {
		case STATE_LANGUAGE:
			if (languageManager.GetComponent<LanguageManager> ().IsSetted ()) {
				canvasManager.GetComponent<CanvasManager> ().canvasLanguage.SetActive (false);
				state = STATE_MENU_IN;
			} else {
				canvasManager.GetComponent<CanvasManager> ().canvasLanguage.SetActive (true);
			}
			break;
		case STATE_MENU_IN:
			canvasManager.GetComponent<CanvasManager> ().MoveIn ();
			if (canvasManager.GetComponent<CanvasManager> ().IsFinishMoveIn ())
				state = STATE_MENU_WAIT;
			break;
		case STATE_MENU_WAIT:
			break;
		case STATE_MENU_OUT:
			canvasManager.GetComponent<CanvasManager> ().MoveOut ();
			if (canvasManager.GetComponent<CanvasManager> ().IsFinishMoveOut ())
				state = STATE_MAZE_IN;
			break;
		case STATE_MAZE_IN:
			maze.GetComponent<Maze> ().KeepMoveIn ();
			if (maze.GetComponent<Maze> ().IsFinishMoveIn ())
				state = STATE_LEVEL;
			break;
		case STATE_LEVEL:
			if (player.GetComponent<Player> ().isWin) {
				maze.GetComponent<Maze> ().PrepareMoveOut ();
				state = STATE_WIN;
			}
			break;
		case STATE_WIN:
			if (currentLevel > finishLevel) {
				finishLevel = currentLevel;
			}
			currentLevel++;
			if (currentLevel >= canvasManager.GetComponent<CanvasManager> ().buttonLevelManager.GetComponent<ButtonLevelManager> ().buttonLevel.Length) {
				currentLevel = canvasManager.GetComponent<CanvasManager> ().buttonLevelManager.GetComponent<ButtonLevelManager> ().buttonLevel.Length - 1;
			}
			PlayerPrefs.SetInt ("GameManager.currentLevel", currentLevel);
			PlayerPrefs.SetInt ("GameManager.finishLevel", finishLevel);
			canvasManager.GetComponent<CanvasManager> ().buttonLevelManager.GetComponent<ButtonLevelManager> ().SetLevel (currentLevel, finishLevel);
			canvasManager.GetComponent<CanvasManager> ().buttonLevelManager.GetComponent<ButtonLevelManager> ().SetButtonLevel ();
			state = STATE_MAZE_OUT;
            showAd();
           break;
		case STATE_MAZE_OUT:
			maze.GetComponent<Maze> ().KeepMoveOut ();
			if (maze.GetComponent<Maze> ().IsFinishMoveOut ()) {
				player.GetComponent<Player> ().PreparePlayer ();
				canvasManager.GetComponent<CanvasManager> ().MoveIn ();
				state = STATE_MENU_IN;
			}
			break;
		default:
			break;
		}
	}

	public void PlayLevel (int setNumber, int setRowCount, int setColumnCount){
		if (state != STATE_MENU_WAIT)
			return;
		//set canvas
		currentLevel = setNumber;
		canvasManager.GetComponent<CanvasManager> ().buttonLevelManager.GetComponent<ButtonLevelManager> ().SetLevel (currentLevel, finishLevel);
		//canvasManager.GetComponent<CanvasManager> ().buttonLevelManager.GetComponent<ButtonLevelManager> ().SetButtonLevel ();
		//set build maze and move maze
		maze.GetComponent<Maze> ().BuildMaze (setRowCount, setColumnCount);		//start without waiting menu finish moving out
		maze.GetComponent<Maze> ().PrepareMoveIn();
		//set player and target
		player.transform.position = new Vector3 (maze.GetComponent<Maze> ().playerColumnNumber + 0.5f, player.transform.position.y, maze.GetComponent<Maze> ().playerRowNumber + 0.5f);
		target.transform.position = new Vector3 (maze.GetComponent<Maze> ().targetColumnNumber + 0.5f, target.transform.position.y, maze.GetComponent<Maze> ().targetRowNumber + 0.5f);
		state = STATE_MENU_OUT;
	}

	public bool IsStateLevel() {
		if (state == STATE_LEVEL)
			return true;
		return false;
	}

	public bool IsStateMenuWait() {
		if (state == STATE_MENU_WAIT)
			return true;
		return false;
	}

    public class MAdListener : HuaweiService.ads.AdListener
    {
        private HuaweiService.ads.InterstitialAd ad;
        public MAdListener(HuaweiService.ads.InterstitialAd _ad) : base()
        {
            ad = _ad;
        }
        public override void onAdLoaded()
        {
           
            ad.show();
        }

        public override void onAdFailed(int arg0)
        {
        }

        public override void onAdOpened()
        {
        }

        public override void onAdClicked()
        {
        }

        public override void onAdLeave()
        {
        }

        public override void onAdClosed()
        {
        }
    }

    public void showAd()
    {
		HuaweiService.ads.InterstitialAd ad = new HuaweiService.ads.InterstitialAd(new Context());
        ad.setAdId("teste9ih9j0rc3"); //此处为测试广告ID
        ad.setAdListener(new MAdListener(ad));
		HuaweiService.ads.AdParam.Builder builder = new HuaweiService.ads.AdParam.Builder();
		HuaweiService.ads.AdParam adParam = builder.build();
        ad.loadAd(adParam);
    }
}
