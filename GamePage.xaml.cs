namespace WordleProject;

public partial class GamePage : ContentPage
{
    string gameRandomWord;
    HashSet<string> validWordSet;
    int currentRow = 0;
    int timerToggleGame;
    private int elapsedSeconds = 0;
    private IDispatcherTimer gameTimer;

    public GamePage(string randomWord)
	{ 
        InitializeComponent();
        InitializeRow();
        gameRandomWord = randomWord.ToUpper();
        LoadWordAsync();
        timerToggleGame = AppSettings.Toggle;
        if(timerToggleGame == 1)
        {
            TimerLabel.IsVisible = true;
            StartTimer();
        }
        else
        {
            TimerLabel.IsVisible = false;
        }
        
        
	}

    private void InitializeRow()
    {
        for (int i = 0; i <= 5; i++)
        {
            SetRowEnabled(i, false);
        }

        SetRowEnabled(0, true);
        elapsedSeconds = 0;
    }

    private void SetRowEnabled(int rowCount, bool isEnabled)
    {
        switch (rowCount)
        {
            case 0:
                Row0Entry1.IsEnabled = isEnabled;
                Row0Entry2.IsEnabled = isEnabled;
                Row0Entry3.IsEnabled = isEnabled;
                Row0Entry4.IsEnabled = isEnabled;
                Row0Entry5.IsEnabled = isEnabled;
                break;
            case 1:
                Row1Entry1.IsEnabled = isEnabled;
                Row1Entry2.IsEnabled = isEnabled;
                Row1Entry3.IsEnabled = isEnabled;
                Row1Entry4.IsEnabled = isEnabled;
                Row1Entry5.IsEnabled = isEnabled;
                break;
            case 2:
                Row2Entry1.IsEnabled = isEnabled;
                Row2Entry2.IsEnabled = isEnabled;
                Row2Entry3.IsEnabled = isEnabled;
                Row2Entry4.IsEnabled = isEnabled;
                Row2Entry5.IsEnabled = isEnabled;
                break;
            case 3:
                Row3Entry1.IsEnabled = isEnabled;
                Row3Entry2.IsEnabled = isEnabled;
                Row3Entry3.IsEnabled = isEnabled;
                Row3Entry4.IsEnabled = isEnabled;
                Row3Entry5.IsEnabled = isEnabled;
                break;
            case 4:
                Row4Entry1.IsEnabled = isEnabled;
                Row4Entry2.IsEnabled = isEnabled;
                Row4Entry3.IsEnabled = isEnabled;
                Row4Entry4.IsEnabled = isEnabled;
                Row4Entry5.IsEnabled = isEnabled;
                break;
            case 5:
                Row5Entry1.IsEnabled = isEnabled;
                Row5Entry2.IsEnabled = isEnabled;
                Row5Entry3.IsEnabled = isEnabled;
                Row5Entry4.IsEnabled = isEnabled;
                Row5Entry5.IsEnabled = isEnabled;
                break;
        }

    }
    private async Task LoadWordAsync()
    {
        var fileDownloader = new FileDownloader();
        var validWordArray = await fileDownloader.GetWordsAsync();

        // Initialize HashSet for efficient lookups
        validWordSet = new HashSet<string>(validWordArray.Select(word => word.ToUpper()));

    }

    private void OnTextChangedRow0(object sender, TextChangedEventArgs e)
    {
        var currentEntry = sender as Entry;
        //If statement generated with the assistance of chatgpt; When letter is typed it jumps from one box to another
        // Check if text has been entered
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            // Determine the next Entry to focus
            if (currentEntry == Row0Entry1)
                Row0Entry2.Focus();
            else if (currentEntry == Row0Entry2)
                Row0Entry3.Focus();
            else if (currentEntry == Row0Entry3)
                Row0Entry4.Focus();
            else if (currentEntry == Row0Entry4)
                Row0Entry5.Focus();
            //Guess 2
            else if (currentEntry == Row1Entry1)
                Row1Entry2.Focus();
            else if (currentEntry == Row1Entry2)
                Row1Entry3.Focus();
            else if (currentEntry == Row1Entry3)
                Row1Entry4.Focus();
            else if (currentEntry == Row1Entry4)
                Row1Entry5.Focus();
            //Guess 3
            else if (currentEntry == Row2Entry1)
                Row2Entry2.Focus();
            else if (currentEntry == Row2Entry2)
                Row2Entry3.Focus();
            else if (currentEntry == Row2Entry3)
                Row2Entry4.Focus();
            else if (currentEntry == Row2Entry4)
                Row2Entry5.Focus();
            //Guess 4
            else if (currentEntry == Row3Entry1)
                Row3Entry2.Focus();
            else if (currentEntry == Row3Entry2)
                Row3Entry3.Focus();
            else if (currentEntry == Row3Entry3)
                Row3Entry4.Focus();
            else if (currentEntry == Row3Entry4)
                Row3Entry5.Focus();
            //Guess 5
            else if (currentEntry == Row4Entry1)
                Row4Entry2.Focus();
            else if (currentEntry == Row4Entry2)
                Row4Entry3.Focus();
            else if (currentEntry == Row4Entry3)
                Row4Entry4.Focus();
            else if (currentEntry == Row4Entry4)
                Row4Entry5.Focus();
            //Guess 6
            else if (currentEntry == Row5Entry1)
                Row5Entry2.Focus();
            else if (currentEntry == Row5Entry2)
                Row5Entry3.Focus();
            else if (currentEntry == Row5Entry3)
                Row5Entry4.Focus();
            else if (currentEntry == Row5Entry4)
                Row5Entry5.Focus();
        }
    }

    private async void EnterButton_Clicked(object sender, EventArgs e)
    {

        string enteredWord = GetWordFromCurrentRow();

        //Checking if user entered 5 letters
        if (enteredWord.Length != 5 || enteredWord.Any(char.IsWhiteSpace))
        {
            await DisplayAlert("Error", "Please enter 5 letters.", "Ok");
            return;
        }

        if (!validWordSet.Contains(enteredWord))
        {
            await DisplayAlert("Error", "That is not a word from the list", "Ok");
            return;
        }



        if (enteredWord.ToUpper() == gameRandomWord.ToUpper())
        {
            await DisplayAlert("Congrats", "You guessed the word", "Ok");
        }
        else if(validWordSet.Contains(enteredWord) && enteredWord.ToUpper() != gameRandomWord.ToUpper())
        {
            DisplayAlert("oops", "try again", "ok");
        }

        if (currentRow == 5) // Last row
        {
            await DisplayAlert("Good game", "You've run out of guesses. Better luck next time!", "Ok");
            await Navigation.PopToRootAsync();
            return;
        }

        // Move to the next row
        SetRowEnabled(currentRow, false); 
        currentRow++;
        SetRowEnabled(currentRow, true); 
    }

    private string GetWordFromCurrentRow()
    {
       
        return currentRow switch
        {
            0 => $"{Row0Entry1.Text}{Row0Entry2.Text}{Row0Entry3.Text}{Row0Entry4.Text}{Row0Entry5.Text}".ToUpper(),
            1 => $"{Row1Entry1.Text}{Row1Entry2.Text}{Row1Entry3.Text}{Row1Entry4.Text}{Row1Entry5.Text}".ToUpper(),
            2 => $"{Row2Entry1.Text}{Row2Entry2.Text}{Row2Entry3.Text}{Row2Entry4.Text}{Row2Entry5.Text}".ToUpper(),
            3 => $"{Row3Entry1.Text}{Row3Entry2.Text}{Row3Entry3.Text}{Row3Entry4.Text}{Row3Entry5.Text}".ToUpper(),
            4 => $"{Row4Entry1.Text}{Row4Entry2.Text}{Row4Entry3.Text}{Row4Entry4.Text}{Row4Entry5.Text}".ToUpper(),
            5 => $"{Row5Entry1.Text}{Row5Entry2.Text}{Row5Entry3.Text}{Row5Entry4.Text}{Row5Entry5.Text}".ToUpper(),
            _ => string.Empty
        };
    }

    private void StartTimer()
    {
        gameTimer = Dispatcher.CreateTimer();
        gameTimer.Interval = TimeSpan.FromSeconds(1);
        gameTimer.Tick += (s, e) =>
        {
            elapsedSeconds++;
            TimerLabel.Text = $"Time: {elapsedSeconds / 60}:{elapsedSeconds % 60:D2}";
        };
        gameTimer.Start();
    }
}
