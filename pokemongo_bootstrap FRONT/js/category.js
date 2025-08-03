 import { endpoint } from './constant.js'; 


async function getAllCategories() {
  try {
    const response = await fetch(`${endpoint.category}`);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching categories:", error);
    return [];
  }
}
async function getCategoryById(id) {
  try {
    const response = await fetch(`${endpoint.category}/${id}`);
    console.log("salam") ;
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching category by ID:", error);
  }
}
async function getCategoryByName(name) {
  try {
    const response = await fetch(`${baseUrl}/api/Category/name/${name}`);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching category by name:", error);
  }
}
export { getAllCategories, getCategoryById, getCategoryByName };