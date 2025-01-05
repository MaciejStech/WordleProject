namespace WordleProject;

public partial class GamePage : ContentPage
{
	public GamePage(string randomWord)
	{
		InitializeComponent();
       string gameRandomWord = randomWord;
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
            else if (currentEntry == Row0Entry5)
                Row0Entry6.Focus();
        }
    }
}