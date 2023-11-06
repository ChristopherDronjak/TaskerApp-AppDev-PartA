using TaskApp.MVVM.ViewModels;
using TaskApp.MVVM.Models;
using System.Linq;
using System;


namespace TaskApp.MVVM.Views
{
    public partial class NewTaskView : ContentPage
    {
        public NewTaskView()
        {
            InitializeComponent();
        }

        private async void addCategory_btn_Clicked(object sender, EventArgs y) // function to add a new category and will select a random new colour
                                                                               // for the frame of the category on the main page
        {
            var vm = BindingContext as NewTaskViewModel;

            string category = await DisplayPromptAsync("Create New Category", "Category Name: ", maxLength: 30, keyboard: Keyboard.Text);

            if (!string.IsNullOrEmpty(category))
            {
                var randomshuffle = new Random(); 

                var categoryNew = new CategoryCreate // this function will create the new category and assign a random colour to it to be used
                                                     // as its frame on the main page and the colour of the checkbox tasks associated with it
                {
                    id = vm.Categories.Max(x => x.id) + 1,
                    colour = Color.FromRgb(randomshuffle.Next(0, 255), randomshuffle.Next(0, 255), randomshuffle.Next(0, 255)).ToHex(), 
                    catName = category
                };
                vm.Categories.Add(categoryNew);

                
                var viewModelMain = App.Current.MainPage.BindingContext as MainViewModel;
                viewModelMain.updateItems();
            }
        }

        private async void addTask_btn_Clicked(object sender, EventArgs y) // function to add a new task associated with a selected category
        {
            var vm = BindingContext as NewTaskViewModel;
            var CurrentCategory = vm.Categories.Where(x => x.currentSelect).FirstOrDefault();

            if (CurrentCategory != null) // user must select a category with the radio buttons then function will run
            {
                var task = new TaskCreate
                {
                    nameTask = vm.Task,
                    idCat = CurrentCategory.id,
                    colourTask = CurrentCategory.colour
                };

                vm.Tasks.Add(task);


                var viewModelMain = App.Current.MainPage.BindingContext as MainViewModel;
                viewModelMain.updateItems();

                await Navigation.PopAsync();
            }
            else // if user does not select a category then this error will appear instead of function running
            {
                await DisplayAlert("Error", "You cannot not select a category", "Try again");
            }
        }
    }
}
