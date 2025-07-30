import {
  getAllPokemons,
  getPokemonById,
  getPokemonByName,
} from "./pokemon.js";

async function getPokemon() {
  try {
    const pokemons = await getAllPokemons();
    console.log("All Pokemons fetched successfully:", pokemons);
    printPokemon(pokemons);  
  } catch (error) {
    console.error("Error in fetching all Pokemons:", error);
  }
}

async function printPokemon(pokemons) {
  const cards = document.querySelector(".cards");
  pokemons.forEach((p) => {
    const cardWrapper = document.createElement("div");
    cardWrapper.className =
      "col-12 col-md-6 col-lg-4 col-xl-3 d-flex justify-content-center ";

    cardWrapper.innerHTML = ` 
      <div class="card">
        <div class="image-wrapper">
     
        </div>
        <div class="card-body">
          <ul class="list-group list-group-flush desc ulList">
            <li class="list-group-item" style="height: 62px;">
              <b>Title:</b> ${p.name}
            </li>
          </ul>
        </div>
      </div>`;

    cards.appendChild(cardWrapper);
  });
}

getPokemon();
