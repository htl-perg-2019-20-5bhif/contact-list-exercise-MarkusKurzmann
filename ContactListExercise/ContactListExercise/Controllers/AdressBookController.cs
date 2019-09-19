using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactListExercise.Controllers
{
    [ApiController]
    [Route("contacts/")]
    public class AdressBookController : ControllerBase
    {
        private static readonly List<Person> items =
            new List<Person> {
                new Person{ ID = 0, firstName = "Markus", lastName = "Kurzmann", email = "kurzmann1.mk@gmail.com" },
                new Person{ ID = 1, firstName = "Thomas", lastName = "Brych", email = "thomasbrych@outlook.at" },
                new Person{ ID = 2, firstName = "Michael", lastName = "Hitzker", email = "michaelhitzker@gmail.com" },
                new Person{ ID = 3, firstName = "Rene", lastName = "Stadler", email = "stadler.rene@gmx.at" },
                new Person{ ID = 4, firstName = "Markus", lastName = "Kern", email = "samplemail@yahoo.com" },

            };
        private static int lastID = 5;

        [HttpGet]
        public IActionResult GetAllItems() => Ok(items);

        [HttpGet]
        [Route("{index}", Name = "GetSpecificItemIndex")]
        public IActionResult GetItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                return Ok(items[index]);
            }

            return BadRequest("Invalid index");
        }

        [HttpGet]
        [Route("findByName", Name = "GetSpecificItemName")]
        public IActionResult GetItem([FromQuery] String nameFilter)
        {
            //check for null or empty nameFilter
            if(nameFilter != null && nameFilter.Length > 0)
            {
                //create an empty list for potential objects
                List<Person> peopleToReturn = new List<Person>();
                for (int i = 0; i < items.Count; i++)
                {
                    //compare the firstname and the lastname with the nameFilter (obviously the name should start with the nameFilter so i chose to use the method 'StartsWith()')
                    if (items[i].firstName.ToLower().StartsWith(nameFilter.ToLower()) || items[i].lastName.ToLower().StartsWith(nameFilter.ToLower()))
                    {
                        peopleToReturn.Add(items[i]);
                    }
                }

                //check whether there were occurances or not
                if (peopleToReturn.Count > 0)
                {
                    //return the occurances with the status-code 200
                    return Ok(peopleToReturn);
                }
            }
            //return the error-message with the status-code 400
            return BadRequest("Invalid or missing name");
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] Person newItem)
        {
            //check whether the email-field is null or completely empty
            if(newItem.email == null || newItem.email.Length <= 0)
            {
                //return status-code 400 with error-message
                return BadRequest("Invalid input (e.g. required field missing or empty)");
            }

            //set the ID of the new person
            newItem.ID = lastID;
            items.Add(newItem);
            lastID++;

            //return the just created entity
            return CreatedAtRoute("GetSpecificItemIndex", new { index = items.IndexOf(newItem) }, newItem);
        }

        [HttpDelete]
        [Route("{personID}")]
        public IActionResult DeleteItem(int personID)
        {

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == personID)
                {
                    items.RemoveAt(personID);
                    return NoContent();
                }
            }
            return NotFound("Person not found");

            //I have not implemented the status-code 400 with the invalid id as i had no clue whats the difference between 'not found' and 'invalid id'
        }
    }
}
