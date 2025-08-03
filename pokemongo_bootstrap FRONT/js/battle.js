const trainers = {};
const trainer1Select = document.getElementById("trainer1-select");
const trainer2Select = document.getElementById("trainer2-select");
const startBattleBtn = document.getElementById("start-battle");
const resetBattleBtn = document.getElementById("reset-battle");
const backBtn = document.getElementById("back"); // This element doesn't exist in battle.html
const battleArea = document.getElementById("battle-area");
const battleLog = document.getElementById("battle-log");
const pokemon1Select = document.getElementById("pokemon1-select");
const pokemon2Select = document.getElementById("pokemon2-select");

let pokemon1 = null;
let pokemon2 = null;
let connection = null;

// Проверка, можно ли начать бой
function canStartBattle() {
  return (
    trainer1Select && trainer1Select.value &&
    trainer2Select && trainer2Select.value &&
    trainer1Select.value !== trainer2Select.value &&
    pokemon1Select && pokemon1Select.value &&
    pokemon2Select && pokemon2Select.value
  );
}

function updateStartButton() {
  if (startBattleBtn) startBattleBtn.disabled = !canStartBattle();
}

if (trainer1Select) trainer1Select.addEventListener("change", updateStartButton);
if (trainer2Select) trainer2Select.addEventListener("change", updateStartButton);
if (pokemon1Select) pokemon1Select.addEventListener("change", updateStartButton);
if (pokemon2Select) pokemon2Select.addEventListener("change", updateStartButton);

if (trainer1Select) trainer1Select.addEventListener("change", async () => {
  updateStartButton();
  const trainerId = trainer1Select.value;
  if (trainerId) {
    const pokemons = await fetchPokemonByTrainerId(trainerId);
    if (pokemon1Select) pokemon1Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
    pokemons.forEach(p => {
      const option = document.createElement("option");
      option.value = JSON.stringify(p); // сохраним весь объект как JSON-строку
      
      // Access nested pokemon object for name and currentHP at top level
      const pokemonName = p.pokemon?.name || "Unknown";
      const pokemonHp = p.currentHP || 0;
      
      option.textContent = `${pokemonName} (HP: ${pokemonHp})`;
      if (pokemon1Select) pokemon1Select.appendChild(option);
    });
    if (pokemon1Select) pokemon1Select.disabled = false;
      } else {
      if (pokemon1Select) pokemon1Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
      if (pokemon1Select) pokemon1Select.disabled = true;
    }
});

if (trainer2Select) trainer2Select.addEventListener("change", async () => {
  updateStartButton();
  const trainerId = trainer2Select.value;
  if (trainerId) {
    const pokemons = await fetchPokemonByTrainerId(trainerId);
    if (pokemon2Select) pokemon2Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
    pokemons.forEach(p => {
      const option = document.createElement("option");
      option.value = JSON.stringify(p);
      
      // Access nested pokemon object for name and currentHP at top level
      const pokemonName = p.pokemon?.name || "Unknown";
      const pokemonHp = p.currentHP || 0;
      
      option.textContent = `${pokemonName} (HP: ${pokemonHp})`;
      if (pokemon2Select) pokemon2Select.appendChild(option);
    });
    if (pokemon2Select) pokemon2Select.disabled = false;
      } else {
      if (pokemon2Select) pokemon2Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
      if (pokemon2Select) pokemon2Select.disabled = true;
    }
});

// Лог
function log(message) {
  const p = document.createElement("p");
  p.textContent = message;
  if (battleLog) {
    battleLog.appendChild(p);
    battleLog.scrollTop = battleLog.scrollHeight;
  }
}

// Обновление UI покемона
function updatePokemonUI(pokemon, nameId, imgId, hpBarId, hpValueId) {
  // Access nested pokemon object for name and imageUrl
  const pokemonName = pokemon.pokemon?.name || "No name";
  const imageUrl = pokemon.imageUrl || "/pokemon/images/default.png"; // Fallback to default image if imageUrl is missing
  
  const nameElement = document.getElementById(nameId);
  const imgElement = document.getElementById(imgId);
  const hpBarElement = document.getElementById(hpBarId);
  const hpValueElement = document.getElementById(hpValueId);
  
  if (nameElement) nameElement.textContent = pokemonName;
  if (imgElement) imgElement.src = `https://localhost:7019${imageUrl}`; // Set image src with base URL

  const hp = Number(pokemon.currentHP || 0);
  const maxHp = Number(pokemon.maxHp || pokemon.hp || hp);

  if (isNaN(hp) || isNaN(maxHp) || maxHp === 0) {
    if (hpBarElement) hpBarElement.style.width = "0%";
    if (hpValueElement) hpValueElement.textContent = "0";
    return;
  }

  const hpPercent = (hp / maxHp) * 100;
  if (hpBarElement) hpBarElement.style.width = hpPercent + "%";
  if (hpValueElement) hpValueElement.textContent = hp;
}

// Получение тренеров с сервера
async function fetchTrainers() {
  const response = await fetch('https://localhost:7019/api/Trainer');
  if (!response.ok) throw new Error('Failed to load trainers');
  return response.json();
}

// Получение покемонов тренера по id
async function fetchPokemonByTrainerId(trainerId) {
  const response = await fetch(`https://localhost:7019/api/Trainer/${trainerId}/TrainerPokemon`);
  if (!response.ok) throw new Error('Failed to load pokemon');

  const pokemons = await response.json();
  console.log("Raw API response:", pokemons);
  pokemons.forEach(pokemon => {
    console.log("Pokemon object:", pokemon);
    console.log("Available fields:", Object.keys(pokemon));
  });

  return pokemons;
}

// Fill trainer selects
async function startBattleSetup() {
  try {
    const trainersData = await fetchTrainers();

    trainer1Select.innerHTML = `<option value="">-- Select trainer --</option>`;
    trainer2Select.innerHTML = `<option value="">-- Select trainer --</option>`;

    trainersData.forEach(trainer => {
      trainers[trainer.id] = trainer; // Save for reference, but pokemons will be loaded separately
      const optionHTML = `<option value="${trainer.id}">${trainer.name}</option>`;
      trainer1Select.insertAdjacentHTML('beforeend', optionHTML);
      trainer2Select.insertAdjacentHTML('beforeend', optionHTML);
    });
  } catch (error) {
    console.error(error);
  }
}

async function resetBattle() {
  resetBattleBtn.disabled = true;
  backBtn.disabled = true;
      if (backBtn) backBtn.addEventListener("click", () => {
          // For example, hide battleArea and show home page
      if (battleArea) battleArea.style.display = "none";
    // ...your logic for returning to home...
    resetBattle();
  });
  if (typeof resetBattleState === "function") {
    await resetBattleState();
  }
  pokemon1 = null;
  pokemon2 = null;
      if (battleArea) battleArea.style.display = "none";
      if (battleLog) battleLog.innerHTML = ""; // Clear battle log
  const attack1Btn = document.getElementById("attack1");
  const attack2Btn = document.getElementById("attack2");
  if (attack1Btn) attack1Btn.disabled = true;
  if (attack2Btn) attack2Btn.disabled = true;
  if (pokemon1Select) pokemon1Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
  if (pokemon2Select) pokemon2Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
  if (pokemon1Select) pokemon1Select.disabled = true;
  if (pokemon2Select) pokemon2Select.disabled = true;
  if (trainer1Select) trainer1Select.value = "";
  if (trainer2Select) trainer2Select.value = "";
  updateStartButton();
  if (connection) {
    try {
      await connection.stop();
      log("SignalR connection stopped.");
    } catch (err) {
      console.error("Error stopping SignalR connection:", err);
    }
    connection = null; // Очистка соединения
  }
}
if (resetBattleBtn) resetBattleBtn.addEventListener("click", resetBattle);

// Инициализация SignalR
function setupSignalR() {
  connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7019/battleHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

  connection.on("ReceiveTurn", async (turn) => {
    // turn — объект с полями от сервера
    const { AttackerId, DefenderId, Action, BattleId, TurnNumber, Damage } = turn;

    // Если сервер не отправляет damage, можно задать фиктивное значение:
    const damage = Damage !== undefined ? Damage : 10;

    log(`Атака: ${AttackerId} наносит ${damage} урона ${DefenderId}`);

    if (DefenderId.toString() === pokemon1.id.toString()) {
      pokemon1.currentHP = Math.max(Number(pokemon1.currentHP) - damage, 0);
      updatePokemonUI(pokemon1, "name1", "img1", "hp-bar-fill1", "hp1");
    } else if (DefenderId.toString() === pokemon2.id.toString()) {
      pokemon2.currentHP = Math.max(Number(pokemon2.currentHP) - damage, 0);
      updatePokemonUI(pokemon2, "name2", "img2", "hp-bar-fill2", "hp2");
    }

    if (pokemon1.currentHP <= 0 || pokemon2.currentHP <= 0) {
      const winner = pokemon1.currentHP > 0 ? pokemon1 : pokemon2;
      const loser = pokemon1.currentHP <= 0 ? pokemon1 : pokemon2;

      log(`Battle ends! ${winner.id} wins`);
      alert("Битва окончена!");

      const attack1Btn = document.getElementById("attack1");
      const attack2Btn = document.getElementById("attack2");
      if (attack1Btn) attack1Btn.disabled = true;
      if (attack2Btn) attack2Btn.disabled = true;
      if (resetBattleBtn) resetBattleBtn.disabled = false;
      if (backBtn) backBtn.disabled = false;

      // Отправка результата битвы на сервер
      try {
        await fetch('https://localhost:7019/api/Battle', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            winnerId: winner.id,
            loserId: loser.id
          })
        });
        log("Результат битвы отправлен в API.");
      } catch (apiErr) {
        console.error("Ошибка отправки результата битвы в API:", apiErr);
        log("Ошибка отправки результата битвы в API.");
      }
    }
  });

  // Обработчик события закрытия соединения и попытка переподключения
  connection.onclose(async () => {
    log("SignalR connection closed. Попытка переподключения...");
    try {
      await connection.start();
      log("Переподключение успешно");
    } catch {
      log("Ошибка переподключения");
    }
  });
}
// Старт битвы — загружаем выбранных покемонов и стартуем SignalR
if (startBattleBtn) startBattleBtn.addEventListener("click", async () => {
  if (!canStartBattle()) {
    alert("Выберите двух разных тренеров и по одному покемону для каждого.");
    return;
  }

  try {
    if (!pokemon1Select.value || !pokemon2Select.value) {
      alert("Пожалуйста, выберите покемонов для обоих тренеров.");
      return;
    }

    try {
      pokemon1 = JSON.parse(pokemon1Select.value);
      pokemon2 = JSON.parse(pokemon2Select.value);
    } catch (e) {
      alert("Ошибка при распознавании выбранных покемонов.");
      console.error(e);
      return;
    }

    console.log(`Покемоны загружены: ${pokemon1.id} и ${pokemon2.id}`);

    // Добавим maxHp, если нет
    pokemon1.maxHp = pokemon1.maxHp || pokemon1.hp;
    pokemon2.maxHp = pokemon2.maxHp || pokemon2.hp;

    updatePokemonUI(pokemon1, "name1", "img1", "hp-bar-fill1", "hp1");
    updatePokemonUI(pokemon2, "name2", "img2", "hp-bar-fill2", "hp2");

    if (battleArea) battleArea.style.display = "flex";
    log(`Битва между ${pokemon1.pokemon?.name || "Unknown"} и ${pokemon2.pokemon?.name || "Unknown"} началась!`);

    if (!connection) setupSignalR();

    await connection.start();

    const attack1Btn = document.getElementById("attack1");
    const attack2Btn = document.getElementById("attack2");
    if (attack1Btn) attack1Btn.disabled = false;
    if (attack2Btn) attack2Btn.disabled = true; // Start with player 1's turn
    if (resetBattleBtn) resetBattleBtn.disabled = true;
    if (backBtn) backBtn.disabled = true;
    if (startBattleBtn) startBattleBtn.disabled = true; // Битва началась, кнопка больше не нужна

    } catch (error) {
      console.error(error);
      alert("Ошибка при старте битвы. Проверьте консоль.");
    }
});

// Атаки
const attack1Btn = document.getElementById("attack1");
const attack2Btn = document.getElementById("attack2");

if (attack1Btn) {
  attack1Btn.addEventListener("click", async () => {
    if (!pokemon1 || !pokemon2 || pokemon2.currentHP <= 0) return;
    const damage = Math.floor(Math.random() * 20) + 5;
    try {
      console.log(`Отправка атаки: ${pokemon1.id} атакует ${pokemon2.id} с уроном ${damage}`);
      await connection.invoke("SendAttack", pokemon1.id, pokemon2.id, damage);
      if (attack1Btn) attack1Btn.disabled = true;
      if (attack2Btn) attack2Btn.disabled = false; // Switch turns
    } catch (err) {
      console.error(err);
    }
  });
}

if (attack2Btn) {
  attack2Btn.addEventListener("click", async () => {
    if (!pokemon1 || !pokemon2 || pokemon1.currentHP <= 0) return;
    const damage = Math.floor(Math.random() * 20) + 5;
    try {
      console.log(`Отправка атаки: ${pokemon2.id} атакует ${pokemon1.id} с уроном ${damage}`);
      await connection.invoke("SendAttack", pokemon2.id, pokemon1.id, damage);
      if (attack2Btn) attack2Btn.disabled = true;
      if (attack1Btn) attack1Btn.disabled = false; // Switch turns
    } catch (err) {
      console.error(err);
    }
  });
}

// Загрузить тренеров при загрузке страницы
startBattleSetup();



