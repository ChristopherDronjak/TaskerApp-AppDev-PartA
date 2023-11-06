using TaskApp.MVVM.ViewModels;

namespace TaskApp.MVVM.Views;

public partial class MainView : ContentPage
{
    private MainViewModel viewmodel_main = new MainViewModel();
    public MainView()
    {
        InitializeComponent();
        BindingContext = viewmodel_main;

    }

    private void checkbox_check(object sender, CheckedChangedEventArgs y) // calls function to update items on screen when checkbox is checked
    {
        viewmodel_main.updateItems();
    }

    private void addingnewtasks_btn_Clicked(object sender, EventArgs y) // function to take user to 2nd page
    {
        var newView = new NewTaskView()
        {
            BindingContext = new NewTaskViewModel
            {
                Tasks = viewmodel_main.Tasks,
                Categories = viewmodel_main.Categories
            }
        };

        if (newView != null)
        {
            Navigation.PushAsync(newView);
        }
        else // if loading page for any reason doesnt work this will be displayed
        {
            DisplayAlert("Something went wrong", "Attempting Load", "Try again");
        }


    }
}