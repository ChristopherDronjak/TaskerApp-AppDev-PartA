using System.Collections.ObjectModel;
using TaskApp.MVVM.Models;
using PropertyChanged;
using System;

public class NewTaskViewModel
{
    public event EventHandler<TaskAddedEventArgs> TaskAdded;

    public string Task { get; set; }
    public ObservableCollection<TaskCreate> Tasks { get; set; } // holds the variables from the task class
    public ObservableCollection<CategoryCreate> Categories { get; set; } // holds the variables from the Category class
    public CategoryCreate currentCategory { get; set; } // currently selected category

    public class TaskAddedEventArgs : EventArgs // event for new task being added
    {
        public TaskCreate TaskNew { get; }

        public TaskAddedEventArgs(TaskCreate taskNew) 
        {
            TaskNew = taskNew;
        }
    }

    public void AddTask() // function for adding new tasks to a selected category
    {
        if (currentCategory != null)
        {
            var newTask = new TaskCreate { nameTask = Task, finished = false, idCat = currentCategory.id };
            currentCategory.taskAdd(newTask);
            TaskAdded?.Invoke(this, new TaskAddedEventArgs(newTask));
            Task = string.Empty;
        }
    }
}
