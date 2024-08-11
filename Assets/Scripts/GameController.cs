using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Video;


public class GameController : MonoBehaviour
{
    public AudioSource audioSrc;
    public VideoPlayer videoSrc;
    public int whosTurn;
    public int turnCount;
    public GameObject[] turnIcons;
    public Sprite[] playIcons;
    public Button[] tictactoeSpaces;
    public List<Button> clickedBtns;
    public int[] markedSpaces;
    public Text WinnerText;
    public GameObject[] winningLines;
    public GameObject WinnerPannel;
    public int xPlayersScore;
    public int oPlayersScore;
    public Text xPlaterScoreText;
    public Text oPlayerScoreText;
    public Button xPlayerButton;
    public Button oPlayerButton;
    public GameObject TieImage;
    public string playerName;
    public int coordinate;
    public int rowNo;
    public int columnNo;
    public List<string> chancesMoved;
    public RawImage videoImg;
    public RawImage audioImg;
    public UnityAction<List<string>> jsonDataParseEvent;
    public UnityAction audioEvent;
    public UnityAction videoEvent;
    public int pressedBtn;

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
        videoSrc.loopPointReached += EndReached;
        LoadMultipleObjectsFromRemoteServer.Instance.audioAction += AudioStart;
    }

    void GameSetup() 
    {

        whosTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < tictactoeSpaces.Length; i++) 
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        videoImg.gameObject.SetActive(false); 
        audioImg.gameObject.SetActive(false);
      

        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            if(i!=0 && i%3==0)
            {
                rowNo++;
                columnNo = 0;
            }
            string indexString = rowNo.ToString() + columnNo.ToString();
            markedSpaces[i] = int.Parse(indexString);
            columnNo++;
            
        }
        clickedBtns.Clear();
        //for (int i = 0; i < tictactoeSpaces.Length; i++)
        //{
        //    markedSpaces[i] = -100;
        //}

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void TicTacToeButton(int whichnumber) 
    {
        pressedBtn = whichnumber;
        xPlayerButton.interactable = false;
        oPlayerButton.interactable = false;
        tictactoeSpaces[whichnumber].interactable = false;
        coordinate = markedSpaces[whichnumber];
        markedSpaces[whichnumber] = whosTurn+1;
        turnCount++;
        foreach (Button btn in tictactoeSpaces)
        {
            btn.interactable = false;
        }
        clickedBtns.Add(tictactoeSpaces[whichnumber]);

        if (turnCount >= 4) {
           bool iswinner =  Winnercheck();
            if (turnCount == 9 && iswinner==false) {
                TieFunction();
            }
        }
        if (whosTurn == 0)
        {
            whosTurn = 1;
            playerName = "P1";
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
            videoEvent?.Invoke();
            videoImg.gameObject.SetActive(true);
            videoImg.transform.position = tictactoeSpaces[whichnumber].transform.position;

        }
        else
        {
            whosTurn = 0;
            playerName = "P2";
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
            audioEvent?.Invoke();
            audioImg.gameObject.SetActive(true);
            audioImg.transform.position = tictactoeSpaces[whichnumber].transform.position;
            
        }
        FillingList(playerName, coordinate);
    
    }

    bool Winnercheck() 
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];
        var Solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < Solutions.Length; i++)
        {
            if (Solutions[i] == 3 *(whosTurn + 1))
            {

                winnerDisplay(i);
                return true;
            }
            
        }

        return false;
    }


    void winnerDisplay(int index ) 
    {
        WinnerPannel.gameObject.SetActive(true);
        if (whosTurn == 0) {
           
            WinnerText.text = "Player X Wins!";
            xPlayersScore++;
            xPlaterScoreText.text = xPlayersScore.ToString();
        }
        else if(whosTurn==1){
            
            WinnerText.text = "Player O Wins!";
            oPlayersScore++;
            oPlayerScoreText.text = oPlayersScore.ToString();
        }
        winningLines[index].SetActive(true);
        jsonDataParseEvent?.Invoke(chancesMoved);
        MatchEnd();
    }

    public void Rematch() 
    {
        TieImage.SetActive(false);
        xPlayerButton.interactable = true;
        oPlayerButton.interactable = true;
        GameSetup();
        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
        WinnerPannel.SetActive(false);
    }

    public void ReStart() 
    {
        TieImage.SetActive(false);
        Rematch();
        xPlayersScore = 0;
        oPlayersScore = 0;
        xPlaterScoreText.text = "0";
        oPlayerScoreText.text = "0";

    }

    public void SwitchPlayer(int whichplayer) 
    {
        if (whichplayer == 0)
        {
            whosTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);

        }
        else if (whichplayer == 1) {
            whosTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }


    }

    void TieFunction() {
        TieImage.SetActive(true);
        jsonDataParseEvent?.Invoke(chancesMoved);
        MatchEnd();
    }

    public void FillingList(string playerName,int coordinates)
    {
        string temp;
        if (coordinates<3)
        {
            temp = playerName + "-0" +coordinates;
        }
        else
            temp = playerName + "-" + coordinates;
        chancesMoved.Add(temp);
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        tictactoeSpaces[pressedBtn].image.sprite = playIcons[0];
        videoImg.gameObject.SetActive(false);
        foreach (Button btn in tictactoeSpaces)
        {
            btn.interactable = true;
        }
        foreach (Button btn in clickedBtns)
        {
            btn.interactable = false;
        }
        tictactoeSpaces[pressedBtn].interactable = false;
        //vp.playbackSpeed = vp.playbackSpeed / 10.0F;

    }

    void AudioStart(AudioClip clip)
    {
        Invoke(nameof(EndAudio), clip.length);
    }
    void EndAudio()
    {
        tictactoeSpaces[pressedBtn].image.sprite = playIcons[1];
        audioImg.gameObject.SetActive(false);
        foreach (Button btn in tictactoeSpaces)
        {
            btn.interactable = true;
        }
        foreach (Button btn in clickedBtns)
        {
            btn.interactable = false;
        }
    }

    void MatchEnd()
    {
        videoSrc.Stop();
        audioSrc.Stop();
        
    }
}
