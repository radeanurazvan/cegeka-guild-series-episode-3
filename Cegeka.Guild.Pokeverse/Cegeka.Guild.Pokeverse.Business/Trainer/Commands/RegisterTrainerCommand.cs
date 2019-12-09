using MediatR;

namespace Cegeka.Guild.Pokeverse.Business.Trainer.Commands
{
    public sealed class RegisterTrainerCommand : IRequest
    {
        public RegisterTrainerCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}