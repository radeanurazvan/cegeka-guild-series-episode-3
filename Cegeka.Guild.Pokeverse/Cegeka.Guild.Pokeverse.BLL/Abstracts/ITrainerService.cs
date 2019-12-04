using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.BLL.Models;

namespace Cegeka.Guild.Pokeverse.BLL.Abstracts
{
    public interface ITrainerService
    {
        IEnumerable<TrainerModel> GetAll();

        void Register(string name);
    }
}