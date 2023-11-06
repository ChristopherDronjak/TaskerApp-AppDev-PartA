using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TaskApp.MVVM.Models;
using PropertyChanged;
using System.Linq;

namespace TaskApp.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public ObservableCollection<CategoryCreate> Categories { get; set; } // holds the variables for the categorys
        public ObservableCollection<TaskCreate> Tasks { get; set; } // holds the variables for the tasks

        public MainViewModel() // load initial data
        {
            dataLoad();
            Tasks.CollectionChanged += Tasks_CollectionChanged;

            
            foreach (var category in Categories)
            {
                category.Tasks = new ObservableCollection<TaskCreate>(Tasks.Where(task => task.idCat == category.id));
                category.updatetasksTotal();
            }
        }

        private void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs y) // update information if a new task is added
        {
            foreach (TaskCreate task in y.NewItems)
            {
                CategoryCreate category = Categories.FirstOrDefault(c => c.id == task.idCat);
                if (category != null)
                {
                    category.Tasks.Add(task);
                    category.tasksUncompleted = category.Tasks.Count(t => !t.finished);
                    category.percent = (float)category.Tasks.Count(t => t.finished) / category.Tasks.Count;
                    category.updatetasksTotal();
                }
            }
        }

        private void dataLoad() // data that will be loaded as categorys and associated tasks when the app is first run
        {
            Categories = new ObservableCollection<CategoryCreate>()
            {
                new CategoryCreate
                {
                    id = 1,
                    catName = "Retail Staff",
                    colour = "#F1F80A"
                },

                new CategoryCreate
                {
                    id = 2,
                    catName = "Bakers",
                    colour = "#F80AA9"
                },

                new CategoryCreate
                {
                    id = 3,
                    catName = "Stock Management",
                    colour = "#BC0BC4"
                },
            };

            Tasks = new ObservableCollection<TaskCreate>()
            {
            new TaskCreate
            {
                nameTask = "Package Products on or in Sheleves/Bags",
                finished = false,
                idCat = 1,
            },
            new TaskCreate
            {
                nameTask = "Clean store to prepare for customers",
                finished = false,
                idCat = 1,
            },
            new TaskCreate
            {
                nameTask = "Bake white bread",
                finished = false,
                idCat = 2,
            },
            new TaskCreate
            {
                nameTask = "Bake Sourdough",
                finished = false,
                idCat = 2,
            },
            new TaskCreate
            {
                nameTask = "Check the stock levels",
                finished = false,
                idCat = 3,
            },
            new TaskCreate
            {
                nameTask = "Purchase more flour for stock",
                finished = false,
                idCat = 3,
            }
            };
            updateItems();
        }

        public void updateItems() // function created for various methods across the project that will update something on the UI that needs changing
                                  // as the user interacts with the app
        {
            foreach (var c in Categories)
            {
                var tasks = from t in Tasks
                            where t.idCat == c.id
                            select t;

                var acomplished = from t in tasks
                                where t.finished == true
                                select t;

                var numAcomplished = from t in tasks
                                  where t.finished == false
                                  select t;

                c.tasksUncompleted = numAcomplished.Count();
                c.percent = (float)acomplished.Count() / (float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                var catColor =
                    (
                        from c in Categories
                        where c.id == t.idCat
                        select c.colour
                    ).FirstOrDefault();
                t.colourTask = catColor;
            }
        }
    }
}