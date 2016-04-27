using Client.TicTacToeService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class BoardControl : UserControl
    {
        private MainForm mainForm;
        private bool viewOnly;
        private const int CELL_SIZE = 50;

        public int boardSize { get; set; }      
        public char myToken { get; set; }
        public bool myTurn { get; set; }
        public char playMode { get; set; }

        private Grid grid;

        // Constructor for view only
        public BoardControl()
        {
            InitializeComponent();
            this.viewOnly = true;
        }

        // Constructor for play
        public BoardControl(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.viewOnly = false;
            this.myTurn = false;
        }

        // Methos inits the board
        public void InitBoard()
        {
            grid = new Grid();
            this.Content = grid;

            grid.Effect = drawShadow();
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;
            
            ColumnDefinition[] columnDefinition = new ColumnDefinition[boardSize];
            RowDefinition[] rowDefinition = new RowDefinition[boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                columnDefinition[i] = new ColumnDefinition();
                rowDefinition[i] = new RowDefinition();
                columnDefinition[i].Width = new GridLength(CELL_SIZE);
                rowDefinition[i].Height = new GridLength(CELL_SIZE);
                grid.ColumnDefinitions.Add(columnDefinition[i]);
                grid.RowDefinitions.Add(rowDefinition[i]);
            }

            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                    drawEmptyCell(i, j);
        }


        // Method draws an empty cell
        public void drawEmptyCell(int i, int j)
        {
            Button button = createButton(i, j);
            Storyboard storyboard = setFadeInAnimation(button);

            if (viewOnly)
                button.Focusable = false;
            else
                button.Click += Button_Click;

            Grid.SetRow(button, i);
            Grid.SetColumn(button, j);
            grid.Children.Add(button);

            // Begin the storyboard
            storyboard.Begin(this);
        }

        // Method draw the winner move path
        public void drawWinMove(char winnerToken, int[] moves) {

            foreach (int index in moves)
            {
                int row = index / boardSize;
                int col = index % boardSize;

                setStepOnBoard(row, col, winnerToken);
            }
        }

        // Method commits turn
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameClient c = mainForm.ClientService;

            if (myTurn)
            {
                Button btn = sender as Button;
                string[] coords = btn.Name.Split('_');
                c.CommitTurn(mainForm.BoardName, playMode, mainForm.UserName, myToken, int.Parse(coords[1]), int.Parse(coords[2]), boardSize);
            }
        }

        // Method commit the step and fill the correct cell
        public void setStepOnBoard(int i, int j, char winToken = ' ')
        {
            Button button = createButton(i, j);
            button.Focusable = false;

            Storyboard storyboard;
            if (winToken != ' ')
            {
                storyboard = setBlinkAnimation(button);
                drawToken(winToken, ref button, true);
            }
            else
            {
                storyboard = setFadeInAnimation(button);
                if (myTurn)
                    drawToken(myToken, ref button);
                else if (myToken == 'X')
                    drawToken('O', ref button);
                else
                    drawToken('X', ref button);
            }
            
            Grid.SetRow(button, i);
            Grid.SetColumn(button, j);
            grid.Children.Add(button);

            // Begin the storyboard
            storyboard.Begin(this);
        }

        // Method switch the turn
        public void SwitchTurn()
        {
            this.myTurn = !this.myTurn;
        }

        // Method init new button and returns it
        private Button createButton(int i, int j)
        {
            Button button = new Button();
            button.Name = "_" + i + "_" + j;
            button.BorderThickness = new Thickness(2, 2, 2, 2);
            button.BorderBrush = Brushes.DarkBlue;

            return button;
        }

        // Method draw the token on the button
        private void drawToken(char token, ref Button button, bool winToken = false)
        {
            System.Drawing.Bitmap bitmap;
            if(winToken)
                bitmap = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject(token.ToString().ToLower() + "Win");
            else 
                bitmap = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject(token.ToString().ToUpper());
            ImageSource image = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            button.Content = new Image
            {
                Source = image,
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        // Method returns StoryBoard animation of fade on the button
        private Storyboard setFadeInAnimation(Button button)
        {
            // Create a storyboard to contain the animations.
            Storyboard storyboard = new Storyboard();
            TimeSpan duration = new TimeSpan(0, 0, 1);

            // Create a DoubleAnimation to fade the not selected option control
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0.0;
            animation.To = 1.0;
            animation.Duration = new Duration(duration);

            // Configure the animation to target de property Opacity
            Storyboard.SetTarget(animation, button);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Control.OpacityProperty));

            // Add the animation to the storyboard
            storyboard.Children.Add(animation);

            return storyboard;
        }

        // Method returns StoryBoard animation of blink on the button
        private Storyboard setBlinkAnimation(Button button)
        {
            // Create a storyboard to contain the animations.
            Storyboard blinkStoryboard = new Storyboard();
            blinkStoryboard.Duration = TimeSpan.FromSeconds(1);
            blinkStoryboard.RepeatBehavior = RepeatBehavior.Forever;

            // Create a DoubleAnimation to switch off
            DoubleAnimation switchOffAnimation = new DoubleAnimation();
            switchOffAnimation.To = 0;
            switchOffAnimation.Duration = TimeSpan.Zero;

            // Create a DoubleAnimation to switch on
            DoubleAnimation switchOnAnimation = new DoubleAnimation();
            switchOnAnimation.To = 1;
            switchOnAnimation.Duration = TimeSpan.Zero;
            switchOnAnimation.BeginTime = TimeSpan.FromSeconds(0.5);

            // Configure the animation to target de property Opacity
            Storyboard.SetTarget(switchOffAnimation, button);
            Storyboard.SetTargetProperty(switchOffAnimation, new PropertyPath(Canvas.OpacityProperty));
            blinkStoryboard.Children.Add(switchOffAnimation);

            // Configure the animation to target de property Opacity
            Storyboard.SetTarget(switchOnAnimation, button);
            Storyboard.SetTargetProperty(switchOnAnimation, new PropertyPath(Canvas.OpacityProperty));
            blinkStoryboard.Children.Add(switchOnAnimation);

            return blinkStoryboard;
        }

        // Methods inits the shadow effect
        private DropShadowEffect drawShadow()
        {
            DropShadowEffect shadowEffect = new DropShadowEffect();

            // Set the color of the shadow to Black.
            Color shadowColor = new Color();
            shadowColor.ScA = 1;
            shadowColor.ScB = 0;
            shadowColor.ScG = 0;
            shadowColor.ScR = 0;
            shadowEffect.Color = shadowColor;

            // Set the direction of where the shadow is cast to 320 degrees.
            shadowEffect.Direction = 320;

            // Set the depth of the shadow being cast.
            shadowEffect.ShadowDepth = 25;

            // Set the shadow softness to the maximum (range of 0-1).
            shadowEffect.BlurRadius = 24;

            // Set the shadow opacity to half opaque or in other words - half transparent. The range is 0-1.
            shadowEffect.Opacity = 0.5;

            return shadowEffect;
        }
    }


}
