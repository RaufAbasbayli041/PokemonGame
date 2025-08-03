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

    async function getPokemonById(id) {
        try {
            const response = await fetch(`${endpoint.pokemon}/${id}`);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return await response.json();
        } catch (error) {
            console.error("Error fetching Pokemon data:", error);
            return null;
        }
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