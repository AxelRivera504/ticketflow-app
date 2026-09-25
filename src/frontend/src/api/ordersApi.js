import axios from 'axios'

const ordersApi = axios.create({
  baseURL: import.meta.env.VITE_ORDERS_API_URL
})

export default ordersApi
