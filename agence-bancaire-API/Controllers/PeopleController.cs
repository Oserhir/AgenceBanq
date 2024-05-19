using agence_bancaire_API.DTO;
using agence_bancaire_Business_Layer;
using agence_bancaire_Business_Layer.Enums;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace agence_bancaire_API.Controllers
{
    // https://localhost:7252/api/People
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> createPerson([FromBody] CreatePersonRequestDTO request)
        {
            clsPerson _Person = new clsPerson();

            _Person.firstName = request.firstName;
            _Person.lastName = request.lastName;
            _Person.PhoneNumber = request.PhoneNumber;
            _Person.DateOfBirth = request.DateOfBirth;
            _Person.Address = request.Address;
            _Person.Email = request.Email;
            _Person.CIN = request.CIN;

            try
            {
                if(_Person.Save())
                {
                    return CreatedAtAction(nameof(createPerson), new { id = _Person.PersonID }, _Person);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create Person. Internal server error occurred.");
                }
               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllPeople()
        {
            DataTable dt = clsPerson.GetAllPeople();

            // Map DataTable to DTO

            var list = new List<PersonDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new PersonDTO(row.Field<int>("PersonID"), row["firstName"].ToString().Trim(), row["lastName"].ToString().Trim(), Convert.ToDateTime(row["DateOfBirth"])
                    , row["PhoneNumber"].ToString().Trim(), row["Email"].ToString().Trim(), row["Address"].ToString().Trim(), row["CIN"].ToString().Trim()
                    ));
            }

            return Ok(list);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetPersonID([FromRoute] int id)
        {
            clsPerson person = clsPerson.Find(id);

            if(person is null) { return NotFound(); }

            var newPerson = new PersonDTO(person.PersonID, person.firstName.Trim(), person.lastName.Trim(), person.DateOfBirth, person.PhoneNumber.Trim()
                , person.Email.Trim(), person.Address.Trim(), person.CIN.Trim());

            return Ok(newPerson);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePerson([FromRoute] int id, UpdatePersonRequestDTO request)
        {
            clsPerson person = clsPerson.Find(id);

            if (person is null) { return NotFound(); }

            person.firstName = request.firstName; 
            person.lastName = request.lastName;
            person.PhoneNumber = request.PhoneNumber;
            person.Address = request.Address;
            person.DateOfBirth = request.DateOfBirth;
            person.Email = request.Email;
            person.CIN = request.CIN;

            if (person.Save())
            {
                return Ok(person);
            }else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Update Person. Internal server error occurred.");
            }
           
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            clsPerson person = clsPerson.Find(id);

            if (person is null) { return NotFound(); }

            if (clsPerson.DeletePerson(id))
            {
                return Ok(person);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Update Person. Internal server error occurred.");
            }

        }

    }



}
