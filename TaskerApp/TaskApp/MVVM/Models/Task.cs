using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyChanged;
using System.Linq;
using System.Text;
using System;

namespace TaskApp.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class TaskCreate
    {
        public string colourTask { get; set; } // for the colour of the task
        public string nameTask { get; set; } // for the name of tasks
        public bool finished { get; set; } // for finished tasks
        public int idCat { get; set; } // for the id of associated category
    }
}
