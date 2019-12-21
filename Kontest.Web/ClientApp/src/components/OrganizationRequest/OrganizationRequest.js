import React, { Component } from 'react'
import { FormGroup, Col, Label, Input } from 'reactstrap'

export class OrganizationRequest extends Component {
    constructor(props) {
        super(props);
        this.state = {
            request: {
                OrganizationName: '',
                OrganizationCategory: ''
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

    handleSubmit(event) {
        event.preventDefault();
        alert(`Name ${this.state.request.OrganizationName} Category ${this.state.request.OrganizationCategory}`)
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
                        <Label for="OrganizationName" sm={2}>Organization fullname</Label>
                        <Col sm={10}>
                            <Input type="text" name="OrganizationName" value={this.state.request.OrganizationName || ''} onChange={this.handleOnChange}></Input>
                        </Col>
                    </FormGroup>
                    <FormGroup row>
                        <Label for="OrganizationCategory" sm={2}>Organization category</Label>
                        <Col sm={10}>
                            <Input type="select" name="OrganizationCategory" value={this.state.request.OrganizationCategory || ''} onChange={this.handleOnChange}>
                                {this.state.categories.map(category =>
                                    <option key={category.value} value={category.value}>{category.text}</option>
                                )}
                            </Input>
                        </Col>
                    </FormGroup>
                    <Input type="submit" value="Submit"></Input>
                </form>
            </div>
        )
    }

    populateCategory() {
        const categories = this.fakeFetchCategory() || [];
        this.setState({ categories });
        categories.length > 0 && this.setState({ request: { OrganizationCategory: categories[0].value } })
    }

    fakeFetchCategory = () => [
        {
            text: 'Học thuật',
            value: 1
        },
        {
            text: 'Nghệ thuật',
            value: 2
        },
        {
            text: 'Võ thuật',
            value: 3
        },
        {
            text: 'Bí thuật',
            value: 4
        },
        {
            text: 'Ma thuật',
            value: 5
        }
    ];
}