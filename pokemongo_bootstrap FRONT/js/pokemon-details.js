import { getPokemonById } from './pokemon.js';
import { getAllSkills } from './skill.js';
import { getAllCategories } from './category.js';

// Получаем ID Pokemon из URL параметров
function getPokemonIdFromUrl() {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get('id');
}

// Загружаем данные Pokemon
async function loadPokemonDetails() {
    const pokemonId = getPokemonIdFromUrl();
    
    if (!pokemonId) {
        alert('Pokemon ID not found in URL');
        window.location.href = 'index.html';
        return;
    }

    try {
        // Загружаем Pokemon, Skills и Categories параллельно
        const [pokemon, skills, categories] = await Promise.all([
            getPokemonById(pokemonId),
            getAllSkills(),
            getAllCategories()
        ]);

        if (!pokemon) {
            alert('Pokemon not found');
            window.location.href = 'index.html';
            return;
        }

        displayPokemonDetails(pokemon, skills, categories);
    } catch (error) {
        console.error('Error loading Pokemon details:', error);
        alert('Error loading Pokemon details');
    }
}

// Отображаем детали Pokemon
function displayPokemonDetails(pokemon, skills, categories) {
    console.log('Pokemon data:', pokemon);
    console.log('Skills data:', skills);
    console.log('Categories data:', categories);

    // Заполняем основную информацию
    document.getElementById('pokemon-name').textContent = pokemon.name || 'Unknown Pokemon';
    document.getElementById('pokemon-hp').textContent = pokemon.hp || 'N/A';
    document.getElementById('pokemon-level').textContent = pokemon.level || 'N/A';
    document.getElementById('pokemon-id').textContent = pokemon.id || 'N/A';
    
    console.log("Pokemon image URL from API:", pokemon.imageUrl);

    // Устанавливаем изображение из базы данных
    const pokemonImage = document.getElementById('pokemon-image');
    const imageUrl = pokemon.imageUrl
      ? (pokemon.imageUrl.startsWith('http') ? pokemon.imageUrl : `https://localhost:7019${pokemon.imageUrl}`)
      : '/pokemon/images/default.png';
    pokemonImage.src = imageUrl;
    pokemonImage.alt = pokemon.name || 'Pokemon';
    pokemonImage.onload = function() {
        console.log('Detail image loaded successfully:', this.src);
    };
    pokemonImage.onerror = function() {
        console.error('Detail image failed to load:', this.src);
        this.src = '/pokemon/images/default.png';
    };

    // Отображаем категории
    displayCategories(pokemon, categories);

    // Отображаем навыки
    displaySkills(pokemon, skills);
}

// Отображаем категории
function displayCategories(pokemon, categories) {
    const categoriesContainer = document.getElementById('categories-container');
    
    // Пробуем разные возможные названия полей для категорий
    const categoryIds = pokemon.categoriesIds || pokemon.categoryId || pokemon.categories || pokemon.category || [];
    
    console.log('Category IDs found:', categoryIds);
    
    if (Array.isArray(categoryIds) && categoryIds.length > 0) {
        const categoryNames = categoryIds.map(id => {
            const category = categories.find(c => c.id === id);
            console.log(`Looking for category with ID ${id}:`, category);
            return category?.name || `Unknown (ID: ${id})`;
        });
        
        categoryNames.forEach(name => {
            const tag = document.createElement('span');
            tag.className = 'category-tag';
            tag.textContent = name;
            categoriesContainer.appendChild(tag);
        });
    } else {
        categoriesContainer.innerHTML = '<p>No categories available</p>';
    }
}

// Отображаем навыки
function displaySkills(pokemon, skills) {
    const skillsContainer = document.getElementById('skills-container');
    
    // Пробуем разные возможные названия полей для навыков
    const skillIds = pokemon.skillIds || pokemon.skillId || pokemon.skills || pokemon.skill || [];
    
    console.log('Skill IDs found:', skillIds);
    
    if (Array.isArray(skillIds) && skillIds.length > 0) {
        const skillNames = skillIds.map(id => {
            const skill = skills.find(s => s.id === id);
            console.log(`Looking for skill with ID ${id}:`, skill);
            return skill?.name || `Unknown (ID: ${id})`;
        });
        
        skillNames.forEach(name => {
            const tag = document.createElement('span');
            tag.className = 'skill-tag';
            tag.textContent = name;
            skillsContainer.appendChild(tag);
        });
    } else {
        skillsContainer.innerHTML = '<p>No skills available</p>';
    }
}

// Загружаем детали при загрузке страницы
document.addEventListener('DOMContentLoaded', loadPokemonDetails);