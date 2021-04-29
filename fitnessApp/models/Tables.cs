using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace fitnessApp.models
{
    public class Tables
    {
       public class Point
        {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int RunID { get; set; }
        public double longatide { get; set; }
        public double latitude { get; set; }
        public DateTime timestamp { get; set; }
        public double speed { get; set; }
        }

       public class Person
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }
            public string first_name { get; set; }
            public string second_name { get; set; }
            public int age { get; set; }

            public double weight { get; set; }
            public double height { get; set; }
            public double total_dis { get; set; }

            public double today_dis { get; set; }

            public double calories { get; set; }
            public DateTime currentDay { get; set; }


        }

    }
    
}
