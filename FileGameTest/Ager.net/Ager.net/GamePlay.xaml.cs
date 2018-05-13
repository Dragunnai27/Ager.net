using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Ager.net
{
    /// <summary>
    /// Interaction logic for GamePlay.xaml
    /// </summary>
    public partial class GamePlay : Window
    {
        // gọi các class 
        Player player;
        // gọi các class AI và 
        AI ai0; 
        AI ai1; int rf1;
        AI ai2; int rf2;
        AI ai3; int rf3;
        Food food;

        // list hình foods và RectFoods;
        List<Food> foods;
        List<Ellipse> foodsImg;
        List<Rect> foodsRect;

        // biến Dispatcher
        DispatcherTimer gameTime;
        // biến tọa độ chuột
        double mouseX;
        double mouseY;

        // biến tọa độ màn hình
        double WindowWidth;
        double WindowHeight;

        // Rect của player
        Rect playerRect;

        // Rect của AI
        Rect ai0Rect;
        Rect ai1Rect;
        Rect ai2Rect;
        Rect ai3Rect;



        public GamePlay()
        {
            InitializeComponent();
            // lấy kích cỡ của màn hình
            if (this.WindowState == WindowState.Maximized)
            {
                WindowWidth = (double)System.Windows.SystemParameters.PrimaryScreenWidth;
                WindowHeight = (double)System.Windows.SystemParameters.PrimaryScreenHeight;
            }
            // đổi hình chuột
            this.Cursor = Cursors.Cross;
            // Hàm tạo nhân vật (Bên dưới)
            CreatePlayer();
            // Hàm tạo AI (Bên dưới)
            CreateAI();
            // Hàm tạo mảng đồ ăn (Bên dưới)
            FoodList();
            // Hàm random ví trị đồ ăn khi bắt đầu (Bên dưới)
            FoodStartRandom();

            AIPosStartRandom();

            //==============================================================================================//
            gameTime = new DispatcherTimer
            {
                Interval = TimeSpan.FromTicks(50000)
            };
            gameTime.Tick += (sender, e) =>
            {
                // Hàm di chuyển AI (Bên dưới)
                AIMove();
                // Hàm di chuyển nhân vật (Bên dưới)
                PlayerMove();
                // Hàm tạo Rect cho đồ ăn (Bên dưới)
                FoodSetRect();
                // Hàm tạo Rect cho người chơi và AI (Bên dưới)
                OtherSetReact();
                // Hàm va chạm người chơi với đồ ăn (Bên dưới)
                FoodCollisionPlayer();
                // Hàm random di chuyển AI (Bên dưới)
                FoodCollisionAI();
                // Hàm va chạm người chơi với AI (Bên dưới)
                PlayerCollisionAI();
            };
            gameTime.Start();
        }

        // Sự kiện bấm phím
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
        // Sự kiện di chuyển chuột
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            mouseX = position.X;
            mouseY = position.Y;
        }
        // Hàm di chuyển nhân vật
        public void PlayerMove()
        {
            if (player.PX + PlayerImg.ActualWidth / 2 < mouseX) player.PX += player.PSpeed;
            if (player.PX + PlayerImg.ActualWidth / 2 > mouseX) player.PX -= player.PSpeed;
            if (player.PY + PlayerImg.ActualHeight / 2 < mouseY) player.PY += player.PSpeed;
            if (player.PY + PlayerImg.ActualHeight / 2 > mouseY) player.PY -= player.PSpeed;

            //if (player.PX + PlayerImg.ActualWidth / 2 < mouseX && player.PY + PlayerImg.ActualHeight / 2 < mouseY)
            //{
            //    player.PX += player.PSpeed;
            //    player.PY += player.PSpeed;
            //}
            //if (player.PX + PlayerImg.ActualWidth / 2 > mouseX && player.PY + PlayerImg.ActualHeight / 2 < mouseY)
            //{
            //    player.PX -= player.PSpeed;
            //    player.PY += player.PSpeed;
            //}
            //if (player.PX + PlayerImg.ActualWidth / 2 > mouseX && player.PY + PlayerImg.ActualHeight / 2 > mouseY)
            //{
            //    player.PX -= player.PSpeed;
            //    player.PY -= player.PSpeed;
            //}
            //if (player.PX + PlayerImg.ActualWidth / 2 < mouseX && player.PY + PlayerImg.ActualHeight / 2 > mouseY)
            //{
            //    player.PX += player.PSpeed;
            //    player.PY -= player.PSpeed;
            //}
            Canvas.SetLeft(PlayerImg, player.PX);
            Canvas.SetTop(PlayerImg, player.PY);

        }
        // Hàm di chuyển AI
        public void AIMove()
        {
            if (ai0.Alive != false)
            {
                if (ai0.AX + AI0Img.ActualWidth / 2 < player.PX + PlayerImg.ActualWidth) ai0.AX += ai0.ASpeed;
                if (ai0.AX + AI0Img.ActualWidth / 2 > player.PX + PlayerImg.ActualWidth) ai0.AX -= ai0.ASpeed;
                if (ai0.AY + AI0Img.ActualHeight / 2 < player.PY + PlayerImg.ActualHeight) ai0.AY += ai0.ASpeed;
                if (ai0.AY + AI0Img.ActualHeight / 2 > player.PY + PlayerImg.ActualHeight) ai0.AY -= ai0.ASpeed;

                Canvas.SetLeft(AI0Img, ai0.AX);
                Canvas.SetTop(AI0Img, ai0.AY);
            }

            //============================================================//

            if (ai1.Alive != false)
            {
                if (ai0.Alive == false)
                {
                    if (ai1.AX + AI1Img.ActualWidth / 2 < player.PX + PlayerImg.ActualWidth) ai1.AX += ai1.ASpeed;
                    if (ai1.AX + AI1Img.ActualWidth / 2 > player.PX + PlayerImg.ActualWidth) ai1.AX -= ai1.ASpeed;
                    if (ai1.AY + AI1Img.ActualHeight / 2 < player.PY + PlayerImg.ActualHeight) ai1.AY += ai1.ASpeed;
                    if (ai1.AY + AI1Img.ActualHeight / 2 > player.PY + PlayerImg.ActualHeight) ai1.AY -= ai1.ASpeed;
                }
                else
                {
                    if (ai1.AX + AI1Img.ActualWidth / 2 < foods[rf1].FX + foodsImg[rf1].ActualWidth) ai1.AX += ai1.ASpeed;
                    if (ai1.AX + AI1Img.ActualWidth / 2 > foods[rf1].FX + foodsImg[rf1].ActualWidth) ai1.AX -= ai1.ASpeed;
                    if (ai1.AY + AI1Img.ActualHeight / 2 < foods[rf1].FY + foodsImg[rf1].ActualHeight) ai1.AY += ai1.ASpeed;
                    if (ai1.AY + AI1Img.ActualHeight / 2 > foods[rf1].FY + foodsImg[rf1].ActualHeight) ai1.AY -= ai1.ASpeed;
                }
                

                Canvas.SetLeft(AI1Img, ai1.AX);
                Canvas.SetTop(AI1Img, ai1.AY);
            }

            //============================================================//
            if (ai2.Alive != false)
            {
                if (ai2.AiWidthAndHeight%2 == 0)
                {
                    if (ai2.AX + AI2Img.ActualWidth / 2 < player.PX + PlayerImg.ActualWidth) ai2.AX += ai2.ASpeed;
                    if (ai2.AX + AI2Img.ActualWidth / 2 > player.PX + PlayerImg.ActualWidth) ai2.AX -= ai2.ASpeed;
                    if (ai2.AY + AI2Img.ActualHeight / 2 < player.PY + PlayerImg.ActualHeight) ai2.AY += ai2.ASpeed;
                    if (ai2.AY + AI2Img.ActualHeight / 2 > player.PY + PlayerImg.ActualHeight) ai2.AY -= ai2.ASpeed;
                }
                else
                {
                    if (ai2.AX + AI2Img.ActualWidth / 2 < foods[rf2].FX + foodsImg[rf2].ActualWidth) ai2.AX += ai2.ASpeed;
                    if (ai2.AX + AI2Img.ActualWidth / 2 > foods[rf2].FX + foodsImg[rf2].ActualWidth) ai2.AX -= ai2.ASpeed;
                    if (ai2.AY + AI2Img.ActualHeight / 2 < foods[rf2].FY + foodsImg[rf2].ActualHeight) ai2.AY += ai2.ASpeed;
                    if (ai2.AY + AI2Img.ActualHeight / 2 > foods[rf2].FY + foodsImg[rf2].ActualHeight) ai2.AY -= ai2.ASpeed;

                }
               
                Canvas.SetLeft(AI2Img, ai2.AX);
                Canvas.SetTop(AI2Img, ai2.AY);
            }

            //============================================================//
            if (ai3.Alive != false)
            {
                if (ai2.AiWidthAndHeight % 2 != 0)
                {
                    if (ai3.AX + AI3Img.ActualWidth / 2 < player.PX + PlayerImg.ActualWidth) ai3.AX += ai3.ASpeed;
                    if (ai3.AX + AI3Img.ActualWidth / 2 > player.PX + PlayerImg.ActualWidth) ai3.AX -= ai3.ASpeed;
                    if (ai3.AY + AI3Img.ActualHeight / 2 < player.PY + PlayerImg.ActualHeight) ai3.AY += ai3.ASpeed;
                    if (ai3.AY + AI3Img.ActualHeight / 2 > player.PY + PlayerImg.ActualHeight) ai3.AY -= ai3.ASpeed;

                }
                else
                {
                    if (ai3.AX + AI3Img.ActualWidth / 2 < foods[rf3].FX + foodsImg[rf3].ActualWidth) ai3.AX += ai3.ASpeed;
                    if (ai3.AX + AI3Img.ActualWidth / 2 > foods[rf3].FX + foodsImg[rf3].ActualWidth) ai3.AX -= ai3.ASpeed;
                    if (ai3.AY + AI3Img.ActualHeight / 2 < foods[rf3].FY + foodsImg[rf3].ActualHeight) ai3.AY += ai3.ASpeed;
                    if (ai3.AY + AI3Img.ActualHeight / 2 > foods[rf3].FY + foodsImg[rf3].ActualHeight) ai3.AY -= ai3.ASpeed;
                }
                    
                Canvas.SetLeft(AI3Img, ai3.AX);
                Canvas.SetTop(AI3Img, ai3.AY);
            }
        }
        // Hàm tạo nhân vật
        public void CreatePlayer()
        {
            // tạo với thêm thuộc tính cho player
            player = new Player();
            PlayerImg.Width = PlayerImg.Height = player.PlayerWidthAndHeight;
            Canvas.SetLeft(PlayerImg, WindowWidth / 2); player.PX = Canvas.GetLeft(PlayerImg);
            Canvas.SetTop(PlayerImg, WindowHeight / 2); player.PY = Canvas.GetTop(PlayerImg);
        }
        // Hàm tạo AI
        public void CreateAI()
        {
            ai0 = new AI();
            AI0Img.Width = AI0Img.Height = ai0.AiWidthAndHeight;
            Canvas.SetLeft(AI0Img, 0); ai0.AX = Canvas.GetLeft(AI0Img);
            Canvas.SetTop(AI0Img, 0); ai0.AY = Canvas.GetTop(AI0Img);
            AI0Img.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/AI.png")) };

            ai1 = new AI();
            AI1Img.Width = AI1Img.Height = ai1.AiWidthAndHeight;
            Canvas.SetLeft(AI1Img, WindowWidth - 20); ai1.AX = Canvas.GetLeft(AI1Img);
            Canvas.SetTop(AI1Img, 0); ai1.AY = Canvas.GetTop(AI1Img);
            AI1Img.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/AI.png")) };

            ai2 = new AI();
            AI2Img.Width = AI2Img.Height = ai2.AiWidthAndHeight;
            Canvas.SetLeft(AI2Img, 0); ai2.AX = Canvas.GetLeft(AI2Img);
            Canvas.SetTop(AI2Img, WindowHeight - 20); ai2.AY = Canvas.GetTop(AI2Img);
            AI2Img.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/AI.png")) };

            ai3 = new AI();
            AI3Img.Width = AI3Img.Height = ai3.AiWidthAndHeight;
            Canvas.SetLeft(AI3Img, WindowWidth - 20); ai3.AX = Canvas.GetLeft(AI3Img);
            Canvas.SetTop(AI3Img, WindowHeight - 20); ai3.AY = Canvas.GetTop(AI3Img);
            AI3Img.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Image/AI.png")) };
        }
        //Hàm tạo mảng đồ ăn
        public void FoodList()
        {
            foods = new List<Food>();
            foodsImg = new List<Ellipse> { Food0, Food1, Food2, Food3, Food4, Food5, Food6, Food7, Food8, Food9, Food10,
                                           Food11,Food12, Food13, Food14, Food15, Food16, Food17, Food18, Food19, Food20,
                                           Food21,Food22, Food23, Food24, Food25, Food26, Food27, Food28, Food29, Food30,
                                           Food31,Food32, Food33, Food34, Food35, Food36, Food37, Food38, Food39, Food40,
                                           Food41,Food42, Food43, Food44, Food45, Food46, Food47, Food48, Food49, Food50,
                                           Food51,Food52, Food53, Food54, Food55, Food56, Food57, Food58, Food59, Food60,
                                           Food61,Food62, Food63, Food64, Food65, Food66, Food67, Food68, Food69, Food70,
                                           Food71,Food72, Food73, Food74, Food75, Food76, Food77, Food78, Food79, Food80,
                                           Food81,Food82, Food83, Food84, Food85, Food86, Food87, Food88, Food89, Food90,};
            for (int i = 0; i < foodsImg.Count; i++)
            {
                food = new Food();
                foods.Add(food);
                foodsImg[i].Width = foodsImg[i].Height = food.FoodWidthAndHeight;
            }
        }
        // Hàm random ví trị đồ ăn khi bắt đầu
        public void FoodStartRandom()
        {
            Random rnd = new Random();
            Random r = new Random();
            for (int i = 0; i < foodsImg.Count; i++)
            {
                foods[i].FX = rnd.Next(10, (int)WindowWidth - 10);
                foods[i].FY = rnd.Next(10, (int)WindowHeight - 10);
                foodsImg[i].Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                Canvas.SetLeft(foodsImg[i], foods[i].FX);
                Canvas.SetTop(foodsImg[i], foods[i].FY);
            }
        }
        // Hàm random di chuyển AI
        public void AIPosStartRandom()
        {
            Random randomFood = new Random();
            rf1 = randomFood.Next(0, 89);
            rf2 = randomFood.Next(0, 89);
            rf3 = randomFood.Next(0, 89);
        }
        // Hàm tạo Rect cho đồ ăn
        public void FoodSetRect()
        {
            foodsRect = new List<Rect>();
            for (int i = 0; i < foodsImg.Count; i++)
            {
                foods[i].FRectX = Canvas.GetLeft(foodsImg[i]);
                foods[i].FRectY = Canvas.GetTop(foodsImg[i]);
                Rect foodRect = Function.CreateRect(foods[i].FRectX, foods[i].FRectY, foodsImg[i].ActualWidth, foodsImg[i].ActualHeight);
                foodsRect.Add(foodRect);
            }
        }
        // Hàm va chạm người chơi với đồ ăn
        public void FoodCollisionPlayer()
        {
            for (int i = 0; i < foodsImg.Count; i++)
            {
                Random rnd = new Random();
                Random r = new Random();
                if (Function.Collision(playerRect, foodsRect[i]) == true)
                {
                    foods[i].FX = rnd.Next(10, (int)WindowWidth - 10);
                    foods[i].FY = rnd.Next(10, (int)WindowHeight - 10);
                    foodsImg[i].Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                    Canvas.SetLeft(foodsImg[i], foods[i].FX);
                    Canvas.SetTop(foodsImg[i], foods[i].FY);

                    PlayerImg.Width = PlayerImg.Height = player.PlayerWidthAndHeight += 1;
                    if (player.PSpeed != 0.01)
                    {
                        player.PSpeed -= 0.01;
                    }
                }
            }
        }
        // Hàm va chạm AI với đồ ăn
        public void FoodCollisionAI()
        {
            for (int i = 0; i < foodsImg.Count; i++)
            {
                Random rnd = new Random();
                Random r = new Random();
                if (Function.Collision(ai0Rect, foodsRect[i]) == true)
                {
                    foods[i].FX = rnd.Next(10, (int)WindowWidth - 10);
                    foods[i].FY = rnd.Next(10, (int)WindowHeight - 10);
                    foodsImg[i].Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                    Canvas.SetLeft(foodsImg[i], foods[i].FX);
                    Canvas.SetTop(foodsImg[i], foods[i].FY);

                    AI0Img.Width = AI0Img.Height = ai0.AiWidthAndHeight += 1;
                    if (ai0.ASpeed != 0.01)
                    {
                        ai0.ASpeed -= 0.01;
                    }
                }
                if (Function.Collision(ai1Rect, foodsRect[i]) == true)
                {
                    foods[i].FX = rnd.Next(10, (int)WindowWidth - 10);
                    foods[i].FY = rnd.Next(10, (int)WindowHeight - 10);
                    foodsImg[i].Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                    Canvas.SetLeft(foodsImg[i], foods[i].FX);
                    Canvas.SetTop(foodsImg[i], foods[i].FY);

                    AI1Img.Width = AI1Img.Height = ai1.AiWidthAndHeight += 1;
                    if (ai1.ASpeed != 0.01)
                    {
                        ai1.ASpeed -= 0.01;
                    }
                }
                if (Function.Collision(ai2Rect, foodsRect[i]) == true)
                {
                    foods[i].FX = rnd.Next(10, (int)WindowWidth - 10);
                    foods[i].FY = rnd.Next(10, (int)WindowHeight - 10);
                    foodsImg[i].Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                    Canvas.SetLeft(foodsImg[i], foods[i].FX);
                    Canvas.SetTop(foodsImg[i], foods[i].FY);

                    AI2Img.Width = AI2Img.Height = ai2.AiWidthAndHeight += 1;
                    if (ai2.ASpeed != 0.01)
                    {
                        ai2.ASpeed -= 0.01;
                    }
                }
                if (Function.Collision(ai3Rect, foodsRect[i]) == true)
                {
                    foods[i].FX = rnd.Next(10, (int)WindowWidth - 10);
                    foods[i].FY = rnd.Next(10, (int)WindowHeight - 10);
                    foodsImg[i].Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                    Canvas.SetLeft(foodsImg[i], foods[i].FX);
                    Canvas.SetTop(foodsImg[i], foods[i].FY);

                    AI3Img.Width = AI3Img.Height = ai3.AiWidthAndHeight += 1;
                    if (ai3.ASpeed != 0.01)
                    {
                        ai3.ASpeed -= 0.01;
                    }
                }


            }
        }
        // Hàm tạo Rect cho người chơi và AI
        public void OtherSetReact()
        {
            playerRect = Function.CreateRect(Canvas.GetLeft(PlayerImg), Canvas.GetTop(PlayerImg),
                   PlayerImg.ActualWidth, PlayerImg.ActualHeight);
            ai0Rect = Function.CreateRect(Canvas.GetLeft(AI0Img), Canvas.GetTop(AI0Img),
                   AI0Img.ActualWidth, AI0Img.ActualHeight);
            ai1Rect = Function.CreateRect(Canvas.GetLeft(AI1Img), Canvas.GetTop(AI1Img),
                   AI1Img.ActualWidth, AI1Img.ActualHeight);
            ai2Rect = Function.CreateRect(Canvas.GetLeft(AI2Img), Canvas.GetTop(AI2Img),
                   AI2Img.ActualWidth, AI2Img.ActualHeight);
            ai3Rect = Function.CreateRect(Canvas.GetLeft(AI3Img), Canvas.GetTop(AI3Img),
                   AI3Img.ActualWidth, AI3Img.ActualHeight);
        }
        // Hàm va chạm người chơi và AI
        public void PlayerCollisionAI()
        {
            if(Function.Collision(ai0Rect,playerRect) == true)
            {
                if(ai0.AiWidthAndHeight > player.PlayerWidthAndHeight)
                {
                    Close();
                }
                else
                {
                    ai0.Alive = false;
                    PlayerImg.Width = PlayerImg.Height = player.PlayerWidthAndHeight -= ai0.AiWidthAndHeight/2;
                    Canvas.SetLeft(AI0Img, 90000);
                    Canvas.SetTop(AI0Img, 90000);
                    ai0.ASpeed = 0;
                }
            }

            if (Function.Collision(ai1Rect, playerRect) == true)
            {
                if (ai1.AiWidthAndHeight > player.PlayerWidthAndHeight)
                {
                    Close();
                }
                else
                {
                    ai1.Alive = false;
                    PlayerImg.Width = PlayerImg.Height = player.PlayerWidthAndHeight -= ai1.AiWidthAndHeight / 2;
                    Canvas.SetLeft(AI1Img, 90000);
                    Canvas.SetTop(AI1Img, 90000);
                    ai1.ASpeed = 0;
                }
            }

            if (Function.Collision(ai2Rect, playerRect) == true)
            {
                if (ai2.AiWidthAndHeight > player.PlayerWidthAndHeight)
                {
                    Close();
                }
                else
                {
                    ai2.Alive = false;
                    PlayerImg.Width = PlayerImg.Height = player.PlayerWidthAndHeight -= ai2.AiWidthAndHeight / 2;
                    Canvas.SetLeft(AI2Img, 90000);
                    Canvas.SetTop(AI2Img, 90000);
                    ai2.ASpeed = 0;
                }
            }

            if (Function.Collision(ai3Rect, playerRect) == true)
            {
                if (ai3.AiWidthAndHeight > player.PlayerWidthAndHeight)
                {
                    Close();
                }
                else
                {
                    ai3.Alive = false;
                    PlayerImg.Width = PlayerImg.Height = player.PlayerWidthAndHeight -= ai3.AiWidthAndHeight / 2;
                    Canvas.SetLeft(AI3Img, 90000);
                    Canvas.SetTop(AI3Img, 90000);
                    ai3.ASpeed = 0;
                }
            }
        }
    }
}
