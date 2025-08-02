const trainers = {};
const trainer1Select = document.getElementById("trainer1-select");
const trainer2Select = document.getElementById("trainer2-select");
const startBattleBtn = document.getElementById("start-battle");
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
    trainer1Select.value &&
    trainer2Select.value &&
    trainer1Select.value !== trainer2Select.value &&
    pokemon1Select.value &&
    pokemon2Select.value
  );
}

function updateStartButton() {
  startBattleBtn.disabled = !canStartBattle();
}

trainer1Select.addEventListener("change", updateStartButton);
trainer2Select.addEventListener("change", updateStartButton);
pokemon1Select.addEventListener("change", updateStartButton);
pokemon2Select.addEventListener("change", updateStartButton);

trainer1Select.addEventListener("change", async () => {
  updateStartButton();
  const trainerId = trainer1Select.value;
  if (trainerId) {
    const pokemons = await fetchPokemonByTrainerId(trainerId);
    pokemon1Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
    pokemons.forEach(p => {
      const option = document.createElement("option");
      option.value = JSON.stringify(p); // сохраним весь объект как JSON-строку
      option.textContent = `${p.name} (HP: ${p.hp})`;
      pokemon1Select.appendChild(option);
    });
    pokemon1Select.disabled = false;
  } else {
    pokemon1Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
    pokemon1Select.disabled = true;
  }
});

trainer2Select.addEventListener("change", async () => {
  updateStartButton();
  const trainerId = trainer2Select.value;
  if (trainerId) {
    const pokemons = await fetchPokemonByTrainerId(trainerId);
    pokemon2Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
    pokemons.forEach(p => {
      const option = document.createElement("option");
      option.value = JSON.stringify(p);
      option.textContent = `${p.name} (HP: ${p.hp})`;
      pokemon2Select.appendChild(option);
    });
    pokemon2Select.disabled = false;
  } else {
    pokemon2Select.innerHTML = `<option value="">-- Select pokemon --</option>`;
    pokemon2Select.disabled = true;
  }
});

// Лог
function log(message) {
  const p = document.createElement("p");
  p.textContent = message;
  battleLog.appendChild(p);
  battleLog.scrollTop = battleLog.scrollHeight;
}

// Обновление UI покемона
function updatePokemonUI(pokemon, nameId, imgId, hpBarId, hpValueId) {
  document.getElementById(nameId).textContent = pokemon.name || "No name";

  const hp = Number(pokemon.hp);
  const maxHp = Number(pokemon.maxHp) || hp;

  if (isNaN(hp) || isNaN(maxHp) || maxHp === 0) {
    document.getElementById(hpBarId).style.width = "0%";
    document.getElementById(hpValueId).textContent = "0";
    return;
  }

  const hpPercent = (hp / maxHp) * 100;
  document.getElementById(hpBarId).style.width = hpPercent + "%";
  document.getElementById(hpValueId).textContent = hp;
}

// Получение тренеров с сервера
async function fetchTrainers() {
  const response = await fetch('https://localhost:7019/api/Trainer');
  if (!response.ok) throw new Error('Failed to load trainers');
  return response.json();
}

// Получение покемонов тренера по id
async function fetchPokemonByTrainerId(trainerId) {
  const response = await fetch(`https://localhost:7019/api/trainer/${trainerId}/trainerPokemon`);
  if (!response.ok) throw new Error('Failed to load pokemon');

  const pokemons = await response.json();
  pokemons.forEach(pokemon => {
    console.log("Покемон:", pokemon.name);
  });

  return pokemons;
}

// Заполнение селектов тренерами
async function startBattleSetup() {
  try {
    const trainersData = await fetchTrainers();

    trainer1Select.innerHTML = `<option value="">-- Выберите тренера --</option>`;
    trainer2Select.innerHTML = `<option value="">-- Выберите тренера --</option>`;

    trainersData.forEach(trainer => {
      trainers[trainer.id] = trainer; // можно сохранить для справки, но покемонов загружать будем отдельно
      const optionHTML = `<option value="${trainer.id}">${trainer.name}</option>`;
      trainer1Select.insertAdjacentHTML('beforeend', optionHTML);
      trainer2Select.insertAdjacentHTML('beforeend', optionHTML);
    });
  } catch (error) {
    console.error(error);
  }
}

// Инициализация SignalR
function setupSignalR() {
  connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7019/battlehub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

  connection.on("ReceiveAttack", (attackerId, defenderId, damage) => {
    log(`Атака: ${attackerId} наносит ${damage} урона ${defenderId}`);

    // Приводим к строке для сравнения
    if (defenderId.toString() == pokemon1.id.toString()) {
      pokemon1.hp = Math.max(Number(pokemon1.hp) - damage, 0);
      updatePokemonUI(pokemon1, "name1", "img1", "hp-bar-fill1", "hp1");
    } else if (defenderId.toString() == pokemon2.id.toString()) {
      pokemon2.hp = Math.max(Number(pokemon2.hp) - damage, 0);
      updatePokemonUI(pokemon2, "name2", "img2", "hp-bar-fill2", "hp2");
    }

    if (pokemon1.hp <= 0 || pokemon2.hp <= 0) {
      log(`Битва окончена! Победил покемон с ID ${pokemon1.hp > 0 ? pokemon1.id : pokemon2.id}`);
      alert("Битва окончена!");

      document.getElementById("attack1").disabled = true;
      document.getElementById("attack2").disabled = true;
    }
  });

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
startBattleBtn.addEventListener("click", async () => {
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

    battleArea.style.display = "flex";
    log(`Битва между ${pokemon1.name} и ${pokemon2.name} началась!`);

    if (!connection) setupSignalR();

    await connection.start();

    document.getElementById("attack1").disabled = false;
    document.getElementById("attack2").disabled = false;

  } catch (error) {
    console.error(error);
    alert("Ошибка при старте битвы. Проверьте консоль.");
  }
});

// Атаки
document.getElementById("attack1").addEventListener("click", async () => {
  if (!pokemon1 || !pokemon2 || pokemon2.hp <= 0) return;
  const damage = Math.floor(Math.random() * 20) + 5;
  try {
    console.log(`Отправка атаки: ${pokemon1.id} атакует ${pokemon2.id} с уроном ${damage}`);
    await connection.invoke("SendAttack", pokemon1.id, pokemon2.id, damage);
  } catch (err) {
    console.error(err);
  }
});

document.getElementById("attack2").addEventListener("click", async () => {
  if (!pokemon1 || !pokemon2 || pokemon1.hp <= 0) return;
  const damage = Math.floor(Math.random() * 20) + 5;
  try {
    await connection.invoke("SendAttack", pokemon2.id, pokemon1.id, damage);
  } catch (err) {
    console.error(err);
  }
});

// Загрузить тренеров при загрузке страницы
startBattleSetup();