//המוסיקה מתננגת לפי כול שלב יש שיר אחר עם את מפסיד אז גם ש מנגינה זה לא עובד בווינדוס 8 וגם יש מדיה פלייר נמוך מ 10*
// השחקנים הרעים זזים לפי מיקום של הטוב במידה והטוב לא זז והם נוגעים בקיר הם משנים מיקום
//ניתן לבחור רמת קושי על ידי לחיצה התחל מפתחות 3 אופציות קל בינוני קשה
//לחצן ריסטרט בעצם סוגר את האפליקציה הנוכחית ופותח חדשה 
//**אם רוצים שהמשחק ירוץ תקין יש ללחוץ בפעמים בודדות ולא ברצף להזזת השחקן הטוב**
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using System.Collections;
using Microsoft.Win32;

namespace ProjectDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private const int PlayerSize = 30;
        private GoodPlayer _goodPlayer;
        private BadPlayer _badPlayer;
        private Cherry _cherry;
        private readonly Random _rnd;
        private DispatcherTimer _timer;
        private GameState _gameState;
        private int _badCount;
        BadPlayer[] bad_player_array = new BadPlayer[6];
        int qa;
        int d_l = 1;
        int d_t = 1;
        Boolean cherry_add = true; 
        public int _num_of_bad;
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        DispatcherTimer cherry_t = new DispatcherTimer();
        string currentTime;
        int score = 10;
        public MainWindow()
        {
            InitializeComponent();
             _rnd = new Random();
            _timer = new DispatcherTimer();
            
           
        }
        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
                label1.Content= currentTime;
            }
        }
     

        private void InitializeGame(int num_of_bad)
        {
            _num_of_bad =num_of_bad;
            _gameState = GameState.None;
            InitializGoodPlayer();
            InitializBadPlayer(num_of_bad);
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            cherry_t.Tick += new EventHandler(cherry_t_Tick);
            cherry_t.Interval = new TimeSpan(0, 0, 5);
            
            
        }

        void cherry_t_Tick(object sender, EventArgs e)
        {
            if (cherry_add)
            {
                _cherry = new Cherry(PlayerSize);
                int left = GetRandomNumber();
                int top = GetRandomNumber();
                _cherry.Location = new Location(left, top);
                Canvas.SetLeft(_cherry.PlayerImage, left);
                Canvas.SetTop(_cherry.PlayerImage, top);
                canvas.Children.Add(_cherry.PlayerImage);
            }
            cherry_add = false;
        }
        private void InitializGoodPlayer()
        {
            _goodPlayer = new GoodPlayer(PlayerSize);
            int left = GetRandomNumber();
            int top = GetRandomNumber();
            _goodPlayer.Location = new Location(left, top);
            Canvas.SetLeft(_goodPlayer.PlayerImage, left);
            Canvas.SetTop(_goodPlayer.PlayerImage, top);
            canvas.Children.Add(_goodPlayer.PlayerImage);
        }
        private void InitializBadPlayer(int num_of_bad)
        {
            for (int i = 0; i < num_of_bad; i++)
            {
                _badPlayer = new BadPlayer(PlayerSize);
                _badPlayer.Left_Direction1 = 1;
                _badPlayer.TOP_Direction1 = 1;
                int left = GetRandomNumber();
                int top = GetRandomNumber();
                _badPlayer.Location = new Location(left, top);
                Canvas.SetLeft(_badPlayer.PlayerImage, left);
                Canvas.SetTop(_badPlayer.PlayerImage, top);
                canvas.Children.Add(_badPlayer.PlayerImage);
                bad_player_array[i] = _badPlayer;
              
            }
           
            _timer.Tick += new EventHandler(dispatcherTimer_Tick);
           _timer.Interval = new TimeSpan(10000);
            cherry_t.Start();
           _timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < _num_of_bad; i++) // לןלאה שסופרת את כמות הרעים שאינם נראים ברגע שיש אותה עמות כמו הרעים עצמם המשחק נגמר
            {
                if (bad_player_array[i].PlayerImage.IsVisible == false)
                {
                    _badCount++;
                }
            }
            if (_badCount == _num_of_bad)
            {
                MessageBox.Show("you win");
                this.Close();
            }
            else
            {
                _badCount = 0;
            }
            
            for (int  i = 0; i <_num_of_bad; i++)
            {
             
                for (int j = 1+i; j <_num_of_bad; j++)   //בודק את המיקומים של השחקנים הרעים הנראים במידה ויש התאמה הופך אותם ללא נראים 
			{
                if (  bad_player_array[i].Location.Left + 20             > bad_player_array[j].Location.Left
                   && bad_player_array[i].Location.Left                  < bad_player_array[j].Location.Left
                   && bad_player_array[i].Location.Top + 20              > bad_player_array[j].Location.Top
                   && bad_player_array[i].Location.Top                   < bad_player_array[j].Location.Top 
                   && bad_player_array[i].PlayerImage.IsVisible ||

                      bad_player_array[i].Location.Top                  == bad_player_array[j].Location.Top
                   && bad_player_array[i].Location.Left + 20             > bad_player_array[j].Location.Left
                   && bad_player_array[i].Location.Left                  < bad_player_array[j].Location.Left
                   && bad_player_array[i].PlayerImage.IsVisible ||

                     bad_player_array[i].Location.Left                  == bad_player_array[j].Location.Left
                   && bad_player_array[i].Location.Top + 20              > bad_player_array[j].Location.Top
                   && bad_player_array[i].Location.Top                   < bad_player_array[j].Location.Top
                   && bad_player_array[i].PlayerImage.IsVisible ||

                    bad_player_array[j].Location.Left + 20               > bad_player_array[i].Location.Left
                   && bad_player_array[j].Location.Left                  < bad_player_array[i].Location.Left
                   && bad_player_array[j].Location.Top + 20              > bad_player_array[i].Location.Top
                   && bad_player_array[j].Location.Top                   < bad_player_array[i].Location.Top 
                   && bad_player_array[j].PlayerImage.IsVisible ||

                      bad_player_array[j].Location.Top                  == bad_player_array[i].Location.Top
                   && bad_player_array[j].Location.Left + 20             > bad_player_array[i].Location.Left
                   && bad_player_array[j].Location.Left                  < bad_player_array[i].Location.Left
                   && bad_player_array[j].PlayerImage.IsVisible ||

                     bad_player_array[j].Location.Left                  == bad_player_array[i].Location.Left
                   && bad_player_array[j].Location.Top +20               > bad_player_array[i].Location.Top
                   && bad_player_array[j].Location.Top                   < bad_player_array[i].Location.Top
                   && bad_player_array[j].PlayerImage.IsVisible 
                  
                  )
                    
                {
                  
                    bad_player_array[i].PlayerImage.Visibility = Visibility.Hidden;
                    bad_player_array[i].PlayerImage.IsEnabled = false;
                    bad_player_array[j].PlayerImage.Visibility = Visibility.Hidden;
                    bad_player_array[j].PlayerImage.IsEnabled = false;
                    

                    
                   
                }

			}
                
                Canvas.SetLeft(bad_player_array[i].PlayerImage, bad_player_array[i].Location.Left += bad_player_array[i].Left_Direction1);
                Canvas.SetTop(bad_player_array[i].PlayerImage, bad_player_array[i].Location.Top += bad_player_array[i].TOP_Direction1);
                if (_goodPlayer.Location.Left > bad_player_array[i].Location.Left)
                {
                    bad_player_array[i].Left_Direction1 = 1;
                }
                if (_goodPlayer.Location.Left < bad_player_array[i].Location.Left)
                {
                    bad_player_array[i].Left_Direction1 = -1;
                }
                if (_goodPlayer.Location.Top > bad_player_array[i].Location.Top)
                {
                    bad_player_array[i].TOP_Direction1 = 1;
                }
                if (_goodPlayer.Location.Top < bad_player_array[i].Location.Top)
                {
                    bad_player_array[i].TOP_Direction1 = -1;
                }
               
                if (bad_player_array[i].Location.Left >= Width - PlayerSize * 1.5)
                {
                    bad_player_array[i].Left_Direction1 = -1;
                }
                if (bad_player_array[i].Location.Top >= Height - PlayerSize * 3.5)
                {               
                    bad_player_array[i].TOP_Direction1 = -1;
                
                }
                if (bad_player_array[i].Location.Top < 5.2)
                {
                    bad_player_array[i].TOP_Direction1 = 1;
                  
                }
                if (bad_player_array[i].Location.Left < 4.7)
                {
                    bad_player_array[i].Left_Direction1 = 1;
                   
                }
                //אין לניקוד איזה משמעות או כלל מסוים סתם נראה יפה שהמספרים רצים ככה וניהיה ניקוד גבוה
                if (_goodPlayer.Location.Top == _cherry.Location.Top && _goodPlayer.Location.Left == _cherry.Location.Left)
                {
              
                    canvas.Children.Remove(_cherry.PlayerImage);
                    label3.Content = score.ToString();
                    score += 10;
                    cherry_add = true;
                }
                if (bad_player_array[i].Location.Left + PlayerSize/2 > _goodPlayer.Location.Left
                   && bad_player_array[i].Location.Left              < _goodPlayer.Location.Left
                   && bad_player_array[i].Location.Top               < _goodPlayer.Location.Top
                   && bad_player_array[i].Location.Top               > _goodPlayer.Location.Top - PlayerSize/2
                   && bad_player_array[i].PlayerImage.IsVisible                                                 ||

                      _goodPlayer.Location.Left + PlayerSize / 2 > bad_player_array[i].Location.Left
                   && _goodPlayer.Location.Left                  < bad_player_array[i].Location.Left
                   && bad_player_array[i].Location.Top           < _goodPlayer.Location.Top
                   && bad_player_array[i].Location.Top           > _goodPlayer.Location.Top - PlayerSize / 2
                   && bad_player_array[i].PlayerImage.IsVisible)
                {
                    MessageBox.Show("you loss");
                    this.Close();
                }
                
              
            }
           
        }

        private int GetRandomNumber()
        {
            int num = _rnd.Next(1, 20);
            return num * PlayerSize;
        }


        private void OnWindowKeyDown(object sender, KeyEventArgs e)
        {


            double top = _goodPlayer.Location.Top;
            double left = _goodPlayer.Location.Left;

            switch (e.Key) // Check that the good stays inside the screen border
            {
                case Key.Up:
                    top = _goodPlayer.Location.Top - PlayerSize;
                    if (top < 0)
                    {
                        return;
                    }
                 
                    break;

                case Key.Down:
                    top = _goodPlayer.Location.Top + PlayerSize;
                    if (top >= Height - PlayerSize * 3)
                    {

                        return;
                    }
                    break;

                case Key.Left:
                    left = _goodPlayer.Location.Left - PlayerSize;
                    if (left < 0)
                    {
                        return;
                    }
                    break;

                case Key.Right:
                    left = _goodPlayer.Location.Left + PlayerSize;
                    if (left >= Width - PlayerSize)
                    {
                        return;
                    }
                    break;

                case Key.Space:

                    break;
            }

            Canvas.SetLeft(_goodPlayer.PlayerImage, left);
            Canvas.SetTop(_goodPlayer.PlayerImage, top);
            _goodPlayer.Location = new Location(left, top);

        }

        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Easy_level_Click(object sender, RoutedEventArgs e)
        {
            medium_level.IsEnabled = false;
            hard_level.IsEnabled = false;
            stopWatch.Start();
            dt.Start();
            InitializeGame(2);
        }

        private void medium_level_Click(object sender, RoutedEventArgs e)
        {
            easy_level.IsEnabled = false;
            hard_level.IsEnabled = false;
            
            
            stopWatch.Start();
            dt.Start();
            InitializeGame(4);
        }

        private void hard_level_Click(object sender, RoutedEventArgs e)
        {
            easy_level.IsEnabled = false;
            medium_level.IsEnabled = false;
            
            stopWatch.Start();
            dt.Start();
            InitializeGame(6);
        }

        private void select_pic_Click(object sender, RoutedEventArgs e)
        {
           
            OpenFileDialog name_file = new OpenFileDialog();

            name_file.Title = "Select a picture";
            name_file.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (name_file.ShowDialog() == true)
            {
                Uri uri = new Uri(name_file.FileName);
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(@name_file.FileName, UriKind.Relative));
                canvas.Background = ib;
                
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
                  
        }
    

    }
}

