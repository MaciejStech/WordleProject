namespace WordleProject;

public partial class GamePage : ContentPage
{
    string gameRandomWord;
    HashSet<string> validWordSet;

    public GamePage(string randomWord)
	{
		InitializeComponent();
        gameRandomWord = randomWord.ToUpper();
        LoadWordAsync();
        
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
        }
    }

    private async void EnterButton_Clicked(object sender, EventArgs e)
    {
        
        string enteredWord = $"{Row0Entry1.Text}{Row0Entry2.Text}{Row0Entry3.Text}{Row0Entry4.Text}{Row0Entry5.Text}".ToUpper();

        //Checking if user entered 5 letters
        if(enteredWord.Length != 5 || enteredWord.Any(char.IsWhiteSpace))
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
        else
        {
            DisplayAlert("oops", "try again", "ok");
        }
    }
}