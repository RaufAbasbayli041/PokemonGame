const trainerSelect = document.getElementById("trainer-select");
const pokemonSelect = document.getElementById("pokemon-select");
const startBtn = document.getElementById("start-wild-battle");
const resetBtn = document.getElementById("reset-wild-battle");
const backBtn = document.getElementById("back-wild");
const wildName = document.getElementById("wild-name");
const wildImg = document.getElementById("wild-img");
const wildHp = document.getElementById("wild-hp");
const wildHpBar = document.getElementById("wild-hp-bar-fill");
const wildBattleArea = document.getElementById("wild-battle-area");
const yourName = document.getElementById("your-name");
const yourImg = document.getElementById("your-img");
const yourHp = document.getElementById("your-hp");
const yourHpBar = document.getElementById("your-hp-bar-fill");
const wildNameBattle = document.getElementById("wild-name-battle");
const wildImgBattle = document.getElementById("wild-img-battle");
const wildHpBattle = document.getElementById("wild-hp-battle");
const wildHpBarBattle = document.getElementById("wild-hp-bar-fill-battle");
const yourAttackBtn = document.getElementById("your-attack");
const battleLog = document.getElementById("wild-battle-log");
const wildPokemonSelect = document.getElementById("wild-pokemon-select");
const rBtn = document.getElementById("r");

let yourPokemon = null;
let wildPokemon = null;

function log(message) {
  const p = document.createElement("p");
  p.textContent = message;
  battleLog.appendChild(p);
  battleLog.scrollTop = battleLog.scrollHeight;
}

// Fetch trainers
async function fetchTrainers() {
  const response = await fetch('https://localhost:7019/api/Trainer');
  if (!response.ok) throw new Error('Failed to load trainers');
  return response.json();
}

// Fetch pokemons by trainer
async function fetchPokemonByTrainerId(trainerId) {
  const response = await fetch(`https://localhost:7019/api/trainer/${trainerId}/trainerPokemon`);
  if (!response.ok) throw new Error('Failed to load pokemon');
  return response.json();
}

 

// Fetch all wild pokemons
async function fetchAllWildPokemons() {
  const response = await fetch('https://localhost:7019/api/WildPokemon');
  if (!response.ok) throw new Error('Failed to load wild pokemons');
  return response.json();
}

// Update UI
function updatePokemonUI(pokemon, nameEl, imgEl, hpBarEl, hpValueEl) {
  if (!nameEl || !imgEl || !hpBarEl || !hpValueEl) return;

  // Берём все данные из p.pokemon если есть, иначе из p
  const base = pokemon.pokemon ? pokemon.pokemon : pokemon;
  const name = base.name || "Unknown";
  const hp = Number(pokemon.currentHP ?? base.hp ?? 0);
  const maxHp = Number(pokemon.maxHp ?? base.hp ?? hp);

  nameEl.textContent = name;
  imgEl.src = base.imageUrl
    ? (base.imageUrl.startsWith("http") ? base.imageUrl : `https://localhost:7019${base.imageUrl}`)
    : "/pokemon/images/default.png";
  const percent = maxHp ? (hp / maxHp) * 100 : 0;
  hpBarEl.style.width = percent + "%";
  hpValueEl.textContent = hp;
}

// Setup trainer and pokemon selects
async function setupWildBattle() {
  const trainers = await fetchTrainers();
  trainerSelect.innerHTML = `<option value="">-- Select trainer --</option>`;
  trainers.forEach(trainer => {
    const option = document.createElement("option");
    option.value = trainer.id;
    option.textContent = trainer.name;
    trainerSelect.appendChild(option);
  });
  pokemonSelect.innerHTML = `<option value="">-- Select pokemon --</option>`;
  pokemonSelect.disabled = true;

  // Заполнить wild pokemon select
  const wildPokemons = await fetchAllWildPokemons();
  wildPokemonSelect.innerHTML = `<option value="">-- Select wild pokemon --</option>`;
  wildPokemons.forEach(p => {
    // Берём имя и HP из p.pokemon если есть, иначе из p
    const base = p.pokemon ? p.pokemon : p;
    const name = base.name || "Unknown";
    const hp = base.hp || "??";
    const option = document.createElement("option");
    option.value = JSON.stringify(p);
    option.textContent = `${name} (HP: ${hp})`;
    wildPokemonSelect.appendChild(option);
  });
}

trainerSelect.addEventListener("change", async () => {
  const trainerId = trainerSelect.value;
  if (trainerId) {
    const pokemons = await fetchPokemonByTrainerId(trainerId);
    pokemonSelect.innerHTML = `<option value="">-- Select pokemon --</option>`;
    pokemons.forEach(p => {
      const option = document.createElement("option");
      option.value = JSON.stringify(p);
      option.textContent = `${p.pokemon?.name || p.name || "Unknown"} (HP: ${p.currentHP || p.hp || 0})`;
      pokemonSelect.appendChild(option);
    });
    pokemonSelect.disabled = false;
  } else {
    pokemonSelect.innerHTML = `<option value="">-- Select pokemon --</option>`;
    pokemonSelect.disabled = true;
  }
});

resetBtn.addEventListener("click", () => {
  wildBattleArea.style.display = "none";
  resetBtn.disabled = true;
  startBtn.disabled = false;
  battleLog.innerHTML = "";
  setupWildBattle();
});

backBtn.addEventListener("click", () => {
  window.location.href = "index.html";
});

startBtn.addEventListener("click", async () => {
  if (!trainerSelect.value || !pokemonSelect.value || !wildPokemonSelect.value) {
    alert("Please select a trainer, your pokemon, and a wild pokemon.");
    return;
  }
  yourPokemon = JSON.parse(pokemonSelect.value);
  wildPokemon = JSON.parse(wildPokemonSelect.value);

  // Set maxHp if not present
  yourPokemon.maxHp = yourPokemon.maxHp || yourPokemon.hp;
  yourPokemon.currentHP = yourPokemon.currentHP || yourPokemon.hp;
  wildPokemon.maxHp = wildPokemon.maxHp || wildPokemon.hp;
  wildPokemon.currentHP = wildPokemon.currentHP || wildPokemon.hp;

  // Show wild pokemon preview
  updatePokemonUI(wildPokemon, wildName, wildImg, wildHpBar, wildHp);

  // Setup battle area
  wildBattleArea.style.display = "flex";
  updatePokemonUI(yourPokemon, yourName, yourImg, yourHpBar, yourHp);
  updatePokemonUI(wildPokemon, wildNameBattle, wildImgBattle, wildHpBarBattle, wildHpBattle);

  yourAttackBtn.disabled = false;
  resetBtn.disabled = true;
  startBtn.disabled = true;
  battleLog.innerHTML = "";
  log(`Battle started! ${yourPokemon.name} vs ${wildPokemon.name}`);
});

yourAttackBtn.addEventListener("click", async () => {
  if (!yourPokemon || !wildPokemon || wildPokemon.currentHP <= 0 || yourPokemon.currentHP <= 0) return;
  // Your attack
  const damage = Math.floor(Math.random() * 20) + 5;
  wildPokemon.currentHP = Math.max(wildPokemon.currentHP - damage, 0);
  updatePokemonUI(wildPokemon, wildNameBattle, wildImgBattle, wildHpBarBattle, wildHpBattle);
  log(`${yourPokemon.name} hits wild ${wildPokemon.name} for ${damage} damage!`);

  if (wildPokemon.currentHP <= 0) {
    log(`You win!`);
    yourAttackBtn.disabled = true;
    resetBtn.disabled = false;
    // Send result to API
    try {
      await fetch('https://localhost:7019/api/WildBattle', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          winnerId: (yourPokemon.pokemon ? yourPokemon.pokemon.id : yourPokemon.id),
          loserId: (wildPokemon.pokemon ? wildPokemon.pokemon.id : wildPokemon.id)
        })
      });
      log("Battle result sent to API.");
    } catch (err) {
      log("Failed to send result to API.");
    }
    return;
  }

  // Wild attack
  setTimeout(() => {
    if (wildPokemon.currentHP > 0 && yourPokemon.currentHP > 0) {
      const wildDamage = Math.floor(Math.random() * 20) + 5;
      yourPokemon.currentHP = Math.max(yourPokemon.currentHP - wildDamage, 0);
      updatePokemonUI(yourPokemon, yourName, yourImg, yourHpBar, yourHp);
      log(`Wild ${wildPokemon.name} hits ${yourPokemon.name} for ${wildDamage} damage!`);
      if (yourPokemon.currentHP <= 0) {
        log(`You lose!`);
        yourAttackBtn.disabled = true;
        resetBtn.disabled = false;
        // Send result to API
        fetch('https://localhost:7019/api/WildBattle', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            winnerId: wildPokemon.id,
            loserId: yourPokemon.id
          })
        }).then(() => log("Battle result sent to API."))
          .catch(() => log("Failed to send result to API."));
      }
    }
  }, 800);
});

if (rBtn) {
  rBtn.addEventListener("click", () => {
    // Тук добавете желаното действие при натискане на бутона "r"
    log("Сигнал R е активиран!");
    // Може да добавите и друга логика тук
  });
}

// Initial setup
setupWildBattle();