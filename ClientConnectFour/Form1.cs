using ClientConnectFour.Data;
using ClientConnectFour.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientConnectFour
{
    public partial class Form1 : Form
    {

        // Client
        private Client client;
        private bool isLogin;

        // Game class
        private Game game;
        private DBContext _context;


        public Form1()
        {
            InitializeComponent();
            // Setup Client
            client = new Client();
            isLogin = false;
            this.PIC_Board.MouseClick += PIC_Board_Click;

            _context = new DBContext();
        }

        // ---------- Init/Terminate game methods ----------
        private void InitGame()
        {
            game = new Game(PIC_Board, client);
            game.initGame();
            BTN_StartGame.Visible = true;

            COMBO_Games.SelectedIndexChanged -= COMBO_Games_SelectedIndexChanged;
            List<GameView> list = _context.GameViews.Where(game => game.UserID == client.ID).ToList();
            // Load games
            COMBO_Games.DataSource = list;
            COMBO_Games.DisplayMember = "GameID";
            COMBO_Games.ValueMember = "GameID";
            COMBO_Games.SelectedIndexChanged += COMBO_Games_SelectedIndexChanged;
        }
        private void TerminateGame()
        {
            game.terminateGame();
        }

        // ---------- On State ----------
        private void OnLogin()
        {
            isLogin = true;
            // Disable all nodes
            BTN_Login.Visible = false;
            LBL_Login.Visible = false;
            INPUT_Login.Visible = false;
            BTN_Register_Forward.Visible = false;
            BTN_Logout.Visible = true;
            LBL_GamesTitle.Visible = true;
            COMBO_Games.Visible = true;
            // Initialize the game
            InitGame();
        }

        private void OnLogout()
        {
            isLogin = false;
            // Enable all nodes
            BTN_Login.Visible = true;
            LBL_Login.Visible = true;
            INPUT_Login.Visible = true;
            BTN_Register_Forward.Visible = true;
            BTN_Logout.Visible = false;
            LBL_GamesTitle.Visible = false;
            COMBO_Games.Visible = false;
            // Terminate the game
            TerminateGame();
        }

        private void OnStartGame()
        {
            BTN_StartGame.Visible = false;
            PIC_Board.Enabled = true;
            game.StartGame();

            // cancel recovery games
            COMBO_Games.Visible = false;
            LBL_GamesTitle.Visible = false;
        }

        // ---------- Buttons click methods ----------
        private async void BTN_Login_Click(object sender, EventArgs e)
        {
            string responseMSG;
            bool responseBool;
            (responseBool, responseMSG) = await client.LoginUserGET(INPUT_Login.Text);

            DialogResult res = MessageBox.Show(responseMSG, "Login response message", MessageBoxButtons.OK);
            if (responseBool & res == DialogResult.OK)
            {
                OnLogin();
            }
        }

        private void BTN_Register_Forward_Click(object sender, EventArgs e)
        {
            client.RegisterForward();
        }

        private void BTN_Logout_Click(object sender, EventArgs e)
        {
            OnLogout();
        }

        private async void BTN_StartGame_Click(object sender, EventArgs e)
        {
            string responseMSG;
            bool responseBool;
            (responseBool, responseMSG) = await client.StartGamePOST();

            DialogResult res = MessageBox.Show(responseMSG, "Start response message", MessageBoxButtons.OK);
            if (responseBool & res == DialogResult.OK)
            {
                OnStartGame();
            }

        }

        private void PIC_Board_Click(object sender, MouseEventArgs e)
        {
            game.OnClickBoard(e.Location);
        }

        private void COMBO_Games_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int gameID_Selectable = ((ComboBox)sender).SelectedItem;
            game.moves_recover = ((GameView)(((ComboBox)sender).SelectedItem)).Moves;
            game.initGame();
            BTN_StartGame.Visible = false;
            game.StartRecover();
        }
    }
}
