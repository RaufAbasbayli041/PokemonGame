 import { endpoint } from './constant.js'; 

 async function getAllSkills() {
    try {
        const response = await fetch(endpoint.skill);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
    } catch (error) {
        console.error("Error fetching data:", error);
        return [];
    }
}

    function getSkillById(id) {
        fetch(`${endpoint.skill}/${id}`)
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

    function getskillByName(name) {
        fetch(`${endpoint.skill}/name/${name}`)
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

 export { getAllSkills, getSkillById, getskillByName };