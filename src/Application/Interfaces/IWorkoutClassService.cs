using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWorkoutClassService
    {
        WorkoutClass Create(CreationWorkoutClassDto creationWorkoutClassDto);

        List<WorkoutClassDto> GetAllWorkoutClass();
        WorkoutClassDto GetById(int id);
        void Update(int id, CreationWorkoutClassDto creationWorkoutClassDto);
        void Delete(int id);
    }
}
