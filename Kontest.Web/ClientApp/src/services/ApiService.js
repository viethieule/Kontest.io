import axios from 'axios'
import authService from './AuthService';

export const apiUrl = 'https://localhost:5200/api';

export class ApiService {
    async getSingle(url) {
        const data = await this.get(url, null);
        return data;
    }

    async getMulti(url) {
        const data = await this.get(url, []);
        return data;
    }

    async get(url, defaultValue) {
        const token = await authService.getAccessToken();
        try {
            let response = await axios.get(`${apiUrl}${url}`, {
                mode: 'cors',
                credentials: 'same-origin',
                headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
            });

            const ok = response && response.status === 200;
            if (!ok) {
                console.log(response);
                return defaultValue;
            }

            const data = await response.data;
            return data;
        } catch (error) {
            console.log(error);
            return defaultValue;
        }
    }
}

const apiService = new ApiService();
export default apiService;