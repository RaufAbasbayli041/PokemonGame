using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PokemonGame.Contracts.Dtos;

namespace PokemonGame.Application.Validators
{
	public class WildPokemonValidator :AbstractValidator<WildPokemonDto>
	{
		public WildPokemonValidator()
		{
			RuleFor(x => x.PokemonId)
				.NotEmpty().WithMessage("PokemonId is required.")
				.GreaterThan(0).WithMessage("PokemonId must be greater than 0.");
			RuleFor(x => x.Level)
				.NotEmpty().WithMessage("Level is required.")
				.GreaterThan(0).WithMessage("Level must be greater than 0.");
			RuleFor(x => x.CurrentHP)
				.NotEmpty().WithMessage("CurrentHP is required.")
				.GreaterThanOrEqualTo(0).WithMessage("CurrentHP must be greater than or equal to 0.");


		}
	}
}
