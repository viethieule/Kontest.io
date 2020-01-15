import apiService from "./ApiService";

const controllerPath = '/userorganization';

class UserOrganizationService {
    async addUserOrganization(userOrganization) {
        const addedRecord = await apiService.addRecord(`${controllerPath}/addUserOrganization`, userOrganization);
        return addedRecord;
    }

    async getUserOrganizationsByUserId(userId) {
        const userOrganizations =
            await apiService.getMulti(`${controllerPath}/getuserorganizationsbyuserid/${userId}`);
        return userOrganizations;
    }

    async getUserOrganizationsByOrganizationId(organizationId) {
        const userOrganizations =
            await apiService.getMulti(`${controllerPath}/getUserOrganizationsByOrganizationId/${organizationId}`);
        return userOrganizations;
    }

    async deleteUserOrganizationById(userOrganizationId) {
        const ok =
            await apiService.delete(`${controllerPath}/deleteUserOrganizationByid/${userOrganizationId}`);
        return ok;
    }
}

const userOrganizationService = new UserOrganizationService();
export default userOrganizationService;

export const UserOrganizationsRoleType = {
    CREATOR: 0,
    ADMIN: 1,
    MEMBER: 2
}