import apiService from "./ApiService";

const controllerPath = '/organizationrequest';

export class OrganizationRequestService {
    async create(organizationRequest) {
        const createdRequest = await apiService.addRecord(`${controllerPath}/create`, organizationRequest);
        return createdRequest;
    }
}

const organizationRequestService = new OrganizationRequestService();
export default organizationRequestService