import apiService from "./ApiService";

export class UserService {
    async getUserById(id) {
        const user = await apiService.getSingle(`/account/getbyid/${id}`);
        return user;
    }

    async getMembersByOrganizationId(id) {
        const users = await apiService.getMulti(`/account/getbyorgid/${id}`);
        return users;
    }
}

const userService = new UserService();
export default userService;