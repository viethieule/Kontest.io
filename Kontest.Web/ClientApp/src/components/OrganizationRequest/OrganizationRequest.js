import React, { Component } from 'react'
import { FormGroup, Col, Label, Input } from 'reactstrap'
import organizationCategoryService from '../../services/OrganizationCategoryService';
import organizationRequestService from '../../services/OrganizationRequestService';
import authService from '../../services/AuthService';

export class OrganizationRequest extends Component {
    constructor(props) {
        super(props);
        this.state = {
            request: {
                organizationName: '',
                organizationCategoryId: ''
            },
            categories: []
        }

        this.handleOnChange = this.handleOnChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleOnChange(event) {
        const target = event.target;
        const name = target.name;
        const value = target.value;

        this.setState(prevState => ({
            ...prevState,
            request: {
                ...prevState.request,
                [name]: value
            }
        }));
    }

    async handleSubmit(event) {
        event.preventDefault();


        const currentUser = await authService.getUser();
        if (currentUser) {
            const createdRequest = await organizationRequestService.create({
                ...this.state.request,
                requestingUserId: currentUser.sub
            });

            if (createdRequest) {
                var { organizationName, organizationCategoryId } = this.state.request;
                alert(`Request created for ${organizationName}, category ${organizationCategoryId}`);
            }
        }
    }

    componentDidMount() {
        this.populateCategory();
    }

    render() {
        return (
            <div>
                <h1>Request tạo organization</h1>
                <form onSubmit={this.handleSubmit}>
                    <FormGroup row>
                        <Label for="organizationName" sm={2}>Organization fullname</Label>
                        <Col sm={10}>
                            <Input type="text" name="organizationName" value={this.state.request.organizationName || ''} onChange={this.handleOnChange}></Input>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label for="organizationCategoryId" sm={2}>Organization category</Label>
                        <Col sm={10}>
                            <Input type="select" name="organizationCategoryId" value={this.state.request.organizationCategoryId || ''} onChange={this.handleOnChange}>
                                {this.state.categories.map(category =>
                                    <option key={category.id} value={category.id}>{category.name}</option>
                                )}
                            </Input>
                        </Col>
                    </FormGroup>
                    <Input type="submit" value="Submit"></Input>
                </form>
            </div>
        )
    }

    async populateCategory() {
        const categories = await organizationCategoryService.getAllOrgCategory();
        this.setState({ categories });
        categories.length > 0 && this.setState({ request: { organizationCategoryId: categories[0].id } })
    }
}