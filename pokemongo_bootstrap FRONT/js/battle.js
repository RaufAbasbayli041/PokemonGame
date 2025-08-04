// Global variables for DOM elements and state
const trainers = {};
const trainer1Select = document.getElementById("trainer1-select");
const trainer2Select = document.getElementById("trainer2-select");
const startBattleBtn = document.getElementById("start-battle");
const resetBattleBtn = document.getElementById("reset-battle");
const battleArea = document.getElementById("battle-area");
const battleLog = document.getElementById("battle-log");
const pokemon1Select = document.getElementById("pokemon1-select");
const pokemon2Select = document.getElementById("pokemon2-select");

let pokemon1 = null;
let pokemon2 = null;
let connection = null;

// Check if all conditions are met to start a battle
function canStartBattle() {
  return (
    trainer1Select && trainer1Select.value &&
    trainer2Select && trainer2Select.value &&
    trainer1Select.value !== trainer2Select.value &&
    pokemon1Select && pokemon1Select.value &&
    pokemon2Select && pokemon2Select.value
  );
}

// Update start button state based on selections
function updateStartButton() {
  if (startBattleBtn) {
    startBattleBtn.disabled = !canStartBattle();
  } else {
    console.error("Start battle button not found");
  }
}

// Populate Pokémon dropdown for trainer 1
if (trainer1Select) {
  trainer1Select.addEventListener("change", async () => {
    updateStartButton();
    const trainerId = trainer1Select.value;
    if (trainerId) {
      try {
        const pokemons = await fetchPokemonByTrainerId(trainerId);
        if (pokemon1Select) {
          pokemon1Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
          pokemons.forEach(p => {
            const option = document.createElement("option");
            option.value = JSON.stringify(p);
            const pokemonName = p.pokemon?.name || p.name || "Unknown";
            const pokemonHp = p.currentHP || p.hp || 0;
            option.textContent = `${pokemonName} (HP: ${pokemonHp})`;
            pokemon1Select.appendChild(option);
          });
          pokemon1Select.disabled = false;
        }
      } catch (err) {
        console.error("Failed to load Pokémon for trainer 1:", err);
        log("Ошибка: Не удалось загрузить покемонов для тренера 1");
      }
    } else {
      if (pokemon1Select) {
        pokemon1Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
        pokemon1Select.disabled = true;
      }
    }
  });
}

// Populate Pokémon dropdown for trainer 2
if (trainer2Select) {
  trainer2Select.addEventListener("change", async () => {
    updateStartButton();
    const trainerId = trainer2Select.value;
    if (trainerId) {
      try {
        const pokemons = await fetchPokemonByTrainerId(trainerId);
        if (pokemon2Select) {
          pokemon2Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
          pokemons.forEach(p => {
            const option = document.createElement("option");
            option.value = JSON.stringify(p);
            const pokemonName = p.pokemon?.name || p.name || "Unknown";
            const pokemonHp = p.currentHP || p.hp || 0;
            option.textContent = `${pokemonName} (HP: ${pokemonHp})`;
            pokemon2Select.appendChild(option);
          });
          pokemon2Select.disabled = false;
        }
      } catch (err) {
        console.error("Failed to load Pokémon for trainer 2:", err);
        log("Ошибка: Не удалось загрузить покемонов для тренера 2");
      }
    } else {
      if (pokemon2Select) {
        pokemon2Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
        pokemon2Select.disabled = true;
      }
    }
  });
}

// Add event listeners for select changes
if (pokemon1Select) pokemon1Select.addEventListener("change", updateStartButton);
if (pokemon2Select) pokemon2Select.addEventListener("change", updateStartButton);

// Log messages to the battle log with auto-scroll
function log(message) {
  if (!battleLog) {
    console.error("Battle log element not found");
    return;
  }
  const p = document.createElement("p");
  p.textContent = message;
  const isScrolledToBottom = battleLog.scrollHeight - battleLog.scrollTop <= battleLog.clientHeight + 10;
  battleLog.appendChild(p);
  if (isScrolledToBottom) {
    battleLog.scrollTop = battleLog.scrollHeight;
  }
}

// Update Pokémon UI elements
function updatePokemonUI(pokemon, nameId, imgId, hpBarId, hpValueId) {
  if (!pokemon) {
    console.error("Pokemon object is undefined or null");
    log("Ошибка: Покемон не определён");
    return;
  }

  const pokemonName = pokemon.pokemon?.name || pokemon.name || "Unknown";
  const imageUrl = pokemon.imageUrl || "/pokemon/images/default.png";
  const hp = Number(pokemon.currentHP || pokemon.hp || 0);
  const maxHp = Number(pokemon.maxHp || pokemon.hp || 100);

  const nameElement = document.getElementById(nameId);
  const imgElement = document.getElementById(imgId);
  const hpBarElement = document.getElementById(hpBarId);
  const hpValueElement = document.getElementById(hpValueId);

  if (!nameElement || !imgElement || !hpBarElement || !hpValueElement) {
    console.error(`Missing UI elements: nameId=${nameId}, imgId=${imgId}, hpBarId=${hpBarId}, hpValueId=${hpValueId}`);
    log("Ошибка: Не удалось обновить интерфейс покемона");
    return;
  }

  if (isNaN(hp) || isNaN(maxHp) || maxHp <= 0) {
    console.error(`Invalid HP values for ${pokemonName}: hp=${hp}, maxHp=${maxHp}`);
    log(`Ошибка: Некорректные значения HP для ${pokemonName}`);
    hpBarElement.style.width = "0%";
    hpValueElement.textContent = "0";
    return;
  }

  nameElement.textContent = pokemonName;
  imgElement.src = `https://localhost:7019${imageUrl}`;
  const hpPercent = (hp / maxHp) * 100;
  hpBarElement.style.width = `${hpPercent}%`;
  hpValueElement.textContent = hp.toString();
}

// Fetch trainers from server
async function fetchTrainers() {
  const response = await fetch('https://localhost:7019/api/Trainer');
  if (!response.ok) {
    console.error(`Failed to load trainers: ${response.status}`);
    throw new Error('Failed to load trainers');
  }
  return response.json();
}

// Fetch Pokémon by trainer ID
async function fetchPokemonByTrainerId(trainerId) {
  const response = await fetch(`https://localhost:7019/api/Trainer/${trainerId}/TrainerPokemon`);
  if (!response.ok) {
    console.error(`Failed to load Pokémon for trainer ${trainerId}: ${response.status}`);
    throw new Error('Failed to load Pokémon');
  }
  const pokemons = await response.json();
  console.log("Raw Pokémon API response:", pokemons);
  pokemons.forEach(pokemon => {
    console.log("Pokemon object:", pokemon);
    console.log("Available fields:", Object.keys(pokemon));
  });
  return pokemons;
}

// Initialize trainer dropdowns
async function startBattleSetup() {
  try {
    const trainersData = await fetchTrainers();
    trainer1Select.innerHTML = `<option value="">-- Select trainer --</option>`;
    trainer2Select.innerHTML = `<option value="">-- Select trainer --</option>`;
    trainersData.forEach(trainer => {
      trainers[trainer.id] = trainer;
      const optionHTML = `<option value="${trainer.id}">${trainer.name}</option>`;
      trainer1Select.insertAdjacentHTML('beforeend', optionHTML);
      trainer2Select.insertAdjacentHTML('beforeend', optionHTML);
    });
  } catch (error) {
    console.error("Error loading trainers:", error);
    alert("Ошибка при загрузке тренеров. Проверьте консоль.");
  }
}

// Reset battle state
async function resetBattle() {
  if (resetBattleBtn) resetBattleBtn.disabled = true;
  pokemon1 = null;
  pokemon2 = null;
  if (battleArea) battleArea.style.display = "none";
  if (battleLog) battleLog.innerHTML = "";
  const attack1Btn = document.getElementById("attack1");
  const attack2Btn = document.getElementById("attack2");
  if (attack1Btn) attack1Btn.disabled = true;
  if (attack2Btn) attack2Btn.disabled = true;
  if (pokemon1Select) {
    pokemon1Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
    pokemon1Select.disabled = true;
  }
  if (pokemon2Select) {
    pokemon2Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
    pokemon2Select.disabled = true;
  }
  if (trainer1Select) trainer1Select.value = "";
  if (trainer2Select) trainer2Select.value = "";
  updateStartButton();
  if (connection) {
    try {
      await connection.stop();
      log("SignalR connection stopped.");
    } catch (err) {
      console.error("Error stopping SignalR connection:", err);
      log("Ошибка при остановке соединения SignalR");
    }
    connection = null;
  }
}
if (resetBattleBtn) resetBattleBtn.addEventListener("click", resetBattle);

// Setup SignalR connection
function setupSignalR() {
  connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7019/battleHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

  connection.on("ReceiveAttack", async (turn) => {
    console.log("ReceiveAttack payload:", turn);
    if (!turn || !turn.battleId || !turn.attackerId || !turn.defenderId || turn.damage == null) {
      console.error("Invalid ReceiveAttack payload:", turn);
      log("Ошибка: Некорректные данные атаки");
      return;
    }

    const { battleId, attackerId, defenderId, damage } = turn;
    const attackerName = (pokemon1?.id?.toString() === attackerId.toString() ? (pokemon1.pokemon?.name || pokemon1.name || "Unknown") : (pokemon2.pokemon?.name || pokemon2.name || "Unknown"));
    const defenderName = (pokemon1?.id?.toString() === defenderId.toString() ? (pokemon1.pokemon?.name || pokemon1.name || "Unknown") : (pokemon2.pokemon?.name || pokemon2.name || "Unknown"));
    log(`Атака: ${attackerName} наносит ${damage} урона ${defenderName}`);

    if (pokemon1 && defenderId.toString() === pokemon1.id?.toString()) {
      pokemon1.currentHP = Math.max(Number(pokemon1.currentHP || pokemon1.hp || 0) - Number(damage), 0);
      console.log(`Pokemon1 HP updated: ${pokemon1.currentHP}`);
      updatePokemonUI(pokemon1, "name1", "img1", "hp-bar-fill1", "hp1");
    } else if (pokemon2 && defenderId.toString() === pokemon2.id?.toString()) {
      pokemon2.currentHP = Math.max(Number(pokemon2.currentHP || pokemon2.hp || 0) - Number(damage), 0);
      console.log(`Pokemon2 HP updated: ${pokemon2.currentHP}`);
      updatePokemonUI(pokemon2, "name2", "img2", "hp-bar-fill2", "hp2");
    } else {
      console.error(`No matching defender found for ID: ${defenderId}`);
      log(`Ошибка: Покемон с ID ${defenderId} не найден`);
    }

    if (pokemon1 && pokemon2) {
      console.log(`Checking battle end: pokemon1 HP=${pokemon1.currentHP}, pokemon2 HP=${pokemon2.currentHP}`);
      if (Number(pokemon1.currentHP) <= 0 || Number(pokemon2.currentHP) <= 0) {
        const winner = Number(pokemon1.currentHP) > 0 ? pokemon1 : pokemon2;
        const winnerName = winner.pokemon?.name || winner.name || "Unknown";
        log(`Битва окончена! ${winnerName} победил!`);
        alert(`Битва окончена! ${winnerName} победил!`);
        const attack1Btn = document.getElementById("attack1");
        const attack2Btn = document.getElementById("attack2");
        if (attack1Btn) attack1Btn.disabled = true;
        if (attack2Btn) attack2Btn.disabled = true;
        if (resetBattleBtn) resetBattleBtn.disabled = false;
      }
    }
  });

  connection.on("BattleEnded", async (result) => {
    console.log("BattleEnded payload:", result);
    if (!result || !result.battleId || !result.winnerId || !result.loserId) {
      console.error("Invalid BattleEnded payload:", result);
      log("Ошибка: Некорректные данные завершения битвы");
      return;
    }
    const winner = pokemon1?.id?.toString() === result.winnerId.toString() ? pokemon1 : pokemon2;
    const winnerName = winner?.pokemon?.name || winner?.name || "Unknown";
    log(`Битва окончена! ${winnerName} победил!`);
    alert(`Битва окончена! ${winnerName} победил!`);
    const attack1Btn = document.getElementById("attack1");
    const attack2Btn = document.getElementById("attack2");
    if (attack1Btn) attack1Btn.disabled = true;
    if (attack2Btn) attack2Btn.disabled = true;
    if (resetBattleBtn) resetBattleBtn.disabled = false;
  });

  connection.onclose(async () => {
    log("SignalR connection closed. Попытка переподключения...");
    await tryReconnect();
  });

  async function tryReconnect(attempt = 1, maxAttempts = 5) {
    if (attempt > maxAttempts) {
      log("Не удалось переподключиться после нескольких попыток");
      alert("Не удалось восстановить соединение с сервером. Попробуйте перезагрузить страницу.");
      return;
    }
    try {
      await connection.start();
      log("Переподключение успешно");
    } catch (err) {
      console.error(`Reconnect attempt ${attempt} failed:`, err);
      setTimeout(() => tryReconnect(attempt + 1, maxAttempts), 1000 * attempt);
    }
  }
}

// Start battle
if (startBattleBtn) {
  let isStartingBattle = false; // Prevent multiple clicks
  startBattleBtn.addEventListener("click", async () => {
    if (isStartingBattle) return;
    isStartingBattle = true;

    if (!canStartBattle()) {
      alert("Выберите двух разных тренеров и по одному покемону для каждого.");
      isStartingBattle = false;
      return;
    }

    try {
      if (!pokemon1Select.value || !pokemon2Select.value) {
        alert("Пожалуйста, выберите покемонов для обоих тренеров.");
        isStartingBattle = false;
        return;
      }

      try {
        pokemon1 = JSON.parse(pokemon1Select.value);
        pokemon2 = JSON.parse(pokemon2Select.value);
        console.log("Pokemon1 loaded:", pokemon1);
        console.log("Pokemon2 loaded:", pokemon2);
      } catch (e) {
        console.error("Parse error:", e);
        log("Ошибка: Некорректные данные покемонов");
        alert("Ошибка при выборе покемонов. Проверьте консоль.");
        isStartingBattle = false;
        return;
      }

      if (!pokemon1.id || !pokemon2.id) {
        console.error("Invalid pokemon IDs:", { pokemon1, pokemon2 });
        log("Ошибка: Некорректные ID покемонов");
        alert("Ошибка: Выбранные покемоны имеют некорректные данные.");
        isStartingBattle = false;
        return;
      }

      // Attempt to start battle via API
      let battleId = 1; // Fallback battleId
      try {
        console.log("Attempting to start battle with POST...");
        const battleResponse = await fetch('https://localhost:7019/api/Battle/Start', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ pokemon1Id: pokemon1.id, pokemon2Id: pokemon2.id })
        });
        if (!battleResponse.ok) {
          console.warn(`POST failed with status ${battleResponse.status}`);
          if (battleResponse.status === 405) {
            console.log("Trying GET request as fallback...");
            const getResponse = await fetch(`https://localhost:7019/api/Battle/Start?pokemon1Id=${pokemon1.id}&pokemon2Id=${pokemon2.id}`);
            if (!getResponse.ok) {
              console.error(`GET request failed with status ${getResponse.status}`);
              log("Ошибка: Не удалось начать битву через API");
            } else {
              const getData = await getResponse.json();
              battleId = getData.battleId || battleId;
              console.log("Battle started with GET, battleId:", battleId);
            }
          } else {
            throw new Error(`Failed to start battle: ${battleResponse.status}`);
          }
        } else {
          const battleData = await battleResponse.json();
          battleId = battleData.battleId || battleId;
          console.log("Battle started with POST, battleId:", battleId);
        }
      } catch (err) {
        console.error("Battle start API error:", err);
        log("Ошибка: Не удалось начать битву через API, используется временный ID битвы");
        alert("Не удалось связаться с сервером битвы. Используется временный ID битвы.");
      }

      pokemon1.battleId = battleId;
      pokemon2.battleId = battleId;

      pokemon1.maxHp = Number(pokemon1.maxHp || pokemon1.hp || 100);
      pokemon2.maxHp = Number(pokemon2.maxHp || pokemon2.hp || 100);
      pokemon1.currentHP = Number(pokemon1.currentHP || pokemon1.hp || pokemon1.maxHp);
      pokemon2.currentHP = Number(pokemon2.currentHP || pokemon2.hp || pokemon2.maxHp);

      updatePokemonUI(pokemon1, "name1", "img1", "hp-bar-fill1", "hp1");
      updatePokemonUI(pokemon2, "name2", "img2", "hp-bar-fill2", "hp2");

      if (battleArea) battleArea.style.display = "flex";
      log(`Битва между ${pokemon1.pokemon?.name || pokemon1.name || "Unknown"} и ${pokemon2.pokemon?.name || pokemon2.name || "Unknown"} началась!`);

      if (!connection) setupSignalR();

      try {
        await connection.start();
        console.log("SignalR connection started");
      } catch (err) {
        console.error("SignalR connection failed:", err);
        log("Ошибка: Не удалось подключиться к серверу битвы");
        alert("Ошибка подключения к серверу битвы. Проверьте консоль.");
        isStartingBattle = false;
        return;
      }

      const attack1Btn = document.getElementById("attack1");
      const attack2Btn = document.getElementById("attack2");
      if (attack1Btn) attack1Btn.disabled = false;
      if (attack2Btn) attack2Btn.disabled = true;
      if (resetBattleBtn) resetBattleBtn.disabled = true;
      if (startBattleBtn) startBattleBtn.disabled = true;

    } catch (error) {
      console.error("Start battle error:", error);
      log("Ошибка при старте битвы");
      alert("Ошибка при старте битвы. Проверьте консоль.");
    } finally {
      isStartingBattle = false;
    }
  });
}

// Attack buttons
const attack1Btn = document.getElementById("attack1");
const attack2Btn = document.getElementById("attack2");

let isAttack1Processing = false;
if (attack1Btn) {
  attack1Btn.addEventListener("click", async () => {
    if (isAttack1Processing) return;
    isAttack1Processing = true;

    try {
      if (!pokemon1 || !pokemon2 || Number(pokemon2.currentHP) <= 0) {
        console.warn("Attack1 blocked: Invalid pokemon or battle ended");
        log("Ошибка: Битва завершена или покемоны не выбраны");
        return;
      }
      if (!pokemon1.id || !pokemon2.id || !pokemon1.battleId) {
        console.error("Invalid pokemon or battle IDs", { pokemon1, pokemon2 });
        log("Ошибка: Некорректные ID покемонов или битвы");
        return;
      }
      const damage = Math.floor(Math.random() * 20) + 5;
      if (isNaN(damage) || damage < 0) {
        console.error("Invalid damage value", { damage });
        log("Ошибка: Некорректное значение урона");
        return;
      }

      // Ensure connection is active before invoking
      if (!connection || connection.state !== signalR.HubConnectionState.Connected) {
        console.error("SignalR connection not active, state:", connection?.state);
        log("Ошибка: Соединение с сервером битвы не активно");
        if (connection) {
          await connection.start();
          console.log("SignalR reconnected, state:", connection.state);
          log("Переподключение к серверу успешно");
        } else {
          log("Ошибка: Не удалось установить соединение с сервером");
          return;
        }
      }

      console.log(`Sending attack with battleId: ${pokemon1.battleId}, attackerId: ${pokemon1.id}, defenderId: ${pokemon2.id}, damage: ${damage}`);
      await connection.invoke("SendAttack", pokemon1.battleId.toString(), pokemon1.id.toString(), pokemon2.id.toString(), damage);
      if (attack1Btn) attack1Btn.disabled = true;
      if (attack2Btn) attack2Btn.disabled = false;
    } catch (err) {
      console.error("Attack1 error:", err);
      log(`Ошибка при атаке игрока 1: ${err.message || 'Неизвестная ошибка на сервере'}`);
      alert("Не удалось выполнить атаку. Проверьте консоль или перезапустите битву.");
      if (connection && connection.state === signalR.HubConnectionState.Disconnected) {
        try {
          await connection.start();
          log("Переподключение к серверу успешно");
        } catch (reconnectErr) {
          console.error("Reconnect error:", reconnectErr);
          log("Ошибка переподключения к серверу");
        }
      }
    } finally {
      isAttack1Processing = false;
    }
  });
}

let isAttack2Processing = false;
if (attack2Btn) {
  attack2Btn.addEventListener("click", async () => {
    if (isAttack2Processing) return;
    isAttack2Processing = true;

    try {
      if (!pokemon1 || !pokemon2 || Number(pokemon1.currentHP) <= 0) {
        console.warn("Attack2 blocked: Invalid pokemon or battle ended");
        log("Ошибка: Битва завершена или покемоны не выбраны");
        return;
      }
      if (!pokemon1.id || !pokemon2.id || !pokemon2.battleId) {
        console.error("Invalid pokemon or battle IDs", { pokemon1, pokemon2 });
        log("Ошибка: Некорректные ID покемонов или битвы");
        return;
      }
      const damage = Math.floor(Math.random() * 20) + 5;
      if (isNaN(damage) || damage < 0) {
        console.error("Invalid damage value", { damage });
        log("Ошибка: Некорректное значение урона");
        return;
      }

      if (!connection || connection.state !== signalR.HubConnectionState.Connected) {
        console.error("SignalR connection not active, state:", connection?.state);
        log("Ошибка: Соединение с сервером битвы не активно");
        if (connection) {
          await connection.start();
          console.log("SignalR reconnected, state:", connection.state);
          log("Переподключение к серверу успешно");
        } else {
          log("Ошибка: Не удалось установить соединение с сервером");
          return;
        }
      }

      console.log(`Sending attack with battleId: ${pokemon2.battleId}, attackerId: ${pokemon2.id}, defenderId: ${pokemon1.id}, damage: ${damage}`);
      await connection.invoke("SendAttack", pokemon2.battleId.toString(), pokemon2.id.toString(), pokemon1.id.toString(), damage);
      if (attack2Btn) attack2Btn.disabled = true;
      if (attack1Btn) attack1Btn.disabled = false;
    } catch (err) {
      console.error("Attack2 error:", err);
      log(`Ошибка при атаке игрока 2: ${err.message || 'Неизвестная ошибка на сервере'}`);
      alert("Не удалось выполнить атаку. Проверьте консоль или перезапустите битву.");
      if (connection && connection.state === signalR.HubConnectionState.Disconnected) {
        try {
          await connection.start();
          log("Переподключение к серверу успешно");
        } catch (reconnectErr) {
          console.error("Reconnect error:", reconnectErr);
          log("Ошибка переподключения к серверу");
        }
      }
    } finally {
      isAttack2Processing = false;
    }
  });
}

// Initialize trainers on page load
startBattleSetup();