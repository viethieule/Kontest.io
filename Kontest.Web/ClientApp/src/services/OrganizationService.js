import apiService from "./ApiService";

export class OrganizationService {
    async getOrganizationById(id) {
        const org = await apiService.getSingle(`/organization/getbyid/${id}`);
        return org;
    }
}

const organizationService = new OrganizationService();
export default organizationService;