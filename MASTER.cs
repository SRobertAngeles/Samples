using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace WiseGuy_Tournament_Director
{
    public partial class MASTER : Form
    {

        OpenFileDialog MYOpenFile = new OpenFileDialog();
        SaveFileDialog MYSaveFile = new SaveFileDialog();

        public BuyIn_SLAVE MYBuyIn_SLAVE = new BuyIn_SLAVE();
        public PlayerEntry_SLAVE MYPlayerEntry_SLAVE = new PlayerEntry_SLAVE();
        public BlindStructure_SLAVE MYBlindStructure_SLAVE = new BlindStructure_SLAVE();
        public ReBuy_SLAVE MYReBuy_SLAVE = new ReBuy_SLAVE();
        public PlayerDown_SLAVE MYPlayerDown_SLAVE = new PlayerDown_SLAVE();

        public string NoPlayers = "0";
        public int PlayerField;

        public int rebuyCount;

        public int Reset;
        public int EntryKey;

        long seconds_RunTime = 0;
        public long seconds_NextLevel = 0;
        long seconds_PreviousLevel = 0;

        int LevelCount = 0;

        public ArrayList PlayerList = new ArrayList();

        
        public MASTER()
        {
            InitializeComponent();

            MYBuyIn_SLAVE.MYMASTER = this;
            MYPlayerEntry_SLAVE.MYMASTER = this;
            MYBlindStructure_SLAVE.MYMASTER = this;
            MYReBuy_SLAVE.MYMASTER = this;
            MYPlayerDown_SLAVE.MYMASTER = this;
        }

        private void MASTER_Load(object sender, EventArgs e)
        {
            //Initialize All Result Labels on Master Form Load

            NoPlayers = "0";
            PlayerField = 0;
            rebuyCount = 0;
            seconds_RunTime = 0;
            seconds_NextLevel = 0;

            NextLevelResult.Text = "Waiting";
            SmallBlindResult.Text = MYBlindStructure_SLAVE.Level1SmResult.Text;
            BigBlindResult.Text = MYBlindStructure_SLAVE.Level1BigResult.Text;
            CurrentLevelResult.Text = "Waiting";
            ChipsInPlayResult.Text = "0";
            PlayersResult.Text = "0";
            BuyInResult.Text = "0 @ 500 chips for $10";
            RebuyResult.Text = "0 @ 200 chips for $5";

            RunTimeResult.Text = "00:00:00";
            ColorChangeResult.Text = "00:00";
            NextSmResult.Text = MYBlindStructure_SLAVE.Level2SmResult.Text;
            NextBigResult.Text = MYBlindStructure_SLAVE.Level2BigResult.Text;
            PrizeResult.Text = "$0";
            RebuyPoolResult.Text = "$0";

            PlacesResult.Text = "1st Place: $0\n2nd Place: $0\n3rd Place: $0";
            

            //Initialize Button States on Master Form Load

            BuyInButton.Visible = true;
            
            StartButton.Visible = false;
            PauseButton.Visible = false;
            ResumeButton.Visible = false;
            ResetButton.Visible = false;


            RebuyButton.Visible = false;
            RebuyButton.Enabled = true;

            PreviousLevelButton.Visible = false;

            NextLevelButton.Visible = false;
            NextLevelButton.Enabled = true;

            PlayerDownButton.Visible = false;
            PlayerDownButton.Enabled = true;

            MenuBar_Setup.Enabled = true;

            //Initialize Active Player States on Master Form Load

            LabelActive.Text = "Active Players";
            LabelActive.Visible = false;

            Player1.Visible = false;
            Player2.Visible = false;
            Player3.Visible = false;
            Player4.Visible = false;
            Player5.Visible = false;
            Player6.Visible = false;
            Player7.Visible = false;
            Player8.Visible = false;

            Player9.Visible = false;
            Player10.Visible = false;
            Player11.Visible = false;
            Player12.Visible = false;
            Player13.Visible = false;
            Player14.Visible = false;
            Player15.Visible = false;
            Player16.Visible = false;

            PlayerList.Add("Lance A.");
            PlayerList.Add("Scott A.");
            PlayerList.Add("Nick M.");
            PlayerList.Add("Joe P.");
            PlayerList.Add("Gabe V.");
            PlayerList.Add("Ryan J.");
            PlayerList.Add("Chris L.");
            PlayerList.Add("Linh K.");
            PlayerList.Add("Jeremiah A.");
            PlayerList.Add("Jeremy B.");
            PlayerList.Add("Sarah C.");
            PlayerList.Add("Matt K.");
            PlayerList.Add("Sam D.");
            PlayerList.Add("Amanda N.");
        }

        //Menus under "File" in Menu Bar

        private void MenuBar_File_New_Click(object sender, EventArgs e)
        {
            EntryKey = 0;

            Reset++;

            MASTER_Load(sender, e);
        }

        private void MenuBar_File_Open_Click(object sender, EventArgs e)
        {
            MYOpenFile.ShowDialog();
        }

        private void MenuBar_File_SaveAs_Click(object sender, EventArgs e)
        {
            MYSaveFile.ShowDialog();
        }

        private void MenuBar_File_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Menus under "Setup" in Menu Bar

        private void MenuBar_Setup_Options_Click(object sender, EventArgs e)
        {

        }

        private void MenuBar_Setup_Structure_Click(object sender, EventArgs e)
        {
            MYBlindStructure_SLAVE.ShowDialog();         
        }

        private void MenuBar_Setup_Alerts_Click(object sender, EventArgs e)
        {

        }

        private void MenuBar_Setup_Customize_Click(object sender, EventArgs e)
        {

        }

        //Menus under "Help" in Menu Bar

        private void MenuBar_Help_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version 1.1: Programmed by Scott Robert Angeles", "About...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuBar_Help_Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Do you really need help with this?", "Help", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
        }

        //Buttons (Top to Bottom, Left to Right)

        private void StartButton_Click(object sender, EventArgs e)
        {
            PlayerField = int.Parse(NoPlayers);

            if (PlayerField >= 2)
            {
                BuyInButton.Visible = false;
                
                RebuyButton.Visible = true;
                PreviousLevelButton.Visible = true;
                PreviousLevelButton.Enabled = false;
                NextLevelButton.Visible = true;
                PlayerDownButton.Visible = true;

                MenuBar_Setup.Enabled = false;

                CurrentLevelResult.Text = "1";

                StartButton.Visible = false;
                PauseButton.Visible = true;

                LevelCount = int.Parse(MYBlindStructure_SLAVE.Level1TimeResult.Text);
         
                seconds_NextLevel = LevelCount * 60;

                Timer_RunTime.Start();
            }
            else
            {
                MessageBox.Show("You need at least 2 players to start a game", "Sorry...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            PauseButton.Visible = false;
            ResumeButton.Visible = true;

            Timer_RunTime.Stop();
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            ResumeButton.Visible = false;
            PauseButton.Visible = true;
            
            Timer_RunTime.Start();
        }    

        private void ResetButton_Click(object sender, EventArgs e)
        {
            EntryKey = 0;

            Reset++;

            Timer_RunTime.Stop();
                        
            MASTER_Load(sender, e);
        }

        private void BuyInButton_Click(object sender, EventArgs e)
        {
            MYBuyIn_SLAVE.ShowDialog();
        }

        private void RebuyButton_Click(object sender, EventArgs e)
        {
            MYReBuy_SLAVE.ShowDialog();
        }

        private void PreviousLevelButton_Click(object sender, EventArgs e)
        {
            PreviousLevelButton.Enabled = false;
            
            seconds_NextLevel = seconds_PreviousLevel;
                        
            LevelCount = int.Parse(CurrentLevelResult.Text);
    
            LevelCount--;

            CurrentLevelResult.Text = LevelCount.ToString();

            if (LevelCount < 20)
            {
                NextLevelButton.Enabled = true;
            }
  
            switch (LevelCount)
            {
                case 1:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level1SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level1BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level2SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level2BigResult.Text;
                    break;

                case 2:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level2SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level2BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level3SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level3BigResult.Text;
                    break;

                case 3:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level3SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level3BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level4SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level4BigResult.Text;
                    break;

                case 4:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level4SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level4BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level5SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level5BigResult.Text;
                    break;

                case 5:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level5SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level5BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level6SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level6BigResult.Text;
                    break;

                case 6:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level6SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level6BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level7SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level7BigResult.Text;
                    break;

                case 7:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level7SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level7BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level8SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level8BigResult.Text;
                    break;

                case 8:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level8SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level8BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level9SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level9BigResult.Text;
                    break;

                case 9:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level9SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level9BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level10SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level10BigResult.Text;
                    break;

                case 10:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level10SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level10BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level11SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level11BigResult.Text;
                    break;

                case 11:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level11SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level11BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level12SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level12BigResult.Text;
                    break;

                case 12:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level12SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level12BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level13SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level13BigResult.Text;
                    break;

                case 13:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level13SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level13BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level14SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level14BigResult.Text;
                    break;

                case 14:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level14SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level14BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level15SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level15BigResult.Text;
                    break;

                case 15:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level15SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level15BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level16SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level16BigResult.Text;
                    break;

                case 16:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level16SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level16BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level17SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level17BigResult.Text;
                    break;

                case 17:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level17SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level17BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level18SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level18BigResult.Text;
                    break;

                case 18:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level18SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level18BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level19SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level19BigResult.Text;
                    break;

                case 19:
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level19SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level19BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level20SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level20BigResult.Text;
                    break;
            }
          }

        private void NextLevelButton_Click(object sender, EventArgs e)
        {
            seconds_PreviousLevel = seconds_NextLevel;
            
            LevelCount = int.Parse(CurrentLevelResult.Text);

            LevelCount++;

            CurrentLevelResult.Text = LevelCount.ToString();

            if (seconds_PreviousLevel != 0)
            {
                PreviousLevelButton.Enabled = true;
            }
         
            switch (LevelCount)
            {
                case 1:
                    int t = int.Parse(MYBlindStructure_SLAVE.Level1TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level1SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level1BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level2SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level2BigResult.Text;
                    break;

                case 2:
                    t = int.Parse(MYBlindStructure_SLAVE.Level2TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level2SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level2BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level3SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level3BigResult.Text;
                    break;

                case 3:
                    t = int.Parse(MYBlindStructure_SLAVE.Level3TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level3SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level3BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level4SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level4BigResult.Text;
                    break;

                case 4:
                    t = int.Parse(MYBlindStructure_SLAVE.Level4TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level4SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level4BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level5SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level5BigResult.Text;
                    break;

                case 5:
                    t = int.Parse(MYBlindStructure_SLAVE.Level5TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level5SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level5BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level6SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level6BigResult.Text;
                    break;

                case 6:
                    t = int.Parse(MYBlindStructure_SLAVE.Level6TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level6SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level6BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level7SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level7BigResult.Text;
                    break;

                case 7:
                    t = int.Parse(MYBlindStructure_SLAVE.Level7TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level7SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level7BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level8SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level8BigResult.Text;
                    break;

                case 8:
                    t = int.Parse(MYBlindStructure_SLAVE.Level8TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level8SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level8BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level9SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level9BigResult.Text;
                    break;

                case 9:
                    t = int.Parse(MYBlindStructure_SLAVE.Level9TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level9SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level9BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level10SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level10BigResult.Text;
                    break;

                case 10:
                    t = int.Parse(MYBlindStructure_SLAVE.Level10TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level10SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level10BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level11SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level11BigResult.Text;
                    break;

                case 11:
                    t = int.Parse(MYBlindStructure_SLAVE.Level11TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level11SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level11BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level12SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level12BigResult.Text;
                    break;

                case 12:
                    t = int.Parse(MYBlindStructure_SLAVE.Level12TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level12SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level12BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level13SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level13BigResult.Text;
                    break;

                case 13:
                    t = int.Parse(MYBlindStructure_SLAVE.Level13TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level13SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level13BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level14SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level14BigResult.Text;
                    break;

                case 14:
                    t = int.Parse(MYBlindStructure_SLAVE.Level14TimeResult.Text);
                    seconds_NextLevel = t * 60;
                    
                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level14SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level14BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level15SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level15BigResult.Text;
                    break;

                case 15:
                    t = int.Parse(MYBlindStructure_SLAVE.Level15TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level15SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level15BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level16SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level16BigResult.Text;
                    break;

                case 16:
                    t = int.Parse(MYBlindStructure_SLAVE.Level16TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level16SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level16BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level7SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level7BigResult.Text;
                    break;

                case 17:
                    t = int.Parse(MYBlindStructure_SLAVE.Level17TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level17SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level17BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level18SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level18BigResult.Text;
                    break;

                case 18:
                    t = int.Parse(MYBlindStructure_SLAVE.Level18TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level18SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level18BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level19SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level19BigResult.Text;
                    break;

                case 19:
                    t = int.Parse(MYBlindStructure_SLAVE.Level19TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level19SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level19BigResult.Text;

                    NextSmResult.Text = MYBlindStructure_SLAVE.Level20SmResult.Text;
                    NextBigResult.Text = MYBlindStructure_SLAVE.Level20BigResult.Text;
                    break;

                case 20:
                    t = int.Parse(MYBlindStructure_SLAVE.Level20TimeResult.Text);
                    seconds_NextLevel = t * 60;

                    SmallBlindResult.Text = MYBlindStructure_SLAVE.Level20SmResult.Text;
                    BigBlindResult.Text = MYBlindStructure_SLAVE.Level20BigResult.Text;

                    NextSmResult.Text = "MAX";
                    NextBigResult.Text = "MAX";
                        
                    NextLevelButton.Enabled = false;

                    MessageBox.Show("Maximum Value of Blinds Reached...", "Holy Crap!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
       
        }

        private void PlayerDownButton_Click(object sender, EventArgs e)
        {
            MYPlayerDown_SLAVE.ShowDialog();
        }

        private void Timer_RunTime_Tick(object sender, EventArgs e)
        {
            if (seconds_NextLevel != 0)
            {
                seconds_NextLevel--;

                string val2 = (seconds_NextLevel / 60).ToString("00") + ":" + (seconds_NextLevel % 60).ToString("00");

                NextLevelResult.Text = val2;
            }
            else
            {
                NextLevelButton.PerformClick();
            }

            //Running Time
            seconds_RunTime++;

            string val = (seconds_RunTime / 3600).ToString("00") + ":" + ((seconds_RunTime % 3600) / 60).ToString("00") + ":" + (seconds_RunTime % 60).ToString("00");

            RunTimeResult.Text = val;
        }




     }
}