using System;

namespace ZeitPlan.Model
{
    public class Calendar
    {
        public string Id { get; set; }
        
    }
    
    public class JobDefinition
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Calendar { get; set; }
    }

    public class JobTrigger
    {
        
    }

    public class JobActionApp
    {
        public string Application { get; set; }
        public string Arguments { get; set; }
    }
}

