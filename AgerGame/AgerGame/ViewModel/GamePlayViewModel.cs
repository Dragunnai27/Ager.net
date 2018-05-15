using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgerGame.Views;
using AgerGame.Model;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Controls;
using AgerGame.ViewModel;
using System.Windows.Media;

namespace AgerGame.ViewModel
{
    public sealed class GamePlayViewModel
    {
        // logical handle for game play
       
        public GamePlay Game { get; set; }
        public GamePlayViewModel()
        {
            Game = new GamePlay(Ultil.CreatePlayer())
            {
                Visibility = Visibility.Hidden
            };
        }        
    }

}

namespace AgerGame.Views
{
    public partial class GamePlay : UserControl
    {
        // logical handle for game play

        // player + Bot
        Player[] players;
        // Rect của player + AI
        List<Rect> PAIRect;
        
        int rf1;
        int rf2;
        int rf3;
        
        // list hình foods và RectFoods;
        Food[] foods;
        //List<Ellipse> foodsImg;
        List<Rect> foodsRect;

        // biến Dispatcher
        public DispatcherTimer gameTime;

        // biến tọa độ chuột
        double mouseX;
        double mouseY;

        // biến tọa độ màn hình
        double WindowWidth = Ultil.WindowWidth;
        double WindowHeight = Ultil.WindowHeight;

        public GamePlay(Player p)
        {
            InitializeComponent();
            players = new Player[5];
            players[0] = p;
            foods = Ultil.CreateFoods();
            CreateAI();            
            //bo vao mang add
            GamePlayCanvas.Children.Add(players[0].PlayerImg);            
            GamePlayCanvas.Children.Add(players[1].PlayerImg);
            GamePlayCanvas.Children.Add(players[2].PlayerImg);            
            GamePlayCanvas.Children.Add(players[3].PlayerImg);            
            GamePlayCanvas.Children.Add(players[4].PlayerImg);
            AddFood();
            FoodStartRandom();
            AIPosStartRandom();
            SetGameTime();

            this.GamePlayCanvas.MouseMove += (sender, e) =>
            {
                mouseX = e.GetPosition(GamePlayCanvas).X;
                mouseY = e.GetPosition(GamePlayCanvas).Y;
            };

            GamePlayCanvas.PreviewKeyDown += (sender, e) =>
            {
                if (e.Key == Key.A)
                {
                    MessageBox.Show("A");
                }
            };
        }
        
        private void SetGameTime()
        {
            gameTime = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };
            gameTime.Tick += (sender, e) =>
            {
                AIMove();
                PlayerMove();
                SetPlayerAIRect();
                FoodSetRect();
                FoodCollisionPlayerAI();               
                PlayerCollisionAI();
            };
        }
        // Player di chuyển
        public void PlayerMove()
        {
            if (players[0].PosX + players[0].PlayerImg.Width / 2 < mouseX) players[0].PosX += players[0].Speed;
            if (players[0].PosX + players[0].PlayerImg.Height / 2 > mouseX) players[0].PosX -= players[0].Speed;
            if (players[0].PosY + players[0].PlayerImg.Width / 2 < mouseY) players[0].PosY += players[0].Speed;
            if (players[0].PosY + players[0].PlayerImg.Height / 2 > mouseY) players[0].PosY -= players[0].Speed;

            Canvas.SetLeft(players[0].PlayerImg, players[0].PosX);
            Canvas.SetTop(players[0].PlayerImg, players[0].PosY);
        }
        // Create Bots
        public void CreateAI()
        {
            players[1] = new Bot();            
            Canvas.SetLeft(players[1].PlayerImg, 0); players[1].PosX = Canvas.GetLeft(players[1].PlayerImg);
            Canvas.SetTop(players[1].PlayerImg, 0); players[1].PosY = Canvas.GetTop(players[1].PlayerImg);            

            players[2] = new Bot();

            Canvas.SetLeft(players[2].PlayerImg, WindowWidth - 20); players[2].PosX = Canvas.GetLeft(players[2].PlayerImg);
            Canvas.SetTop(players[2].PlayerImg, 0); players[2].PosY = Canvas.GetTop(players[2].PlayerImg);

            players[3] = new Bot();

            Canvas.SetLeft(players[3].PlayerImg, 0); players[3].PosX = Canvas.GetLeft(players[3].PlayerImg);
            Canvas.SetTop(players[3].PlayerImg, WindowHeight - 20); players[3].PosY = Canvas.GetTop(players[3].PlayerImg);

            players[4] = new Bot();

            Canvas.SetLeft(players[4].PlayerImg, WindowWidth - 20); players[4].PosX = Canvas.GetLeft(players[4].PlayerImg);
            Canvas.SetTop(players[4].PlayerImg, WindowHeight - 20); players[4].PosY = Canvas.GetTop(players[4].PlayerImg);
        }
        // Bot Move
        //split out
        public void AIMove()
        {
            //theo người chơi
            if (((Bot)players[1]).IsAlive)
            {
                if (players[1].PosX + players[1].PlayerImg.ActualWidth / 2 < players[0].PosX + players[0].PlayerImg.ActualWidth / 2) players[1].PosX += players[1].Speed;
                if (players[1].PosX + players[1].PlayerImg.ActualWidth / 2 > players[0].PosX + players[0].PlayerImg.ActualWidth / 2) players[1].PosX -= players[1].Speed;
                if (players[1].PosY + players[1].PlayerImg.ActualHeight / 2 < players[0].PosY + players[0].PlayerImg.ActualHeight / 2) players[1].PosY += players[1].Speed;
                if (players[1].PosY + players[1].PlayerImg.ActualHeight / 2 > players[0].PosY + players[0].PlayerImg.ActualHeight / 2) players[1].PosY -= players[1].Speed;

                Canvas.SetLeft(players[1].PlayerImg, players[1].PosX);
                Canvas.SetTop(players[1].PlayerImg, players[1].PosY);
            }

            //============================================================//

            if (((Bot)players[2]).IsAlive)
            {
                if (!((Bot)players[1]).IsAlive)
                {
                    if (players[2].PosX + players[2].PlayerImg.ActualWidth / 2 < players[0].PosX + players[0].PlayerImg.ActualWidth / 2) players[2].PosX += players[2].Speed;
                    if (players[2].PosX + players[2].PlayerImg.ActualWidth / 2 > players[0].PosX + players[0].PlayerImg.ActualWidth / 2) players[2].PosX -= players[2].Speed;
                    if (players[2].PosY + players[2].PlayerImg.ActualHeight / 2 < players[0].PosY + players[0].PlayerImg.ActualHeight / 2) players[2].PosY += players[2].Speed;
                    if (players[2].PosY + players[2].PlayerImg.ActualHeight / 2 > players[0].PosY + players[0].PlayerImg.ActualHeight / 2) players[2].PosY -= players[2].Speed;
                }
                else
                {
                    if (players[2].PosX + players[2].PlayerImg.ActualWidth / 2 < foods[rf1].PosX + foods[rf1].Img.ActualWidth / 2) players[2].PosX += players[2].Speed;
                    if (players[2].PosX + players[2].PlayerImg.ActualWidth / 2 > foods[rf1].PosX + foods[rf1].Img.ActualWidth / 2) players[2].PosX -= players[2].Speed;
                    if (players[2].PosY + players[2].PlayerImg.ActualHeight / 2 < foods[rf1].PosY + foods[rf1].Img.ActualHeight / 2) players[2].PosY += players[2].Speed;
                    if (players[2].PosY + players[2].PlayerImg.ActualHeight / 2 > foods[rf1].PosY + foods[rf1].Img.ActualHeight / 2) players[2].PosY -= players[2].Speed;
                }


                Canvas.SetLeft(players[2].PlayerImg, players[2].PosX);
                Canvas.SetTop(players[2].PlayerImg, players[2].PosY);
            }

            ////============================================================//
            // bot 3
            if (((Bot)players[3]).IsAlive)
            {
                if (players[3].WidthAndHeight % 2 == 0)
                {
                    if (players[3].PosX + players[3].PlayerImg.ActualWidth / 2 < players[0].PosX + players[0].PlayerImg.ActualWidth / 2) players[3].PosX += players[3].Speed;
                    if (players[3].PosX + players[3].PlayerImg.ActualWidth / 2 > players[0].PosX + players[0].PlayerImg.ActualWidth / 2) players[3].PosX -= players[3].Speed;
                    if (players[3].PosY + players[3].PlayerImg.ActualHeight / 2 < players[0].PosY + players[0].PlayerImg.ActualHeight / 2) players[3].PosY += players[3].Speed;
                    if (players[3].PosY + players[3].PlayerImg.ActualHeight / 2 > players[0].PosY + players[0].PlayerImg.ActualHeight / 2) players[3].PosY -= players[3].Speed;
                }
                else
                {
                    if (players[3].PosX + players[3].PlayerImg.ActualWidth / 2 < foods[rf2].PosX + foods[rf2].Img.ActualWidth / 2) players[3].PosX += players[3].Speed;
                    if (players[3].PosX + players[3].PlayerImg.ActualWidth / 2 > foods[rf2].PosX + foods[rf2].Img.ActualWidth / 2) players[3].PosX -= players[3].Speed;
                    if (players[3].PosY + players[3].PlayerImg.ActualHeight / 2 < foods[rf2].PosY + foods[rf2].Img.ActualHeight / 2) players[3].PosY += players[3].Speed;
                    if (players[3].PosY + players[3].PlayerImg.ActualHeight / 2 > foods[rf2].PosY + foods[rf2].Img.ActualHeight / 2) players[3].PosY -= players[3].Speed;
                }

                Canvas.SetLeft(players[3].PlayerImg, players[3].PosX);
                Canvas.SetTop(players[3].PlayerImg, players[3].PosY);
            }

            ////============================================================//
            // bot 4
            if (((Bot)players[4]).IsAlive)
            {
                if (players[4].WidthAndHeight % 2 != 0)
                {
                    if (players[4].PosX + players[4].PlayerImg.ActualWidth / 2 < players[0].PosX + players[0].PlayerImg.ActualWidth / 2) players[4].PosX += players[4].Speed;
                    if (players[4].PosX + players[4].PlayerImg.ActualWidth / 2 > players[0].PosX + players[0].PlayerImg.ActualWidth / 2) players[4].PosX -= players[4].Speed;
                    if (players[4].PosY + players[4].PlayerImg.ActualHeight / 2 < players[0].PosY + players[0].PlayerImg.ActualHeight / 2) players[4].PosY += players[4].Speed;
                    if (players[4].PosY + players[4].PlayerImg.ActualHeight / 2 > players[0].PosY + players[0].PlayerImg.ActualHeight / 2) players[4].PosY -= players[4].Speed;
                }
                else
                {
                    if (players[4].PosX + players[4].PlayerImg.ActualWidth / 2 < foods[rf3].PosX + foods[rf3].Img.ActualWidth / 2) players[4].PosX += players[4].Speed;
                    if (players[4].PosX + players[4].PlayerImg.ActualWidth / 2 > foods[rf3].PosX + foods[rf3].Img.ActualWidth / 2) players[4].PosX -= players[4].Speed;
                    if (players[4].PosY + players[4].PlayerImg.ActualHeight / 2 < foods[rf3].PosY + foods[rf3].Img.ActualHeight / 2) players[4].PosY += players[4].Speed;
                    if (players[4].PosY + players[4].PlayerImg.ActualHeight / 2 > foods[rf3].PosY + foods[rf3].Img.ActualHeight / 2) players[4].PosY -= players[4].Speed;
                }

                Canvas.SetLeft(players[4].PlayerImg, players[4].PosX);
                Canvas.SetTop(players[4].PlayerImg, players[4].PosY);
            }
        }

        private void AddFood()
        {
            for (int i = 0; i < foods.Length; i++)
            {
                GamePlayCanvas.Children.Add(foods[i].Img);
            }
        }
        //place food
        public void FoodStartRandom()
        {
            Random rnd = new Random();
            Random r = new Random();
            for (int i = 0; i < foods.Length; i++)
            {
                foods[i].PosX = rnd.Next(10, (int)WindowWidth - 10);
                foods[i].PosY = rnd.Next(10, (int)WindowHeight - 10);
                foods[i].Img.Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                Canvas.SetLeft(foods[i].Img, foods[i].PosX);
                Canvas.SetTop(foods[i].Img, foods[i].PosY);
            }
        }

        // Hàm random di chuyển AI
        public void AIPosStartRandom()
        {
            Random randomFood = new Random();
            rf1 = randomFood.Next(0,maxValue : 90);
            rf2 = randomFood.Next(0,maxValue : 90);
            rf3 = randomFood.Next(0, maxValue: 90);
        }

        //Handle Run Time
        public void FoodSetRect()
        {
            foodsRect = new List<Rect>();
            foreach (Food item in foods)
            {
                item.RectX = Canvas.GetLeft(item.Img);
                item.RectY = Canvas.GetTop(item.Img);
                Rect foodRect = Ultil.CreateRect(item.RectX, item.RectY, item.Img.ActualWidth, item.Img.ActualHeight);
                foodsRect.Add(foodRect);
            }
        }
        //will update later
        public void FoodCollisionPlayerAI()
        {
            int fCount = foods.Length;
            int pCount = players.Length;
            Random rnd, r;
            for (int i = 0; i < pCount; i++)
            {
                for (int j = 0; j < fCount; j++)
                {
                    rnd = new Random();
                    r = new Random();
                    if (Ultil.Collision(PAIRect[i], foodsRect[j]))
                    {
                        foods[j].PosX = rnd.Next(10, (int)WindowWidth - 10);
                        foods[j].PosY = rnd.Next(10, (int)WindowHeight - 10);
                        foods[j].Img.Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                        Canvas.SetLeft(foods[j].Img, foods[j].PosX);
                        Canvas.SetTop(foods[j].Img, foods[j].PosY);

                        players[i].PlayerImg.Width = players[i].PlayerImg.Height = players[i].WidthAndHeight += 1;
                        players[i].Speed = players[i].Speed < 0.01 ? 0.01 : players[i].Speed - 0.01;                        
                    }
                }
            }   
        }

        //Players rect
        public void SetPlayerAIRect()
        {
            double rectX, rectY, tWidth, tHeight;
            PAIRect = new List<Rect>();
            foreach (Player p in players)
            {
                rectX = Canvas.GetLeft(p.PlayerImg);
                rectY = Canvas.GetTop(p.PlayerImg);
                tWidth = p.PlayerImg.ActualWidth;
                tHeight = p.PlayerImg.ActualHeight;
                Rect rect = Ultil.CreateRect(rectX, rectY, tWidth, tHeight);
                PAIRect.Add(rect);
            }            
        }
        public void PlayerCollisionAI()
        {
            // Bot 1
            if (Ultil.Collision(PAIRect[1], PAIRect[0]) == true)
            {
                if (players[1].WidthAndHeight >= players[0].WidthAndHeight)
                {
                    GamePlayCanvas.Visibility = Visibility.Hidden;
                }
                else
                {
                    ((Bot)players[1]).IsAlive = false;
                    players[0].PlayerImg.Width = players[0].PlayerImg.Height = players[0].WidthAndHeight -= players[1].WidthAndHeight / 2;
                    Canvas.SetLeft(players[1].PlayerImg, 90000);
                    Canvas.SetTop(players[1].PlayerImg, 90000);
                    players[1].Speed = 0;
                }
            }

            //////////////////////////////////////////////////////
            // Bot 2
            if (Ultil.Collision(PAIRect[2], PAIRect[0]) == true)
            {
                if (players[2].WidthAndHeight >= players[0].WidthAndHeight)
                {
                    GamePlayCanvas.Visibility = Visibility.Hidden;
                }
                else
                {
                    ((Bot)players[2]).IsAlive = false;
                    players[0].PlayerImg.Width = players[0].PlayerImg.Height = players[0].WidthAndHeight -= players[2].WidthAndHeight / 2;
                    Canvas.SetLeft(players[2].PlayerImg, 90000);
                    Canvas.SetTop(players[2].PlayerImg, 90000);
                    players[2].Speed = 0;
                }
            }
            //////////////////////////
            // Bot 3
            if (Ultil.Collision(PAIRect[3], PAIRect[0]) == true)
            {
                if (players[3].WidthAndHeight >= players[0].WidthAndHeight)
                {
                    GamePlayCanvas.Visibility = Visibility.Hidden;
                }
                else
                {
                    ((Bot)players[3]).IsAlive = false;
                    players[0].PlayerImg.Width = players[0].PlayerImg.Height = players[0].WidthAndHeight -= players[3].WidthAndHeight / 2;
                    Canvas.SetLeft(players[3].PlayerImg, 90000);
                    Canvas.SetTop(players[3].PlayerImg, 90000);
                    players[3].Speed = 0;
                }
            }
            //////////////////////////////////
            // bot 4
            if (Ultil.Collision(PAIRect[4], PAIRect[0]) == true)
            {
                if (players[4].WidthAndHeight >= players[0].WidthAndHeight)
                {
                    GamePlayCanvas.Visibility = Visibility.Hidden;
                }
                else
                {
                    ((Bot)players[4]).IsAlive = false;
                    players[0].PlayerImg.Width = players[0].PlayerImg.Height = players[0].WidthAndHeight -= players[4].WidthAndHeight / 2;
                    Canvas.SetLeft(players[4].PlayerImg, 90000);
                    Canvas.SetTop(players[4].PlayerImg, 90000);
                    players[3].Speed = 0;
                }
            }
        }

    }
}
