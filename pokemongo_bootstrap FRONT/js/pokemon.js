 import { endpoint } from './constant.js'; 

async function getAllPokemons() {
    try {
        const response = await fetch(endpoint.pokemon);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
    } catch (error) {
        console.error("Error fetching data:", error);
        return [];
    }
}

    function getPokemonById(id) {
        fetch(`${endpoint.pokemon}/${id}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
           
            .catch(error => {
                console.error("Error fetching Pokemon data:", error);
            });
    }

    function getPokemonByName(name) {
        fetch(`${endpoint.pokemon}/name/${name}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            
            .catch(error => {
                console.error("Error fetching Pokemon data:", error);
            });
    }

 export { getAllPokemons, getPokemonById, getPokemonByName };