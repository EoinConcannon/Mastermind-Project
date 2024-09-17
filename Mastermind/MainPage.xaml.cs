using Microsoft.Maui.Controls.Shapes;

namespace Mastermind;

public partial class MainPage : ContentPage
{
    //declaring variables
    //sets the currentRow for Button_Clicked Event
    int currentRow = 9;
    //users code array
    int[] pin = {0, 1, 2, 3};
    //randomly generated code array
    public int[] pinCode = {0, 0, 0, 0};

    //prevents the code from being generated on second turn
    int codeSet = 0;

    //grey hint = right colour, wrong place
    //white hint = right colour, right place
    int hintGray = 0;
    int hintWhite = 0;

    Random codeGenerator;
    public MainPage()
	{
		InitializeComponent();
        codeGenerator = new Random();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
        //creating ellipse/ label objects
        Ellipse clone = new Ellipse();
        Label cloneText = new Label();
        hintGray = 0;
        hintWhite = 0;

        //sets the code once
        if (codeSet == 0)
        {
            codeSet++;

            for (int i = 0; i < 4; i++)
            {
                int num = codeGenerator.Next(0, 8);
                pinCode[i] = num;//assigns random number to array
                for (int j = 0; j < i; j++)
                {
                    //prevents duplicates, there are no duplicate colours in a code
                    if (num == pinCode[j])
                        i--;
                }
            }
            //red = 0
            //blue = 1
            //green = 2
            //yellow = 3
            //purple = 4
            //pink = 5
            //brown = 6
            //turqouise = 7
        }//end of if

        //checks the users code
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (pin[i] == pinCode[j])
                {
                    hintGray++;//if right colour, wrong place
                    if(i == j)
                    {
                        hintGray--;
                        hintWhite++;//if right colour, right place
                    }
                }
            }
        }

        //if the user guess correctly
        if(hintWhite == 4)
        {
            changeColor(hidden0, pinCode[0]);
            changeColor(hidden1, pinCode[1]);
            changeColor(hidden2, pinCode[2]);
            changeColor(hidden3, pinCode[3]);
            mainButton.IsEnabled = false;
            currentRow++;
            DisplayAlert("You win", "CONGRATULATIONS", "Ok");
        }

        //if the user passes top of grid - loses
        if (currentRow == 0)
        {
            changeColor(hidden0, pinCode[0]);
            changeColor(hidden1, pinCode[1]);
            changeColor(hidden2, pinCode[2]);
            changeColor(hidden3, pinCode[3]);
            mainButton.IsEnabled = false;
            DisplayAlert("You lose", "GAME OVER", "Ok");
            //following for loop does not work
            //for(int r = 0; r < 10; r++)
            //{
            //    for(int c = 0; c < 7; c++)
            //    {
            //        mainGrid.Children.Remove(clone);
            //        mainGrid.Children.Remove(cloneText);
            //    }
            //}
            currentRow++;
        }

        for (int c = 0; c < 7; c++)
        {
            //4 is a gap
            if (c != 4)
            {
                clone = new Ellipse();
                cloneText = new Label();
                mainGrid.Children.Add(clone);
                clone.HeightRequest = 35;
                clone.WidthRequest = 35;

                //creating hint pins
                if (c == 5 || c == 6)
                {
                    mainGrid.Children.Add(cloneText);
                    if (c == 5)
                    {
                        clone.Fill = Colors.Gray;
                        cloneText.Text = hintGray.ToString();
                    }
                    if (c == 6)
                    {
                        clone.Fill = Colors.White;
                        cloneText.Text = hintWhite.ToString();
                    }

                    cloneText.TextColor = Colors.Black;
                    cloneText.HorizontalOptions = LayoutOptions.Center;
                    cloneText.VerticalOptions = LayoutOptions.Center;
                    cloneText.SetValue(Grid.RowProperty, currentRow);
                    cloneText.SetValue(Grid.ColumnProperty, c);
                }
                //create pins that display previous user code with !
                else
                {
                    changeColor(clone, pin[c]);

                    mainGrid.Children.Add(cloneText);
                    cloneText.Text = "!";
                    cloneText.TextColor = Colors.Black;
                    cloneText.HorizontalOptions = LayoutOptions.Center;
                    cloneText.VerticalOptions = LayoutOptions.Center;
                    cloneText.SetValue(Grid.RowProperty, currentRow);
                    cloneText.SetValue(Grid.ColumnProperty, c);
                }
                clone.SetValue(Grid.RowProperty, currentRow);
                clone.SetValue(Grid.ColumnProperty, c);
            }
        }//end of for loop

        //move interactable pins up
        currentRow--;
		Pin0.SetValue(Grid.RowProperty, currentRow);
        Pin1.SetValue(Grid.RowProperty, currentRow);
        Pin2.SetValue(Grid.RowProperty, currentRow);
        Pin3.SetValue(Grid.RowProperty, currentRow);
    }

    private void Pin_Clicked0(object sender, EventArgs e)
    {
        //if user taps left pin
        //colours changes / value of user array goes up
        pin[0]++;
        if (pin[0] == 8)
            pin[0] = 0;
        changeColor(sender, pin[0]);
    }
    private void Pin_Clicked1(object sender, EventArgs e)
    {
        pin[1]++;
        if (pin[1] == 8)
            pin[1] = 0;
        changeColor(sender, pin[1]);
    }
    private void Pin_Clicked2(object sender, EventArgs e)
    {
        pin[2]++;
        if (pin[2] == 8)
            pin[2] = 0;
        changeColor(sender, pin[2]);
    }
    private void Pin_Clicked3(object sender, EventArgs e)
    {
        //if user taps right pin
        pin[3]++;
        if (pin[3] == 8)
            pin[3] = 0;
        changeColor(sender, pin[3]);
    }

    private void changeColor(object sender, int colorVal)
    {
        switch (colorVal)//update the colour of a pin
        {
            case 0:
            {
                ((Ellipse)sender).Fill = Colors.Red;
                break;
            }
            case 1:
            {
                ((Ellipse)sender).Fill = Colors.Blue;
                break;
            }
            case 2:
            {
                ((Ellipse)sender).Fill = Colors.Green;
                break;
            }
            case 3:
            {
                ((Ellipse)sender).Fill = Colors.Yellow;
                break;
            }
            case 4:
            {
                ((Ellipse)sender).Fill = Colors.Purple;
                break;
            }
            case 5:
            {
                ((Ellipse)sender).Fill = Colors.Pink;
                break;
            }
            case 6:
            {
                ((Ellipse)sender).Fill = Colors.SaddleBrown;
                break;
            }
            case 7:
            {
                ((Ellipse)sender).Fill = Colors.Turquoise;
                break;
            }
        }
    }

    private void playButton_Clicked(object sender, EventArgs e)
    {
        //hides menu, brings up main grid
        topUI.IsVisible = true;
        mainGrid.IsVisible = true;
        helpButton.IsVisible = false;
        playButton.IsVisible = false;
        titleScreen.IsVisible = false;
    }

    private void helpButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Help", "Welcome to Mastermind, the aim of the game is to guess the randomly generated code at the top of the screen, tap your pins at the bottom of the screen to change colours, there are NO duplicate colours in the generated code. HAVE FUN", "Ok");
    }

    private void homeButton_Clicked(object sender, EventArgs e)
    {
        //hides main grid, brings up menu
        topUI.IsVisible = false;
        mainGrid.IsVisible = false;
        helpButton.IsVisible = true;
        playButton.IsVisible = true;
        titleScreen.IsVisible = true;
    }
}