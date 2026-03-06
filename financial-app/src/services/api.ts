import axios from "axios";

//conexão com a api via axios.
export const api = axios.create({
  baseURL: "https://localhost:7003/api"
});