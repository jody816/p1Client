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

namespace p1Client
{
    /// <summary>
    /// Pacman.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Pacman : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();

        bool goLeft, goRight, goDown, goUp;
        bool noLeft, noRight, noDown, noUp;

        int speed = 8;
        Rect pacmanHitBox;

        int ghostSpeed = 10;
        int ghostMoveStep = 160;
        int currentGhostStep;

        int score = 0;

        public Pacman()
        {
            InitializeComponent();

            GameSetUp();

            MediaElement me = new MediaElement();
            me.Source = new Uri(@"C:/Users/jody8/source/repos/p1Client/BGM/Pacman.mp3", UriKind.Absolute);

            me.LoadedBehavior = MediaState.Manual;
            me.Play();
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && noLeft == false)   // 왼쪽
            {
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;

                goLeft = true;

                pacman.RenderTransform = new RotateTransform(-180, pacman.Width / 2, pacman.Height / 2);
            }
            if (e.Key == Key.Right && noRight == false) // 오른쪽
            {
                noLeft = noUp = noDown = false;
                goLeft = goUp = goDown = false;
                goRight = true;

                pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);
            }
            if (e.Key == Key.Up && noUp == false)       // 위쪽
            {
                noRight = noDown = noLeft = false;
                goRight = goDown = goLeft = false;
                goUp = true;

                pacman.RenderTransform = new RotateTransform(-90, pacman.Width / 2, pacman.Height / 2);
            }
            if (e.Key == Key.Down && noDown == false)   // 아래쪽
            {
                noUp = noLeft = noRight = false;
                goUp = goLeft = goRight = false;
                goDown = true;

                pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2);
            }
        }
        private void GameSetUp()
        {
            MyCanvas.Focus();

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            currentGhostStep = ghostMoveStep;

            ImageBrush pacmanImage = new ImageBrush();
            pacmanImage.ImageSource = new BitmapImage(new Uri("C:/Users/jody8/source/repos/p1Client/Images/pacman.jpg"));
            pacman.Fill = pacmanImage;

            ImageBrush redGhost = new ImageBrush();
            redGhost.ImageSource = new BitmapImage(new Uri("C:/Users/jody8/source/repos/p1Client/Images/red.jpg"));
            redGuy.Fill = redGhost;

            ImageBrush orangeGhost = new ImageBrush();
            orangeGhost.ImageSource = new BitmapImage(new Uri("C:/Users/jody8/source/repos/p1Client/Images/orange.jpg"));
            orangeGuy.Fill = orangeGhost;

            ImageBrush pinkGhost = new ImageBrush();
            pinkGhost.ImageSource = new BitmapImage(new Uri("C:/Users/jody8/source/repos/p1Client/Images/pink.jpg"));
            pinkGuy.Fill = pinkGhost;
        }
        private void GameLoop(object sender, EventArgs e)
        {
            txtScore.Content = "Score: " + score;
            // 점수 변환

            if (goRight)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + speed);
            }
            if (goLeft)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - speed);
            }
            if (goUp)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) - speed);
            }
            if (goDown)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) + speed);
            }
            // 위치지정(움직임)

            if (goDown && Canvas.GetTop(pacman) - 70 > Application.Current.MainWindow.Height)
            {
                noDown = true;
                goDown = false;
            }
            if (goUp && Canvas.GetTop(pacman) < 1)
            {
                noUp = true;
                goUp = false;
            }
            if (goLeft && Canvas.GetLeft(pacman) < 10)
            {
                noLeft = true;
                goLeft = false;
            }
            if (goRight && Canvas.GetLeft(pacman) + 52 > Application.Current.MainWindow.Width)
            {
                noRight = true;
                goRight = false;
            }
            // 정해진 윈도우창 범위 내에서만 이동

            pacmanHitBox = new Rect(Canvas.GetLeft(pacman), Canvas.GetTop(pacman), pacman.Width, pacman.Height);

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())    // MyCanvas 내에서 쓰인 것들에게 특성(?)을 줌
            {
                Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                if ((string)x.Tag == "wall") // 벽
                {
                    if (goLeft == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + 10);
                        noLeft = true;
                        goLeft = false;
                    }
                    if (goRight == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - 10);
                        noRight = true;
                        goRight = false;
                    }
                    if (goDown == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) - 10);
                        noDown = true;
                        goDown = false;
                    }
                    if (goUp == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) + 10);
                        noUp = true;
                        goUp = false;
                    }
                }
                if ((string)x.Tag == "coin")    // 동전, 점수 증가
                {
                    if (pacmanHitBox.IntersectsWith(hitBox) && x.Visibility == Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;   // 동전과 교차하게되면 안보이게 만들어줌
                        score++;
                    }
                }
                if ((string)x.Tag == "ghost")   // 유령
                {
                    if (pacmanHitBox.IntersectsWith(hitBox))
                    {
                        GameOver("고스트가 잡았습니다. 다시 시작해주세요.");
                    }
                    if (x.Name.ToString() == "orangeGuy")   // 고스트의 이동
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - ghostSpeed);  // 오렌지가이가 왼쪽으로
                    }
                    else
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + ghostSpeed);  // 나머지 오른쪽
                    }
                    currentGhostStep--;

                    if (currentGhostStep < 1)               // 고스트의 currentGhostStep이 1보다 작아지게 되면
                    {                                       // currentGhostStep이 다시 97의 값을 받고
                        currentGhostStep = ghostMoveStep;   // ghostSpeed = -ghostSpeed; 에 의해서
                        ghostSpeed = -ghostSpeed;           // Canvas.SetLeft(x, Canvas.GetLeft(x) - ghostSpeed); 가
                    }                                       // Canvas.SetLeft(x, Canvas.GetLeft(x) + ghostSpeed); 로 되면서
                }                                           // 왔다갔다 하게 됌.
            }
            if (score == 97)
            {
                GameWin("승리하였습니다!!!!");
            }
        }

        private void GameOver(string message)
        {
            gameTimer.Stop();
            MessageBox.Show(message, "죽었습니다. 다시 시작하세요.");

            Application.Current.Shutdown();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);

        }

        private void GameWin(string message)
        {
            gameTimer.Stop();
            MessageBox.Show(message, "휴 성공이다.");

            Application.Current.Shutdown();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);

        }
    }
}