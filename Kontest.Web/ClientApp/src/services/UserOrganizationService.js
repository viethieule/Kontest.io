import apiService from "./ApiService";

class UserOrganizationService {
    async getUserOrganizationsByUserId(userId) {
        const userOrganizations = await apiService.getMulti(`/userorganization/getuserorganizationsbyuserid/${userId}`);
        return userOrganizations;
    }
}

const userOrganizationService = new UserOrganizationService();
export default userOrganizationService;