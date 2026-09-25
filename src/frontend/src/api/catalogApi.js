import axios from 'axios'

const catalogApi = axios.create({
  baseURL: import.meta.env.VITE_CATALOG_API_URL
})

export default catalogApi
