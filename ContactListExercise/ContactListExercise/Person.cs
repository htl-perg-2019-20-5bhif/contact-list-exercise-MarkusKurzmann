using System;
using System.ComponentModel.DataAnnotations;

namespace ContactListExercise
{
    public class Person
    {
        public Int32 ID { get; set; }

        public String firstName { get; set; }

        public String lastName { get; set; }

        public String email { get; set; }
    }
}
