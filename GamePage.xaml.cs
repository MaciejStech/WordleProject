using Microsoft.Maui.Storage;
using System.IO;

namespace WordleProject;

public partial class GamePage : ContentPage
{
    string gameRandomWord;
    HashSet<string> validWordSet;
    int currentRow = 0;
    int timerToggleGame;
    private int elapsedSeconds = 0;
    private IDispatcherTimer gameTimer;
    int guessCounter = 1;
    int loginInfo = 0;

    public GamePage(string randomWord)
    {
        InitializeComponent();
        InitializeRow();
        gameRandomWord = randomWord.ToUpper();
        LoadWordAsync();
        timerToggleGame = AppSettings.Toggle;
        loginInfo = AppSettings.Log;
        if (timerToggleGame == 1)
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

        // Checking if user entered 5 letters
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

        // Track if the word is correct
        bool isCorrectWord = enteredWord.ToUpper() == gameRandomWord.ToUpper();

        // Check each letter and update the color
        for (int i = 0; i < 5; i++)
        {
            Entry currentEntry = GetEntryForRow(currentRow, i);

            await FlipEntryBox(currentEntry);

            if (enteredWord[i] == gameRandomWord[i])
            {
                // Correct letter, correct position -> Green
                currentEntry.BackgroundColor = Color.FromArgb("#00FF00");
            }
            else if (gameRandomWord.Contains(enteredWord[i]))
            {
                // Correct letter, wrong position -> Yellow
                currentEntry.BackgroundColor = Color.FromArgb("#FFFF00");
            }
            else
            {
                // Incorrect letter -> Gray
                currentEntry.BackgroundColor = Color.FromArgb("#808080");
            }
        }

        if (isCorrectWord)
        {
            // If the word is correct, stop the timer and show a success message
            if (timerToggleGame == 1 && loginInfo == 1)
            {
                gameTimer.Stop();
                string timeTaken = $"{elapsedSeconds / 60}:{elapsedSeconds % 60:D2}";
                await DisplayAlert("Game Complete!", $"Word: {gameRandomWord}\nTime: {timeTaken}\nGuesses: {guessCounter}", "Ok");
                string infoTimer = $"\nWord: {gameRandomWord}     Time: {timeTaken}       Guesses: {guessCounter}";
                File.AppendAllText(AppSettings.UserSave, infoTimer);
            }
            else if (timerToggleGame == 0 && loginInfo == 1)
            {
                await DisplayAlert("Game Complete!", $"Word: {gameRandomWord}\nGuesses: {guessCounter}", "Ok");
                string infoTimer = $"\nWord: {gameRandomWord}     Time: N/A       Guesses: {guessCounter}";
                File.AppendAllText(AppSettings.UserSave, infoTimer);
            }
            else if (timerToggleGame == 1 && loginInfo == 0)
            {
                gameTimer.Stop();
                string timeTaken = $"{elapsedSeconds / 60}:{elapsedSeconds % 60:D2}";
                await DisplayAlert("Game Complete!", $"Word: {gameRandomWord}\nTime: {timeTaken}\nGuesses: {guessCounter}", "Ok");
            }
            else
            {
                await DisplayAlert("Game Complete!", $"Word: {gameRandomWord}\nGuesses: {guessCounter}", "Ok");
            }

            Navigation.PopAsync();
        }
        else if (validWordSet.Contains(enteredWord) && enteredWord.ToUpper() != gameRandomWord.ToUpper())
        {
            guessCounter++;
        }

        if (currentRow == 5) // Last row
        {
            await DisplayAlert("Good game", $"The Word was : {gameRandomWord}", "Ok");
            await Navigation.PopToRootAsync();
            return;
        }

        
        // Move to the next row
        SetRowEnabled(currentRow, false);
        currentRow++;
        SetRowEnabled(currentRow, true);
    }

    private Entry GetEntryForRow(int row, int column)
    {
        switch (row)
        {
            case 0:
                return column switch
                {
                    0 => Row0Entry1,
                    1 => Row0Entry2,
                    2 => Row0Entry3,
                    3 => Row0Entry4,
                    4 => Row0Entry5,
                    _ => null
                };
            case 1:
                return column switch
                {
                    0 => Row1Entry1,
                    1 => Row1Entry2,
                    2 => Row1Entry3,
                    3 => Row1Entry4,
                    4 => Row1Entry5,
                    _ => null
                };
            case 2:
                return column switch
                {
                    0 => Row2Entry1,
                    1 => Row2Entry2,
                    2 => Row2Entry3,
                    3 => Row2Entry4,
                    4 => Row2Entry5,
                    _ => null
                };
            case 3:
                return column switch
                {
                    0 => Row3Entry1,
                    1 => Row3Entry2,
                    2 => Row3Entry3,
                    3 => Row3Entry4,
                    4 => Row3Entry5,
                    _ => null
                };
            case 4:
                return column switch
                {
                    0 => Row4Entry1,
                    1 => Row4Entry2,
                    2 => Row4Entry3,
                    3 => Row4Entry4,
                    4 => Row4Entry5,
                    _ => null
                };
            case 5:
                return column switch
                {
                    0 => Row5Entry1,
                    1 => Row5Entry2,
                    2 => Row5Entry3,
                    3 => Row5Entry4,
                    4 => Row5Entry5,
                    _ => null
                };
            default:
                return null;
        }
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
    private async Task FlipPreviousRow()
    {
        int previousRow = currentRow;

        // Get all the entries in the previous row based on previousRow index
        Entry[] previousRowEntries = previousRow switch
        {
            0 => new[] { Row0Entry1, Row0Entry2, Row0Entry3, Row0Entry4, Row0Entry5 },
            1 => new[] { Row1Entry1, Row1Entry2, Row1Entry3, Row1Entry4, Row1Entry5 },
            2 => new[] { Row2Entry1, Row2Entry2, Row2Entry3, Row2Entry4, Row2Entry5 },
            3 => new[] { Row3Entry1, Row3Entry2, Row3Entry3, Row3Entry4, Row3Entry5 },
            4 => new[] { Row4Entry1, Row4Entry2, Row4Entry3, Row4Entry4, Row4Entry5 },
            5 => new[] { Row5Entry1, Row5Entry2, Row5Entry3, Row5Entry4, Row5Entry5 },
            _ => new Entry[] { }
        };

        // Flip each entry in the previous row
        foreach (var entry in previousRowEntries)
        {
            await FlipEntryBox(entry);
        }
    }

    private async Task FlipEntryBox(Entry entry)
    {
        // Rotate the Entry 90 degrees around the X-axis
        await entry.RotateXTo(90, 250); // Half-flip (hide the front face)

        // Complete the flip back to 0 degrees (original state)
        await entry.RotateXTo(0, 250); // Finish the flip
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

