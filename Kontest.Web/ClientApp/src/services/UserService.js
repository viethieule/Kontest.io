import apiService from "./ApiService";

const controllerPath = '/account';

export class UserService {
    async getUserById(id) {
        const user = await apiService.getSingle(`${controllerPath}/getbyid/${id}`);
        return user;
    }

    async getMembersByOrganizationId(id) {
        const users = await apiService.getMulti(`${controllerPath}/getbyorgid/${id}`);
        return users;
    }

    async searchUserByUsername(keyword) {
        const users = await apiService.getMulti(`${controllerPath}/searchUserByUsername/${keyword}`);
        return users;
    }
}

const userService = new UserService();
export default userService;