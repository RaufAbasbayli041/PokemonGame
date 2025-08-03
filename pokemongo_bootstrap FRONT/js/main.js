import {
  getAllPokemons,
  getPokemonById,
  getPokemonByName,
} from "./pokemon.js";

import {
  getAllSkills,
  getSkillById,
  getskillByName,
} from "./skill.js";

import { getAllCategories, getCategoryById, getCategoryByName } from "./category.js";

async function getPokemon() {
  try {
    const [pokemons, skills, categories] = await Promise.all([
      getAllPokemons(),
      getAllSkills(),
      getAllCategories(),
    ]);
    console.log("Pokemon image URLs:", pokemons.map(p => ({ id: p.id, name: p.name, imageUrl: p.imageUrl })));
    console.log("All Skills fetched successfully:", skills);
    console.log("All Categories fetched successfully:", categories);
    printPokemon(pokemons, skills, categories);
  } catch (error) {
    console.error("Error in fetching all Pokemons:", error);
  }
}

async function printPokemon(pokemons, skills, categories) {
  const cards = document.querySelector(".cards");
  if (!cards) {
    console.error("Element with class 'cards' not found in the DOM");
    return;
  }

  pokemons.forEach((p) => {
    console.log("Pokemon object:", p);
    console.log("Available fields:", Object.keys(p));

         // Get image from wwwroot folder
     let imageUrl = "";
     
     console.log("Pokemon:", p.name, "ID:", p.id, "ImageUrl:", p.imageUrl);
     
     if (p.imageUrl && p.imageUrl !== "null" && p.imageUrl !== "undefined" && p.imageUrl !== "string" && p.imageUrl.trim() !== "") {
       // Check if it's already a full URL
       if (p.imageUrl.startsWith("http")) {
         imageUrl = p.imageUrl;
         console.log("Using full URL:", imageUrl);
       } else {
         // Use image from pokemon/images folder
         imageUrl = `https://localhost:7019/pokemon/images/${p.imageUrl}`;
         console.log("Image URL from pokemon/images:", imageUrl);
       }
     } else {
       // Use fallback image
       imageUrl = "./images/pokeball.png";
       console.log("Using fallback image for:", p.name);
     }

    const cardWrapper = document.createElement("div");
    cardWrapper.className = "col-md-4";

    const skillIds = p.skillIds || p.skillId || p.skills || p.skill || [];
    const categoryIds = p.categoriesIds || p.categoryId || p.categories || p.category || [];

    console.log("Skill IDs found:", skillIds);
    console.log("Category IDs found:", categoryIds);

    const skillNames = Array.isArray(skillIds)
      ? skillIds
          .map((id) => {
            const skill = skills.find((s) => s.id === id);
            return skill?.name || `Unknown (ID: ${id})`;
          })
          .join(", ")
      : "No skills";

    const categoryNames = Array.isArray(categoryIds)
      ? categoryIds
          .map((id) => {
            const category = categories.find((c) => c.id === id);
            return category?.name || `Unknown (ID: ${id})`;
          })
          .join(", ")
      : "No categories";

    cardWrapper.innerHTML = `
      <div class="pokeball-wrapper">
        <img src="./images/pokeball.png" alt="Pokeball" />
      </div>
      <div class="enhanced">
        <h2>${p.name}</h2>
                 <img 
  class="pokemon-card-img"
  src="${imageUrl}" 
  alt="${p.name}" 
  style="display:block;margin:0 auto 12px auto;max-width:120px;max-height:120px;border-radius:8px;background:#f3f3f3;"
  onerror="this.onerror=null; this.src='./images/pokeball.png';" 
/>
        <p>Health Point: ${p.hp}</p>
        <p>Level: ${p.level}</p>
        <p>Category: ${categoryNames}</p>
        <p>Skills: ${skillNames}</p>
        <p style="margin: 0px 0 16px;"><a class="btn btn-default" href="pokemon-details.html?id=${p.id}" role="button">View details &raquo;</a></p>
      </div>
    `;

    cards.appendChild(cardWrapper);
  });
}

getPokemon();