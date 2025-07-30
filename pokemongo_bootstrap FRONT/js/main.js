import {
  getAllPokemons,
  getPokemonById,
  getPokemonByName,
} from "./pokemon.js";

import {
  getAllSkills,
  getSkillById,
  getskillByName
} from "./skill.js"
 
import { getAllCategories, getCategoryById, getCategoryByName } from "./category.js";

async function getPokemon() {
  try {
     const [pokemons, skills,categories] = await Promise.all([
      getAllPokemons(),
      getAllSkills(),
      getAllCategories()
    ]);
    console.log("All Pokemons fetched successfully:", pokemons);
    printPokemon(pokemons,skills,categories);  
  } catch (error) {
    console.error("Error in fetching all Pokemons:", error);
  }
}

async function printPokemon(pokemons,skills,categories) {
  const cards = document.querySelector(".cards");
  pokemons.forEach((p) => {
    const cardWrapper = document.createElement("div");
     const skillNames = p.skillIds.map(
      (id) => skills.find((s) => s.id === id)?.name || "Unknown"
    ).join(", ");
    const categoryNames = p.categoriesIds.map(
      (id) => categories.find((c) => c.id === id)?.name || "Unknown"
    ).join(", ");

    cardWrapper.innerHTML = ` 
     <div class="col-md-4">
              <div class="pokeball-wrapper">
                <img src="./images/pokeball.png" />
              </div>
              <div class="enhanced">
                <h2>${p.name}</h2>
                <img class="pokemon small" src="${p.imageUrl}" />
                <p>Health Point:${p.hp}</p>
                <p>Level:${p.level}</p>
              <p> Category: ${categoryNames} </p>
             <p>Skills: ${skillNames}</p>
                <p><a class="btn btn-default" href="#" role="button">View details &raquo;</a></p>
              </div>
            </div>`;

    cards.appendChild(cardWrapper);
  });
}

getPokemon();
