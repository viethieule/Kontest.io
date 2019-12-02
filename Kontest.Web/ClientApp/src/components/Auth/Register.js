import React, { Component } from 'react'
import { Redirect } from 'react-router-dom'
import { Form, FormGroup, Col, Label, Input } from 'reactstrap'
import authService from '../../services/AuthService'

export class Register extends Component {
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            passwordConfirm: '',
            email: '',
        }

        this.handleOnChange = this.handleOnChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleOnChange(event) {
        const target = event.target;
        const name = target.name;
        const value = target.value;

        this.setState({ [name]: value });
    }

    handleSubmit(event) {
        alert('Your username is: ' + this.state.username + '. Your email is ' + this.state.email);
        event.preventDefault();
    }

    render() {
        return (
            <div>
                <h1>Tạo tài khoản</h1>
                <form onSubmit={this.handleSubmit}>
                    <Form>
                        <FormGroup row>
                            <Label for="username" sm={2}>Tên tài khoản</Label>
                            <Col sm={10}>
                                <Input type="text" name="username" value={this.state.username} onChange={this.handleOnChange}></Input>
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="email" sm={2}>Email</Label>
                            <Col sm={10}>
                                <Input type="email" name="email" value={this.state.email} onChange={this.handleOnChange}></Input>
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="password" sm={2}>Mật khẩu</Label>
                            <Col sm={10}>
                                <Input type="password" name="password" value={this.state.password} onChange={this.handleOnChange}></Input>
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="passwordConfirm" sm={2}>Xác nhận mật khẩu</Label>
                            <Col sm={10}>
                                <Input type="password" name="passwordConfirm" value={this.state.passwordConfirm} onChange={this.handleOnChange}></Input>
                            </Col>
                        </FormGroup>
                        <Input type="submit" value="Submit"></Input>
                    </Form>
                </form>
            </div>
        )
    }
}
