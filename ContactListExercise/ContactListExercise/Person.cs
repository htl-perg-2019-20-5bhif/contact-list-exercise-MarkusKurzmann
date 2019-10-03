using System;
using System.ComponentModel.DataAnnotations;

namespace ContactListExercise
{
    public class Person
    {
        // Pefer lowercase data types for basic data types (e.g. int, string instead of Int32, String)
        public Int32 ID { get; set; }

        public String firstName { get; set; }

        public String lastName { get; set; }

        public String email { get; set; }
    }
}
