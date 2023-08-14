using ClientConnectFour.Data;
using ClientConnectFour.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientConnectFour
{
    internal class Game : Form
    {
        // Design
        private Bitmap bitmap;
        public Rectangle[,] rectangles { get; set; }
        private PictureBox pic_board;
        private Graphics g;

        // Values
        private int[,] boardValues;
        private const int ROWS = 6, COLS = 7;
        private const int USER = 1, SERVER = 2;
        private bool myTurn;
        private int rectSize;


        private int col_timer = -1;
        private int row_timer = -1;

        // Animation
        private Timer animationTimer;
        private const int TIMER_DELAY = 150;


        private Client client;
        private Timer serverTurnTimer;
        private const int SERVER_TIMER_DELAY = 700;

        // For DB
        private GameView gameView;
        private DBContext _context;

        // Recover
        private int col_timer_recover = -1;
        private int row_timer_recover = -1;
        private int columnStringIndex = 0;
        public string moves_recover { get; set; }
        public bool isUserTurnRecover = true;
        // Animation
        private Timer animationTimerRecover;
        private const int TIMER_DELAY_RECOVER = 300;

        public Game(PictureBox pictureBox, Client client)
        {
            this.client = client;
            boardValues = new int[ROWS, COLS];
            rectangles = new Rectangle[ROWS, COLS];
            pic_board = pictureBox;
            myTurn = true;
            rectSize = pic_board.Width / COLS;
            animationTimer = new Timer();
            animationTimer.Interval = TIMER_DELAY;

            serverTurnTimer = new Timer();
            serverTurnTimer.Interval = SERVER_TIMER_DELAY;

            gameView = new GameView();
            gameView.UserID = client.ID;

            _context = new DBContext();

            //recover
            animationTimerRecover = new Timer();
            animationTimerRecover.Interval = TIMER_DELAY_RECOVER;
        }

        public void initGame()
        {
            resetBoardValues();
            initPicBoard();
        }

        public void terminateGame()
        {
            pic_board.Visible = false;
            pic_board.Enabled = false;
            animationTimer.Dispose();
        }

        public void StartGame()
        {
            animationTimer.Tick += AnimatePiece;
            //animationTimer.Start();
        }

        private void initPicBoard()
        {
            pic_board.Visible = true;
            bitmap = new Bitmap(rectSize * COLS + 2, rectSize * ROWS + 2);
            g = Graphics.FromImage(bitmap);
            using (g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Wheat);
                for (int row = 0; row < ROWS; row++)
                {
                    for (int column = 0; column < COLS; column++)
                    {
                        int x = column * rectSize;
                        int y = row * rectSize;

                        // Draw the cell
                        rectangles[row, column] = new Rectangle(x, y, rectSize, rectSize);
                        g.DrawRectangle(Pens.Black, rectangles[row, column]);

                        Rectangle cellRect = new Rectangle(x + 5, y + 5, rectSize - 10, rectSize - 10);
                        g.FillEllipse(Brushes.White, cellRect);
                    }
                }
            }
            pic_board.Image = bitmap;
        }

        public void resetBoardValues()
        {
            for (int i = 0; i < ROWS; i++)
                for (int j = 0; j < COLS; j++)
                    boardValues.SetValue(0, i, j);
        }
        
        public void OnClickRectangle(Rectangle rectangle)
        {
            using (g = Graphics.FromImage(bitmap))
            {
                g.FillEllipse(Brushes.Red, rectangle);
                pic_board.Image = bitmap;
            }
        }

        public async void OnClickBoard(Point mousePos)
        {
            if (pic_board.ClientRectangle.Contains(mousePos) && myTurn && !animationTimer.Enabled)
            {
                int select_column = mousePos.X / rectSize;
                if (DoStep(USER, select_column))
                {
                    myTurn = false;
                }
            }
        }

        public bool DoStep(int idSelect, int selected_column)
        {
            int row = boardValues[0, selected_column];
            if (row != 0)
            {
                MessageBox.Show("Column is full. Choose another column.");
                return false;
            }
            boardValues[row, selected_column] = idSelect;
            row_timer = 0;
            col_timer = selected_column;

            gameView.Moves += selected_column;


            animationTimer.Start();
            return true;
        }

        public async Task<bool> DoServerStep(int idSelect)
        {
            int server_column = await client.StepGET();
            int row = boardValues[0, server_column];
            if (server_column == -1 || row != 0)
            {
                return false;
            }
            boardValues[row, server_column] = idSelect;
            col_timer = server_column;
            gameView.Moves += server_column;
            row_timer = 0;
            animationTimer.Start();
            return true;
        }

        private async void AnimatePiece(object sender, EventArgs e)
        {
            AnimateRectangles();
            if (row_timer < ROWS - 1)
            {
                if (boardValues[row_timer, col_timer] != 0 && boardValues[row_timer+1, col_timer] == 0)
                {
                    boardValues[row_timer + 1, col_timer] = boardValues[row_timer, col_timer];
                    boardValues[row_timer, col_timer] = 0;
                }
                row_timer++;
            } else
            {
                animationTimer.Stop();
                if (await OnWin())
                {
                    return;
                }
                if (!myTurn)
                {
                    while (!await DoServerStep(SERVER) && myTurn) ;
                    myTurn = true;
                    return;
                }
                if (await OnWin())
                {
                    return;
                }
            }
        }

        private void AnimatePieceRecover(object sender, EventArgs e)
        {
            if (columnStringIndex == moves_recover.Length && row_timer_recover == ROWS - 1)
            {
                animationTimerRecover.Stop();
                if (CheckForWin(USER))
                {
                    MessageBox.Show("User win");

                }
                else if (CheckForWin(SERVER))
                {
                    MessageBox.Show("Server win");
                }
                
                return;
            }
            if (row_timer_recover == ROWS - 1)
            {
                row_timer_recover = 0;
                int val = int.Parse(moves_recover[columnStringIndex] + "");
                col_timer_recover = val;
                isUserTurnRecover = !isUserTurnRecover;
                int valueOfMove = (isUserTurnRecover == true) ? 1 : 2;
                boardValues[row_timer_recover, col_timer_recover] = valueOfMove;
                columnStringIndex++;
            }
            
            AnimateRectangles();
            if (row_timer_recover < ROWS - 1)
            {
                if (boardValues[row_timer_recover, col_timer_recover] != 0 && boardValues[row_timer_recover + 1, col_timer_recover] == 0)
                {
                    boardValues[row_timer_recover + 1, col_timer_recover] = boardValues[row_timer_recover, col_timer_recover];
                    boardValues[row_timer_recover, col_timer_recover] = 0;
                }
                row_timer_recover++;
            }
            
        }

        private async Task<bool> OnWin()
        {
            if (CheckForWin(USER))
            {
                MessageBox.Show("User win");
                await client.EndGamePOST(true);
                _context.GameViews.Add(gameView);
                _context.SaveChanges();
                return true;
            }
            else if (CheckForWin(SERVER))
            {
                MessageBox.Show("Server win");
                await client.EndGamePOST(false);
                _context.GameViews.Add(gameView);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private void AnimateRectangles()
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int value = boardValues[i, j];
                    Brush b;
                    if (value == 0)
                        b = Brushes.White;
                    else if (value == 1)
                        b = Brushes.Yellow;
                    else 
                        b = Brushes.Red;


                    using (g = Graphics.FromImage(bitmap))
                    {
                        g.FillEllipse(b, rectangles[i,j]);
                        pic_board.Image = bitmap;
                        pic_board.Refresh();
                    }
                }
            }
            
        }

        private bool CheckForWin(int player)
        {
            // Check for horizontal win
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col <= COLS - 4; col++)
                {
                    if (boardValues[row, col] == player &&
                        boardValues[row, col + 1] == player &&
                        boardValues[row, col + 2] == player &&
                        boardValues[row, col + 3] == player)
                    {
                        return true; // Horizontal win
                    }
                }
            }

            // Check for vertical win
            for (int row = 0; row <= ROWS - 4; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    if (boardValues[row, col] == player &&
                        boardValues[row + 1, col] == player &&
                        boardValues[row + 2, col] == player &&
                        boardValues[row + 3, col] == player)
                    {
                        return true; // Vertical win
                    }
                }
            }

            // Check for diagonal win (top-left to bottom-right)
            for (int row = 0; row <= ROWS - 4; row++)
            {
                for (int col = 0; col <= COLS - 4; col++)
                {
                    if (boardValues[row, col] == player &&
                        boardValues[row + 1, col + 1] == player &&
                        boardValues[row + 2, col + 2] == player &&
                        boardValues[row + 3, col + 3] == player)
                    {
                        return true; // Diagonal win (top-left to bottom-right)
                    }
                }
            }

            // Check for diagonal win (top-right to bottom-left)
            for (int row = 0; row <= ROWS - 4; row++)
            {
                for (int col = 3; col < COLS; col++)
                {
                    if (boardValues[row, col] == player &&
                        boardValues[row + 1, col - 1] == player &&
                        boardValues[row + 2, col - 2] == player &&
                        boardValues[row + 3, col - 3] == player)
                    {
                        return true; // Diagonal win (top-right to bottom-left)
                    }
                }
            }

            return false; // No win
        }

        public void StartRecover()
        {
            // Recover
            col_timer_recover = -1;
            row_timer_recover = -1;
            columnStringIndex = 0;
            isUserTurnRecover = true;

            animationTimerRecover.Tick -= AnimatePieceRecover;
            animationTimerRecover.Tick += AnimatePieceRecover;
            row_timer_recover = 0;

            int val = int.Parse(moves_recover[columnStringIndex] + "");
            col_timer_recover = val;
            int valueOfMove = (isUserTurnRecover == true) ? 1 : 2;
            boardValues[row_timer_recover, col_timer_recover] = valueOfMove;
            columnStringIndex++;
            animationTimerRecover.Start();

        }
    }
}
