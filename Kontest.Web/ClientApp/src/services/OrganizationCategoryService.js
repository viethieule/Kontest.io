import apiService from "./ApiService";

const controllerPath = '/organizationcategory';

export class OrganizationCategoryService {
    async getAllOrgCategory() {
        const categories = await apiService.getMulti(`${controllerPath}/getAll`);
        return categories;
    }
}

const organizationCategoryService = new OrganizationCategoryService();
export default organizationCategoryService;