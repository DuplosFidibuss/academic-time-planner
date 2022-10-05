﻿namespace AcademicTimePlanner.DataMapping.Toggl
{
    public class TogglTask
    {
        private Guid _id;
        private int _togglId;
        private string _name;

        public TogglTask(int togglId, string name)
        {
            _id = Guid.NewGuid();
            _togglId = togglId;
            Name = name;
        }

        public Guid Id { 
            get { return _id; } 
        }

        public int TogglId { 
            get { return _togglId; } 
        }

        public string Name { 
            get { return _name; } 
            set { _name = value; } 
        }

        //TODO get all entry sums belonging to this task. List seems pointless as they are irrelevant after reading...
    }
}