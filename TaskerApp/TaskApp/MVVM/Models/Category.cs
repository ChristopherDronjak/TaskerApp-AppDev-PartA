using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using PropertyChanged;

namespace TaskApp.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class CategoryCreate
    {
        public string catName { get; set; } //for category name
        public int tasksUncompleted { get; set; } // for uncompleted tasks
        public float percent { get; set; } // for percentage
        public bool currentSelect { get; set; } // for currently selected category
        public string colour { get; set; } // for category colour
        public int id { get; set; } // for category ID

        public ObservableCollection<TaskCreate> Tasks { get; set; } // holds the variables from the task class

        private int tasksTotal;

        public int tasks_total // function for number of tasks total for a given category
        {
            get { return tasksTotal; }
            set
            {
                if (tasksTotal != value)
                {
                    tasksTotal = value;
                }
            }
        }

        public CategoryCreate() 
        {         
            Tasks = new ObservableCollection<TaskCreate>();
            updatetasksTotal();
        }

        public void taskAdd(TaskCreate task) // function to add tasks to categorys
        {
            Tasks.Add(task);
            updatetasksTotal();
        }

        public void updatetasksTotal() // function to increase the total number of tasks for a category
        {
            tasks_total = Tasks.Count;
        }
    }
}
